using Yapily.Core.SDK;
using Yapily.Core.SDK.SDK.Account;
using Yapily.Core.SDK.SDK.Consent;
using Yapily.Core.SDK.SDK.Institutions;
using Yapily.Core.SDK.SDK.Transactions;
using Yapily.Core.SDK.SDK.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Bind YapilyConfig settings from appsettings.json
var yConfig = builder.Configuration.GetSection("YapilyConfig").Get<YapilySettings>();

//// Initialize YapilyConfig with values
YapilyConfig.Initialize(yConfig.AppKey, yConfig.AppSecret, yConfig.BaseUrl, yConfig.CallbackUrl);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<IConsentService, ConsentService>();

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

app.Run();

public class YapilySettings
{
    public string AppKey { get; set; }

    public string AppSecret { get; set; }

    public string BaseUrl { get; set; }

    public string CallbackUrl { get; set; }

}
