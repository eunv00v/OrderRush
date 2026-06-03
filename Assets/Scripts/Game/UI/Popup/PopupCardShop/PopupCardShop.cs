using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCardShop : MonoBehaviour
{
    [NotNull][SerializeField] private CardItemView[] _cardItems;
    [NotNull][SerializeField] private Button _skipButton;

    public Button SkipButton => _skipButton;

    public void SetupCards(List<CardData> cards, int currentCoins, Action<CardData> onCardClicked)
    {
        for (int i = 0; i < _cardItems.Length; i++)
        {
            if (i < cards.Count)
            {
                bool canPurchase = currentCoins >= cards[i].Cost;
                _cardItems[i].Setup(cards[i], canPurchase, onCardClicked);
                _cardItems[i].gameObject.SetActive(true);
            }
            else
            {
                _cardItems[i].gameObject.SetActive(false);
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
