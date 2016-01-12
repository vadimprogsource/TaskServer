using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{

    public enum StatusCode : int {Pending =1 , InProcess =2, Done=3,Accepted=4,Revision=5,Refused = 6,Completed =7}

   public interface IStatus : IClassifier
   {
        StatusCode Code { get; }
   }
}
