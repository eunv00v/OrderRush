using Cysharp.Threading.Tasks;

public interface ILevelContextPresenter
{
    LevelData CurrentLevelData { get; }
    LevelContext CurrentLevelContext { get; }
    UniTask LoadLevelContext(int levelNumber);
}
