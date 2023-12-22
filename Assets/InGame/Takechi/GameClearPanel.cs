using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Takechi.UI
{
    public class GameClearPanel : MonoBehaviour
    {
        [SerializeField] Button _stuffedRoll;
        [SerializeField] Button _titleButton;
        [SerializeField] Text _clearTimeText;
        [SerializeField] Text _bestTimeText;
        [SerializeField, Header("タイトルシーンの名前")] string _titleSceneName = "Title";
        [SerializeField, Header("スタッフロールシーンの名前")] string _stuffedRollSceneName = "Staffroll";
        [SerializeField] GameManager _gm;
        IntReactiveProperty _clearTimeRP = new();
        public float ClearTime
        {
            set { _clearTimeText.text = Takechi.Utility.Time.ToTime(value); }
        }
        public float BestTime
        {
            set { _bestTimeText.text = Takechi.Utility.Time.ToTime(value); }
        }
        void Awake()
        {
            _clearTimeRP.Subscribe(f => _clearTimeText.text = Takechi.Utility.Time.ToTime(f)).AddTo(this);
            _stuffedRoll.OnClickAsObservable().Subscribe( _=> SceneManager.LoadSceneAsync(_stuffedRollSceneName)).AddTo(this);
            _titleButton.OnClickAsObservable().Subscribe( _=> SceneManager.LoadSceneAsync(_titleSceneName)).AddTo(this);
            gameObject.SetActive(false);
        }
        private void Update()
        {
            _clearTimeRP.Value = (int)_gm.GameTime;
        }
    }
}
namespace Takechi.Utility
{
    public static class Time
    {
        public static string ToTime(float t)
        {
            int minutes = Mathf.FloorToInt(t / 60);
            int seconds = Mathf.FloorToInt(t % 60);
            return $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        }
    }
}