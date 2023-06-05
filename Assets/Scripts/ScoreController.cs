using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score;
    public int Score{get{return score;}}
    [SerializeField] private AudioSource coinPick;

    void Start()
    {
        BinaryFormatter bf = new BinaryFormatter();
        SaveData saveData = null;
        FileStream file = File.Open(Application.persistentDataPath + "/testSave.dat", FileMode.Open);
        saveData = (SaveData)bf.Deserialize(file);
        file.Close();

        score = saveData.savedScore;
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        scoreText.SetText("Score: " + score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            if(collision.name[0].CompareTo('C') == 0)
                score++;
            else
                score += 10;
            
            collision.gameObject.SetActive(false);
            scoreText.SetText("Score: " + score);
            coinPick.Play();
        }
    }
}
