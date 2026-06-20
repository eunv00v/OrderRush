public readonly struct PlateOnCounterEvent
{
    public readonly Counter Counter;

    public PlateOnCounterEvent(Counter counter)
    {
        Counter = counter;
    }
}
