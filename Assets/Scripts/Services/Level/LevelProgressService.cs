using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private readonly IResourcesLoaderService _resourceLoader;
    private readonly IUserDataService _userDataService;
    private LevelsData _levelsData;
    private int _maxReachedLevel = 1;
    private int _selectedLevel = 1;

    public LevelProgressService(
        IResourcesLoaderService resourceLoader,
        IUserDataService userDataService)
    {
        _resourceLoader = resourceLoader;
        _userDataService = userDataService;
    }

    public async UniTask LoadLevelsData()
    {
        string path = DataKeys.GetDataPath(DataKeys.LevelSettings);
        _levelsData = await _resourceLoader.LoadAsync<LevelsData>(path);
    }

    public LevelData GetLevelData(int levelNumber)
    {
        if (_levelsData == null)
        {
            Debug.LogError("LevelsData not loaded yet!");
            return null;
        }

        return _levelsData.GetLevel(levelNumber);
    }

    public int GetMaxReachedLevel() => _maxReachedLevel;

    public void SetMaxReachedLevel(int levelNumber)
    {
        if (levelNumber > _maxReachedLevel)
        {
            _maxReachedLevel = levelNumber;
            _userDataService.SaveInt(UserDataKeys.MaxReachedLevel, _maxReachedLevel);
        }
    }

    public int GetTotalLevelCount()
    {
        return _levelsData != null ? _levelsData.GetTotalLevelCount() : 0;
    }

    private void LoadMaxReachedLevel()
    {
        _maxReachedLevel = _userDataService.LoadInt(UserDataKeys.MaxReachedLevel, 1);
    }

    void ILevelProgressService.LoadMaxReachedLevel()
    {
        LoadMaxReachedLevel();
    }

    public void SetSelectedLevel(int levelNumber)
    {
        _selectedLevel = levelNumber;
    }

    public int GetSelectedLevel()
    {
        return _selectedLevel;
    }
}
