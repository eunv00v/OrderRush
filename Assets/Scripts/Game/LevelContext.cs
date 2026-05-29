using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    [NotNull][SerializeField] DiningTable _diningTable;
    [NotNull][SerializeField] Transform[] _tablePoints;
    [NotNull][SerializeField] Transform _spawnPoint;
    [NotNull][SerializeField] Transform _waitingPoint;

    public List<DiningTable> DiningTables { get; private set; }
    public Transform SpawnPoint => _spawnPoint;
    public Transform WaitingPoint => _waitingPoint;

    void Awake()
    {
        DiningTables = new List<DiningTable> { _diningTable };
    }

    public Transform GetNextTableSpawnPoint()
    {
        int index = DiningTables.Count - 1;
        if (index < 0 || index >= _tablePoints.Length)
            return null;
        return _tablePoints[index];
    }

    public void AddDiningTable(DiningTable table)
    {
        DiningTables.Add(table);
    }
}
