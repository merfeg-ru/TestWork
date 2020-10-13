using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonData;
using MassTransit;

namespace ReceiverService
{
    public interface IReceiverService
    {
        void InitializeReceiverRepository();

        Task AddUserAsync(IUser user);
    }
}
