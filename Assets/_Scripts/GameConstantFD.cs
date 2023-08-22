using UnityEngine.SceneManagement;

public static class GameConstantFD
{
    public const string OutFitFreeReward = "OutFitFreeReward";


//    public static GameFreeRewardsTypes currentActiveFreeReward;
    // Const For Scene Names
    public const string DressUpScene="DressUpScene";
    
    
    // Const For Scene Names
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

}
