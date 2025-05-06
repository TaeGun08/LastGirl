using Michsky.MUIP;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class SettingData
{
    public float BGM = 0.5f;
    public float SFX = 0.5f;
    public float Sensitivity = 0.5f;
}

public class Setting : MonoBehaviour
{
    private AudioManager audioManager;
    private PlayerCamera playerCamera;
    
    [Header("Settings")]
    [SerializeField] private ButtonManager button;
    [SerializeField] private Slider[] sliders;
    
    private SettingData settingData = new SettingData();
    
    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            audioManager.SetSfxClip(audioManager.AudioObject.uiClips.ButtonsClips[0]);
            settingData.BGM = sliders[0].value;
            settingData.SFX = sliders[1].value;
            settingData.Sensitivity = sliders[2].value;
            PlayerPrefs.SetString("SettingData", JsonConvert.SerializeObject(settingData));
            gameObject.SetActive(false);
        });

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("SettingData"))) return;
        settingData = JsonConvert.DeserializeObject<SettingData>(PlayerPrefs.GetString("SettingData"));
        sliders[0].value = settingData.BGM;
        sliders[1].value = settingData.SFX;
        sliders[2].value = settingData.Sensitivity;
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
    }

    private void Update()
    {
        audioManager.SetBgmVolume(sliders[0].value);
        audioManager.SetSfxVolume(sliders[1].value);
        
        if (Input.GetKeyDown(KeyCode.Escape) == false) return;

        if (playerCamera != null)
        {
            playerCamera.SetSensitivity(sliders[2].value);
        } 
        
        settingData.BGM = sliders[0].value;
        settingData.SFX = sliders[1].value;
        settingData.Sensitivity = sliders[2].value;
        PlayerPrefs.SetString("SettingData", JsonConvert.SerializeObject(settingData));
        gameObject.SetActive(false);
    }
}
