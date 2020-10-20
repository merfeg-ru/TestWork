using AutoFixture.Xunit2;
using CommonData;
using FakeItEasy;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sender;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SenderServiceTest
{
    public class SenderServiceTest
    {
        [Fact]
        public async Task AddUserAsyncTest()
        {
            var publishEndpoint = A.Fake<IPublishEndpoint>();
            var logger = A.Fake<ILogger<SenderService>>();

            var service = new SenderService(logger, publishEndpoint);
            await service.Send(null, CancellationToken.None);

            A.CallTo(() => publishEndpoint.Publish<IUserMessage>(A<UserMessage>._, A<CancellationToken>._)).MustHaveHappened();
        }
    }
}
