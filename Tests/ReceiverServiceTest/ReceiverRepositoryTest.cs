using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

using Xunit;
using AutoFixture.Xunit2;

using Microsoft.EntityFrameworkCore;

using ReceiverService;
using ReceiverService.Context;
using ReceiverService.Models;
using System.Threading;

namespace ReceiverServiceTest
{
    [ExcludeFromCodeCoverage]
    public class ReceiverRepositoryTest
    {
        [Theory, AutoData]
        public async Task AddUserAsyncTest(string fName, string lName, string mName, string eMail, string phone)
        {
            // Создание БД в памяти
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "AddUserAsync")
                .Options;

            // Добавление данных в БД

            var userId = 0;
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                userId = await repository.AddUserAsync(new UserDTO 
                {
                    FirstName = fName,
                    LastName = lName,
                    MiddleName = mName,
                    EMail = eMail,
                    PhoneNumber = phone
                }, CancellationToken.None);
            }

            // Проверка результата
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                var user = repository.GetUsers().FirstOrDefault(f => f.UserId == userId);

                Assert.Equal(fName, user.FirstName);
                Assert.Equal(lName, user.LastName);
                Assert.Equal(mName, user.MiddleName);
                Assert.Equal(eMail, user.EMail);
                Assert.Equal(phone, user.PhoneNumber);
            }
        }

        [Theory, AutoData]
        public async Task AddUserToOrganizationAsyncTest(string orgName)
        {
            // Создание БД в памяти
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "AddUserToOrganizationAsync")
                .Options;

            // Добавление данных в БД
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                repository.Initialize();
                context.Organizations.Add(new OrganizationDTO { Name = orgName });
                context.SaveChanges();
            }

            // Проверка результата
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                var user = repository.GetUsers().FirstOrDefault();
                var org = repository.GetOrganizations().FirstOrDefault(o => o.Name == orgName);

                await repository.AddUserToOrganizationAsync(user.UserId, org.OrganizationId, CancellationToken.None);

                var userTest = context.Users.Include(u => u.Organization).FirstOrDefault();
                Assert.Equal(orgName, userTest.Organization.Name);
            }
        }

        [Theory, AutoData]
        public void GetOrganizationsTest(string orgName)
        {
            // Создание БД в памяти
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "GetOrganizations")
                .Options;

            // Добавление данных в БД
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                repository.Initialize();

                context.Organizations.Add(new OrganizationDTO 
                {
                    Name = orgName
                });

                context.SaveChanges();
            }

            // Проверка результата
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                var result = repository.GetOrganizations();

                Assert.NotNull(result.FirstOrDefault(f => f.Name == orgName));
                Assert.True(result.Count() == 5);
            }
        }

        [Theory, AutoData]
        public async void UpdateUserAsyncTest(string fName, string lName, string mName, string eMail, string phone)
        {
            // Создание БД в памяти
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "UpdateUser")
                .Options;

            // Добавление данных в БД
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                repository.Initialize();
            }

            // Проверка результата
            using (var context = new UsersContext(options))
            {
                var repository = new ReceiverRepository(context);
                var user = repository.GetUsers().FirstOrDefault();
                user.FirstName = fName;
                user.LastName = lName;
                user.MiddleName = mName;
                user.EMail = eMail;
                user.PhoneNumber = phone;

                await repository.UpdateUserAsync(user, CancellationToken.None);

                var userTest = repository.GetUsers().FirstOrDefault();
                Assert.Equal(fName, userTest.FirstName);
                Assert.Equal(lName, userTest.LastName);
                Assert.Equal(mName, userTest.MiddleName);
                Assert.Equal(eMail, userTest.EMail);
                Assert.Equal(phone, userTest.PhoneNumber);
            }
        }
    }
}
