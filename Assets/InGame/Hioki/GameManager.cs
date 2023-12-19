//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("クリア画面UI")]
    GameObject _clearUI = default;
    [SerializeField, Tooltip("ゲームオーバー画面UI")]
    GameObject _gameOverUI = default;
    [SerializeField, Tooltip("制限時間(s)")]
    float _timeLimit = 180f;

    [Header("Debug")]
    [SerializeField, Tooltip("タイム")]
    float _gameTime = 0f;
    [SerializeField, Tooltip("ステート")]
    State _state = State.None;

    void Start()
    {
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
        _state = State.None;
        _gameTime = 0f;
    }

    void Update()
    {
        //ゲーム中
        if (_state == State.None)
        {
            _gameTime += Time.deltaTime;

            //時間切れ
            if (_gameTime >= _timeLimit)
            {
                _state = State.Over;
            }
        }
        //ゲームオーバー
        else if (_state == State.Over)
        {
            _gameOverUI.SetActive(true);    //UI出す
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //仮の落下
        if (collision.gameObject.tag == "Player" && _state == State.None)
        {
            _state = State.Over;
        }
    }

    /// <summary>ゴールしたら呼ぶ</summary>
    public void GameClear()
    {
        if (_state == State.None)
        {
            _state = State.Clear;
            _clearUI.SetActive(true);       //UI出す
        }
    }

    enum State
    {
        None,
        Clear,
        Over,
    }
}
