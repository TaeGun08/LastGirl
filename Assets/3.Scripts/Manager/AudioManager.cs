using UnityEngine;
using Newtonsoft.Json;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("AudioObject")]
    [SerializeField] private AudioObject audioObject;
    public AudioObject AudioObject => audioObject;
    
    [Header("AudioSource Settings")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Audio Settings")] 
    [SerializeField] private AudioClip bgmClip;

    private SettingData settingData = new SettingData();
    
    private void Awake()
    {
        Instance = this;
        
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("SettingData"))) return;
        settingData = JsonConvert.DeserializeObject<SettingData>(PlayerPrefs.GetString("SettingData"));
        SetBgmVolume(settingData.BGM);
        SetSfxVolume(settingData.SFX);
    }
    
    private void Start()
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.Play();
    }

    public void SetBgmClip(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void SetSfxClip(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void SetBgmVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }
}
