//日本語対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    GameManager _gm = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySE(AudioManager.SeSoundData.SE.Goal);
            AudioManager.instance.PlayBGM(AudioManager.BgmSoundData.BGM.Dream);
            _gm.GameClear();
        }
    }
}
