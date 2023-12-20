//日本語対応
using UnityEngine;
using System;

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
    /// <summary>経過時間</summary>
    public float GameTime => _gameTime;
    [SerializeField, Tooltip("ステート")]
    State _state = State.None;

    SaveScore _save;
    LoadScore _load;

    void Start()
    {
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
        _state = State.None;
        _gameTime = 0f;
        _save = GetComponent<SaveScore>();
        _load = GetComponent<LoadScore>();
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
                GameOver();
            }
        }
        //ゲームオーバー
        else if (_state == State.Over)
        {
            _gameOverUI.SetActive(true);    //UI出す
        }
    }

    /// <summary>ゴールしたら呼ぶ</summary>
    public void GameClear()
    {
        if (_state == State.None)
        {
            //ここがランキング確認するために必要なもの
            string timeScore = _gameTime.ToString("f2");
            _save.ScoreSave(float.Parse(timeScore), DateTime.Now.ToString());
            _load.ScoreLoad();

            AudioManager.instance.PlaySE(AudioManager.SeSoundData.SE.Clear);
            _state = State.Clear;
            _clearUI.SetActive(true);       //UI出す
        }
    }

    /// <summary>プレイヤーから呼ぶ</summary>
    public void GameOver()
    {
        if (_state == State.None)
        {
            AudioManager.instance.PlaySE(AudioManager.SeSoundData.SE.GameOver);
            _state = State.Over;
        }
    }

    public void Ranking(string name)
    {
        string timeScore = _gameTime.ToString("f2");
        _save.ScoreSave(float.Parse(timeScore), DateTime.Now.ToString());
        _load.ScoreLoad();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //仮の落下
        if (collision.gameObject.tag == "Player" && _state == State.None)
        {
            GameOver();
        }
    }

    enum State
    {
        None,
        Clear,
        Over,
    }

    /* #region　シングルトン
     public static GameManager instance;
     public static GameManager Instance
     {
         get
         {
             if (!instance)
             {
                 SetupInstance();
             }

             return instance;
         }
     }

     void Awake()
     {
         if (!instance)
         {
             instance = this;
         }
         else if (instance != this)
         {
             Destroy(gameObject);
             return;
         }

         DontDestroyOnLoad(gameObject);
     }

     static void SetupInstance()
     {
         instance = FindObjectOfType<GameManager>();

         if (!instance)
         {
             GameObject go = new GameObject();
             instance = go.AddComponent<GameManager>();
             go.name = instance.GetType().Name;
             DontDestroyOnLoad(go);
         }
     }
     #endregion*/
}