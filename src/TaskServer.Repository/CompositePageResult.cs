using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Repository
{
    internal class CompositePageResult<T> : PageResult<T> , ICompositeResultSet<T>
    {

        internal CompositePageResult(int pageIndex, int pageSize):base(pageIndex,pageSize)
        {

        }

        public IPage<T> Page
        {
            get
            {
                return this;
            }
        }
    }
}
