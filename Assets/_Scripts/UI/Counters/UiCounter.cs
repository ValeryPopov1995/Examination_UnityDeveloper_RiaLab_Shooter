using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RiaShooter.Scripts.UI
{
    /// <summary>
    /// Отображает числовое значение
    /// </summary>
    public abstract class UiCounter : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI text;
        [SerializeField, Min(1)] float animationDuration = 1.5f;

        private int targetValue;
        private int currentValue;
        [SerializeField] private UnityEvent OnStartSetValueFx;



        private void OnEnable()
        {
            StartCoroutine(CountCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }




        protected void SetValueImmidiatly(int value)
        {
            currentValue = targetValue = value;
            text.text = currentValue.ToString();
        }

        protected void SetValue(int value)
        {
            targetValue = value;

            if (!isActiveAndEnabled) return;

            StopAllCoroutines();
            StartCoroutine(CountCoroutine());

            OnStartSetValueFx?.Invoke();
        }

        private IEnumerator CountCoroutine()
        {
            if (currentValue == targetValue) yield break;

            int framesCount = Mathf.RoundToInt(60 * animationDuration);
            int difference = Mathf.Abs(targetValue - currentValue);
            if (framesCount > difference)
                framesCount = difference;
            for (int i = 0; i <= framesCount; i++)
            {
                currentValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, (float)i / framesCount));
                text.text = currentValue.ToString();
                yield return null;
            }
        }
    }
}