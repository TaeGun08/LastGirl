using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AudioObject", menuName = "Audio/AudioObject")]
public class AudioObject : ScriptableObject
{
    [System.Serializable]
    public class BgmClips
    {
        public AudioClip[] TitleBgmClips;
        public AudioClip[] LobbyBgmClips;
        public AudioClip[] GameBgmClips;
    }
    
    [System.Serializable]
    public class UIClips
    {
        public AudioClip[] ButtonsClips;
    }
    
    [System.Serializable]
    public class WeaponClips
    {
        public AudioClip[] ARClips;
    }

    [System.Serializable]
    public class AbilityClips
    {
        public AudioClip[] FireClips;
        public AudioClip[] LaserClips;
    }

    [System.Serializable]
    public class EnemyClips
    {
        public AudioClip[] HitClips;
        public AudioClip[] DeadClips;
    }
    
    [System.Serializable]
    public class PlayerClips
    {
        public AudioClip[] HeadClips;
        public AudioClip[] ChestClips;
        public AudioClip[] BellyClips;
        public AudioClip[] DontTouchClips;
        public AudioClip[] LegClips;
        public AudioClip[] DashClips;
        public AudioClip[] HitClips;
        public AudioClip[] DeadClips;
    }
    
    public BgmClips bgmClips;
    public UIClips uiClips;
    public WeaponClips weaponClips;
    public AbilityClips abilityClips;
    public EnemyClips enemyClips;
    public PlayerClips playerClips;
}
