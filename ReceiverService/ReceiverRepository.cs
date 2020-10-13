using CommonData;
using ReceiverService.Context;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly UsersContext _context;

        public ReceiverRepository(UsersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            _context.Users.Add(userDTO);
            await _context.SaveChangesAsync();
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
