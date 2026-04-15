using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CustomerService : ICustomerService, ITickable
{
    private LevelContext _levelContext;
    private List<RecipeData> _availableRecipes;
    private float _spawnInterval;
    private readonly IOrderService _orderService;
    private readonly SpawnFactory _spawnFactory;
    private readonly ILevelProgressService _levelProgressService;
    private float _timer;
    private bool _hasSpawned;

    public CustomerService(IOrderService orderService, SpawnFactory spawnFactory, ILevelProgressService levelProgressService)
    {
        _orderService = orderService;
        _spawnFactory = spawnFactory;
        _levelProgressService = levelProgressService;
    }

    public void SetTables(LevelContext levelContext, LevelData levelData)
    {
        _levelContext = levelContext;
        _availableRecipes = levelData.AvailableRecipes;
        _spawnInterval = 10f;
        _hasSpawned = false;
    }

    public void Tick()
    {
        if (_levelContext == null) return;
        if (_hasSpawned) return;

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _hasSpawned = true;
            TrySpawn();
        }
    }

    private async void TrySpawn()
    {
        var table = _levelContext.DiningTables.FirstOrDefault(t => t.GetAvailableSeat() != null);
        if (table == null) return;

        var availableSeat = table.GetAvailableSeat();
        if (availableSeat == null) return;

        if (_availableRecipes == null || _availableRecipes.Count == 0) return;
        var randomRecipe = _availableRecipes[Random.Range(0, _availableRecipes.Count)];

        var customer = await _spawnFactory.Create<CustomerCharacter>(PrefabKeys.GetPrefabPath(PrefabKeys.CustomerCharacter1));
        customer.transform.position = _levelContext.SpawnTransform.position;
        customer.transform.SetParent(_levelContext.transform, worldPositionStays: true);
        customer.transform.localScale = Vector3.one;

        customer.GoToSeat(availableSeat, randomRecipe);
    }


}
