using System.Collections;
using Takechi.InGame;
using UnityEngine;

public class VectorReverse : MonoBehaviour
{
    [SerializeField] public float _reverseTime = 5f;
    IEnumerator Reverse(Player player)
    {
        Debug.Log("�d�͂����]����");
        player.Hourglass = true;
        yield return new WaitForSeconds(_reverseTime);
        player.Hourglass = false;
        Debug.Log("�d�͂��߂���");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player)
            {
                AudioManager.instance.PlaySE(AudioManager.SeSoundData.SE.Hourglass);
                StartCoroutine(Reverse(player));
                Debug.Log("�����v");
            }
        }
    }
}

