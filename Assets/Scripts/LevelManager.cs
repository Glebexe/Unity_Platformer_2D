using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] collectables;

    private BinaryFormatter bf;
    void Start()
    {
        bf = new BinaryFormatter();
        Vector3 spawnPostion = SaveDataLoad();

        var playerInst = Instantiate(player,spawnPostion,new Quaternion(0,0,0,0)); 
        player.GetComponent<PlayerMovement>().isRespawing = true;
        var cinemaCamera = GameObject.Find("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        cinemaCamera.Follow = playerInst.transform;
    }

    private Vector3 SaveDataLoad()
    {
        SaveData saveData = GetSaveData();

        if(saveData.savedCollectables == null)
            SavedCollectablesInit(saveData);

        CollectablesLoad(saveData);

        return SpawnPosition(saveData);
    }

    private void SavedCollectablesInit(SaveData saveData)
    {
        FileStream file = File.Create(Application.persistentDataPath + "/testSave.dat");
        SaveData newSaveData = new SaveData();
        newSaveData.savedLastCheckpoint = saveData.savedLastCheckpoint;
        newSaveData.savedLevel = saveData.savedLevel;
        newSaveData.savedScore = saveData.savedScore;

        newSaveData.savedCollectables = SavedCollectablesArrayInit();

        bf.Serialize(file,newSaveData);
        file.Close();

        saveData.savedCollectables = newSaveData.savedCollectables;
    }
    private bool[,] SavedCollectablesArrayInit()
    {
        bool[,] collectables = new bool[14,2];
        for (int i = 0; i < 14; i++)
            for(int j = 0; j < 2; j++)
                collectables[i,j] = true;
        return collectables;
    }

    private void CollectablesLoad(SaveData saveData)
    {
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        for(int i = 0; i < collectables.Length; i++)
            collectables[i].SetActive(saveData.savedCollectables[i,saveData.savedLevel-1]);
    }

    private Vector3 SpawnPosition(SaveData saveData)
    {
        return new Vector3(float.Parse(saveData.savedLastCheckpoint.Split(" ")[0]),
                           float.Parse(saveData.savedLastCheckpoint.Split(" ")[1]),
                           float.Parse(saveData.savedLastCheckpoint.Split(" ")[2]));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(0);
            else
            {
                UpdateSaveDataForNextLevel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void UpdateSaveDataForNextLevel()
    {
        SaveData saveData = GetSaveData();
        FileStream file = File.Create(Application.persistentDataPath + "/testSave.dat");
        SaveData data = new SaveData();
        data.savedLevel++;
        data.savedLastCheckpoint = "-6 0 0";
        data.savedScore = saveData.savedScore;
        bf.Serialize(file,data);
        file.Close();        
    }

    private SaveData GetSaveData()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/testSave.dat", FileMode.Open);
        SaveData saveData = (SaveData)bf.Deserialize(file);
        file.Close();
        return saveData;
    }
}
