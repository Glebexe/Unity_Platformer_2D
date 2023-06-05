using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerController : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private GameObject[] collectables;
    private ScoreController scoreController;
    [SerializeField] private AudioSource death;
    [SerializeField] private LayerMask sawLayer;    

    private PlayerMovement player;
    private BinaryFormatter bf;

    void Start()
    {
        bf = new BinaryFormatter();
        scoreController = GetComponent<ScoreController>();
        collectables = GameObject.FindObjectOfType<LevelManager>().collectables;
        lastCheckpoint = transform.position;
        player = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "DeathZone" || collision.tag == "Enemies")
            Respawn();

        if(collision.tag == "Checkpoint")
        {
            lastCheckpoint = collision.transform.position;
            lastCheckpoint.y += 3;

            UpdateSaveData();
        }
    }
    
    private void UpdateSaveData()
    {
        SaveData saveData = new SaveData();

        saveData.savedLevel = SceneManager.GetActiveScene().buildIndex;
        saveData.savedScore = scoreController.Score;
        saveData.savedLastCheckpoint = lastCheckpoint.x + " " + lastCheckpoint.y + " " + lastCheckpoint.z;

        saveData.savedCollectables = UpdateCollectables(saveData.savedLevel);
        
        FileStream file = File.Create(Application.persistentDataPath + "/testSave.dat");
        bf.Serialize(file,saveData);
        file.Close();
    }
    private bool[,] UpdateCollectables(int level)
    {
        FileStream file = File.Open(Application.persistentDataPath + "/testSave.dat", FileMode.Open);
        SaveData saveData = (SaveData)bf.Deserialize(file);
        file.Close();

        bool[,] collectablesForUpdate = saveData.savedCollectables;
        for (int i = 0; i < collectables.Length; i++)
            collectablesForUpdate[i,level-1] = collectables[i].activeInHierarchy;
        
        return collectablesForUpdate;
    }

    private void Respawn()
    {
        player.isRespawing = true;
        transform.position = new Vector3(lastCheckpoint.x,lastCheckpoint.y,0); 
        death.Play();
    }
}
