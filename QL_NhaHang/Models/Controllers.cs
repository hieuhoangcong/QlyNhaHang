using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace QL_NhaHang.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecaptchaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string RecaptchaSecretKey = "YOUR_SECRET_KEY";

        public RecaptchaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyRecaptcha([FromBody] RecaptchaRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={RecaptchaSecretKey}&response={request.Token}",
                null);

            var jsonString = await response.Content.ReadAsStringAsync();
            var recaptchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(jsonString);

            if (recaptchaResponse.Success)
            {
                return Ok();
            }

            return BadRequest();
        }

        private class RecaptchaResponse
        {
            public bool Success { get; set; }
            public string Challenge_ts { get; set; }
            public string Hostname { get; set; }
        }

        public class RecaptchaRequest
        {
            public string Token { get; set; }
        }
    }
}
