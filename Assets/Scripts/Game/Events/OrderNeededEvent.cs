public readonly struct OrderNeededEvent
{
    public readonly DiningTable Table;

    public OrderNeededEvent(DiningTable table)
    {
        Table = table;
    }
}
