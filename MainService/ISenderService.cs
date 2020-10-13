using SenderService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SenderService
{
    public interface ISenderService
    {
        Task<bool> Send(User user, CancellationToken cancellationToken);
    }
}
