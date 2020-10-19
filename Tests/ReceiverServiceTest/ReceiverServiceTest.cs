using AutoFixture.Xunit2;
using FakeItEasy;
using Receiver;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using System.Linq;
using System.Linq.Expressions;
using MockQueryable.FakeItEasy;

namespace ReceiverTest
{
    [ExcludeFromCodeCoverage]
    public class ReceiverServiceTest
    {
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();

                cfg.CreateMap<Organization, OrganizationDTO>();
                cfg.CreateMap<OrganizationDTO, Organization>();
            });

            return config.CreateMapper();
        }

        [Theory, AutoData]
        public async Task AddUserAsyncTest(int newUserId)
        {
            var repository = A.Fake<IReceiverRepository>();
            A.CallTo(() => repository.AddUserAsync(A<UserDTO>._, CancellationToken.None)).Returns(newUserId);

            var service = new ReceiverService(repository, GetMapper());
            var result = await service.AddUserAsync(new User(), CancellationToken.None);

            Assert.Equal(newUserId, result);
        }

        [Theory, AutoData]
        public async Task GetOrganizationsAsyncTest(string str1, string str2, string str3)
        {
            var repository = A.Fake<IReceiverRepository>();
            var tempOrg = new List<OrganizationDTO>
            {
                new OrganizationDTO { Name = str1, Users = new List<UserDTO> { new UserDTO { FirstName = str1} } },
                new OrganizationDTO { Name = str2, Users = new List<UserDTO> { new UserDTO { FirstName = str2} } },
                new OrganizationDTO { Name = str3, Users = new List<UserDTO> { new UserDTO { FirstName = str3} } }
            };
            A.CallTo(() => repository.GetOrganizations())
                .Returns(tempOrg.AsQueryable().BuildMock());

            var service = new ReceiverService(repository, GetMapper());
            var result = await service.GetOrganizationsAsync(CancellationToken.None);

            Assert.Equal(str1, result[0].Name);
            Assert.Equal(str2, result[1].Name);
            Assert.Equal(str3, result[2].Name);

            Assert.Equal(str1, result[0].Users[0].FirstName = str1);
            Assert.Equal(str2, result[1].Users[0].FirstName = str2);
            Assert.Equal(str3, result[2].Users[0].FirstName = str3);
        }

        [Theory, AutoData]
        public async Task GetUsersPageCountAsyncTest(int userCount, int pageSize)
        {
            var repository = A.Fake<IReceiverRepository>();

            var org = new OrganizationDTO
            {
                Name = "org",
                OrganizationId = 1
            };

            var userList = new List<UserDTO>();
            for (int i = 0; i < userCount; i++)
            {
                userList.Add(new UserDTO 
                {
                    FirstName = $"user{i}",
                    Organization = org
                });
            }

            A.CallTo(() => repository.GetUsers())
                .Returns(userList.AsQueryable().BuildMock());

            var service = new ReceiverService(repository, GetMapper());
            var result = await service.GetUsersPageCountAsync(1, pageSize, CancellationToken.None);
            var expected = (userCount / pageSize) + 1;


            Assert.Equal(expected, result);
        }
    }
}
