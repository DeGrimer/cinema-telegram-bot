using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;

namespace TelegramBot
{
    class SearchFilm
    {
        public readonly TelegramBotClient Bot;
        public readonly Telegram.Bot.Args.CallbackQueryEventArgs e;
        public SearchFilm(TelegramBotClient Bot, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            this.Bot = Bot;
            this.e = e;
        }
        public async void Search()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Боевик"),
                            InlineKeyboardButton.WithCallbackData("Комедия")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Детектив"),
                            InlineKeyboardButton.WithCallbackData("Фантастика")
                        }
                    });
            await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Выбери жанр фильма", replyMarkup: inlineKeyboard);
        }
        public async void RandFilm(string name)
        {
            Random rnd = new Random();
            List<string> films = new List<string>();
            string path = @"D:\cinem-proj\"+name+".txt";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] filmsT = sr.ReadToEnd().Split(',');
                    foreach (var t in filmsT)
                        films.Add(t);
                }
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Другой фильм", name),
                            InlineKeyboardButton.WithCallbackData("Другой жанр", "Поиск случайного фильма")
                        }
                    });
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, films[rnd.Next(films.Count)],replyMarkup:inlineKeyboard);
            }
            catch(IOException z)
            {
                Console.WriteLine(z);
            }
        }
    }
}
