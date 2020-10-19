using MediatR;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver.Queries
{
    public class GetListOrganizationsQuery : IRequest<List<Organization>> { }
}
