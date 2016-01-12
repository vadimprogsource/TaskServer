using System;
using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class StatusDto :ClassifierDto ,IStatus
    {
        public StatusDto() { }
        public StatusDto(IStatus other) : base(other) { }

        public StatusCode Code
        {
            get
            {
                return (StatusCode)Id;
            }
        }
    }
}
