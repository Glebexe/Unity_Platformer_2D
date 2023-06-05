using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/testSave.dat"))
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/testSave.dat");
            SaveData saveData = (SaveData)bf.Deserialize(file);
            SceneManager.LoadScene(saveData.savedLevel); 
            file.Close();
        }
        else
        {
            Debug.Log("no file");
            ResetProgress();
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ResetProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/testSave.dat");
        SaveData saveData = new SaveData();
        saveData.savedLevel = 1;
        saveData.savedScore = 0;
        saveData.savedLastCheckpoint = "-6 0 0";
        saveData.savedCollectables = null;
        bf.Serialize(file,saveData);
        file.Close();
    }
}
