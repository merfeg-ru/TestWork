using MediatR;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Queries
{
    public class GetListOrganizationsQuery : IRequest<List<Organization>> { }
}
