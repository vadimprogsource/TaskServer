using TaskServer.Interfaces;

namespace TaskServer.Entities
{

    public class PriorityEntity : ClassifierEntity, IPriority
    {
        public PriorityCode Code
        {
            get
            {
                return (PriorityCode)Id;
            }
        }
    }
}
