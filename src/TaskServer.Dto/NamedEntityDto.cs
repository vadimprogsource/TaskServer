using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class NamedEntityDto : EntityDto, INamedEntity
    {
        public string Name { get; set; }

        public NamedEntityDto() { }

        public NamedEntityDto(INamedEntity other) : base(other)
        {
            Name = other.Name;
        }
    }
}
