using System.Collections.Generic;

[System.Serializable]
public class StageClearData
{
    public List<LevelDataSO> levelDataSOLists = new List<LevelDataSO>();
    public List<bool> levelClearStatus = new List<bool>(); // Use order based on level SO list
    public List<bool> levelFirstClearStatus = new List<bool>(); // Use order based on level SO list
    public List<string> coinIDLists = new List<string>(); // Store all coins using its ID
    public List<string> npcIDLists = new List<string>(); // Store all ISO NPC using its ID
}
