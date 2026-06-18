public abstract class UIV_PopUP : UIViewBase
{    
    public abstract void Bind(IPopupData data);

    public virtual void Show() => View();
    public virtual void Close() => Hide();
}
public abstract class UIV_PopUP<TData> : UIV_PopUP where TData : IPopupData
{
    public override void Bind(IPopupData data) 
    {
        BindTyped((TData)data);
    }
    protected abstract void BindTyped(TData data);
}