using NCMB;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI
{
    public class RankingPanel : MonoBehaviour
    {
        [SerializeField] InputField _inputField;
        [SerializeField] Button _rankingButton;
        [SerializeField] GameManager _gm;
        [SerializeField] SaveScore _save;
        [SerializeField, Header("プレイヤーのデータを表示する親オブジェクト")] Transform _content;
        [SerializeField] Text _playerTextPrefab;
        StringReactiveProperty _input;
        List<IDisposable> _subscriptions = new();
        void Start()
        {
            _rankingButton.interactable = false;    //  名前を入力完了するまでランキングを見れないようにする
            _input = new(_inputField.text);
            _subscriptions.Add(_inputField.OnSubmitAsObservable().Subscribe(_ => _input.Value = _inputField.text).AddTo(this));
            _subscriptions.Add(_input.Skip(1).Subscribe(s =>
            {
                if( _gm != null )
                {
                    Debug.Log($"名前を登録する：{_inputField.text}");
                    _save.ScoreSave(_gm.GameTime, s);
                    ScoreLoad();
                    _rankingButton.interactable = true;
                    _inputField.interactable = false;
                    foreach (var subscription in _subscriptions)
                    {
                        subscription.Dispose();
                    }
                    _subscriptions.Clear();
                }
            }).AddTo(this));
        }
        public void ScoreLoad()
        {
            int rank;
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreClass");
            query.OrderByAscending("score");
            query.Limit = 10;
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.LogWarning("error: " + e.ErrorMessage);
                }
                else
                {
                    for (int i = 0; i < objList.Count; i++)
                    {
                        rank = i + 1;
                        var playerData = Instantiate(_playerTextPrefab);
                        playerData.text = $"{rank}位: {objList[i]["name"]} {Takechi.Utility.Time.ToTime(Convert.ToInt32(objList[i]["score"]))}";
                        //Debug.Log($"{rank}位: {objList[i]["name"]} {Takechi.Utility.Time.ToTime(Convert.ToInt32(objList[i]["score"]))}");
                        playerData.transform.SetParent(_content);
                    }
                }
            });
        }
    }
}