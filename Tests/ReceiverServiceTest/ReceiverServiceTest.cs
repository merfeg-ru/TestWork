using AutoFixture.Xunit2;
using FakeItEasy;
using ReceiverService;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReceiverServiceTest
{
    [ExcludeFromCodeCoverage]
    public class ReceiverServiceTest
    {
        [Theory, AutoData]
        public async Task AddUserAsyncTest(string fName, string lName, string mName, string eMail, string phone)
        {
            var repository = A.Fake<IReceiverRepository>();
            A.CallTo(() => repository.GetInvoices(null)).Returns(Array.Empty<Invoice>().AsQueryable().BuildMockDbSet());
            var service = new InvoiceService(repository, null, null, null, null);
            service.GetInvoices(null);
        }
    }
}
