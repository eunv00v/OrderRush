using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [NotNull][SerializeField] private TMP_Text _coinText;
    [NotNull][SerializeField] private Image _timerFill;

    public void SetCoin(int value)
    {
        _coinText.text = value.ToString();
    }

    public void SetTimeGauge(float ratio)
    {
        _timerFill.fillAmount = ratio;
    }

}
