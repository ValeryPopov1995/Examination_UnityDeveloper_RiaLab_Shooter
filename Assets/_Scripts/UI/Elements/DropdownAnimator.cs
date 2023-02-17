using TMPro;
using UnityEngine;

namespace RiaShooter.Scripts.UI
{
    internal class DropdownAnimator : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            
        }
    }
}
