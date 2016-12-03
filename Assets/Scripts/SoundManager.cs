using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public enum BGM
    {
        Lobby,
        InGame_Default,//기본값
        InGame_Boss,//보스가 HP 30%이하
        InGame_Player,//아군파티2명일때
        Clear,//보스사망
        Fail,// 아군보스사망



    }
    public enum Effect
    {
        ButtonClick,
        ButtonClose,
        ButtonSuccess,
        BreakShield,

        Assain_Hit,
        Bard_Burf,
        Lancer_Hit,
        Paladin_HitAndHeal,
        PriestHeal,
        Shielder_Stun,
        Warrior_Hit,
        Wizard_Hit,





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
    void Awake()
    {
        EffectSource = gameObject.AddComponent<AudioSource>();
        BgmSource = gameObject.AddComponent<AudioSource>();
        BgmSource.loop = true;
        LoadSound();

    }
    void Start()
    {
        LoadSound();
    }
    public void LoadSound()//사운드 추가하는곳
    {


        effect = new Dictionary<Effect, AudioClip>();
        bgm = new Dictionary<BGM, AudioClip>();
        #region Bgm
        bgm.Add(BGM.Lobby, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_Lobby") as AudioClip);

        bgm.Add(BGM.InGame_Default, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_InGame(Default)") as AudioClip);

        bgm.Add(BGM.InGame_Boss, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_InGame(Boss)") as AudioClip);

        bgm.Add(BGM.InGame_Player, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_InGame(Player)") as AudioClip);

        bgm.Add(BGM.Clear, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_Clear") as AudioClip);

        bgm.Add(BGM.Fail, Resources.Load("Sound/Bgm/(BGM)DeongeonCatHeroes_Fail") as AudioClip);

        #endregion
        #region Effect
        effect.Add(Effect.Assain_Hit, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Assassin_Hit") as AudioClip);

        effect.Add(Effect.Bard_Burf, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Bard_Buff") as AudioClip);
        effect.Add(Effect.Lancer_Hit, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Lancer_Hit") as AudioClip);
        effect.Add(Effect.Paladin_HitAndHeal, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Paladin_Hit&Heal") as AudioClip);
        effect.Add(Effect.PriestHeal, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Priest_Heal") as AudioClip);
        effect.Add(Effect.Shielder_Stun, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Shielder_Stun") as AudioClip);
        effect.Add(Effect.Warrior_Hit, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Warrior_Hit") as AudioClip);
        effect.Add(Effect.Wizard_Hit, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_Wizard_Hit") as AudioClip);

        effect.Add(Effect.ButtonClick, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_UI_Click") as AudioClip);
        effect.Add(Effect.ButtonClose, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_UI_Close") as AudioClip);
        effect.Add(Effect.ButtonSuccess, Resources.Load("Sound/Effect/(SFX)DeongeonCatHeroes_UI_Success") as AudioClip);

        #endregion
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
            BgmSource.clip = bgm[Sound];
            BgmSource.Play();

        }
    }
}
