using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class BaseEntityChild<TParent> : BaseEntity
    {
        public int ParentId { get; set; }

        public TParent Parent { get; private set; }
    }
}
