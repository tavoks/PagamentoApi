using PagamentoAPI.Models;

namespace PagamentoAPI.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(PaymentRequest paymentRequest);
    }
}
