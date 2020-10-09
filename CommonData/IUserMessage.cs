using System;

namespace CommonData
{
    public interface IUserMessage
    {
        Guid MessageId { get; set; }
        IUser User { get; set; }
        DateTime CreationDate { get; set; }
    }
}
