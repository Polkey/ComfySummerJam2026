using System;

public interface ISequence 
{
    event Action<Sequence> OnFinish;
    bool IsFinished { get; }
    bool IsStackable { get; }
    void _Start();
    void _Stop();
    void _Tick();
}
public class Sequence : ISequence 
{
    public virtual bool IsFinished { get; protected set; }
    public virtual bool IsStackable { get; protected set; }

    public event Action<Sequence> OnFinish;
    public virtual void _Tick() 
    {
        if (IsFinished) return;
    }
    public virtual void _Start() 
    {
        IsFinished = false;
    }
    public virtual void _Stop() 
    {
        IsFinished = true;
        OnFinish?.Invoke(this);
    }
}