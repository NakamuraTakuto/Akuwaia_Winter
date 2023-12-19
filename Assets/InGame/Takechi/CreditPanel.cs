using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CreditPanel : MonoBehaviour
    {
        CanvasGroup _canvasGroup;
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            InactivePanel();
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
        public void TweenActivePanel()
        {
            transform.localScale = Vector3.zero;
            ActivePanel();
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).SetLink(gameObject);
        }
        public void TweenInactivePanel()
        {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(() => 
            {
                InactivePanel();
                transform.localScale = Vector3.one;
            }).SetLink(gameObject);
        }
    }
}