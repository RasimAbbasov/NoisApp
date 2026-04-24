using Microsoft.Extensions.Configuration;
using Nois.Application.Interfaces;
using OpenAI.Chat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nois.Infrastructure.Services
{
	public class GeminiChatbotService : IChatbotService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		// Using the fast, free tier model we discussed
		private const string Model = "gemini-2.5-flash";

		public GeminiChatbotService(HttpClient httpClient, IConfiguration config)
		{
			_httpClient = httpClient;
			_apiKey = config["Gemini:ApiKey"]
				?? throw new ArgumentNullException("Gemini API key is missing");
		}

		public async Task<string> GetResponseAsync(string userMessage)
		{
			var url = $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={_apiKey}";

			var payload = new
			{
				// 1. The System Prompt (Shop Assistant Persona)
				system_instruction = new
				{
					parts = new[]
					{
						new { text = "You are a helpful and stylish AI assistant for the Nois clothing shop. Help customers with sizing, style advice, and general inquiries. Keep answers concise and friendly." }
					}
				},
				// 2. The User's Message
				contents = new[]
				{
					new { parts = new[] { new { text = userMessage } } }
				}
			};

			var response = await _httpClient.PostAsJsonAsync(url, payload);

			// If something goes wrong (like a bad API key), this will throw a clear exception
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadFromJsonAsync<JsonElement>();

			// Extract the string from Gemini's JSON response structure
			return json
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text").GetString()!;
		}
	}
}
