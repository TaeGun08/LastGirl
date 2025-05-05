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
    public class PlayerClips
    {
        public AudioClip[] HeadClips;
        public AudioClip[] ChestClips;
        public AudioClip[] BellyClips;
        public AudioClip[] DontTouchClips;
        public AudioClip[] LegClips;
        public AudioClip[] DashClips;
        public AudioClip[] HitClips;
    }
    
    public BgmClips bgmClips;
    public PlayerClips playerClips;
}
