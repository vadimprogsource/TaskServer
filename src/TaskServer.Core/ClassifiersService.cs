using System;
using System.Collections.Generic;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Repository;
using TaskServer.Interfaces.Services;

namespace TaskServer.Core
{
    public class ClassifiersService : IClassifiersService
    {
        private IClassifierRepository  clsRepository;
        private IUserRepository        userRepository;


        public ClassifiersService(IClassifierRepository clsRepository,IUserRepository userRepository)
        {
            this.clsRepository  = clsRepository;
            this.userRepository = userRepository;

        }


        public IEnumerable<IPriority> GetPriorities()
        {
            return clsRepository.GetTaskPriorities();
        }

        public IPriority GetPriority(PriorityCode code)
        {
            return clsRepository.GetPriority((int)code);
        }

        public IStatus GetStatus(StatusCode code)
        {
            return clsRepository.GetStatus((int)code);
        }

        public IEnumerable<IStatus> GetStatuses()
        {
            return clsRepository.GetTaskStatuses();
        }

        public IEnumerable<IUser> GetSystemUsers()
        {
            return userRepository.GetUsers();
        }
    }
}
