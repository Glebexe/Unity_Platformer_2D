using System;

[Serializable]
class SaveData
{
    public int savedLevel;
    public int savedScore;
    public string savedLastCheckpoint;
    public bool[,] savedCollectables;
}
