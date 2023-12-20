using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CreditPanel : MonoBehaviour
    {
        [SerializeField] Button _openButton;
        [SerializeField] Button _closeButton;
        CanvasGroup _canvasGroup;
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            InactivePanel();
            _openButton.OnClickAsObservable().Subscribe(_ =>TweenActivePanel()).AddTo(this);
            _closeButton.OnClickAsObservable().Subscribe(_ => TweenInactivePanel()).AddTo(this);
        }
        void ActivePanel()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        void InactivePanel()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        void TweenActivePanel()
        {
            transform.localScale = Vector3.zero;
            ActivePanel();
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).SetLink(gameObject);
        }
        void TweenInactivePanel()
        {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(() => 
            {
                InactivePanel();
                transform.localScale = Vector3.one;
            }).SetLink(gameObject);
        }
    }
}