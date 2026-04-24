using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.ChatbotDtos;
using Nois.Application.Interfaces;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;

namespace Nois.API.Controllers
{
	public class ChatbotController : BaseController
	{
		private readonly IChatbotService _chatbotService;

		public ChatbotController(IChatbotService chatbotService)
		{
			_chatbotService = chatbotService;
		}

		[HttpPost("ask")]
		public async Task<IActionResult> Ask([FromBody] ChatRequestDto request)
		{
			if (string.IsNullOrWhiteSpace(request.Message))
				return BadRequest("Message cannot be empty.");

			var response = await _chatbotService.GetResponseAsync(request.Message);

			return Ok(new ChatResponseDto(response));
		}
	}
}
