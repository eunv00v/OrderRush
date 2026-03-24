// Scripts/Core/GameBootstrap.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Launcher : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("Gameplay");
    }
}