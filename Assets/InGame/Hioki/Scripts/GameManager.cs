//日本語対応
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
    /// <summary>経過時間</summary>
    public float GameTime => _gameTime;
    [SerializeField, Tooltip("ステート")]
    State _state = State.None;
    [SerializeField, Tooltip("セーブ")]
    SaveScore _save;
    [SerializeField, Tooltip("ロード")]
    LoadScore _load;

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

    SaveLoadManager _s;

    /// <summary>ゴールしたら呼ぶ</summary>
    public void GameClear()
    {
        _s = GetComponent<SaveLoadManager>();
        if (_state == State.None)
        {
            _save.ScoreSave(_gameTime);
            _load.ScoreLoad();

            _state = State.Clear;
            _clearUI.SetActive(true);       //UI出す
        }
    }

    /// <summary>プレイヤーから呼ぶ</summary>
    public void GameOver()
    {
        if (_state == State.None)
        {
            _state = State.Over;
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