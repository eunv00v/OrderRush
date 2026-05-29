using System.Threading;
using Cysharp.Threading.Tasks;
using OrderRush.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInitiator : IStartable
{
    private readonly ILevelContextPresenter _levelPresenter;
    private readonly IDayProgressService _dayProgressService;
    private readonly ICustomerService _customerService;
    private readonly IAccountService _accountService;

    public GameInitiator(
        ILevelContextPresenter levelPresenter,
        IDayProgressService dayProgressService,
        ICustomerService customerService,
        IAccountService accountService)
    {
        _levelPresenter = levelPresenter;
        _dayProgressService = dayProgressService;
        _customerService = customerService;
        _accountService = accountService;
    }

    public async void Start()
    {
        Debug.Log("GameInitiator: Initializing game...");

        await _dayProgressService.Initialize();

        int currentDay = _accountService.Account.CurrentDay;
        _dayProgressService.StartDay(currentDay);

        await _levelPresenter.LoadLevelContext(1);

        _customerService.Initialize();

        Debug.Log("GameInitiator: Game initialized!");
    }
}
