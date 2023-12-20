using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Takechi.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] Button _retryButton;
        [SerializeField] Button _titleButton;
        [SerializeField, Header("タイトルシーンの名前")] string _titleSceneName = "Title";
        void Awake()
        {
            _retryButton.OnClickAsObservable()
                .Subscribe(_ => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name)).AddTo(this);
            _titleButton.OnClickAsObservable().Subscribe(_ => SceneManager.LoadSceneAsync(_titleSceneName)).AddTo(this);
            gameObject.SetActive(false);
        }
    }
}