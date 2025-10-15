using System.Web;
using Yapily.Core.SDK.SDK.Account;
using Yapily.Core.SDK.SDK.Consent;
using Yapily.Core.SDK.SDK.Institutions;
using Yapily.Core.SDK.SDK.Interfaces;
using Yapily.Core.SDK.SDK.Transactions;
using Yapily.Core.SDK.SDK.Users;

public class YapilyClient
{
    public static async Task RunAsync(string CALLBACK_URL)
    {
        IUserService userService = new UserService();
        IAccountsService accountService = new AccountsService();
        IConsentService consentService = new ConsentService();
        IInstitutionService institutionService = new InstitutionService();
        ITransactionService transactionService = new TransactionService();

        // STEP 1: Create Yapily User (maps to your ERP user)
        var user = await userService.CreateUserAsync(null);
        Console.WriteLine($" Created Yapily User: {user.Uuid}");

        // ---------------------------------------------
        // OPTION 1 — Hosted Page (Recommended)
        // ---------------------------------------------
        Console.WriteLine(" Using Hosted Consent Page...");
        var hostedAuth = await accountService.CreateAccountAuthRequestAsync(
            userUuid: user.Uuid,
            institutionId: "modelo-sandbox", // choose one if you already know
            applicationUserId: null,
            callbackUrl: CALLBACK_URL,
            oneTimeToken: false
        );

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
        var accounts = await accountService.GetAccountsAsync(consentToken);
        foreach (var acc in accounts.Data)
        {
            Console.WriteLine($" Account {acc.Id} - {acc.Currency}");
        }

        // STEP 4: Fetch balance & transactions for first account
        var accId = accounts.Data[0].Id;
        var balances = await accountService.GetAccountBalancesAsync(consentToken, accId);
        Console.WriteLine($" Balance: {balances.Data.Balances[0].BalanceAmount.Amount} {balances.Data.Balances[0].BalanceAmount.Currency}");

        var txs = await transactionService.GetTransactionsAsync(
            consentToken,
            accId,
            DateTime.UtcNow.AddDays(-30),
            DateTime.UtcNow
        );

        foreach (var t in txs.Data)
        {
            Console.WriteLine($" {t.BookingDateTime:d}: {t.TransactionInformation} {t.Amount} {t.Currency} ");
        }

        // ---------------------------------------------
        // OPTION 2 — Get institutions list yourself
        // ---------------------------------------------
        Console.WriteLine("\n Listing institutions manually...");
        var institutions = await institutionService.GetInstitutionsAsync("NL");

        foreach (var bank in institutions.Data)
        {
            Console.WriteLine($"{bank.Id} - {bank.Name}");
        }

        // then choose one, and create an auth request for it
        string selectedInstitutionId = institutions.Data[0].Id;

        var authRequest = await accountService.CreateAccountAuthRequestAsync(
            user.Uuid,
            selectedInstitutionId,
            "client_12345",
            CALLBACK_URL,
            false
        );

        Console.WriteLine($"Redirect user to: {authRequest.Data.AuthorisationUrl}");
    }
}
