using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public TMP_InputField PlayerName; 

    public string Name = string.Empty;

    public int Score;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetPlayerName()
    {
        Name = PlayerName.text;
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void SaveData()
    {
        SavePlayerData data = new SavePlayerData();
        data.Name = Name;
        data.Score = Score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

            Name = data.Name;
            Score = data.Score;
        }
    }
}
