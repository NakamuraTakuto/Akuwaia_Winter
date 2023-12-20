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
        [SerializeField, Header("空中でジャンプできる回数")] int _maxJumpCount = 1;
        [SerializeField, Header("キャラクターを表示するためのオブジェクト")] Transform _body;
        [SerializeField, Header("キャラクターのアニメーター")] Animator _animator;
        BoolReactiveProperty _hourglass = new(false);
        public bool Hourglass
        {
            get { return _hourglass.Value; }
            set { _hourglass.Value = value; }
        }
        Rigidbody2D _rb;
        /// <summary>減速をかけるための変数</summary>
        float _deceleration = 1;
        float _horizontal;
        int _jumpCount = 0;
        /// <summary>接地判定フラグ</summary>
        bool _isGrounded = false;
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            //  _hourglassのValueが変化した際に実行するメソッドを登録
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
            _rb.gravityScale = hourglass ? -1 : 1;  //  重力の向き
            //transform.rotation = Quaternion.Euler(euler.x, euler.y, hourglass ? 180 : 0);   //  体の上下の向き
            transform.DORotate(new Vector3(0, 0, hourglass ? 180 : 0), 1).SetEase(Ease.InQuart).SetLink(gameObject);
        }
        void Jump(bool hourglass = false)
        {
            Vector2 jumpDirection = hourglass ? Vector2.down : Vector2.up;

            if (Input.GetButtonDown("Jump") && (_isGrounded || _jumpCount <= _maxJumpCount))
            {
                _animator.SetTrigger("Jump");
                //  連打したときに飛びすぎないようにAddForce前にyのvelocityを0にする。
                _rb.velocity = new Vector2(_rb.velocity.x, 0);  
                _rb.AddForce(jumpDirection * _jumpPower, ForceMode2D.Impulse);
                if (!_isGrounded) _jumpCount++;
            }
        }
        void Walk(bool hourglass = false)
        {
            float rightRotation = hourglass ? 180 : 0;
            float leftRotation = hourglass ? 0 : 180;
            //  空中で移動したときに減速をかける
            if (!_isGrounded && _horizontal != 0)
            {
                if (_deceleration > 0) _deceleration -= Time.deltaTime * 0.3f;
            }

            //  rotationのyで移動方向への方向転換
            if (_horizontal > 0)    //  右
                _body.transform.localRotation = Quaternion.Euler(0, rightRotation, 0);
            else if (_horizontal < 0)   //  左
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