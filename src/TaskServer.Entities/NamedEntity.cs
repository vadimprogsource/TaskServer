using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class NamedEntity : Entity,INamedEntity
    {
        public string Name { get; set; }
    }
}
