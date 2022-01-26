using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public class AccountMessageSender
{
    private readonly string _uri;
    private readonly string _password;
    private readonly string _queueName;
    private readonly string _username;
    private IConnection _connection;

    public AccountMessageSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
    {
        _queueName = rabbitMqOptions.Value.QueueName;
        _uri = rabbitMqOptions.Value.Uri;
        _username = rabbitMqOptions.Value.UserName;
        _password = rabbitMqOptions.Value.Password;

        CreateConnection();
    }

    public void SendAccount(string customerId)
    {
        if (ConnectionExists())
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonSerializer.Serialize(new { CustomerId = customerId });
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }

    private void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_uri),
                UserName = _username,
                Password = _password,
            };

            factory.Ssl.Enabled = true;
            _connection = factory.CreateConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not create connection: {ex.Message}");
        }
    }

    private bool ConnectionExists()
    {
        if (_connection != null)
        {
            return true;
        }

        CreateConnection();

        return _connection != null;
    }
}
