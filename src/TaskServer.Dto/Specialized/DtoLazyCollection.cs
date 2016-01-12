using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Dto.Specialized
{
    public class DtoLazyCollection<T> : IReadOnlyCollection<T>
    {
        private List<T>              listPtr = null;
        private Func<IEnumerable<T>> getList;


        public DtoLazyCollection(Func<IEnumerable<T>> getList)
        {
            this.getList = getList;
        }


        private IList<T> GetList()
        {
            if(listPtr==null)
            {
                listPtr = getList().ToList();
           }

            return listPtr;
        }



        public int Count
        {
            get
            {
                return listPtr.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
