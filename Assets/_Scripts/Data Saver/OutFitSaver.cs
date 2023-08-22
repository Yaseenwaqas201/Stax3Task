using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class OutFitSaver : MonoBehaviour
{
    public OutFitSC_Container dressOutFitCatList;
    public OutFitSC_Container hairsOutFitCatList;
    public OutFitSC_Container shoesOutFitCatList;
    public OutFitSC_Container pantOutFitCatlist;
    public OutFitSC_Container shirtOutFitCatList;

 

    private  string SaveOutFitDataPath;
    private  string SaveOutFitDefaultDataPath;

    private void Start()
    {
        SetOutFitNames();
        SaveOutFitDataPath=Application.persistentDataPath+"/SaveOutFitDataPath.data";
        SaveOutFitDefaultDataPath=Application.persistentDataPath+"/SaveOutFitDefaultDataPath.data";
        SaveDefaultOutFitData();
        LoadAndSetOutFitData();
        LoadSavedLevelsData();
    }

    

    [ContextMenu(" Set OutFit Names")]
   public void SetOutFitNames()
    {
        SetCatNames(dressOutFitCatList);
        SetCatNames(hairsOutFitCatList);
        SetCatNames(shoesOutFitCatList);
        SetCatNames(shirtOutFitCatList);
        SetCatNames(pantOutFitCatlist);
        void SetCatNames(OutFitSC_Container outFitCatList)
        {
            foreach (OutFitSC outfit in outFitCatList.OutFitList)
            {
                string names = outfit.name;
                string resultedName = names.Replace(" ", ""); // Here RemovingSpaces
                outfit.uniqueIdName = resultedName;
            }
        }
    }

    void SaveDefaultOutFitData()
    {
        #if UNITY_EDITOR
        if (!PlayerPrefs.HasKey("DefaultOutFitSaveValues"))
        {
            SaveOutFitDataFunc(SaveOutFitDefaultDataPath);
            PlayerPrefs.SetInt("DefaultOutFitSaveValues",0);
        }
        #endif
    }

    public void SaveChangedInOutFitData()
    {
        SaveOutFitDataFunc(SaveOutFitDataPath);
    }

    [Serializable]
   public class OutFitDataSaver
   {
       public string outFitCatName;
       public List<OutFitData> outFitData =new List<OutFitData>();
    }
   
   public class OutFitData
   {
       public string outFitIDName;
       public bool isUnlock;
       public bool isfreeWithRewarded;
   }
   

    
    public List<OutFitDataSaver> SaveOutFitDataList=new List<OutFitDataSaver>();
    static BinaryFormatter bf = new BinaryFormatter();
    static FileStream file;

    private  string saveOutFitData;


    public void SaveOutFitDataFunc(string saveDataPath)
    {
        SaveOutFitDataList.Clear();
        DataSaverFun(dressOutFitCatList);
        DataSaverFun(shoesOutFitCatList);
        DataSaverFun(hairsOutFitCatList);
        DataSaverFun(pantOutFitCatlist);
        DataSaverFun(shirtOutFitCatList);
        SaveOutFitData(saveDataPath);

        void DataSaverFun(OutFitSC_Container outFitList)
        {
            OutFitDataSaver tempCatData = new OutFitDataSaver();
            tempCatData.outFitCatName = outFitList.name;
            foreach (OutFitSC outFit in outFitList.OutFitList)
            {
                OutFitData tempDataSaver = new OutFitData
                {
                    isUnlock = outFit.isUnlockOutFit,
                    isfreeWithRewarded = outFit.isFreeWithRewarded,
                    outFitIDName = outFit.uniqueIdName
                };
                tempCatData.outFitData.Add(tempDataSaver);
            }

            SaveOutFitDataList.Add(tempCatData);
        }
    }

    
    public  void SaveOutFitData( string saveDataPath)
    {
        saveOutFitData = "";
        saveOutFitData = JsonConvert.SerializeObject(SaveOutFitDataList);
        file = File.Create(saveDataPath);
        bf.Serialize(file, saveOutFitData);
        file.Close(); 
        
    }

    public void LoadAndSetOutFitData()
    {
        if (File.Exists(SaveOutFitDataPath))
        {
            file = File.Open(SaveOutFitDataPath, FileMode.Open);
            SaveOutFitDataList = JsonConvert.DeserializeObject<List<OutFitDataSaver>>(bf.Deserialize(file).ToString());
            file.Close();
            UpdateOutFitsState();
        }
    }

    public void ResetOutFitData()
    {
        SaveOutFitDataPath=Application.persistentDataPath+"/SaveOutFitDataPath.data";
        SaveOutFitDefaultDataPath=Application.persistentDataPath+"/SaveOutFitDefaultDataPath.data";
        if(File.Exists(SaveOutFitDataPath))
            File.Delete(SaveOutFitDataPath);
        if (File.Exists(SaveOutFitDefaultDataPath))
        {
            file = File.Open(SaveOutFitDefaultDataPath, FileMode.Open);
            SaveOutFitDataList = JsonConvert.DeserializeObject<List<OutFitDataSaver>>(bf.Deserialize(file).ToString());
            file.Close();
            UpdateOutFitsState();
        }
    }
    
    
    public void UpdateOutFitsState()
    {
        UpdateOutFitState(dressOutFitCatList);
        UpdateOutFitState(shoesOutFitCatList);
        UpdateOutFitState(pantOutFitCatlist);
        UpdateOutFitState(shirtOutFitCatList);
        UpdateOutFitState(hairsOutFitCatList);
    }
    public void UpdateOutFitState(OutFitSC_Container outFitList)
    {
        foreach (OutFitDataSaver outFitSaved in SaveOutFitDataList)
        {
            if (outFitList.name == outFitSaved.outFitCatName)
            {
                foreach (OutFitData outFitData in outFitSaved.outFitData)
                {
                    foreach (OutFitSC outfit in outFitList.OutFitList)
                    {
                        if (outFitData.outFitIDName == outfit.uniqueIdName)
                        {
                            outfit.isUnlockOutFit = outFitData.isUnlock;
                            outfit.isFreeWithRewarded = outFitData.isfreeWithRewarded;

                            if (outfit.isFreeWithRewarded)
                            {
                                CheckAndUpdateFreeRewardApparelStateAfter12Hours(outfit);
                            }
                            break;
                        }
                    }
                }
            }
        }
        
    }
    
    // here We saving levels Data
    public void SaveLevelData()
    {
        foreach (DressUpLevelSC level in GameDataSaveHandler.Instance.DressUpLevelsContainer.dressUpLevels)
        {
            GameDataSaveHandler.SetIntValue(level.name+"DressUpLevel",level.levelLocked?1:0);
            GameDataSaveHandler.SetIntValue(level.name+"DressUpLevel"+"Stars",level.starsAchieved);
        }
    }
    public void LoadSavedLevelsData()
    {
        int levelCount=0;
        foreach (DressUpLevelSC level in GameDataSaveHandler.Instance.DressUpLevelsContainer.dressUpLevels)
        {
            if(levelCount!=0)
                level.levelLocked = GameDataSaveHandler.GetIntValue(level.name+"DressUpLevel",1)==1;
            level.starsAchieved = GameDataSaveHandler.GetIntValue(level.name+"DressUpLevel"+"Stars");
            levelCount++;
        }
    }

    private bool isSaveDataOF = false;
    void CheckAndUpdateFreeRewardApparelStateAfter12Hours(OutFitSC outFit)
    {
        string outFitDateConst=outFit.uniqueIdName + GameConstantFD.OutFitFreeReward;
        DateTime timeToFree = DateTime.Parse(GameDataSaveHandler.GetStringValue(outFitDateConst));
        TimeSpan timeSpend = DateTime.Now.Subtract(timeToFree);
        if (timeSpend.Hours >= 12 || timeSpend.Days>=1 )
        {
            isSaveDataOF = true;
            outFit.isUnlockOutFit = false;
            outFit.isFreeWithRewarded = false;
            GameDataSaveHandler.RemoveStringSaveValue(outFitDateConst);
        }
    }
}

