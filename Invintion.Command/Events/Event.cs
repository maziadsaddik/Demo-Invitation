namespace Invitation.Command.Events
{

    public abstract record Event(
    int Id,
    string AggregateId,
    int Sequence,
    DateTime DateTime,
    int Version
    )
    {
        public string Type => GetType().Name;
    }

    public abstract record Event<T>(
        string AggregateId,
        int Sequence,
        DateTime DateTime,
        T Data,
        int Version
    ) : Event(Id:default,AggregateId, Sequence, DateTime, Version);
}
