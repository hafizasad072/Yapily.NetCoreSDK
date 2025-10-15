using System.Text;
using Yapily.Core.SDK;

namespace Yapily.Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("// ---------------------------------------------");
            Console.WriteLine("// Yapily .Net Core 8 SDK");
            Console.WriteLine("// ---------------------------------------------");

            string APP_KEY = PromptInput("Please enter App Key:");
            string APP_SECRET = PromptSecret("Please enter App Secret:");
            string CALLBACK_URL = PromptInput("Please enter CALLBACK_URL (default: https://yourapi.com/api/yapily/callback):",
                                              "https://yourapi.com/api/yapily/callback");

            // Output (APP_SECRET is not shown for security)
            Console.WriteLine($"\nAPP_KEY: {APP_KEY}");
            Console.WriteLine($"CALLBACK_URL: {CALLBACK_URL}");

            YapilyConfig.Initialize(APP_KEY, APP_SECRET);

            await YapilyClient.RunAsync(CALLBACK_URL);

            Console.ReadLine();
        }

        static string PromptInput(string message, string defaultValue = "")
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        static string PromptSecret(string message)
        {
            Console.WriteLine(message);
            StringBuilder secret = new StringBuilder();
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && secret.Length > 0)
                {
                    secret.Length--;
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    secret.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            return secret.ToString();
        }
    }
}
