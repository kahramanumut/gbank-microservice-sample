using System;

public class Transaction
{
    public long Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public string AccountId { get; set; }
}