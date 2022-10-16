using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

using Module11.Services;

namespace Module11.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage; 
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчёт символов" , $"count"),
                        InlineKeyboardButton.WithCallbackData($" Вычисление суммы" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>Учебный бот</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Он умеет считать символы в тексте и складывать числа.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    if (_memoryStorage.GetSession(message.Chat.Id).Function== "count")
                    {
                        Console.WriteLine(Handler.Count(message.Text));
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, Handler.Count(message.Text), cancellationToken: ct);
                    }
                    else
                    {
                        Console.WriteLine(Handler.Sum(message.Text));
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, Handler.Sum(message.Text), cancellationToken: ct);
                    }


                    //await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                    break;
            }
        }
    }
}
