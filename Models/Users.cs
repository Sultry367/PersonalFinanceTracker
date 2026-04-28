namespace PersonalFinanceTracker.Models;

public class Users
{
    public Guid Id {get; set;}
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    
    public ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
}