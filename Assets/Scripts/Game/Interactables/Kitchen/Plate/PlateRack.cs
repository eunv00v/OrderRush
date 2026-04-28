using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class PlateRack : InteractableBase
{
    [NotNull][SerializeField] GameObject[] _plates;
    [SerializeField] int _quantity = 4;

    private SpawnFactory _factory;
    private int _currentPlateIndex;

    [Inject]
    public void Construct(SpawnFactory factory)
    {
        _factory = factory;
    }

    void Start()
    {
        _currentPlateIndex = _quantity;
        foreach (var plate in _plates)
        {
            plate.SetActive(true);
        }
    }

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character == null) return;

        if (_currentPlateIndex <= 0)
        {
            return;
        }

        if (character.IsHolding)
        {
            if (character.CurrentCarriable.GetCarriableType() == CarriableType.Ingredient)
            {
                var plate = await GetNewPlate();
                if (plate.TryPlaceOntoOther(character.CurrentCarriable))
                {
                    await character.PickUp(plate);
                    _currentPlateIndex--;
                    UpdatePlateCount();
                }
                else
                {
                    Destroy(plate.gameObject);
                }
            }
        }
        else
        {
            var plate = await GetNewPlate();
            if (plate != null)
            {
                await character.PickUp(plate);
                _currentPlateIndex--;
                UpdatePlateCount();
            }
        }

        await UniTask.CompletedTask;
    }

    private async UniTask<Plate> GetNewPlate()
    {
        return await _factory.Create<Plate>(PrefabKeys.GetPrefabPath(PrefabKeys.Plate));

    }

    void UpdatePlateCount()
    {
        if (_plates.Length <= _currentPlateIndex)
        {
            return;
        }
        _plates[_currentPlateIndex].SetActive(false);
    }
}
