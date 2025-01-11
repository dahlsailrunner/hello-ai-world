using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using OpenAI;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class OpenAiController(OpenAIClient aiClient) : ControllerBase
{
    private const string _chatDeploymentModel = "kyt-gpt-4o-mini";

    // More information: https://github.com/openai/openai-dotnet
    // https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/openai/Azure.AI.OpenAI/README.md
    [HttpGet]
    [Route("hello-chat")]
    [SwaggerOperation("What are the main steps to set up an Azure OpenAI instance?")]
    public async Task<ActionResult<string>> GetHello(CancellationToken cxl)
    {
        var chatClient = aiClient.GetChatClient(_chatDeploymentModel); // set up your own "deployment" of a model

        ChatCompletion completion = await chatClient.CompleteChatAsync(
        [
            //new SystemChatMessage("You are a helpful assistant."),
            //new UserChatMessage("Does Azure OpenAI support customer managed keys?"),
            //new AssistantChatMessage("Yes, customer managed keys are supported by Azure OpenAI"),
            //new UserChatMessage("Do other Azure AI services support this too?")
            new UserChatMessage("What are the main steps to set up an Azure OpenAI instance?")
        ], cancellationToken: cxl);
        return completion.Content[0].Text;
    }

    [HttpGet]
    [Route("stream-chat")]
    [SwaggerOperation("What are the main steps to set up an Azure OpenAI instance?")]
    public async IAsyncEnumerable<string> StreamChat(
        [EnumeratorCancellation] CancellationToken cxl)
    {
        var chatClient = aiClient.GetChatClient(_chatDeploymentModel);

        var response = chatClient.CompleteChatStreamingAsync(
        [new UserChatMessage("What are the main steps to set up an Azure OpenAI instance?")],
                cancellationToken: cxl);

        await foreach (var message in response)
        {
            foreach (var contentPart in message.ContentUpdate)
            {
                yield return contentPart.Text;
            }
        }
    }
}
