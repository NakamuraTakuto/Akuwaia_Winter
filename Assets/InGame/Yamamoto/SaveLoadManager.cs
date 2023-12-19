using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("�ۑ���")] string _fileName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
        Debug.Log(Application.persistentDataPath);

        // �����Ƀt�@�C������邩
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
        // json�ɏ�������
        string json = JsonUtility.ToJson(_data);
        Debug.Log(json);

        // �e�L�X�g�t�@�C���ɏ�������
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
                // �t�@�C����S���ǂݍ���
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
        // �ǂݍ��񂾌�Q�[���ɔ��f
        Load();
        _data.Load();
    }
}
