//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("動く速さ")]
    float _speed = 3f;
    [SerializeField, Tooltip("片道の動く時間")]
    float _time = 2f;

    bool _isRight = false;
    Rigidbody2D _rb = default;
    float _t = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        float horizontalInput = _isRight ? 1 : -1;
        Vector2 moveDir = new Vector2(horizontalInput, 0).normalized;
        _rb.velocity = new Vector2(moveDir.x * _speed, _rb.velocity.y);
    }
    void Update()
    {
        _t += Time.deltaTime;
        if (_t >= _time)
        {
            _isRight = !_isRight;

            Vector3 rotation = transform.eulerAngles;
            rotation.y += 180f;
            transform.eulerAngles = rotation;

            float horizontalInput = _isRight ? 1 : -1;
            Vector2 moveDir = new Vector2(horizontalInput, 0).normalized;
            _rb.velocity = new Vector2(moveDir.x * _speed, _rb.velocity.y);
            _t = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.GameOver();
        }
    }
}
