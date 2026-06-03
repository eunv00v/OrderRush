using UnityEngine;

public class GameUIContext : MonoBehaviour
{
    [NotNull][SerializeField] PopupCompleted _popupCompleted;
    [NotNull][SerializeField] PopupDayFailed _popupDayFailed;
    [NotNull][SerializeField] PopupCardShop _popupCardShop;

    public PopupCompleted PopupCompleted => _popupCompleted;
    public PopupDayFailed PopupDayFailed => _popupDayFailed;
    public PopupCardShop PopupCardShop => _popupCardShop;
}
