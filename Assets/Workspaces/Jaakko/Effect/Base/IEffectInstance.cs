public interface IEffectInstance 
{  
    bool IsFinished { get; }
    void Tick(float dt);
    void OnEnter();
    void OnExit();
}