public static class EventHandler 
{
    public delegate void UpdateCoinsEvent();

    public static event UpdateCoinsEvent updateCoins;
    
    
    public delegate void FreeRewardEvent();

    public static event FreeRewardEvent freeRewardEvent;

    public static void InvokeUpdateCoinsEvent()
    {
        updateCoins?.Invoke();
    }

    public static void InvokeFreeRewardEvent()
    {
        freeRewardEvent?.Invoke();
    }
}
