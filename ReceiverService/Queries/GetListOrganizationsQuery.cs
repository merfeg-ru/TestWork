using System.Collections.Generic;

using MediatR;
using Receiver.Models;

namespace Receiver.Queries
{
    public class GetListOrganizationsQuery : IRequest<List<Organization>> { }
}
