using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI
{
    public class UITester : MonoBehaviour
    {
        [SerializeField] GameClearPanel _gameClearPanel;
        [SerializeField] float _clearTime = 0;
        [SerializeField] float _bestTime = 0;
        void Update()
        {
            _gameClearPanel.ClearTime = _clearTime;
            _gameClearPanel.BestTime = _bestTime;
        }
    }

}