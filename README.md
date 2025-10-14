# Yapily .NET Core 8 SDK

A lightweight and modern **.NET 8 SDK** for integrating with the [Yapily Open Banking API](https://docs.yapily.com/).  
This SDK simplifies consent creation, account retrieval, balance lookup, and transaction history queries using C# and async patterns.

---

## Features

- Written in **.NET 8**
- Async/Await support for all API calls
- Clean model structure using inheritance
- Built-in console sample for quick start
- Supports both **Hosted Page** and **Direct Authentication**

---

## Project Structure

```
Yapily.Client/
│
├── Program.cs                # Entry point (console sample)
├── YapilyClient.cs           # Sample Yapily SDK usage flow
└── Yapily.Core.SDK/
    ├── Models/               # Refactored model files
    ├── SDK / YapilyCoreSDK             # YapilyCoreSDK
    └── Yapily.Core.SDK.csproj
```

---

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet)
- A Yapily [App Key and Secret](https://dashboard.yapily.com/)
- A public callback URL (e.g. `https://yourapi.com/api/yapily/callback`)

---

## Quick Start

### 1️ Clone the repository

```bash
git clone https://github.com/your-username/yapily-dotnet-sdk.git
cd yapily-dotnet-sdk
```

### 2️ Build the project

```bash
dotnet build
```

### 3️ Run the console app

```bash
dotnet run --project Yapily.Client
```

### 4️ Follow the prompts

You’ll be asked for:

- **App Key**
- **App Secret** (hidden as you type)
- **Callback URL** (press Enter for default)

Example:

```
Please enter App Key:  your-yapily-key
Please enter App Secret:  ************
Please enter CALLBACK_URL (default: https://yourapi.com/api/yapily/callback):
```

---

## Example Flow

Once you enter credentials, the console app will:

1. **Create a Yapily user**
2. **Generate a hosted consent page** and prompt you to open the URL
3. **Retrieve consent details** after callback redirection
4. **List user accounts**
5. **Fetch account balances and transactions**
6. **List available institutions**

---

## Example Output

```
---------------------------------------------
 Yapily .Net Core 8 SDK
---------------------------------------------
 Created Yapily User: d83a41a2-xxxx
 Using Hosted Consent Page...
 Redirect the user to Yapily hosted link:
 https://auth.yapily.com/consent/12345
Wait until Yapily redirects user to your CALLBACK with ?consentId=abc
Consent status: AUTHORIZED
 Account 12345 - GBP
 Balance: 1200.50 GBP
```

---

## Extending the SDK

The SDK is designed to be easily extended:

- Add new endpoints to `YapilyCoreSDK.cs`
- Add corresponding models under `Models/`
- Reuse base classes such as `MetaBase`, `BalanceAmountBase`, and `TransactionBase`

---

## Contributing

Pull requests are welcome!  
If you’d like to improve the SDK, add tests, or support additional Yapily endpoints — please open a PR.

---

## Support

- [Yapily Developer Docs](https://docs.yapily.com/)
- [Open Banking Overview](https://www.openbanking.org.uk/)
- Author: [Your Name or Company]
