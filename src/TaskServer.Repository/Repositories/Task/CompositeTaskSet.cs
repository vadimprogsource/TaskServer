using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Entities;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Repository.Repositories.Task
{
    internal class CompositeTaskSet :CompositePageResult<ITask>, ICompositeTaskSet
    {
        public CompositeTaskSet(int pageIndex, int pageSize) : base(pageIndex, pageSize) { }

        public IUser LoggedUser 
        {
            get
            {
                return null;
            }
        }

        public IEnumerable<IAggregateClassifier> Priorities
        {
            get;
            private set;
        }
        public IEnumerable<IAggregateClassifier> Statuses
        {
            get;
            private set;
        }

        public IEnumerable<IUser> Users
        {
            get;
            private set;
        }

        public ICompositeTaskSet ExecuteWith(IQueryable<TaskEntity> query)
        {
            Statuses = query
                            .GroupBy(x => x.StatusId)
                            .Select (x => new  {Id = x.Key, Total = x.Count()}) 
                            .AsEnumerable()
                            .Select(x=>new AggregateClassifierEntity { Id = x.Id , Name = string.Empty , Total = x.Total})
                            .ToArray();

            Priorities = query
                            .GroupBy(x => x.PriorityId)
                            .Select(x => new { Id = x.Key, Total = x.Count() })
                            .AsEnumerable()
                            .Select(x => new AggregateClassifierEntity { Id = x.Id, Name = string.Empty, Total = x.Total })
                            .ToArray();


            Users = query
                            .GroupBy(x => x.EmployeeId)
                            .Select(x => new  { Id = x.Key, TotalTasks = x.Count() })
                            .AsEnumerable()
                            .Select(x=>new UserEntity {Id = x.Id , TotalTasks = x.TotalTasks })
                            .ToArray();

            base.ExecuteWith(query);
            return this;
        }
    }
}
