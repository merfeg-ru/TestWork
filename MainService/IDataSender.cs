using MainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainService
{
    interface IDataSender
    {
        void Send(User user);
    }
}
