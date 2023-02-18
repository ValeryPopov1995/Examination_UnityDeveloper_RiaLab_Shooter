using RiaShooter.Scripts.Localisation;
using RiaShooter.Scripts.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace RiaShooter.Scripts.Settings
{
    internal class GameSettings : MonoBehaviour
    {
        [SerializeField] private DropdownAnimated _language;
        [SerializeField] private Toggle _posteffects;
        [SerializeField] private DropdownAnimated _antializing;
        [Space]
        [SerializeField] private Toggle _sound;
        [SerializeField] AudioMixer _SoundMixer;
        [SerializeField] string _effectsVolumeField;
        [Space]
        [SerializeField] private Toggle _music;
        [SerializeField] AudioMixer _musicMixer;
        [SerializeField] string _musicVolumeField;

        private const string _keySetttingsConfig = "game settings config";
        private GameSettingsData _config;
        private UniversalAdditionalCameraData _cameraData;



        private void Awake()
        {
            _cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();

            AddListiners();
            LoadConfig();
        }

        private void Start()
        {
            SetValuesByConfig();
        }

        private void OnDestroy()
        {
            RemoveListiners();
            SaveConfig();
        }

        private void AddListiners()
        {
            _music.onValueChanged.AddListener(SetMusic);
            _sound.onValueChanged.AddListener(SetSound);
            _language.onValueChanged.AddListener(SetLanguage);
            _posteffects.onValueChanged.AddListener(SetPosteffects);
            _antializing.onValueChanged.AddListener(SetAntializing);
        }

        private void RemoveListiners()
        {
            _music.onValueChanged.RemoveListener(SetMusic);
            _sound.onValueChanged.RemoveListener(SetSound);
            _language.onValueChanged.RemoveListener(SetLanguage);
            _posteffects.onValueChanged.RemoveListener(SetPosteffects);
            _antializing.onValueChanged.RemoveListener(SetAntializing);
        }

        private void LoadConfig()
        {
            if (PlayerPrefs.HasKey(_keySetttingsConfig))
                _config = JsonUtility.FromJson<GameSettingsData>(PlayerPrefs.GetString(_keySetttingsConfig));
            else
                _config = new(language: 1);
        }

        private void SaveConfig()
        {
            string json = JsonUtility.ToJson(_config);
            PlayerPrefs.SetString(_keySetttingsConfig, json);
            Debug.Log(json);
        }




        private void SetValuesByConfig()
        {
            _music.isOn = _config.Music;
            _sound.isOn = _config.Sound;
            _language.value = _config.Language;
            _posteffects.isOn = _config.PostEffects;
            _antializing.value = _config.AntiAlizing;

            SetMusic(_config.Music);
            SetSound(_config.Sound);
            SetLanguage(_config.Language);
            SetPosteffects(_config.PostEffects);
            SetAntializing(_config.AntiAlizing);
        }

        private void SetAntializing(int index)
        {
            if (_cameraData.antialiasing != (AntialiasingMode)index)
                _cameraData.antialiasing = (AntialiasingMode)index;

            if (_config.AntiAlizing != index)
                _config.AntiAlizing = index;
        }

        private void SetPosteffects(bool state)
        {
            _cameraData.renderPostProcessing = state;

            if (_config.PostEffects != state)
                _config.PostEffects = state;
        }

        private void SetLanguage(int index)
        {
            Localisator.Instance.Localize((Localisator.LanguageType)index);

            if (_config.Language != index)
                _config.Language = index;
        }

        private void SetSound(bool state)
        {
            SetVolumeInternal(state, _SoundMixer, _effectsVolumeField, ref _config.Sound);
        }

        private void SetMusic(bool state)
        {
            SetVolumeInternal(state, _musicMixer, _musicVolumeField, ref _config.Music);
        }

        private void SetVolumeInternal(bool enabled, AudioMixer mixer, string exposedParameter, ref bool configState)
        {
            if (enabled)
                mixer.SetFloat(exposedParameter, 0);
            else
                mixer.SetFloat(exposedParameter, -80);

            if (configState != enabled)
                configState = enabled;
        }
    }
}
