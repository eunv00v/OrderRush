using UniRx;

/// <summary>
/// 조리 도구의 상태를 관리하는 모델
/// </summary>
public class CookingToolModel
{
    public ReactiveProperty<float> Progress { get; } = new(0f);
    public ReactiveProperty<bool> IsCooking { get; } = new(false);
    public ReactiveProperty<IngredientState> IngredientState { get; } = new(global::IngredientState.Raw);

    public void Dispose()
    {
        Progress?.Dispose();
        IsCooking?.Dispose();
        IngredientState?.Dispose();
    }
}
