using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    private Renderer[] _renderers;
    private MaterialPropertyBlock _block;
    private static readonly int SelectedProp = Shader.PropertyToID("_Selected");

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _block = new MaterialPropertyBlock();
    }

    public void SetHighlight(bool active)
    {
        foreach (var r in _renderers)
        {
            r.GetPropertyBlock(_block);
            _block.SetFloat(SelectedProp, active ? 1f : 0f);
            r.SetPropertyBlock(_block);
        }
    }
}
