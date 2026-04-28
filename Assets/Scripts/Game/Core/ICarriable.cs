using UnityEngine;

public interface ICarriable
{
    void AttachToSlot(Transform slot);

    CarriableType GetCarriableType();
}


public enum CarriableType
{
    None,
    Ingredient,
    Plate
}
