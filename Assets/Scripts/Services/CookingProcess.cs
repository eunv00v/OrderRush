using System.Threading;
using UnityEngine;

public class CookingProcess
{
    public float Duration { get; set; }
    public IngredientState ResultState { get; set; }
    public CancellationTokenSource Cts { get; set; }
    public float ElapsedTime { get; set; }
    public float Progress => Duration > 0 ? Mathf.Clamp01(ElapsedTime / Duration) : 0f;
}