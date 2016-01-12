using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Dto
{

   
    public class NewTaskDto
    {
        public bool HasOwnControl { get; set; }
        public Guid? TaskId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? ManagerId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

    }
}
