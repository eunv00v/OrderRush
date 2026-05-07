using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;

public class CustomerCharacter : CharacterBase
{
    public Order Order { get; set; }
    public DiningTable AssignedTable { get; private set; }
    public int AssignedSeatIndex { get; private set; }

    private const float EAT_DURATION = 5f;
    public const float WAIT_TIME_LIMIT = 60f;

    private ILevelContextPresenter _levelContext;
    private Vector3 _spawnPosition;

    private IPublisher<PaymentEvent> _paymentPublisher;
    private OrderIconFactory _orderIconFactory;

    [Inject]
    public void Construct(IPublisher<PaymentEvent> paymentPublisher, ILevelContextPresenter levelContext, OrderIconFactory orderIconFactory)
    {
        _paymentPublisher = paymentPublisher;
        _levelContext = levelContext;
        _orderIconFactory = orderIconFactory;
    }

    public void SetSpawnPosition(Vector3 position)
    {
        _spawnPosition = position;
    }

    public void GoToSeat(DiningTable targetTable, int seatIndex)
    {
        AssignedTable = targetTable;
        AssignedSeatIndex = seatIndex;

        // 이동 → 착석 → 주문 생성
        var targetSeat = targetTable.GetSeatTransform(seatIndex);
        if (targetSeat == null)
        {
            Debug.LogError($"[CustomerCharacter] Invalid seat index: {seatIndex}");
            return;
        }
        EnqueueAction(new MoveAction(_mover, targetSeat.position, _animator));
        EnqueueAction(new SitAction(this, AssignedTable, AssignedSeatIndex));
        EnqueueAction(new WaitForOrderAction());
    }

    public void Leave()
    {
        Debug.Log($"[CustomerCharacter] Leaving table...");

        // 기존 액션 모두 취소
        ClearActions();

        // LeaveAction 추가 (이동 후 Destroy)
        EnqueueAction(new LeaveAction(this, _spawnPosition, _mover, _animator));
    }

    public void OnWaitTimeout()
    {
        Debug.Log($"[CustomerCharacter] Wait timeout! Leaving angry...");
        Leave();
    }


    public bool TryTakeOrder()
    {
        var currentActionType = _actionExecutor.CurrentAction != null ? _actionExecutor.CurrentAction.GetType().Name : "null";
        Debug.Log($"[CustomerCharacter] Trying to take order... CurrentAction: {currentActionType}, Order: {Order}");

        // 이미 주문했으면 무시
        if (Order != null)
        {
            Debug.Log("[CustomerCharacter] Already ordered");
            return false;
        }

        // 현재 Action이 WaitForOrderAction이면 취소
        if (_actionExecutor.CurrentAction is WaitForOrderAction)
        {
            Debug.Log("[CustomerCharacter] Cancelling WaitForOrderAction");
            _actionExecutor.CancelCurrentAction();
        }

        EnqueueAction(new OrderAction(this, _levelContext));
        EnqueueAction(new WaitForFoodAction(this, _orderIconFactory));
        return true;
    }

    public void ServedFood()
    {

    }

}
