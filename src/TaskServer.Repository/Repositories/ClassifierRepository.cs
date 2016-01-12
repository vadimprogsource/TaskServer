using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Entities;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Repository;

namespace TaskServer.Repository.Repositories
{
    public class ClassifierRepository : RepositoryBase, IClassifierRepository
    {

        public ClassifierRepository(RepositoryFactory factory) : base(factory)
        {

        }
        public IPriority GetPriority(int id)
        {
            return Context.Set<PriorityEntity>().Find(id);
        }
        public IStatus GetStatus(int id)
        {
            return Context.Set<StatusEntity>().Find(id);
        }

        public IEnumerable<IPriority> GetTaskPriorities()
        {
            return Context.Set<PriorityEntity>().ToArray();
        }

        public IEnumerable<IStatus> GetTaskStatuses()
        {
            return Context.Set<StatusEntity>().ToArray();
        }
    }
}
