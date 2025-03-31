using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using XmlConverter.UI;
using XmlConverter.UI.Components.ModalWindowService;
using XmlConverter.UI.Components.ToolTipService;
using XmlConverter.UI.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<IModalWindowService, ModalWindowService>();
builder.Services.AddScoped<IToolTipService, CustomTooltipService>();
builder.Services.AddUiInfrastructure(builder.Configuration);

await builder.Build().RunAsync();
