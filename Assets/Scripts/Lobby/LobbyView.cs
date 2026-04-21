using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    public Button StartButton => _startButton;
}