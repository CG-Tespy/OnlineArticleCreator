using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.AspNetMvc
{
    public interface IViewModel<TModel>
    {
        TModel CreateModel();
    }
}
