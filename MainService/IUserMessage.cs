using MainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainService
{
    public interface IUserMessage
    {
        Guid MessageId { get; set; }
        User User { get; set; }
        DateTime CreationDate { get; set; }
    }
}
