using CommonData;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService
{
    public interface IReceiverRepository
    {
        /// <summary>
        /// Первоначальная инициализация данных (seed)
        /// </summary>
        void Initialize();

        Task AddUserAsync(UserDTO user);
    }
}
