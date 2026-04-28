namespace PersonalFinanceTracker.Dtos.TransactionDtos;

public class CreateTransactionDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = "General";
    public Guid UserId { get; set; }
    public bool IsSubscription { get; set; } 

}