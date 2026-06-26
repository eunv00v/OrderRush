using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    [NotNull][SerializeField] private Button _startButton;
    [NotNull][SerializeField] private Button _resetButton;
    [NotNull][SerializeField] private TMP_Text _dayText;
    [NotNull][SerializeField] private TMP_Text _coinText;

    public Button StartButton => _startButton;
    public Button ResetButton => _resetButton;

    public void SetDay(int day)
    {
        _dayText.text = $"Day {day}";
    }

    public void SetCoins(int coins)
    {
        _coinText.text = $"{coins}";
    }
}