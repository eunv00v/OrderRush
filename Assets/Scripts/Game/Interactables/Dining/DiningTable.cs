using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DiningTable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _interactPoint;
    [SerializeField] Transform[] _plateSlots;
    [SerializeField] DiningSeat[] _seats;

    public string DisplayName => "DiningTable";
    public Transform InteractPoint => _interactPoint;
    private List<Plate> _currentPlates = new List<Plate>();

    void Awake()
    {
        for (int i = 0; i < _seats.Length; i++)
        {
            _seats[i].Init(this, i);
        }
    }

    public void PlacePlate(int seatIndex, Plate plate)
    {
        _currentPlates[seatIndex] = plate;
        plate.transform.position = _plateSlots[seatIndex].position;
    }

    public DiningSeat GetAvailableSeat()
    {
        return _seats.FirstOrDefault(seat => !seat.IsOccupied);
    }


    public async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character.IsHolding) return;

        Plate plate = null;
        int plateIndex = -1;

        for (int i = 0; i < _currentPlates.Count; i++)
        {
            if (_currentPlates[i] != null)
            {
                plate = _currentPlates[i];
                plateIndex = i;
                break;
            }
        }

        if (plate == null) return;

        character.PickUp(plate);
        _currentPlates[plateIndex] = null;

        await UniTask.CompletedTask;
    }
}
