using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public enum BGM
    {
        Logo,
        Menu,
        InGame,

    }
    public enum Effect
    {
        button,

    }

    private Dictionary<BGM, AudioClip> bgm;
    private Dictionary<Effect, AudioClip> effect;

    private AudioSource EffectSource;
    private AudioSource BgmSource;

    private static SoundManager _instance;


    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                var newobj = new GameObject("SoundManager");
                _instance = newobj.AddComponent<SoundManager>();
            }
            return _instance;
        }
    }

    public void LoadSound()//사운드 추가
    {
        EffectSource = gameObject.AddComponent<AudioSource>();
        BgmSource = gameObject.AddComponent<AudioSource>();

        effect = new Dictionary<Effect, AudioClip>();
        bgm = new Dictionary<BGM, AudioClip>();

        effect.Add(Effect.button, Resources.Load("Sound/Button") as AudioClip);
        bgm.Add(BGM.Logo, Resources.Load("Sound/Logo") as AudioClip);

    }
    public void PlayEffectSound(Effect Sound)
    {
        if (effect[Sound] != null)
        {
            EffectSource.PlayOneShot(effect[Sound]);
        }

    }
    public void PlayBgmSound(BGM Sound)
    {
        if (bgm[Sound] != null)
        {
            BgmSource.PlayOneShot(bgm[Sound]);

        }
    }
}
