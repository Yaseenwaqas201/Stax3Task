using UnityEngine;
using UnityEngine.UI;

public class DressUpUIManager : MonoBehaviour
{
    [Header("References of Main UI of Scene")]
    public GameObject mainBtnsRef;
    public GameObject targetPanel;
    public Text coinsValueTxt;

    private void Start()
    {
        EventHandler.updateCoins += UpdateCoinsValue;
        UpdateCoinsValue();
    }

    private void OnDestroy()
    {
        EventHandler.updateCoins -= UpdateCoinsValue;
    }

    void UpdateCoinsValue()
    {
        coinsValueTxt.text = GameDataSaveHandler.TotalCoinsNo + "";
    }
    
 
}
