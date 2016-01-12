using System;
using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class StatusEntity : ClassifierEntity, IStatus
    {
        public StatusCode Code
        {
            get
            {
                return (StatusCode)Id;
            }
        }
    }
}
