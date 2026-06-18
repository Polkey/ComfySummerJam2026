using System;
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