namespace ApplicationCore.Entities
{
    public class BaseEntityChild<TParent> : BaseEntity
    {
        public int ParentId { get; set; }

        public TParent Parent { get; private set; }
    }
}
