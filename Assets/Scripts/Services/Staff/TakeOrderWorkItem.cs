public class TakeOrderWorkItem : WorkItem
{
    public DiningTable Table { get; }

    public TakeOrderWorkItem(DiningTable table) : base(WorkType.TakeOrder)
    {
        Table = table;
    }

    public override bool IsValid() => Table != null && Table.HasCustomersNeedingOrder;
}
