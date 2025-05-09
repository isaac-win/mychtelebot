using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Threading;
using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {

        // Setup Telegram Bot
<<<<<<< HEAD
        var botClient = new TelegramBotClient("7592491653:AAHrzvzstrUtwff25zQKtGgnpR56uX8FS2w");
=======
        var botClient = new TelegramBotClient("Your-Bot-Key");
>>>>>>> c262537ab467be36ed209c1cf4c628325675c208
        using var cts = new CancellationTokenSource();

        // Start receiving Telegram updates
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // Listen to all types of updates
        };

        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Bot is up and running... Type 'quit' to stop.");

        while (true)
        {
            if (Console.ReadLine()?.ToLower() == "quit")
            {
                Console.WriteLine("Shutting down bot...");
                cts.Cancel();
                break;
            }
        }
    }

    // Handle incoming Telegram messages and callback queries
    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        if (update.Message is not { Text: { } messageText }) return;

        var chatId = update.Message.Chat.Id;
        Console.WriteLine($"Received message from {chatId}: {messageText}");

        // Send the bot logo
        await bot.SendPhoto(
            chatId,
            "https://imgur.com/a/GuYqZtW", // Replace with your logo URL
            cancellationToken: token
        );

        // Send the welcome message and inline keyboard
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[] {
                InlineKeyboardButton.WithUrl("🔑Main Group🔑", "https://your-telemaingroup-link.com")
            },
            new[] {
                InlineKeyboardButton.WithUrl("🔒Vouches🔒", "https://your-vouches-link.com")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl("🚨Backup Group🚨", "https://your-backupgroup-link.com")
            }
        });

        await bot.SendMessage(chatId, "Welcome to the ChavHub Exclusive Gateway!\r\n\r\nTo gain access to the main group, simply press the button below.\r\n\r\nFeel free to explore our Vouches Channel to verify our authenticity. \r\n\r\nDon’t forget to join the Backup Channel in case of any terminations! ", replyMarkup: inlineKeyboard, cancellationToken: token);
    }

    // Handle errors
    static Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        Console.WriteLine("Error: " + exception.Message);
        return Task.CompletedTask;
    }
}
