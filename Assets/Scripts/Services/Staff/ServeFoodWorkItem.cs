public class ServeFoodWorkItem : WorkItem
{
    public Counter Counter { get; }
    public DiningTable Table { get; }

    public ServeFoodWorkItem(Counter counter, DiningTable table) : base(WorkType.ServeFood)
    {
        Counter = counter;
        Table = table;
    }

    public override bool IsValid()
    {
        if (Counter.CurrentItem is not Plate plate || plate.MatchedRecipeID == -1)
            return false;
        return Table != null && Table.GetPendingRecipeIDs().Contains(plate.MatchedRecipeID);
    }
}
