using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Controllers.Chat
{
    public class AiService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _endpoint;
        private readonly string _deployment;

        public AiService(IConfiguration config)
        {
            _http = new HttpClient();
            _apiKey = config["OpenAI:ApiKey"];
            _endpoint = config["OpenAI:Endpoint"];
            _deployment = config["OpenAI:DeploymentName"];
        }

        public async Task<string> ProcessAsync(string prompt)
        {
            var url = $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-15-preview";
            var payload = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3,
                max_tokens = 1500
            };

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("api-key", _apiKey);

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, content);
            var text = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"Azure OpenAI API error: {response.StatusCode}";

            var json = JObject.Parse(text);
            return json["choices"]?[0]?["message"]?["content"]?.ToString()?.Trim() ?? "";
        }
    }
}
