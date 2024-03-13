using Microsoft.AspNetCore.Mvc;
using PagamentoAPI.Interfaces;
using PagamentoAPI.Models;

namespace PagamentoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var result = await _paymentService.ProcessPaymentAsync(paymentRequest);
                if (result)
                {
                    return Ok(new { message = "Pagamento processado com sucesso!" });
                }
                else
                {
                    return BadRequest(new { message = "Falha ao processar pagamento." });
                }
            }
            catch (Exception ex)
            {
                // Logue o erro aqui
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}