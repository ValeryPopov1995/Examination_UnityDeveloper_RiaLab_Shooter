using RiaShooter.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RiaShooter.Scripts.Localisation
{
    internal class Localisator : Singleton<Localisator>
    {
        public static event Action Localized;

        public enum LanguageType { eng, rus }

        private const string _cellEnding = "</Data></Cell>";
        private const int _languageCount = 2;
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private LanguageType _languageType = LanguageType.rus;
        private static Dictionary<string, string> _textData = new();

        protected override void Awake()
        {
            base.Awake();
            Localize(_languageType);
        }

        public void Localize(LanguageType type)
        {
            _textData.Clear();
            int strIndex = -1;
            int dataIndex = (int)type + 1; // 0 - id, 1 - 1st lang
            var strs = _textAsset.text.Split('\n').Where(x => x.Contains(_cellEnding)).ToList();
            strs = strs.GetRange(3, strs.Count-3); // убрать строки id eng rus

            string key = "";

            foreach (var str in strs)
            {
                strIndex++;
                if (strIndex > _languageCount)
                    strIndex = 0;

                string text = str.Substring(0, str.Length - _cellEnding.Length - 1);
                int startIndex = text.LastIndexOf('>') + 1;
                text = text.Substring(startIndex, text.Length - startIndex);

                if (strIndex == 0)
                    key = text;
                else if (strIndex == dataIndex)
                    _textData.Add(key, text);
            }

            Localized?.Invoke();
        }

        public static string GetText(string textId)
        {
            return _textData[textId];
        }
    }
}
