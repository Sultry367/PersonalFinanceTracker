using System.Text.Json.Serialization;

namespace PersonalFinanceTracker.Models;

public class Transactions
{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = "General";
        public Guid UserId { get; set; }
      [JsonIgnore]  public Users User { get; set; } = null!;
    
        public bool IsSubscription { get; set; } 
    
}