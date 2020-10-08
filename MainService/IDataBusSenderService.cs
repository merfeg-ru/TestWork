using MainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainService
{
    public interface IDataBusSenderService
    {
        Task<bool> Send(User user, CancellationToken cancellationToken);
    }
}
