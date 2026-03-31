using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlateFactory
{
    readonly IObjectResolver _container;
    readonly GameObject _platePrefab;

    public PlateFactory(IObjectResolver container, GameObject platePrefab)
    {
        _container = container;
        _platePrefab = platePrefab;
    }

    public Plate Create()
    {
        var plateObj = _container.Instantiate(_platePrefab);
        var plate = plateObj.GetComponent<Plate>();

        if (plate == null)
        {
            Debug.LogError("[PlateFactory] Prefab does not have Plate component!");
            Object.Destroy(plateObj);
            return null;
        }

        return plate;
    }
}
