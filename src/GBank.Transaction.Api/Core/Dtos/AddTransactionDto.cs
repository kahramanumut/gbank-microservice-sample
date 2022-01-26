public class AddTransactionDto
{
    private string _accountId;
    public decimal Amount { get; set; }
    public string AccountId { get { return _accountId; } set { _accountId = value.ToString(); } }
}