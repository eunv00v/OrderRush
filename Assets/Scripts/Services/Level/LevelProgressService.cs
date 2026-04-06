using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private readonly IResourcesLoaderService _resourceLoader;
    private LevelsData _levelsData;
    private int _maxReachedLevel = 1;
    private int _selectedLevel = 1;
    private const string PREF_MAX_LEVEL = "MaxReachedLevel";

    public LevelProgressService(IResourcesLoaderService resourceLoader)
    {
        _resourceLoader = resourceLoader;
    }

    public async UniTask LoadLevelsData()
    {
        string path = PrefabKeys.GetPrefabPath(DataKeys.GetDataPath(DataKeys.LevelSettings));
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
            PlayerPrefs.SetInt(PREF_MAX_LEVEL, _maxReachedLevel);
            PlayerPrefs.Save();
            Debug.Log($"Max reached level updated: {_maxReachedLevel}");
        }
    }

    public int GetTotalLevelCount()
    {
        return _levelsData != null ? _levelsData.GetTotalLevelCount() : 0;
    }

    private void LoadMaxReachedLevel()
    {
        _maxReachedLevel = PlayerPrefs.GetInt(PREF_MAX_LEVEL, 1);
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
