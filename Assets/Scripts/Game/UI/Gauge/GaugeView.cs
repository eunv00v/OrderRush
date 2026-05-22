using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GaugeView : MonoBehaviour, IUIView
{
    [NotNull][SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _warning;

    private void Awake()
    {
        SetWarning(false);
    }

    public void SetProgress(float value)
    {
        _fillImage.fillAmount = Mathf.Clamp01(value);
    }

    public void SetColor(Color color)
    {
        _fillImage.color = color;
    }

    public void SetWarning(bool isShow)
    {
        if (_warning) _warning.SetActive(isShow);
    }


    public void Show()
    {
        gameObject.SetActive(true);
        SetWarning(false);
        _fillImage.fillAmount = 0f;
    }

    public void Hide()
    {
        SetWarning(false);
        gameObject.SetActive(false);

    }

}
