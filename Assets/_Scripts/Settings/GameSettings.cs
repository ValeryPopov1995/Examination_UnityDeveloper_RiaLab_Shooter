using RiaShooter.Scripts.Localisation;
using RiaShooter.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace RiaShooter.Scripts.Settings
{
    internal class GameSettings : MonoBehaviour
    {
        [SerializeField] private Toggle _music;
        [SerializeField] private Toggle _sound;
        [SerializeField] private DropdownAnimated _language;
        [SerializeField] private Toggle _posteffects;
        [SerializeField] private DropdownAnimated _antializing;

        private void Awake()
        {
            _music.onValueChanged.AddListener(SetMusic);
            _sound.onValueChanged.AddListener(SetSound);
            _language.onValueChanged.AddListener(SetLanguage);
            _posteffects.onValueChanged.AddListener(SetPosteffects);
            _antializing.onValueChanged.AddListener(SetAntializing);
        }

        private void SetAntializing(int index)
        {
            Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = (AntialiasingMode)index;
        }

        private void SetPosteffects(bool state)
        {
            Camera.main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = state;
        }

        private void SetLanguage(int index)
        {
            Localisator.Instance.Localize((Localisator.LanguageType)index);
        }

        private void SetSound(bool arg0)
        {
            throw new NotImplementedException();
        }

        private void SetMusic(bool arg0)
        {
            throw new NotImplementedException();
        }
    }
}
