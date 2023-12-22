using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [Tooltip("移動にかける時間")]
    [SerializeField] private float _moveTime = 2f;

    [Tooltip("移動させるオブジェクト")]
    [SerializeField] private List<GameObject> _targetObject;

    [Tooltip("移動させる座標")]
    [SerializeField] private List<Transform> _nextPos = null;

    private List<Vector3> _originalPos = null;

    private bool _isMove = false;

    private bool _isNext = false;

    int _count = 0;

    void Start()
    {

        _originalPos = new List<Vector3>();

        for (int i = 0; i < _targetObject.Count; i++)
        {
            var objPos = _targetObject[i].transform.position;
            _originalPos.Add(objPos);
        }
    }

    void Update()
    {
        if (_isMove)
        {
            // 全てのオブジェクトが移動し終えたとき
            if (_count >= _targetObject.Count)
            {
                _isMove = false;
            }
        }
    }

    // 衝突した場合
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isMove) return;

        if (collision.CompareTag("Player")) 
        { 
                MoveObject();
        }
    }


    /// <summary>
    /// オブジェクトの移動を開始させる 
    /// </summary>
    private void MoveObject()
    {
        _count = 0;

        _isMove = true;
        _isNext = !_isNext;

        // 移動先が_nextPosの場合
        if (_isNext)
        {
            for (int i = 0; i < _targetObject.Count; i++)
            {
                _targetObject[i].transform.DOMove(_nextPos[i].position, _moveTime).OnComplete(EndMoveObject);
            }
        }
        else
        {
            for (int i = 0; i < _targetObject.Count; i++)
            {
                _targetObject[i].transform.DOMove(_originalPos[i], _moveTime).OnComplete(EndMoveObject);
            }
        }
    }


    /// <summary>
    /// 移動し終えたオブジェクトをカウントする。
    /// </summary>
    private void EndMoveObject()
    {
        _count++;
    }
}
