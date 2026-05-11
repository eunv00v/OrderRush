using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GaugeView : MonoBehaviour, IUIView
{
    [NotNull][SerializeField] private Image _fillImage;

    public void SetProgress(float value)
    {
        _fillImage.fillAmount = Mathf.Clamp01(value);
    }

    public void SetColor(Color color)
    {
        _fillImage.color = color;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _fillImage.fillAmount = 0f;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
