using System.Threading;
using UnityEngine;

public class CookingProcess
{
    public CookableAbility Ability { get; set; }

    public CancellationTokenSource Cts { get; set; }
    public float ElapsedTime { get; set; }
    public float Progress => Ability.CookDuration > 0 ? Mathf.Clamp01(ElapsedTime / Ability.CookDuration) : 0f;
}