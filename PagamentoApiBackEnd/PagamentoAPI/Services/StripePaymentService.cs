using PagamentoAPI.Interfaces;
using PagamentoAPI.Models;
using Stripe;
using Stripe.TestHelpers;

namespace PagamentoAPI.Services
{
    public class StripePaymentService : IPaymentService
    {
        public StripePaymentService() // Configure aqui a sua chave da API do Stripe
        {
            StripeConfiguration.ApiKey = "sk_test_51On50kDld9kwFZtQDaZhdqtu8iqIJHB1NhX5Ltklsai6TFXEKh8PhKqASAtEIkEQii8paMEZvKrUTqNXhXL5hQ8d00CuS508fC";
        }

        public async Task<bool> ProcessPaymentAsync(PaymentRequest paymentRequest)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = paymentRequest.CardNumber,
                    ExpMonth = paymentRequest.ExpiryMonth,
                    ExpYear = paymentRequest.ExpiryYear,
                    Cvc = paymentRequest.Cvc,
                },
            };


            Customer customer = new Stripe.CustomerService().Create(null);

            var options = new CardCreateOptions
            {
                Source = "tok_visa_debit",
            };
            var service = new CardService();
            var token = service.Create(customer.Id, options);

            var optionsCharge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(paymentRequest.Amount * 100), // Stripe usa centavos
                Currency = "usd", // ou a moeda de sua escolha
                Source = token.Id,
                Description = "Descrição do pagamento",
                Customer = customer.Id,
            };

            var servicecharge = new ChargeService();
            Charge charge = await servicecharge.CreateAsync(optionsCharge);

            return charge.Paid;
        }
    }
}
