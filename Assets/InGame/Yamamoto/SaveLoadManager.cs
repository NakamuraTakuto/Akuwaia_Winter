using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("保存先")] string _fileName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
        Debug.Log(Application.persistentDataPath);

        // 何処にファイルを作るか
        _filePath = $"{Application.persistentDataPath}/{_fileName}.json";
        _data = GetComponent<SaveData>();

        if (!File.Exists(_filePath))
        {
            Save();
        }

        LoadAction();
    }

    void Save()
    {
        // jsonに書き換え
        string json = JsonUtility.ToJson(_data);
        Debug.Log(json);

        // テキストファイルに書き込む
        using (StreamWriter wrter = new StreamWriter(_filePath, false))
        {
            wrter.WriteLine(json);
            wrter.Close();
        }
    }
    
    void Load()
    {
        if (File.Exists(_filePath))
        {
            using (StreamReader sr = new StreamReader(_filePath))
            {
                // ファイルを全部読み込む
                string json = sr.ReadToEnd();
                Debug.Log(json);
                sr.Close();
                JsonUtility.FromJsonOverwrite(json, _data);
            }
        }
    }

    public void SaveAction()
    {
        _data.Save();
        Save();
    }

    public void LoadAction()
    {
        // 読み込んだ後ゲームに反映
        Load();
        _data.Load();
    }
}
