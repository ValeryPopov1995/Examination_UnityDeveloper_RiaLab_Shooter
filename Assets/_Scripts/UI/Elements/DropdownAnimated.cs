using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace RiaShooter.Scripts.UI
{
    internal class DropdownAnimated : TMP_Dropdown
    {
        protected override GameObject CreateDropdownList(GameObject template)
        {
            var dropdownList = base.CreateDropdownList(template);
            FadeIn(dropdownList);
            return dropdownList;
        }

        private async void FadeIn(GameObject dropdownList)
        {
            await Task.Yield();
            var rect = dropdownList.GetComponent<RectTransform>();
            var endSizeDelta = rect.sizeDelta;
            rect.sizeDelta = new Vector2(endSizeDelta.x, 0);
            await rect.DOSizeDelta(endSizeDelta, alphaFadeSpeed).AsyncWaitForCompletion();
        }
    }
}
