using UnityEngine;

public class Level : ScriptableObject
{
    public bool levelCompleted;
    public int starsAchieved;
    public bool levelLocked;
    public Sprite levelIcon;
    public Sprite LevelBG;
    public string levelSoundName;
    public float levelStartDelay;

    public string[] levelPhrases;
}