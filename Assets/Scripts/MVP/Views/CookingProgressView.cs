using UnityEngine;
using UnityEngine.UI;

public class CookingProgressView : MonoBehaviour
{
    [SerializeField] Image _fill;
    [SerializeField] GameObject _root;

    public void SetProgress(float value)
    {
        _fill.fillAmount = value;
    }

    public void SetVisible(bool visible)
    {
        _root.SetActive(visible);
    }
}
