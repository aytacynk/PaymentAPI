using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAPiInfrastructure;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly PaymentDbContext _context;

        public TransactionsController(PaymentDbContext context)
        {
            _context = context;
        }


    }
}
