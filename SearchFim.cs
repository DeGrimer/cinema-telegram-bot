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
        public async void YearsSearch()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Фильмы 2000-x","2000"),
                            InlineKeyboardButton.WithCallbackData("Фильмы 50-60x","1950")
                        }
                     });
            await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Выбери год фильма", replyMarkup: inlineKeyboard);
        }
            public async void GenresSearch()
            {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Боевик","action"),
                            InlineKeyboardButton.WithCallbackData("Комедия","comedy")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Детектив","detectiv"),
                            InlineKeyboardButton.WithCallbackData("Фантастика","fantasy")
                        }
                     });
            await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Выбери жанр фильма", replyMarkup: inlineKeyboard);
            }
        public async void RandFilmGenres(string name)
        {
            List<Films> films = new List<Films>();
            Random rnd = new Random();
            try
            {
                using(filmdbContext db = new filmdbContext())
                {
                    foreach(var film in db.Films)
                    {
                            if(film.Genres == name)
                                films.Add(film);
                    }
                }
                var k = rnd.Next(films.Count);
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Другой фильм", name),
                            InlineKeyboardButton.WithCallbackData("Другой жанр", "genresSearch")
                        }
                    });
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, $"{films[k].Title} {films[k].Link}",replyMarkup:inlineKeyboard);
            }
            catch(IOException z)
            {
                Console.WriteLine(z);
            }
        }
        public async void RandFilmYears(string name)
        {
            var years = int.Parse(name);
            List<Films> films = new List<Films>();
            Random rnd = new Random();
            try
            {
                using (filmdbContext db = new filmdbContext())
                {
                    foreach (var film in db.Films)
                    {
                        if ((film.Years >= years) && (film.Years <= years + 10))
                            films.Add(film);
                    }
                }
                var k = rnd.Next(films.Count);
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Другой фильм", name),
                            InlineKeyboardButton.WithCallbackData("Другой год", "yearsSearch")
                        }
                    });
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, $"{films[k].Title} {films[k].Link}", replyMarkup: inlineKeyboard);
            }
            catch (IOException z)
            {
                Console.WriteLine(z);
            }
        }
    }
}
