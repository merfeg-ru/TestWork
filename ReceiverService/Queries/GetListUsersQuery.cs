using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ReceiverService.Models;

namespace ReceiverService.Queries
{
    public class GetListUsersQuery : IRequest<List<User>> { }
}
