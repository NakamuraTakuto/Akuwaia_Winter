using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [Tooltip("BGMを再生するAudioSource")]
    [SerializeField] AudioSource _audioBgm;
    [Tooltip("SEを再生するAudioSource")]
    [SerializeField] AudioSource _audioSe;

    [Space]

    [Header("AudioClip")]
    [Tooltip("BGM")]
    [SerializeField] List<BgmSoundData> _bgmSoundDatas;
    [Tooltip("SE")]
    [SerializeField] List<SeSoundData> _seSoundDatas;

    [SerializeField]
    float _masterVolume = 1;
    [SerializeField]
    float _bgmMasterVolume = 1;
    [SerializeField]
    float _seMasterVolume = 1;

    /// <summary>
    /// BGMを再生するようにする
    /// </summary>
    /// <param name="bgm">再生したいBGMのenum</param>
    public void PlayBGM(BgmSoundData.BGM bgm)
    {
        int index = (int)bgm;
        BgmSoundData data = _bgmSoundDatas[index];
        _audioBgm.clip = data._audioClip;

        //音量の調節
        _audioBgm.volume = data._volume * _bgmMasterVolume * _masterVolume;
        _audioBgm.Play();
    }

    /// <summary>
    /// SEを再生するようにする
    /// </summary>
    /// <param name="se">再生したいSEのenum</param>
    public void PlaySE(SeSoundData.SE se)
    {
        int index = (int)se;
        SeSoundData data = _seSoundDatas[index];

        //音量の調節
        _audioSe.volume = data.Volume * _seMasterVolume * _masterVolume;
        _audioSe.PlayOneShot(data.AudioClip);
    }


    //BGM
    [System.Serializable]
    public struct BgmSoundData
    {
        public enum BGM
        {
            [Tooltip("タイトル")]
            Title,
            [Tooltip("夢BGM")]
            Dream,
            [Tooltip("現実BGM")]
            Real,
            [Tooltip("スタッフロール")]
            StaffRolls,
        }

        public BGM _bgm;
        public AudioClip _audioClip;
        [Range(0f, 1f)]
        public float _volume;
    }


    //SE
    [System.Serializable]
    public struct SeSoundData
    {
        public enum SE
        {
            [Tooltip("足音")]
            Footsteps,
            [Tooltip("ジャンプ")]
            Jump,
            [Tooltip("トランプ")]
            Trump,
            [Tooltip("砂時計")]
            Hourglass,
            [Tooltip("鏡")]
            Mirror,
            [Tooltip("穴(入れ替わりの音)")]
            Hole,
            [Tooltip("ゴール")]
            Goal,
            [Tooltip("ゲームオーバー")]
            GameOver,
            [Tooltip("クリア")]
            Clear,
        }

        public SE Se;
        public AudioClip AudioClip;
        [Range(0, 1)]
        public float Volume;
    }

    #region　シングルトン
    public static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (!instance)
            {
                SetupInstance();
            }

            return instance;
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    static void SetupInstance()
    {
        instance = FindObjectOfType<AudioManager>();

        if (!instance)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<AudioManager>();
            go.name = instance.GetType().Name;
            DontDestroyOnLoad(go);
        }
    }
    #endregion
}
