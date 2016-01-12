using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using TaskServer.Interfaces.Repository;
using TaskServer.Interfaces;
using TaskServer.Entities;

namespace TaskServer.Repository.Repositories
{
    internal class UserRepository : RepositoryBase ,IUserRepository
    {
        public UserRepository(RepositoryFactory factory) : base(factory) { }


        private Guid ComputeHash(string password)
        {
            using (MD5 hash = MD5.Create())
            {
                return new Guid(hash.ComputeHash(Encoding.Default.GetBytes(password ?? string.Empty)));
            }
        }


        public IEnumerable<IUser> GetUsers()
        {
            return Context.Set<UserEntity>()
                          .OrderBy(x=>x.Name)
                          .ToArray();
        }

        public IAccount Login(string userName, string password)
        {
            Guid passwordHash = ComputeHash(password);
            return Context.Set<UserEntity>().FirstOrDefault(x => x.LoginName == userName && x.Password == passwordHash);

        }

        public IAccount RegisterUser(string userName, string password)
        {

            Guid passwordHash = ComputeHash(password);

            if (Context.Set<UserEntity>().Any(x => x.Password == passwordHash || x.LoginName == userName))
            {
                throw new Exception("User name or password exists !");
            }



            UserEntity user = CreateNew<UserEntity>();

            user.Id = Guid.NewGuid();

            user.Name      = userName;
            user.LoginName = userName;
            user.Password  = passwordHash;

            

            Context.SaveChanges();
            return user;

        }

        public IAccount SetPassword(Guid userId, string newPassword)
        {
            UserEntity user = Context.Set<UserEntity>().FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                user.Password = ComputeHash(newPassword);
                Context.SaveChanges();
            }

            return user;

        }

        public void UpdateUser(IUser user)
        {

            UserEntity userEntity = Context.Set<UserEntity>().FirstOrDefault(x => x.Id == user.Id);


            if (userEntity == null)
            {
                return;
            }

            userEntity.Name      = user.Name;
            Context.SaveChanges();

        }

        public void RemoveUser(Guid userId)
        {

            UserEntity user = Context.Set<UserEntity>().FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return;
            }


            Context.Set<UserEntity>().Remove(user);
            Context.SaveChanges();
        }
    }
}
