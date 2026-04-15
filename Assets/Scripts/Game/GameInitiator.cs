using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInitiator : IStartable
{
    private readonly ILevelContextPresenter _levelPresenter;
    private readonly ILevelProgressService _levelsDataService;
    private readonly ICustomerService _customerService;

    [Inject]
    public GameInitiator(ILevelProgressService levelsDataService, ILevelContextPresenter levelPresenter, ICustomerService customerService)
    {
        _levelsDataService = levelsDataService;
        _levelPresenter = levelPresenter;
        _customerService = customerService;
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


        _customerService.SetTables(_levelPresenter.CurrentLevelContext, _levelPresenter.CurrentLevelData);

        Debug.Log("GameInitiator: Game initialized!");
    }
}
