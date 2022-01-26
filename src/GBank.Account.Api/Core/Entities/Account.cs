using System;

public class Account
{
    public string Id { get; set; } = Guid.NewGuid().ToString().ToUpper();
    public string CustomerId { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
}