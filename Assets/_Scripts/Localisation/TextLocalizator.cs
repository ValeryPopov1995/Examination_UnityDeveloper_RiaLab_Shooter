using TMPro;
using UnityEngine;

namespace RiaShooter.Scripts.Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    internal class TextLocalizator : MonoBehaviour
    {
        [SerializeField] private string _textId = "new game";
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            Localisator.Localized += Localize;
        }

        private void OnDestroy()
        {
            Localisator.Localized -= Localize;
        }

        private void Localize()
        {
            _text.text = Localisator.GetText(_textId);
        }
    }
}
