using System;

namespace Receiver.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, int id)
            : base($"Запись {entity} с id:{id} не найдена!") { }
    }
}
