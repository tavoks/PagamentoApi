namespace PagamentoAPI.Models
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvc { get; set; }
        public decimal Amount { get; set; }
    }
}
