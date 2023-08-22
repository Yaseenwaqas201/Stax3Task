using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
public class GameDataSaveHandler : MonoBehaviour
{
    static  List<IntValueDataSaver> intDataContainer = new List<IntValueDataSaver>();
    static  List<StringValueDataSaver> stringDataContainer = new List<StringValueDataSaver>();

    public OutFitSaver outFitSaver;
    public static GameDataSaveHandler Instance;
    
    [Header("List Of Levels Container ")] 
    public DressUpLevelsSC_Container DressUpLevelsContainer;
    
    [ContextMenu(" Reset ALL GamedData Save Data ")]
    public void ResetDataSaveValues()
    {
        outFitSaver.ResetOutFitData();
        ResetSaveData();
        void ResetSaveData()
        {
            if (File.Exists(intPath))
            {
                File.Delete(intPath);
            }

            if (File.Exists(stringPath))
            {
                File.Delete(stringPath);
            }
            
        }
    }
    
    
    public static void RemoveStringSaveValue(string prefName)
    {
        foreach (StringValueDataSaver stringData in stringDataContainer)
        {
            if (stringData.PrefKey == prefName)
            {
                stringDataContainer.Remove(stringData);
                break;
            }
        }
    }

    private void Awake()
    {
        intPath =Application.persistentDataPath+"/GameIntDataValues.data";
        stringPath = Application.persistentDataPath+"/GameStringDataValues.data";
        LoadGameData();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    

    public static int TotalCoinsNo
    {
        set => SetIntValue("TotalCoinsNoOfGame",GetIntValue("TotalCoinsNoOfGame")+ value);
        get => GetIntValue("TotalCoinsNoOfGame",1000);
    }
    public static int GetIntValue(string prefName, int defaultValue = 0)
    {
       return GetIntPreferenceValue(prefName,defaultValue);
    }
    

    
    public static void SetIntValue(string prefName, int value = 0)
    {
        SaveIntPreferenceValue(prefName,value);
    }
    
    public static string GetStringValue(string prefName, string defaultValue = "")
    {
        return GetStringPreferenceValue(prefName,defaultValue);
    }
    
    public static  void SetStringValue(string prefName, string value = "")
    {
        SaveStringValues(prefName,value);
    }

     static void SaveIntPreferenceValue(string prefKey, int prefValue)
    {
        foreach (IntValueDataSaver intStruct in intDataContainer)
        {
            if (intStruct.PrefKey == prefKey)
            {
                intDataContainer.Remove(intStruct);
                break;
            }
        }

        IntValueDataSaver temp=new IntValueDataSaver();
        temp.PrefKey = prefKey;
        temp.PrefValue = prefValue;
        intDataContainer.Add(temp);
        SaveData();
    }

     static int GetIntPreferenceValue(string prefKey, int defaultValue=0)
    {
        foreach (IntValueDataSaver intStruct in intDataContainer)
        {
            if (intStruct.PrefKey == prefKey)
            {
                return intStruct.PrefValue;
            }
        }
        IntValueDataSaver temp=new IntValueDataSaver();
        temp.PrefKey = prefKey;
        temp.PrefValue = defaultValue;
        intDataContainer.Add(temp);
        return defaultValue;
    }
    
    static void SaveStringValues(string prefKey, string prefValue)
    {
        foreach (StringValueDataSaver intStruct in stringDataContainer)
        {
            if (intStruct.PrefKey == prefKey)
            {
                stringDataContainer.Remove(intStruct);
                break;
            }
        }

        StringValueDataSaver temp=new StringValueDataSaver();
        temp.PrefKey = prefKey;
        temp.PrefValue = prefValue;
        stringDataContainer.Add(temp);
        SaveData();
    }

     static string GetStringPreferenceValue(string prefKey, string defaultValue="")
    {
        foreach (StringValueDataSaver stringStruct in stringDataContainer)
        {
            if (stringStruct.PrefKey == prefKey)
            {
                return stringStruct.PrefValue;
            }
        }
        StringValueDataSaver temp=new StringValueDataSaver();
        temp.PrefKey = prefKey;
        temp.PrefValue = defaultValue;
        stringDataContainer.Add(temp);
        return defaultValue;
    }
    
   static BinaryFormatter bf = new BinaryFormatter();
   static FileStream file;

    private static string saveData;
    private static string intPath ; 
    static string stringPath;
    
    // Use for saving data locally 
    public static void SaveData()
    {
        saveData = JsonConvert.SerializeObject(intDataContainer);
        file = File.Create(intPath);
        bf.Serialize(file, saveData);
        file.Close();
        saveData = JsonConvert.SerializeObject(stringDataContainer);
        file = File.Create(stringPath);
        bf.Serialize(file, saveData);
        file.Close();
    }

    public  void SaveGameData()
    {
        SaveData();
        outFitSaver.SaveChangedInOutFitData();
        outFitSaver.SaveLevelData();
    }
    
    public void LoadGameData()
    {
        if (File.Exists(intPath))
        {
            file = File.Open(intPath, FileMode.Open);
            intDataContainer = JsonConvert.DeserializeObject<List<IntValueDataSaver>>(bf.Deserialize(file).ToString());
            file.Close();
        }
        if (File.Exists(stringPath))
        {
            file = File.Open(stringPath, FileMode.Open);
            stringDataContainer = JsonConvert.DeserializeObject<List<StringValueDataSaver>>(bf.Deserialize(file).ToString());
            file.Close();
        }
    }
}

[Serializable]
public class IntValueDataSaver
{
    public string PrefKey;
    public int PrefValue;
}

[Serializable]
public class StringValueDataSaver
{
    public string PrefKey;
    public string PrefValue;
}
