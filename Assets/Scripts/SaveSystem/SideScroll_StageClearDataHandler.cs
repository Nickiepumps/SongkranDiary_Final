using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideScroll_StageClearDataHandler : MonoBehaviour, IGameObserver, IBossObserver
{
    public static SideScroll_StageClearDataHandler instance;

    /// <summary>
    /// For Run n Gun mode, use goal subject in gameControllerSubject
    /// For Boss mode, use boss subject in bossSubject
    /// For Isometric mode, use game controller subject
    /// </summary>
    [SerializeField] private GameSubject gameControllerSubject;
    [SerializeField] private BossSubject bossSubject;

    [SerializeField] private LevelDataSO levelDataSO;

    // Hide in inspector
    public string mapName;
    public bool Side_L1_Run;
    public bool Side_L1_Boss;
    public bool ISO_L1;
    public bool Side_L2_Run;
    public bool Side_L2_Boss;
    public bool ISO_L2;
    public bool Side_L3_Run;
    public bool Side_L3_Boss;
    public bool ISO_L3;
    public List<string> coinIDLists = new List<string>();

    public GameType gameType;
    private bool isFoundLevelData = false;
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
    private void OnEnable()
    {
        switch (gameType)
        {
            case (GameType.Isometric):
                //gameControllerSubject.AddGameObserver(this);
                break;
            case (GameType.RunNGun):
                gameControllerSubject.AddSideScrollGameObserver(this);
                break;
            case (GameType.Boss):
                bossSubject.AddBossObserver(this);
                break;
        }   
    }
    private void OnDisable()
    {
        switch (gameType)
        {
            case (GameType.Isometric):
                //gameControllerSubject.RemoveGameObserver(this);
                break;
            case (GameType.RunNGun):
                gameControllerSubject.RemoveSideScrollGameObserver(this);
                break;
            case (GameType.Boss):
                bossSubject.RemoveBossObserver(this);
                break;
        }
    }
    private void Start()
    {
        StageClearData stageClearData = LoadSideScrollStageClear();
        if(stageClearData != null)
        {
            
        }
        if(gameType != GameType.Isometric)
        {
            
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {

    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case (SideScrollGameState.WinRunNGun):
                SaveSideScrollStageClear();
                break;
        }
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Die):
                SaveSideScrollStageClear();
                break;
        }
    }
    public void UpdateSideScrollStageData(StageClearData updatedData)
    {
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string stageClearJson = JsonUtility.ToJson(updatedData);
        File.WriteAllText(Application.persistentDataPath + "/stageClear.json", stageClearJson);
    }
    public void SaveSideScrollStageClear()
    {
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }

        StageClearData stageClearData = LoadSideScrollStageClear();
        if(stageClearData == null)
        {
            stageClearData = new StageClearData();
            stageClearData.levelDataSOLists.Add(levelDataSO);
            stageClearData.levelNameLists.Add(levelDataSO.unitySceneName);
            for(int i = 0; i < levelDataSO.isoLevelBoundaryName.Length; i++)
            {
                stageClearData.levelBoundaryNameLists.Add(levelDataSO.isoLevelBoundaryName[i]);
            }
            stageClearData.levelClearStatus.Add(true);
            stageClearData.levelFirstClearStatus.Add(true);
            for(int i = 0; i < coinIDLists.Count; i++)
            {
                stageClearData.coinIDLists.Add(coinIDLists[i]);
            }
            stageClearData.coinIDLists.Sort();
        }
        else
        {
            for(int i = 0; i < stageClearData.levelDataSOLists.Count; i++)
            {
                if (stageClearData.levelNameLists[i] == levelDataSO.levelName)
                {
                    isFoundLevelData = true;
                    break;
                }
            }
            if(isFoundLevelData == false)
            {
                stageClearData.levelDataSOLists.Add(levelDataSO);
                stageClearData.levelNameLists.Add(levelDataSO.unitySceneName);
                stageClearData.levelClearStatus.Add(true);
                stageClearData.levelFirstClearStatus.Add(true);
            }
            for (int i = 0; i < coinIDLists.Count; i++)
            {
                stageClearData.coinIDLists.Add(coinIDLists[i]);
            }
            stageClearData.coinIDLists.Sort();
        }
        string stageClearJson = JsonUtility.ToJson(stageClearData);
        File.WriteAllText(Application.persistentDataPath + "/stageClear.json", stageClearJson);
    }
    public void ClearSideScrollStageClear()
    {
        StageClearData stageClearData = LoadSideScrollStageClear();
        if (stageClearData != null)
        {
            if (Directory.Exists(Application.persistentDataPath) == false)
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
            stageClearData = null;
            string stageClearJson = JsonUtility.ToJson(stageClearData);
            File.WriteAllText(Application.persistentDataPath + "/stageClear.json", stageClearJson);
        }
    }
    public StageClearData LoadSideScrollStageClear()
    {
        if (File.Exists(Application.persistentDataPath + "/stageClear.json") == false)
        {
            return null;
        }
        string loadedStageClearJson = File.ReadAllText(Application.persistentDataPath + "/stageClear.json");
        StageClearData loadedStageClear = JsonUtility.FromJson<StageClearData>(loadedStageClearJson);
        return loadedStageClear;
    }
}
