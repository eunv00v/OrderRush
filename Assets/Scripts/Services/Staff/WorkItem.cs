public abstract class WorkItem
{
    public WorkType Type { get; }

    protected WorkItem(WorkType type)
    {
        Type = type;
    }

    public abstract bool IsValid();
}
