using BookStore.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private NewBookAlertConfig _newBookAlertConfig;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> optionsMonitor)
        {
            _newBookAlertConfig = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(newBookAlert =>
            {
                _newBookAlertConfig = newBookAlert;
            });
        }

        public string GetName()
        {
            return _newBookAlertConfig.BookName;
        }
    }
}
