public interface ICookingService
{
    void RegisterTool(CookingToolBase tool);
    void UnregisterTool(CookingToolBase tool);
    void StartCooking(CookingToolBase tool);
    void StopCooking(CookingToolBase tool);
    float GetCookingProgress(CookingToolBase tool);
    bool IsCooking(CookingToolBase tool);
    void CancelBurn(CookingToolBase tool);
}
