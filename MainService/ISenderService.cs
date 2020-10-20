using Sender.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    public interface ISenderService
    {
        Task<bool> Send(User user, CancellationToken cancellationToken);
    }
}
