using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItemView : MonoBehaviour
{
    [NotNull][SerializeField] private TMP_Text _nameText;
    [NotNull][SerializeField] private TMP_Text _descriptionText;
    [NotNull][SerializeField] private TMP_Text _costText;
    [NotNull][SerializeField] private Button _button;

    private CardData _cardData;
    private Action<CardData> _onCardClicked;


    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Setup(CardData cardData, bool canPurchase, Action<CardData> onCardClicked)
    {
        _cardData = cardData;
        _onCardClicked = onCardClicked;

        _nameText.text = cardData.CardName;
        _descriptionText.text = cardData.Description;
        _costText.text = cardData.Cost.ToString();

        _button.interactable = canPurchase;
    }

    private void OnButtonClicked()
    {
        _onCardClicked?.Invoke(_cardData);
    }

}
