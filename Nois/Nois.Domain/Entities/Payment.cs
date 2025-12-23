namespace Nois.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }
}
