//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHioki : MonoBehaviour
{
    [SerializeField]
    GameManager _gm = default;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy e))
        {
            _gm.GameOver();
        }
    }
}
