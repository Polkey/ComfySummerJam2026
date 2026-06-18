using System;

public enum GameState
{
    Starting = 0,
    Playing = 1,
    Paused = 2
}
public static class GameEvents 
{
    public static event Action<GameState> OnGameStateChanged;
    public static void RaiseGameStateChanged(GameState state) 
    {
        OnGameStateChanged?.Invoke(state);
    }
    public static event Action<PlayerState> OnPlayerStateChanged;
    public static void RaisePlayerStateChanged(PlayerState state) 
    {
        OnPlayerStateChanged?.Invoke(state);
    }
    public static event Action<MenuState> OnMenuStateChanged;
    public static void RaiseMenuStateChanged(MenuState state) 
    {
        OnMenuStateChanged?.Invoke(state);
    }
}