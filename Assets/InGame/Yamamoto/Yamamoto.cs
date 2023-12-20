using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 穴のスクリプト
/// </summary>
public class Yamamoto : MonoBehaviour
{
    Vector3 _nextPlayerPos;
    Transform _warpPos;

    // Start is called before the first frame update
    void Start()
    {
        _warpPos = transform.GetChild(0).transform;
        _nextPlayerPos = _warpPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Condition condition = (Condition)(_holeDropCount % 2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = _nextPlayerPos;
        }
    }

}
