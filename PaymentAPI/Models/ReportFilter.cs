namespace PaymentAPI.Models
{
    public class ReportFilter
    {
        public int? BankId { get; set; }
        public string Status { get; set; }
        public string OrderReference { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
