using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> IsPrime(int number)
        {
            Thread.Sleep(500);
            if (number <= 1) return Ok(false);
            if (number == 2) return Ok(true);
            if (number % 2 == 0) return Ok(false);

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return Ok(false);

            return Ok(true);
        }
    }
}