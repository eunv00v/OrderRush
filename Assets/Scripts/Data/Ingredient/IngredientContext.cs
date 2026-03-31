using System;

public class IngredientContext
{
    public IngredientData Data { get; private set; }
    IngredientState _state;
    public IngredientState State
    {
        get => _state;
        set
        {
            _state = value;
            OnStateChanged?.Invoke(_state);
        }
    }

    public event Action<IngredientState> OnStateChanged;

    public IngredientContext(IngredientData data)
    {
        Data = data;
        State = data.InitialState;
    }

    public IngredientContext(IngredientData data, IngredientState state)
    {
        Data = data;
        State = state;
    }

    public override string ToString()
    {
        return $"{Data.IngredientName} ({State})";
    }
}
