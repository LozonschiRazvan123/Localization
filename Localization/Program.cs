using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("en-US"),
                   // new CultureInfo("fr")
                };

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var languages = context.Request.Headers["Accept-Language"].ToString();
        var currentLanguage = languages.Split(',').FirstOrDefault();
        var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "en-US" : currentLanguage;

        if ( defaultLanguage != "en-US")
        {
            defaultLanguage = "en-US";
        }

        return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
    }));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(locOptions.Value);

app.Run();
