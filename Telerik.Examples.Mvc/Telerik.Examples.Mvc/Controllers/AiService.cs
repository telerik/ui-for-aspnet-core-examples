using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Controllers
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

        private async Task<string> SendAsync(string url, object payload)
        {
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

        public async Task<string> ProcessAsync(string prompt)
        {
            try
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

                return await SendAsync(url, payload);
            }
            catch
            {
                return "AI configuration missing";
            }
        }

        public async Task<string> AnalyzeGridDataAsync(string instructions, string gridDataJson)
        {
            var systemPrompt = @"
                        You are an AI assistant analyzing Kendo UI Grid data.
                        
                        You ALWAYS receive:
                        1) The full raw data (JSON array)
                        2) A user question
                        
                        RULES:
                        1. If the user explicitly asks for filtering or sorting:
                           Return ONLY a JSON object such as:
                           { ""action"": ""filter"", ""field"": ""Month"", ""operator"": ""eq"", ""value"": ""July"" }
                        
                           or:
                        
                           { ""action"": ""sort"", ""field"": ""Total"", ""dir"": ""desc"" }
                        
                        2. For ALL OTHER QUESTIONS:
                           Return a short natural language answer using ONLY the supplied data.
                        
                        No code, no markdown, no explanations.
                        ";

            try
            {
                var url = $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-15-preview";
                var payload = new
                {
                    messages = new[]
                    {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = $"Grid Data:\n{gridDataJson}" },
                        new { role = "user", content = $"Question:\n{instructions}" }
                    },
                    temperature = 0.3,
                    max_tokens = 1500
                };

                return await SendAsync(url, payload);
            }
            catch
            {
                return "AI configuration missing";
            }
        }

        public async Task<string> EditTextAsync(string text, string instruction)
        {
            try
            {
                var url = $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-15-preview";
                var payload = new
                {
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content =
                                "You are an AI text editor. Modify the text ONLY according to the user's instruction while preserving existing formatting. " +
                                "If the user requests color, bold, or styles, return the text wrapped in proper HTML inline styles (e.g., <span style='color: green;'>text</span>). " +
                                "Do not apply additional formatting unless explicitly requested."
                        },
                        new
                        {
                            role = "user",
                            content =
                                $"Modify this text:\n\n{text}\n\nInstruction: {instruction}\n\nReturn the modified text as valid HTML with inline styles only if formatting changes are required."
                        }
                    },
                    temperature = 0.3,
                    max_tokens = 500
                };

                return await SendAsync(url, payload);
            }
            catch
            {
                return "AI configuration missing";
            }
        }

        public async Task<string> GenerateChartConfigAsync(string instructions)
        {
            var url = $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-15-preview";

            var payload = new
            {
                messages = new[]
                {
                    new { role = "system", content = "Return ONLY valid JSON for a Kendo UI Chart." },
                    new { role = "user", content = $"Generate a Kendo UI Chart JSON configuration. Instructions: {instructions}" }
                },
                temperature = 0.2,
                max_tokens = 600
            };

            var raw = await SendAsync(url, payload);
            if (string.IsNullOrWhiteSpace(raw)) return "{}";

            string config = raw.Trim();

            int start = config.IndexOf('{');
            int end = config.LastIndexOf('}');
            if (start >= 0 && end > start)
                config = config.Substring(start, end - start + 1);

            try
            {
                JObject.Parse(config);
                return config;
            }
            catch
            {
                config = config.Replace(",]", "]").Replace(",}", "}");
                try
                {
                    JObject.Parse(config);
                    return config;
                }
                catch
                {
                    return "{}";
                }
            }
        }

        public async Task<string> GenerateSchedulerConfigAsync(string instructions)
        {
            var url = $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-15-preview";

            var systemPrompt = @"
                               Return ONLY valid JSON for a Kendo UI Scheduler.
                               
                               The JSON object MUST contain:
                               {
                                 ""date"": ""2025-01-10T00:00:00Z"",
                                 ""events"": [
                                     {
                                       ""id"": 1,
                                       ""title"": ""Design Review"",
                                       ""start"": ""2025-01-10T10:00:00Z"",
                                       ""end"": ""2025-01-10T11:00:00Z""
                                     }
                                 ]
                               }
                               
                               RULES:
                               - Convert all dates into ISO UTC strings.
                               - The 'events' array must contain the tasks created or modified.
                               - Do NOT include Kendo configuration like views or transport.
                               ";

            var payload = new
            {
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = instructions }
                },
                temperature = 0.2,
                max_tokens = 600
            };

            var raw = await SendAsync(url, payload);
            if (string.IsNullOrWhiteSpace(raw)) return "{}";

            string config = raw.Trim();

            int start = config.IndexOf("{");
            int end = config.LastIndexOf("}");
            if (start >= 0 && end > start)
                config = config.Substring(start, end - start + 1);

            try
            {
                JObject.Parse(config);
                return config;
            }
            catch
            {
                config = config.Replace(",]", "]").Replace(",}", "}");
                try
                {
                    JObject.Parse(config);
                    return config;
                }
                catch
                {
                    return "{}";
                }
            }
        }
    }
}
