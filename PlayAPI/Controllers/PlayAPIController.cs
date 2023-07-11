using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace PlayAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlayAPIController : Controller
    {
        private readonly IConfiguration _configuration;
        public PlayAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public HttpResponseMessage Index()
        {
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent("Hi", Encoding.Unicode) };
        }

        [HttpPost]
        public HttpStatusCode Post()
        {
            return HttpStatusCode.OK;
        }
        [HttpPost]
        public string Post_formData([FromForm] DataTransferObject formData)
        {
            if (formData != null && !String.IsNullOrWhiteSpace(formData?.content))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Headers.Add("MyHeaderMessage", "MyHeaderMessage");
                response.Content = new StringContent(JsonConvert.SerializeObject(formData));
                return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync());
            }
            return $"formData is {(formData?.content == null ? "null" : "notNull")}";
        }


        [HttpGet]
        public async Task<string> Youbike()
        {
            try
            {
                string reqUrl = _configuration["ApiUrl:YouBike"];
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(reqUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return $"[Error] {JsonConvert.SerializeObject(response)}";
            }
            catch
            {
                return BadRequest().StatusCode.ToString();
            }

        }
        [HttpGet]
        public async Task<string> YoubikeStream()
        {
            string reqUrl = _configuration["ApiUrl:YouBike"];
            HttpClient client = new HttpClient();
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, reqUrl), HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                StreamReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
                StringBuilder stringBuilder = new StringBuilder();
                while (!streamReader.EndOfStream)
                {
                    stringBuilder.Append(await streamReader.ReadLineAsync());
                }
                streamReader.Close();
                return stringBuilder.ToString();
            }
            return $"[Error] {JsonConvert.SerializeObject(response)}";
        }

    }
}
