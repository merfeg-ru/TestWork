using MainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainService
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
