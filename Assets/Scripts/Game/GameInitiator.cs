using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInitiator : IStartable
{
    private readonly LevelContextPresenter _levelPresenter;
    private readonly ILevelProgressService _levelsDataService;

    [Inject]
    public GameInitiator(ILevelProgressService levelsDataService, LevelContextPresenter levelPresenter)
    {
        _levelsDataService = levelsDataService;
        _levelPresenter = levelPresenter;
    }

    public async void Start()
    {
        Debug.Log("GameInitiator: Initializing game...");

        // 레벨 데이터 초기화
        await _levelsDataService.LoadLevelsData();
        _levelsDataService.LoadMaxReachedLevel();

        // 선택된 레벨 로드 (기본값 1)
        int selectedLevel = _levelsDataService.GetSelectedLevel();
        await _levelPresenter.LoadLevelContext(selectedLevel);

        Debug.Log("GameInitiator: Game initialized!");
    }
}
