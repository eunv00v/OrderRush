using Cysharp.Threading.Tasks;

public interface ILevelProgressService
{
    UniTask LoadLevelsData();
    void LoadMaxReachedLevel();
    LevelData GetLevelData(int levelNumber);
    int GetMaxReachedLevel();
    void SetMaxReachedLevel(int levelNumber);
    int GetTotalLevelCount();
    void SetSelectedLevel(int levelNumber);
    int GetSelectedLevel();
}
