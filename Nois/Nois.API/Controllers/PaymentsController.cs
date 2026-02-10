using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nois.Application.Interfaces;

namespace Nois.API.Controllers
{
	[ApiController]
	[Route("api/payments")]
	public class PaymentsController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly IConfiguration _configuration;

		public PaymentsController(
			IPaymentService paymentService,
			IConfiguration configuration)
		{
			_paymentService = paymentService;
			_configuration = configuration;
		}

		//[HttpPost("stripe-webhook")]
		//public async Task<IActionResult> StripeWebhook()
		//{
		//	var json = await new StreamReader(Request.Body).ReadToEndAsync();

		//	var stripeEvent = EventUtility.ParseEvent(json);

		//	if (stripeEvent.Type == "payment_intent.succeeded")
		//	{
		//		var intent = stripeEvent.Data.Object as PaymentIntent;
		//		await _paymentService.ConfirmPaymentAsync(intent!.Id);
		//	}


		//	return Ok();
		//}


		[HttpPost("stripe-webhook")]
		public async Task<IActionResult> StripeWebhook()
		{
			var json = await new StreamReader(Request.Body).ReadToEndAsync();

			var jObject = JObject.Parse(json);
			if (jObject["type"]?.ToString() == "payment_intent.succeeded")
			{
				var intentId = jObject["data"]["object"]["id"].ToString();
				await _paymentService.ConfirmPaymentAsync(intentId);
			}


			return Ok();
		}

	}
}

		//[HttpPost("stripe-webhook")]
		//public async Task<IActionResult> StripeWebhook()
		//{
		//	var json = await new StreamReader(Request.Body).ReadToEndAsync();
		//	var jObject = Newtonsoft.Json.Linq.JObject.Parse(json);

//	// Event type yoxla
//	if (jObject["type"]?.ToString() == "payment_intent.succeeded")
//	{
//		// PaymentIntent ID-ni JSON-dan oxu
//		var intentId = jObject["data"]?["object"]?["id"]?.ToString();

//		// Test məqsədilə confirm əməliyyatını logla və ya fake service ilə çağır
//		Console.WriteLine($"Payment confirmed for intent: {intentId}");
//		// Əgər _paymentService varsa, çağır:
//		// await _paymentService.ConfirmPaymentAsync(intentId);
//	}

//	return Ok();
//}