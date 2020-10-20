using System.Collections.Generic;

using MediatR;
using Receiver.Models;

namespace Receiver.Queries
{
    public class GetListUsersQuery : IRequest<List<User>> { }
}
