using System;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString().ToUpper();
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}