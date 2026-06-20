public class ClearPlateWorkItem : WorkItem
{
    public DiningTable Table { get; }

    public ClearPlateWorkItem(DiningTable table) : base(WorkType.ClearPlate)
    {
        Table = table;
    }

    public override bool IsValid() => Table != null && Table.HasDirtyPlate;
}
