namespace Web.ViewModels
{
    public class BaseChildViewModel<TParent> : BaseViewModel
    {
        public int ParentId { get; set; }
    }
}
