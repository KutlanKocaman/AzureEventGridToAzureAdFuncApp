using Azure;
using Azure.Messaging.EventGrid;

while (true)
{
    Console.WriteLine("write a message to send to the Event Grid and press Enter");
    var message = Console.ReadLine();

    var uri = new Uri(""); //Insert your Event Grid Topic endpoint
    var keyCredential = new AzureKeyCredential(""); //Insert your Event Grid Topic access key

    var client = new EventGridPublisherClient(uri, keyCredential);

    var eventGridEvent = new EventGridEvent(
        "TestSubject",
        "Test.Type",
        "1.0",
        message);

    var result = await client.SendEventAsync(eventGridEvent);

    if (result.IsError)
    {
        Console.WriteLine($"Error: {result.Content}");
    }
    else
    {
        Console.WriteLine("Event published successfully");
    }
}
