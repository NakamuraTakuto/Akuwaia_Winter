using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nakamura : MonoBehaviour
{
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            _rb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        }
    }
}
