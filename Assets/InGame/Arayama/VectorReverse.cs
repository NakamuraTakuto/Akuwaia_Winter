using System.Collections;
using Takechi.InGame;
using UnityEngine;

public class VectorReverse : MonoBehaviour
{
    [SerializeField] public float _reverseTime = 5f;
    IEnumerator Reverse(Player player)
    {
        Debug.Log("重力が反転した");
        player.Hourglass = true;
        yield return new WaitForSeconds(_reverseTime);
        player.Hourglass = false;
        Debug.Log("重力が戻った");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player)
            {
                StartCoroutine(Reverse(player));
                Debug.Log("砂時計");
            }
        }
    }
}

