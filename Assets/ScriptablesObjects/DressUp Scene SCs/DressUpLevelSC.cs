using UnityEngine;

[CreateAssetMenu(fileName = "DressUpLevel", menuName = "Scriptable_Objects/DressUpLevelTarget", order = 1)]
public class DressUpLevelSC : Level
{
    public DressUpLevelTarget[] dressUpLevelTarget;

    public void ResetTargets()
    {
        foreach (DressUpLevelTarget levelTarget in dressUpLevelTarget)
        {
            levelTarget.isTargetCompleted = false;
        }
    }
}
[System.Serializable]
public class DressUpLevelTarget
{
    public bool isSpecificOutFit;
    public string targetOutFitName;
    public bool isSpecificColor;
    public Color targetColor=Color.white;
    public OutFitMainCat outfitCat;
    public bool isTargetCompleted;
    public Sprite targetIcon;
    public Sprite targetColorIcon;
}
