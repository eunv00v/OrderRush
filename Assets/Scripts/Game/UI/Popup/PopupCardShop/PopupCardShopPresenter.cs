using System;
using Cysharp.Threading.Tasks;
using OrderRush.Services;
using VContainer.Unity;

public class PopupCardShopPresenter : IStartable, IDisposable
{
    private readonly PopupCardShop _view;
    private readonly ICardService _cardService;
    private readonly IAccountService _accountService;

    public PopupCardShopPresenter(
        PopupCardShop view,
        ICardService cardService,
        IAccountService accountService)
    {
        _view = view;
        _cardService = cardService;
        _accountService = accountService;
    }

    public void Start()
    {
        _view.Hide();
        _view.SkipButton.onClick.AddListener(OnSkipButtonClicked);
    }

    public void ShowPopup()
    {
        var randomCards = _cardService.GetRandomCardsForSelection(3);
        int currentCoins = _accountService.Account.Coins.Value;

        _view.SetupCards(randomCards, currentCoins, OnCardClicked);
        _view.Show();
    }

    private async void OnCardClicked(CardData card)
    {
        bool success = await _cardService.TryPurchaseCard(card);
        if (success)
        {
            _view.Hide();
        }
    }

    private void OnSkipButtonClicked()
    {
        _view.Hide();
    }

    public void Dispose()
    {
        _view.SkipButton.onClick.RemoveListener(OnSkipButtonClicked);
    }
}
