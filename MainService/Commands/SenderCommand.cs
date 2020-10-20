using MediatR;
using Sender.Models;

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
