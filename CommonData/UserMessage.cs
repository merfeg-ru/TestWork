using System;

namespace CommonData
{
    public class UserMessage : IUserMessage
    {
        public Guid MessageId { get; set; }
        public IUser User { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return $"{MessageId} {CreationDate} [{User}]";
        }
    }
}
