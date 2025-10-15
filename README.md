# Yapily .NET SDK — Open Banking AIS Integration

A **modern, lightweight, and developer-friendly C# SDK** for connecting to European banks using the [Yapily Open Banking API](https://docs.yapily.com/).  
This SDK enables you to easily integrate **Account Information Services (AIS)** — such as fetching balances, accounts, and transactions — into your applications.

> Built for ERP systems, fintech apps, and developers who need simple, secure, PSD2-compliant access to bank data.

---

## Features

- Fully typed .NET 8 SDK
- Works with both Hosted Consent Pages and Embedded Authorisation
- Async API design with HttpClient
- Includes user, consent, account, balance & transaction endpoints
- Simple to integrate into any ERP / Fintech solution
- Lightweight, no external dependencies beyond System.Net.Http

---

## Quick Start

### 1️ Install

```bash
dotnet add package Yapily.Net.SDK
```

### 2️ Initialize Client

```csharp
var yapily = new YapilyClient(
    appKey: "YOUR_APP_KEY",
    appSecret: "YOUR_APP_SECRET"
);
```

### 3️ Create Yapily User

Each of your ERP users must be registered as a Yapily “Application User”.

```csharp
var user = await yapily.CreateUserAsync("erp_user_001");
Console.WriteLine($"Created Yapily User UUID: {user.Uuid}");
```

### 4️ Start Authorisation (Hosted Flow)

This will return a redirect URL for the user to connect their bank.

```csharp
var auth = await yapily.CreateAccountAuthRequestAsync(
    userUuid: user.Uuid,
    institutionId: "abnamro-nl",
    applicationUserId: "erp_user_001",
    callbackUrl: "https://yourapi.com/api/yapily/callback"
);

Console.WriteLine($"Redirect user to: {auth.AuthorisationUrl}");
```

Once the user finishes connecting their bank, Yapily will redirect back to your callback:

```
GET /api/yapily/callback?consentId={consentId}
```

### 5️ Retrieve Bank Data

```csharp
var consent = await yapily.GetConsentAsync(consentId);
var accounts = await yapily.GetAccountsAsync(consentId);
foreach (var acc in accounts)
{
    Console.WriteLine($"{acc.Id}: {acc.Currency}");
}

var balance = await yapily.GetAccountBalancesAsync(consentId, accounts[0].Id);
Console.WriteLine($"Balance: {balance[0].Amount.Amount} {balance[0].Amount.Currency}");

var txs = await yapily.GetTransactionsAsync(consentId, accounts[0].Id, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow);
foreach (var tx in txs)
{
    Console.WriteLine($"{tx.BookingDateTime:d} {tx.TransactionInformation} {tx.Amount.Amount}");
}
```

---

## Example API Callback

```csharp
[ApiController]
[Route("api/yapily")]
public class YapilyCallbackController : ControllerBase
{
    [HttpGet("callback")]
    public IActionResult Callback([FromQuery] string consentId)
    {
        Console.WriteLine($"Yapily consent received: {consentId}");
        return Redirect($"/connected?consentId={consentId}");
    }
}
```

---

## Database Model Example

| Table                  | Columns                                  | Purpose                      |
| ---------------------- | ---------------------------------------- | ---------------------------- |
| **YapilyUsers**        | Uuid, ApplicationUserId                  | Maps ERP user to Yapily user |
| **YapilyConsents**     | ConsentId, UserUuid, Status, Expiry      | Tracks consent lifecycle     |
| **YapilyAccounts**     | AccountId, IBAN, Currency, Balance       | Stores user accounts         |
| **YapilyTransactions** | TransactionId, Date, Amount, Description | Stores transactions          |

---

## Best Practices

- Use Yapily’s Hosted Consent Page for a simple UX
- Store and refresh consent tokens before expiry
- Cache institution lists to reduce API calls
- Handle rate limits and retries
- Use a background job to periodically sync transactions

---

## Example Use Cases

- Accounting / ERP software (like Kapitaal ERP)
- Fintech dashboards
- Budgeting apps
- Credit assessment automation
- Bank statement importers

---

## Tech Stack

- **Language:** C# (.NET 8)
- **HTTP Client:** System.Net.Http
- **Auth:** Basic Auth (App Key + Secret)
- **Standards:** PSD2, Open Banking AIS

---

## Documentation

- [Yapily API Docs](https://docs.yapily.com/)
- [Integration Tutorial](https://docs.yapily.com/pages/data/data-product/tutorial-account-and-trans-data/)
- [Yapily Connect Hosted Pages](https://docs.yapily.com/pages/tools-and-services/yapilyconnect/yapilyconnect-overview/)

---

## Contributing

Contributions are welcome!  
Fork this repo, make your changes, and submit a pull request.

---

## License

This project currently has **no open-source license**.  
All rights reserved.  
Developed by **HM Asad**, 2025.

---

## Support This Project

If this SDK saves you time —  
Give it a ⭐ on GitHub to help other developers discover it!
