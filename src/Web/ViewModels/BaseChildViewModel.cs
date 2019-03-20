using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class BaseChildViewModel<TParent> : BaseViewModel
    {
        public int Parentid { get; set; }
    }
}
