using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Receiver.Models;

namespace Receiver.Queries
{
    public class GetListUsersQuery : IRequest<List<User>> { }
}
