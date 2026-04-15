using UnityEngine;

public class LevelContext : MonoBehaviour
{
    [SerializeField] DiningTable[] _diningTables;
    [SerializeField] Transform _spawnTransform;

    public DiningTable[] DiningTables => _diningTables;
    public Transform SpawnTransform => _spawnTransform;
}
