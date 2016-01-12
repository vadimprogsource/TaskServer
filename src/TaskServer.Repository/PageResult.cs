using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Repository
{
    internal class PageResult<TInterface> : IPage<TInterface>
    {
        private List<TInterface> dataSet;

        public int PageIndex { get;  }
       
        public int Pages { get; private set; }

        public int PageSize { get; private set; }


        internal PageResult(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize  = pageSize<1?10:pageSize;
        }


        public IEnumerator<TInterface> GetEnumerator()
        {
            return dataSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public  IPage<TInterface> ExecuteWith<TEntity>(IQueryable<TEntity> query) where TEntity : TInterface
        {

            int total = query.Count();

            if (PageSize < 1)
            {
                PageSize = 1;
            }

            Pages =  total/ PageSize;

            if ((total % PageSize) > 0)
            {
                ++Pages;
            }


            if (PageIndex > 0)
            {
                query = query.Skip(PageIndex * PageSize);
            }

            dataSet = query
                        .Take(PageSize)
                        .AsEnumerable()
                        .OfType<TInterface>()
                        .ToList();

            return this;
           
        }
    }
}
