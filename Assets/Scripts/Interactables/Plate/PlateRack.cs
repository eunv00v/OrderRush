using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class PlateRack : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _interactPoint;
    [SerializeField] int _quantity = 5;
    [SerializeField] Transform _plateVisual;
    [SerializeField] float _heightPerPlate = 0.05f;
    [SerializeField] Vector3 _basePosition;

    [Inject] GameObjectFactory _factory;


    public string DisplayName => "Plate Rack";
    public Transform InteractPoint => _interactPoint;

    void Start()
    {
        UpdatePlateVisual();
    }

    public async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character == null) return;

        // 이미 들고 있으면 무시
        if (character.IsHolding)
        {
            Debug.Log("[PlateRack] Character is already holding something");
            return;
        }

        // 수량이 0이면 무시
        if (_quantity <= 0)
        {
            Debug.Log("[PlateRack] No plates available");
            return;
        }

        // 접시 생성
        var plate = await _factory.CreateAsync<Plate>(PrefabKeys.Plate);
        if (plate != null)
        {
            character.PickUp(plate);
            plate.OnPickedUp(character.ItemSlot);
            _quantity--;
            UpdatePlateVisual();
            Debug.Log($"[PlateRack] Plate picked up. Remaining: {_quantity}");
        }

        await UniTask.CompletedTask;
    }

    void UpdatePlateVisual()
    {
        if (_plateVisual == null) return;

        // 수량이 0이면 숨기기
        if (_quantity <= 0)
        {
            _plateVisual.gameObject.SetActive(false);
            return;
        }

        // 수량에 따라 높이 조정
        _plateVisual.gameObject.SetActive(true);
        _plateVisual.localPosition = _basePosition + Vector3.up * (_quantity * _heightPerPlate);
    }
}
