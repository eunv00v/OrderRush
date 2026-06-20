using MessagePipe;
using VContainer;

public class ServingCounter : Counter
{
    private IPublisher<PlateOnCounterEvent> _plateOnCounterPublisher;

    [Inject]
    public void Construct(IPublisher<PlateOnCounterEvent> plateOnCounterPublisher)
    {
        _plateOnCounterPublisher = plateOnCounterPublisher;
    }

    void Start()
    {
        ItemPlaced += OnItemPlaced;
    }

    void OnDestroy()
    {
        ItemPlaced -= OnItemPlaced;
    }

    private void OnItemPlaced(Counter counter)
    {
        if (CurrentItem is Plate)
            _plateOnCounterPublisher.Publish(new PlateOnCounterEvent(this));
    }
}
