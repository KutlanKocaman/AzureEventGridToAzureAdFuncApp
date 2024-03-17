using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EventGridSubscriber
{
    public static class EventGridSubscriberFunction
    {
        [FunctionName("EntraSubscriberFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            EventGridEvent eventGridEvent = EventGridEvent.Parse(await BinaryData.FromStreamAsync(req.Body));

            if (eventGridEvent.EventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
            {
                var eventData = JsonNode.Parse(eventGridEvent.Data.ToString());
                string validationCode = eventData["validationCode"].ToString();
                var validationResponse = new { validationResponse = validationCode };
                return new OkObjectResult(validationResponse);
            }

            log.LogInformation($"Data received: {eventGridEvent.Data}");

            return new OkResult();
        }
    }
}
