using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Security;

namespace TaskServer.Core.Helpers
{
    public static class UserContextHelper
    {
        public static bool IsEqual(this IUserContext context , IUser user)
        {
            return context.Sid == user.Id;
        }

        public static bool IsEqualOr(this IUserContext context,params IUser[] users)
        {
            foreach (IUser x in users)
            {
                if (x.Id == context.Sid)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
