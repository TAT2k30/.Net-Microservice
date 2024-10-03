using System;

namespace Auth.Service.Common.Entities
{
    public interface IEntity
    {
        Guid BaseId {get; set;}
    }
}