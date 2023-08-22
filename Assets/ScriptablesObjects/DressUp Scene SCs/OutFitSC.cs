using UnityEngine;

[CreateAssetMenu(fileName = "OutFit", menuName = "Scriptable_Objects/OutFit", order = 1)]
public class OutFitSC : ScriptableObject
{
    public string uniqueIdName;
    public Sprite outFitIcon;
    public Sprite outFitOriginalImg;
    public int priceOfOutFit;
    public OutFitMainCat outFitCat;
    public bool isUnlockOutFit;
    public bool isFreeWithRewarded;
    public Color outFitColor;

}


