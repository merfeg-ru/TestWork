using SenderService.Models;
using System;

namespace SenderService
{
    public class UserMessage : IUserMessage
    {
        public Guid MessageId { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return $"{MessageId} {CreationDate} [{User}]";
        }
    }
}
