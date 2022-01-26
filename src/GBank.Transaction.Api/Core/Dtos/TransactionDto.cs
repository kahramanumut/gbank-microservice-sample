using System;

public class TransactionDto
{
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreationTime { get; set; }

}

    