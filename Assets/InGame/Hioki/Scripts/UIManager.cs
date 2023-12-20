//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    GameManager _gm = default;
    [SerializeField, Tooltip("タイマー表示したいもの")]
    List<Text> _timeGo = default;

    void Update()
    {
        for (int i = 0; i < _timeGo.Count; i++)
        {
            _timeGo[i].text = _gm.GameTime.ToString("f1");
        }
    }
}
