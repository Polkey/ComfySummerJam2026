public enum UIInput 
{
    Pause,
}
public interface IUIComponent
{
    void Initialize();
    void Dispose();
    void Toggle(bool show);
    bool IsVisible();
    void OnInput(UIInput input);
}
