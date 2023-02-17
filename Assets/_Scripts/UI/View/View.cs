using System;
using UnityEngine;
using UnityEngine.Events;

namespace RiaShooter.Scripts.UI
{
    /// <summary>
    /// UI окно, отображающее кнопки и показатели, необходимые в данном состоянии игры. Открыто может быть только одно окно типа Window и одно типа Popup
    /// </summary>
    public class View : MonoBehaviour
    {
        #region DECLARATIONS

        /// <summary>
        /// Вызывается при вызове отображения окна. true если нужно скрывать  остальные окна
        /// </summary>
        private static Action<View, bool> onShowView;

        /// <summary>
        /// Тип окна. Открыто может быть только одно окно типа Window и одно типа Popup
        /// </summary>
        public enum ViewType { Window, Popup }

        [field: SerializeField] public ViewType Type { get; private set; } = ViewType.Window;
        [SerializeField] private bool _hideOnStart = true;
        [SerializeField] private CanvasGroup _canvasGroup;
        private IViewElement[] iViewElements;
        private bool isShown = true;

        public UnityEvent<View> OnShow, OnHide;

        #endregion



        private void Awake()
        {
            GetComponent<RectTransform>().localPosition = Vector3.zero;
            iViewElements = GetComponentsInChildren<IViewElement>();

            onShowView += Hide;
        }

        private void Start()
        {
            if (_hideOnStart) HideOnStart();
            else Show();
        }

        private void HideOnStart()
        {
            isShown = false;
            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
        }

        private void OnDestroy()
        {
            onShowView -= Hide;
        }


        /// <summary>
        /// Метод выключения объекта, для ключа анимации
        /// </summary>
        public void DisableGameObject() => gameObject.SetActive(false);

        /// <summary>
        /// Отобразить окно, скрыв остальные
        /// </summary>
        public void Show() => Show(true);

        /// <summary>
        /// Отобразить окно
        /// </summary>
        /// <param name="hideOthers">Скрыть остальные окна этого типа</param>
        public void Show(bool hideOthers = true)
        {
            if (isShown) return;

            isShown = true;
            for (int i = 0; i < iViewElements.Length; i++) iViewElements[i].OnViewShow();

            _canvasGroup.interactable = true;

            OnShow?.Invoke(this);
            onShowView?.Invoke(this, hideOthers);
        }

        /// <summary>
        /// Скрыть окно при открытии другого окна (по событию)
        /// </summary>
        /// <param name="openedView">Открываемое окно</param>
        /// <param name="hideOthers">Скрывать ли это окно при открытии другого</param>
        private void Hide(View openedView, bool hideOthers)
        {
            if (this != openedView && Type == openedView.Type && hideOthers) Hide();
        }

        /// <summary>
        /// Скрыть окно
        /// </summary>
        public void Hide()
        {
            if (!isShown) return;

            isShown = false;
            for (int i = 0; i < iViewElements.Length; i++) iViewElements[i].OnViewHide();

            _canvasGroup.interactable = false;

            OnHide?.Invoke(this);
        }
    }
}