using static PaymentAPiInfrastructure.Enum.Enum;

namespace PaymentAPI.Models
{
    public class ReportFilter
    {
        public int? BankId { get; set; }
        public StatusOptions? Status { get; set; }
        public string OrderReference { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
