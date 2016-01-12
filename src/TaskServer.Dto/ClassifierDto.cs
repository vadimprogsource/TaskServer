using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class ClassifierDto : IClassifier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ClassifierDto() { }

        public ClassifierDto(IClassifier outer)
        {
            Id   = outer.Id;
            Name = outer.Name;
        }


    }
}
