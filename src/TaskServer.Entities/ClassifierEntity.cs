using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class ClassifierEntity : IClassifier
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
