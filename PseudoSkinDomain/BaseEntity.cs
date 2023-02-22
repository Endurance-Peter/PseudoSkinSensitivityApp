using System;

namespace PseudoSkinDomain
{
    public class BaseEntity 
    {
        public virtual Guid Id { get; set; }
    }

    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
