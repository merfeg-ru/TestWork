using SenderService.Models;
using System;

namespace SenderService
{
    public interface IUserMessage
    {
        Guid MessageId { get; set; }
        User User { get; set; }
        DateTime CreationDate { get; set; }
    }
}
