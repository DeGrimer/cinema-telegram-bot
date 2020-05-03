using System;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient Bot;
        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("1201853070:AAEUE165VlK8r8GEjiqHAk7XK-8OManQdo0") { Timeout = TimeSpan.FromSeconds(10)};
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnCallbackQuery += Bot_OnCallbackQuery;
            var name = Bot.GetMeAsync().Result;
            Console.WriteLine(name.FirstName);
            Bot.StartReceiving();
            Console.ReadLine();
        }

        private static async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var button = e.CallbackQuery.Data;
            if (button == "Отправь картинку")
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "https://goblins-online.ru/images/slide_new/tselnometallicheskaya-obolochka-1987-goblin-2.jpg");
            if (button == "Отправь стикер")
                await Bot.SendStickerAsync(e.CallbackQuery.From.Id, "https://tgram.ru/wiki/stickers/imagepng/EasterBear/EasterBear_11.png");
        
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text)
                return;
            var name = message.From.FirstName;
            Console.WriteLine(message.Text+" от "+name);
            switch(message.Text)
            {
                case "/start":
                    var text = "Для вызова меню - /menu";
                    await Bot.SendTextMessageAsync(e.Message.From.Id, text);
                    break;
                case "/menu":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Кинопоиск","https://www.kinopoisk.ru/"),
                            InlineKeyboardButton.WithUrl("IMDb","https://www.imdb.com/")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Отправь картинку"),
                            InlineKeyboardButton.WithCallbackData("Отправь стикер")
                        }
                    }) ;
                    await Bot.SendTextMessageAsync(message.From.Id, "Выбери пункт меню", replyMarkup: inlineKeyboard);
                    break;
                default:
                    break;
            }
        }
    }
}
