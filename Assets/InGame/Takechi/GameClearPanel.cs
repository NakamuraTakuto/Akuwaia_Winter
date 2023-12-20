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
        [SerializeField] Button _ranking;
        [SerializeField] Button _stuffedRoll;
        [SerializeField] Button _titleButton;
        [SerializeField] Text _clearTimeText;
        [SerializeField] Text _bestTimeText;
        [SerializeField, Header("タイトルシーンの名前")] string _titleSceneName = "Title";
        [SerializeField, Header("スタッフロールシーンの名前")] string _stuffedRollSceneName = "Staffroll";
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
            _ranking.OnClickAsObservable().Subscribe(_ => { }).AddTo(this);
            _stuffedRoll.OnClickAsObservable().Subscribe(_ => SceneManager.LoadSceneAsync(_stuffedRollSceneName)).AddTo(this);
            _titleButton.OnClickAsObservable().Subscribe(_ => SceneManager.LoadSceneAsync(_titleSceneName)).AddTo(this);
            gameObject.SetActive(false);
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