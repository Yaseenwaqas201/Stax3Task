using UnityEngine;
using UnityEngine.UI;

public class outFitSelector : MonoBehaviour
{
    public OutFitSC currentOutFit;
    public Image outFitIcon;
    public Image outFitColorIcon;
    public GameObject coinBr;
    public Text coinPrice;

    public void UpdateOutFitState()
    {
        outFitIcon.sprite = currentOutFit.outFitIcon;
        outFitColorIcon.gameObject.SetActive(false);
        gameObject.SetActive(true);
        if (!currentOutFit.isUnlockOutFit)
        {
            coinBr.SetActive(true);
            coinPrice.text = currentOutFit.priceOfOutFit.ToString();
        }
        else
        {
            coinBr.SetActive(false);
        }
    }


}
