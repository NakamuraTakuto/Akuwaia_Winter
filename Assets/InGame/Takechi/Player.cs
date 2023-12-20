using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Takechi.InGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] float _speed = 2f;
        [SerializeField] float _jumpPower = 10f;
        [SerializeField, Header("�󒆂ŃW�����v�ł����")] int _maxJumpCount = 1;
        [SerializeField, Header("�L�����N�^�[��\�����邽�߂̃I�u�W�F�N�g")] Transform _body;
        [SerializeField, Header("�L�����N�^�[�̃A�j���[�^�[")] Animator _animator;
        BoolReactiveProperty _hourglass = new(false);
        public bool Hourglass
        {
            get { return _hourglass.Value; }
            set { _hourglass.Value = value; }
        }
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
            //  _hourglass��Value���ω������ۂɎ��s���郁�\�b�h��o�^
            _hourglass.Subscribe(SetHourglassGravity).AddTo(this);
        }

        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            Jump(_hourglass.Value);
            Walk(_hourglass.Value);
        }
        void LateUpdate()
        {
            _animator.SetBool("IsGrounded", _isGrounded);
            _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_rb.velocity.x));
        }
        void SetHourglassGravity(bool hourglass = false)
        {
            _rb.gravityScale = hourglass ? -1 : 1;  //  �d�͂̌���
            //transform.rotation = Quaternion.Euler(euler.x, euler.y, hourglass ? 180 : 0);   //  �̂̏㉺�̌���
            transform.DORotate(new Vector3(0, 0, hourglass ? 180 : 0), 1).SetEase(Ease.InQuart).SetLink(gameObject);
        }
        void Jump(bool hourglass = false)
        {
            Vector2 jumpDirection = hourglass ? Vector2.down : Vector2.up;

            if (Input.GetButtonDown("Jump") && (_isGrounded || _jumpCount <= _maxJumpCount))
            {
                _animator.SetTrigger("Jump");
                //  �A�ł����Ƃ��ɔ�т����Ȃ��悤��AddForce�O��y��velocity��0�ɂ���B
                _rb.velocity = new Vector2(_rb.velocity.x, 0);  
                _rb.AddForce(jumpDirection * _jumpPower, ForceMode2D.Impulse);
                if (!_isGrounded) _jumpCount++;
            }
        }
        void Walk(bool hourglass = false)
        {
            float rightRotation = hourglass ? 180 : 0;
            float leftRotation = hourglass ? 0 : 180;
            //  �󒆂ňړ������Ƃ��Ɍ�����������
            if (!_isGrounded && _horizontal != 0)
            {
                if (_deceleration > 0) _deceleration -= Time.deltaTime * 0.3f;
            }

            //  rotation��y�ňړ������ւ̕����]��
            if (_horizontal > 0)    //  �E
                _body.transform.localRotation = Quaternion.Euler(0, rightRotation, 0);
            else if (_horizontal < 0)   //  ��
                _body.transform.localRotation = Quaternion.Euler(0, leftRotation, 0);

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