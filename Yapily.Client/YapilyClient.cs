using System.Web;
using Yapily.Core.SDK;
using Yapily.Core.SDK.SDK.Consent;
using Yapily.Core.SDK.SDK.Institutions;
using Yapily.Core.SDK.SDK.Users;
using Yapily.Core.SDK.SDK.FinancialData;
using Yapily.Core.SDK.Models.Accounts;
using Yapily.Core.SDK.Models.Transactions;
using Yapily.BO.Models;

public class YapilyClient
{
    public static async Task RunAsync()
    {
        IUserService userService = new UserService();
        IFinancialDataService financialDataService = new FinancialDataService();
        IConsentService consentService = new ConsentService();
        IInstitutionService institutionService = new InstitutionService();

        string CALLBACK_URL = YapilyConfig.CallBackURL;

        // STEP 1: Create Yapily User (maps to your ERP user)
        var user = await userService.CreateUserAsync(new UserCreateRequest());
        Console.WriteLine($" Created Yapily User: {user.Uuid}");

        // ---------------------------------------------
        // OPTION 1 — Hosted Page (Recommended)
        // ---------------------------------------------
        Console.WriteLine(" Using Hosted Consent Page...");
        var hostedAuth = await financialDataService.CreateAccountAuthRequestAsync(
            new CreateAccountAuthRequest()
            {
                UserUuid = user.Uuid,
                InstitutionId = "modelo-sandbox", // choose one if you already know
                ApplicationUserId = null,
                Callback = CALLBACK_URL,
                OneTimeToken = false
            });

        Console.WriteLine($" Redirect the user to Yapily hosted link:\n{hostedAuth.Data.AuthorisationUrl}");
        Console.WriteLine($"Wait until Yapily redirects user to your CALLBACK with ? consentId = {hostedAuth.Data.Id}");
        // Your API endpoint will receive ?consentId=abc

        string consentId = hostedAuth.Data.Id;

        // STEP 2: Retrieve consent details
        var consent = await consentService.GetConsentAsync(consentId); // same  hostedAuth.Data.Id
        Console.WriteLine($"Consent status: {consent.Data.Status}");

        Console.WriteLine("Paste the full callback URL after redirection:");
        string fullCallbackUrl = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fullCallbackUrl))
        {
            Console.WriteLine("No URL entered.");
            return;
        }

        var uri = new Uri(fullCallbackUrl);
        var queryParams = HttpUtility.ParseQueryString(uri.Query);
        string consentToken = queryParams["consent"];

        if (string.IsNullOrWhiteSpace(consentToken))
        {
            Console.WriteLine("consentId not found in the URL. Please check and try again.");
            return;
        }

        Console.WriteLine($"Extracted consentId: {consentToken}");

        // STEP 3: Fetch accounts
        var accounts = await financialDataService.GetAccountsAsync(consentToken);
        foreach (var acc in accounts.Data)
        {
            Console.WriteLine($" Account {acc.Id} - {acc.Currency}");
        }

        // STEP 4: Fetch balance & transactions for first account
        var accId = accounts.Data[0].Id;
        var balances = await financialDataService.GetAccountBalancesAsync(consentToken, accId);
        Console.WriteLine($" Balance: {balances.Data.Balances[0].BalanceAmount.Amount} {balances.Data.Balances[0].BalanceAmount.Currency}");

        var txs = await financialDataService.GetTransactionsAsync(
            new GetTransactionsRequest()
            {
                ConsentId = consentToken,
                AccountId = accId,
                From = DateTime.UtcNow.AddDays(-30),
                To = DateTime.UtcNow

            });

        foreach (var t in txs.Data)
        {
            Console.WriteLine($" {t.BookingDateTime:d}: {t.TransactionInformation} {t.Amount} {t.Currency} ");
        }

        // ---------------------------------------------
        // OPTION 2 — Get institutions list yourself
        // ---------------------------------------------
        Console.WriteLine("\n Listing institutions manually...");
        var institutions = await institutionService.GetInstitutionsAsync();

        foreach (var bank in institutions.Data)
        {
            Console.WriteLine($"{bank.Id} - {bank.Name}");
        }

        // then choose one, and create an auth request for it
        string selectedInstitutionId = institutions.Data[0].Id;

        var authRequest = await financialDataService.CreateAccountAuthRequestAsync(
             new CreateAccountAuthRequest()
             {
                 UserUuid = user.Uuid,
                 InstitutionId = selectedInstitutionId,
                 ApplicationUserId = "client_12345",
                 Callback = CALLBACK_URL,
                 OneTimeToken = false
             });

        Console.WriteLine($"Redirect user to: {authRequest.Data.AuthorisationUrl}");
    }
}
