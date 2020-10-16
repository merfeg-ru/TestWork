using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, int id)
            : base($"Запись {entity} с id:{id} не найдена!") { }
    }
}
