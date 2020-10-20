using MediatR;
using Sender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sender.Commands
{
    public class SenderCommand : IRequest<bool>
    {
        public SenderCommand(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
