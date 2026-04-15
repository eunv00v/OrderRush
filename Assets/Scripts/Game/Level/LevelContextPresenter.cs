using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class LevelContextPresenter : ILevelContextPresenter, ITickable, IDisposable
{
    private readonly IResourcesLoaderService _resourcesLoaderService;
    private readonly ILevelProgressService _levelsDataService;
    private readonly IOrderService _orderService;
    private readonly LevelFactory _levelFactory;

    public LevelData CurrentLevelData { get; private set; }
    public LevelContext CurrentLevelContext { get; private set; }
    private int _currentMoney;
    private float _elapsedTime;
    private bool _isLevelActive;


    public LevelContextPresenter(IResourcesLoaderService resourcesLoaderService,
        ILevelProgressService levelsDataService,
        IOrderService orderService,
        LevelFactory levelFactory)
    {
        _resourcesLoaderService = resourcesLoaderService;
        _levelsDataService = levelsDataService;
        _orderService = orderService;
        _levelFactory = levelFactory;
    }

    public async UniTask LoadLevelContext(int levelNumber)
    {
        _isLevelActive = false;

        // 1. 레벨 데이터 가져오기
        CurrentLevelData = _levelsDataService.GetLevelData(levelNumber);
        if (CurrentLevelData == null)
        {
            Debug.LogError($"Level {levelNumber} data not found!");
            return;
        }

        // 2. 레벨 맵 로드 //LoadLevelsData
        var levelContext = await _levelFactory.CreateLevelContext(levelNumber);
        if (levelContext == null)
        {
            Debug.LogError($"Failed to load level map: Level{levelNumber}");
            return;
        }
        CurrentLevelContext = levelContext;

        // 3. 초기화
        _currentMoney = 0;
        _elapsedTime = 0f;
        _isLevelActive = true;

        Debug.Log($"Level {CurrentLevelData.LevelNumber} loaded: {CurrentLevelData.LevelName}");
    }

    public void AddMoney(int amount)
    {
        if (!_isLevelActive) return;

        _currentMoney += amount;

        CheckLevelCompleted();
    }

    public void Tick()
    {
        if (!_isLevelActive || CurrentLevelData == null) return;

        _elapsedTime += Time.deltaTime;

        // 시간 제한 체크
        if (CurrentLevelData.TimeLimit > 0 && _elapsedTime >= CurrentLevelData.TimeLimit)
        {
            CheckLevelFailed();
        }
    }

    private void CheckLevelCompleted()
    {
        if (_currentMoney >= CurrentLevelData.TargetMoney)
        {
            _isLevelActive = false;
            _levelsDataService.SetMaxReachedLevel(CurrentLevelData.LevelNumber + 1);
            Debug.Log($"Level {CurrentLevelData.LevelNumber} completed!");
        }
    }

    private void CheckLevelFailed()
    {
        if (_currentMoney < CurrentLevelData.TargetMoney)
        {
            _isLevelActive = false;
            Debug.Log($"Level {CurrentLevelData.LevelNumber} failed!");
        }
    }

    public void Dispose()
    {
        if (CurrentLevelContext != null && CurrentLevelData != null)
        {
            _levelFactory.ReleaseLevelContext(CurrentLevelData.LevelNumber);
        }
    }
}
