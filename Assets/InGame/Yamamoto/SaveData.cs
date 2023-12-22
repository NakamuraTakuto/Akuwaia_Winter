using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveData : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] float _time;
    [SerializeField] Text _text;

    public void Save()
    {
        float a = GameManager.instance.GameTime;
        _time = _time > a ? a : _time;
    }

    public void Load()
    {
        _text.text = _time.ToString("f1");
    }
}
