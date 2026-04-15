using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class DiningSeat : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _interactPoint;
    [SerializeField] Transform _sitPoint;

    private CustomerCharacter _currentCustomer;
    private DiningTable _table;
    private int _seatIndex;

    [Inject] IOrderService _orderService;

    public string DisplayName => "DiningSeat";
    public Transform InteractPoint => _interactPoint;
    public Transform SitPoint => _sitPoint;
    public bool IsOccupied => _currentCustomer != null;

    public void Init(DiningTable table, int index)
    {
        _table = table;
        _seatIndex = index;
    }

    public void SeatCustomer(CustomerCharacter customer)
    {
        _currentCustomer = customer;
    }

    public void Clear()
    {
        _currentCustomer = null;
    }

    public async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (!IsOccupied) return;

        var plate = character.CurrentCarriable as Plate;
        if (plate == null) return;

        var ingredientDatas = plate.PlacedIngredients.Select(obj => obj.Data).ToList();
        if (!_currentCustomer.Order.Recipe.IsComplete(ingredientDatas))
            return;

        character.PutDown();
        _table.PlacePlate(_seatIndex, plate);

        _orderService.CompleteOrder(_currentCustomer.Order);
        Clear();

        await UniTask.CompletedTask;
    }
}
