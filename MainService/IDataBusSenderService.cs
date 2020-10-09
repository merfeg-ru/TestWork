using SenderService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SenderService
{
    public interface IDataBusSenderService
    {
        Task<bool> Send(User user, CancellationToken cancellationToken);
    }
}
