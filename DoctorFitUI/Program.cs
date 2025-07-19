using DoctorFitUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
    options.ProviderOptions.Authority = "https://localhost:5001";
    options.ProviderOptions.ClientId = "doctorfit_blazor_client";
    options.ProviderOptions.ResponseType = "code"; // PKCE flow
    options.ProviderOptions.DefaultScopes.Add("doctorfit_api");
    options.ProviderOptions.DefaultScopes.Add("roles");
});

await builder.Build().RunAsync();
