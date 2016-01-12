using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class AggregateClassifierDto : ClassifierDto,IAggregateClassifier
    {
        public int Total { get; }

        public AggregateClassifierDto() { }

        public AggregateClassifierDto(IAggregateClassifier other) : base(other)
        {
            Total = other.Total;
        }
    }
}
