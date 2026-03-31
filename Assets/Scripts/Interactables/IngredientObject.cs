using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 월드에 존재하는 재료 오브젝트
/// IngredientData ScriptableObject 데이터를 참조하고 런타임 상태를 관리
/// </summary>
public class IngredientObject : MonoBehaviour, ICarriable
{
    [SerializeField] MeshRenderer _meshRenderer;

    public IngredientContext Context { get; private set; }

    public void Initialize(IngredientData ingredient)
    {
        Context = new IngredientContext(ingredient);
        Context.OnStateChanged += OnStateChanged;  // 상태 변경 구독
        OnStateChanged(Context.State);
    }

    void OnDestroy()
    {
        if (Context != null)
            Context.OnStateChanged -= OnStateChanged;
    }

    public void OnPlaced(Transform slot)
    {
        transform.SetParent(slot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnPickedUp(Transform slot)
    {
        transform.SetParent(slot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public bool CanReceive(ICarriable item) => false;

    public async UniTask Receive(ICarriable item, CharacterBase character, CancellationToken ct)
    {
        await UniTask.CompletedTask;
    }

    void OnStateChanged(IngredientState state)
    {
        Debug.Log($" OnStateChanged state :   {state}");
        var material = Context.Data.GetMaterial(state);
        Debug.Log($" OnStateChanged material :  {material}");
        if (material != null)
            _meshRenderer.material = material;
    }
}
