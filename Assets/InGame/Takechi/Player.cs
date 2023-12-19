using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.InGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] float _speed = 2f;
        [SerializeField] float _jumpPower = 10f;
        [SerializeField,Header("�󒆂ŃW�����v�ł����")] int _maxJumpCount = 1;
        public bool Hourglass = false;
        Rigidbody2D _rb;
        /// <summary>�����������邽�߂̕ϐ�</summary>
        float _deceleration = 1;
        float _horizontal;
        int _jumpCount = 0;
        /// <summary>�ڒn����t���O</summary>
        bool _isGrounded = false;
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            SetHourglassGravity(Hourglass);
            Jump(Hourglass);
            Walk(Hourglass);
        }
        void SetHourglassGravity(bool hourglass = false)
        {
            _rb.gravityScale = hourglass ? -1 : 1;  //  �d�͂̌���
            var euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, hourglass ? 180 : 0);   //  �̂̏㉺�̌���
        }
        void Jump(bool hourglass = false)
        {
            Vector2 jumpDirection = hourglass ? Vector2.down : Vector2.up;

            if (Input.GetButtonDown("Jump") && (_isGrounded || _jumpCount <= _maxJumpCount))
            {
                //  �A�ł����Ƃ��ɔ�т����Ȃ��悤��AddForce�O��y��velocity��0�ɂ���B
                _rb.velocity = new Vector2(_rb.velocity.x, 0);  
                _rb.AddForce(jumpDirection * _jumpPower, ForceMode2D.Impulse);
                if (!_isGrounded) _jumpCount++;
            }
        }
        void Walk(bool hourglass = false)
        {
            float rightRotation = hourglass ? 0 : 180;
            float leftRotation = hourglass ? 180 : 0;
            //  �󒆂ňړ������Ƃ��Ɍ�����������
            if (!_isGrounded && _horizontal != 0)
            {
                if (_deceleration > 0) _deceleration -= Time.deltaTime * 0.3f;
            }
            //  rotation��y�ňړ������ւ̕����]��
            if (_horizontal > 0)    //  �E
            {
                var euler = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(euler.x, rightRotation, euler.z);
            }
            else if (_horizontal < 0)   //  ��
            {
                var euler = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(euler.x, leftRotation, euler.z);
            }
            _rb.velocity = new Vector2(_horizontal * _speed * _deceleration, _rb.velocity.y);
        }
        void OnTriggerStay2D(Collider2D collision)
        {
            _isGrounded = true;
            _jumpCount = 0;
            _deceleration = 1;
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            _isGrounded = false;
            _jumpCount++;
        }
    }
}