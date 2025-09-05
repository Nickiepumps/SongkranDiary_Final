using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOStageDataHandler : MonoBehaviour
{
    public static ISOStageDataHandler instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveISOStageData()
    {
        // Assign logic when there are more object to update in the future
    }
    public void UpdateIsoNPCInteraction(string npcID)
    {
        ISOStageData isoStageData = LoadISOStageData();
        if (isoStageData != null)
        {
            bool npcIDMatched = false;
            for (int i = 0; i < isoStageData.npcIDLists.Count; i++)
            {
                if (npcID == isoStageData.npcIDLists[i])
                {
                    npcIDMatched = true;
                    break;
                }
            }
            if (npcIDMatched == false)
            {
                isoStageData.npcIDLists.Add(npcID);

            }
        }
        else
        {
            isoStageData = new ISOStageData();
            isoStageData.npcIDLists.Add(npcID);
        }
        if(Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        string isoStageDataJson = JsonUtility.ToJson(isoStageData);
        File.WriteAllText(Application.dataPath + "/IsoStage.json", isoStageDataJson);
    }
    public ISOStageData LoadISOStageData()
    {
        if(File.Exists(Application.dataPath + "/IsoStage.json") == false)
        {
            return null;
        }
        string loadedISOStageJson = File.ReadAllText(Application.dataPath + "/IsoStage.json");
        ISOStageData isoStageData = JsonUtility.FromJson<ISOStageData>(loadedISOStageJson);
        return isoStageData;
    }
}
