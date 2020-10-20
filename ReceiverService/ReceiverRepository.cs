using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Receiver.Context;
using Receiver.Exceptions;
using Receiver.Models;

namespace Receiver
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly UsersContext _context;

        public ReceiverRepository(UsersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddUserAsync(UserDTO userDTO, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(userDTO, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return userDTO.UserId;
        }

        public async Task AddUserToOrganizationAsync(int userId, int organizationId, CancellationToken cancellationToken)
        {
            var dbUser = _context.Users.AsNoTracking().FirstOrDefault(user => user.UserId == userId) ??
                throw new EntityNotFoundException("Пользователь", userId);

            var dbOrganization = _context.Organizations.AsNoTracking().FirstOrDefault(org => org.OrganizationId == organizationId) ??
                throw new EntityNotFoundException("Организация", organizationId);

            dbUser.Organization = dbOrganization;

            await UpdateUserAsync(dbUser, cancellationToken);
        }

        public IQueryable<OrganizationDTO> GetOrganizations() =>
            _context.Organizations.AsNoTracking();

        public IQueryable<UserDTO> GetUsers() =>
            _context.Users.AsNoTracking();

        public async Task<UserDTO> UpdateUserAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId) ??
                throw new EntityNotFoundException("Пользователь", user.UserId);

            dbUser.FirstName = user.FirstName;
            dbUser.EMail = user.EMail;
            dbUser.LastName = user.LastName;
            dbUser.MiddleName = user.MiddleName;
            dbUser.Organization = user.Organization;
            dbUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
            return dbUser;
        }

        /// <summary>
        /// Первоначальная инициализация данных (seed)
        /// </summary>
        public void Initialize()
        {
            if (_context.Users.Count() == 0)
            {
                OrganizationDTO oracle = new OrganizationDTO { Name = "Oracle" };
                OrganizationDTO google = new OrganizationDTO { Name = "Google" };
                OrganizationDTO microsoft = new OrganizationDTO { Name = "Microsoft" };
                OrganizationDTO apple = new OrganizationDTO { Name = "Apple" };

                _context.Organizations.AddRange(oracle, microsoft, google, apple);
                _context.Users.AddRange(
                    new UserDTO { FirstName = "Олег", LastName = "Васильев", MiddleName = "Иванович", EMail = "Oleg@mail.ru", PhoneNumber = "+79998886677", Organization = oracle },
                    new UserDTO { FirstName = "Александр", LastName = "Овсов", MiddleName = "Романович", EMail = "Alex@mail.ru", PhoneNumber = "+79998886676", Organization = oracle },
                    new UserDTO { FirstName = "Алексей", LastName = "Петров", MiddleName = "Тимурович", EMail = "Alex123@mail.ru", PhoneNumber = "+79998886675", Organization = microsoft },
                    new UserDTO { FirstName = "Иван", LastName = "Иванов", MiddleName = "Алексеевич", EMail = "Ivan@mail.ru", PhoneNumber = "+79998886674", Organization = microsoft },
                    new UserDTO { FirstName = "Петр", LastName = "Андреев", MiddleName = "Станиславович", EMail = "Petya@mail.ru", PhoneNumber = "+79998886673", Organization = microsoft },
                    new UserDTO { FirstName = "Василий", LastName = "Иванов", MiddleName = "Максимович", EMail = "Vas@mail.ru", PhoneNumber = "+79998886672", Organization = google },
                    new UserDTO { FirstName = "Олег", LastName = "Кузнецов", MiddleName = "Дмитриевич", EMail = "Oleg1995@mail.ru", PhoneNumber = "+79998886671", Organization = google },
                    new UserDTO { FirstName = "Андрей", LastName = "Петров", MiddleName = "Петрович", EMail = "Vah@mail.ru", PhoneNumber = "+79998886670", Organization = apple }
                    );

                _context.SaveChanges();
            }
        }
    }
}
