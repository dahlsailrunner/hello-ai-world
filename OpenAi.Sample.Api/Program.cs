using System.IdentityModel.Tokens.Jwt;
using OpenAi.Sample.Api.StartupServices;
using OpenAi.Sample.Api.Swagger;
using OpenAi.Sample.ServiceDefaults;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAzureOpenAIClient("azureOpenAi");

builder.Services.AddControllers();
builder.Services.AddApiProblemDetails();

builder.Services.AddCustomApiVersioning()  // defined in StartupServices folder
    .AddSwaggerFeatures()
    .AddHttpContextAccessor();

builder.Services
    .AddMvcCore(options => options.AddBaseAuthorizationFilters())
    .AddApiExplorer();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.Audience = builder.Configuration.GetValue<string>("Authentication:ApiName");
    });

var app = builder.Build();

var apiVersionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerFeatures(builder.Configuration, apiVersionProvider, app.Environment);

app.MapDefaultEndpoints();

app.UseRouting()
    .UseAuthentication()
    .UseMiddleware<UserScopeMiddleware>()
    .UseExceptionHandler() // by putting here we can capture the logged in user in log entry
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.Run();
return 0;