# OpenAi.Sample

## Running the Solution

Getting your own instance of Azure OpenAI and
a chat completion deployment model is required
for running this solution.

### 1. Get an Azure OpenAI Instance

In the Azure Portal, create an Azure Open AI resource.
Follow the steps to get it created, and then you
should be able to note and Endpoint and the Key (used
in Step 3 below).

### 2. Set up a Chat Completion Deployment

From the Azure Open AI page, you should be able to
see a button that will take you to the Azure AI Foundry
portal (the raw link is [https://ai.azure.com/resource/playground](https://ai.azure.com/resource/playground)
and you'll have to choose your Azure OpenAI
deployment). From there, go to the Chat playground, and
**Create a new Deployment**.

Choose one of the available Base Models, and give your
deployment a name (I named mine `kyt-gpt-4o-mini` and
used the `gpt-4o-mini` base model).

You will use the name you chose in Step 4 below.

### 3. Configure the User Secrets for the API

Use whatever method you like to "manage the user secrets" for the
API project.  You can also add it to the `appsettings.json` file,
but it's better to never even run the risk of getting the information
committed in the repo.

You should create a connection string like the one here:

```json
{
  "ConnectionStrings": {
    "azureOpenAi": "Endpoint=https://YOUR-VALUE.openai.azure.com/;Key=YOUR-KEY"
  }
}
```

Get both the endpoint and the API key from your Azure OpenAI instance in the Azure Portal.

### 4. Use the Chat Completion Deployment

In the `OpenAi.Sample.Api/Controllers/OpenAiController.cs` file,
update the `_chatDeploymentModel` variable to use the
name of the model you created in step 2 above.

### 5. Run the Solution

Once you have done the above steps, run the solution and it should come up!

The Aspire Dashboard should come up and links / resources will be displayed there.

Open the Angular UI, and try the OpenAI Streaming page.  :)
