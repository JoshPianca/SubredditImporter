using System;
using RedditSharp;
using System.Linq;

namespace RedditImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string importAccountName;
            string importAccountPassword;
            string exportAccountName;
            string exportAccountPassword;

            Console.Write("Enter the account name to import from: ");
            importAccountName = Console.ReadLine();
            Console.Write("Enter password for the import account: ");
            importAccountPassword = Console.ReadLine();
            Console.Write("Enter the account name to export to: ");
            exportAccountName = Console.ReadLine();
            Console.Write("Enter password for the export account: ");
            exportAccountPassword = Console.ReadLine();

            var importReddit = new Reddit();
            importReddit.LogIn(importAccountName, importAccountPassword);
            var importAccount = importReddit.User;
            Console.Write($"Logged in to {importAccountName}");

            var exportReddit = new Reddit();
            exportReddit.LogIn(exportAccountName, exportAccountPassword);
            Console.Write($"Logged in to {exportAccountName}");

            Console.Write("Starting process, this may take a moment and will output progress once connected.");
            int totalSubscriptions = importAccount.SubscribedSubreddits.Count();
            int currentSubscription = 0;

            foreach (string subreddit in importAccount.SubscribedSubreddits.Select(s => s.Name))
            {
                currentSubscription++;
                exportReddit.GetSubreddit(subreddit).Subscribe();
                Console.WriteLine($"Subscribed {exportReddit.User} to {subreddit} ({currentSubscription} of {totalSubscriptions})");
            }
            Console.Write("Update complete.");

        }
    }
}