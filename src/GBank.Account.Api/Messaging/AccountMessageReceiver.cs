using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class AccountMessageReceiver : BackgroundService
{
    private IModel _channel;
    private IConnection _connection;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _uri;
    private readonly string _queueName;
    private readonly string _username;
    private readonly string _password;

    public AccountMessageReceiver(IServiceProvider serviceProvider, IOptions<RabbitMqConfiguration> rabbitMqOptions)
    {
        _uri = rabbitMqOptions.Value.Uri;
        _queueName = rabbitMqOptions.Value.QueueName;
        _username = rabbitMqOptions.Value.UserName;
        _password = rabbitMqOptions.Value.Password;
        _serviceProvider = serviceProvider;

        InitializeRabbitMqListener();
    }

    private void InitializeRabbitMqListener()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_uri),
            UserName = _username,
            Password = _password,
        };

        factory.Ssl.Enabled = true;

        _connection = factory.CreateConnection();
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var accountDto = JsonSerializer.Deserialize<AddAccountDto>(content);

            HandleMessage(accountDto);

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        consumer.Shutdown += OnConsumerShutdown;
        consumer.Registered += OnConsumerRegistered;
        consumer.Unregistered += OnConsumerUnregistered;
        consumer.ConsumerCancelled += OnConsumerCancelled;

        _channel.BasicConsume(_queueName, false, consumer);

        return Task.CompletedTask;
    }

    private void HandleMessage(AddAccountDto addAccountDto)
    {
         using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IAccountService _accountService =
                scope.ServiceProvider.GetRequiredService<IAccountService>();

            _accountService.AddAccount(addAccountDto);
        }
    }

    private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
    {
    }

    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
    {
    }

    private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
    {
    }

    private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
    {
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}