using System;
using UnityEngine;
using UnityEngine.Events;

namespace RiaShooter.Scripts.UI
{
    /// <summary>
    /// UI ����, ������������ ������ � ����������, ����������� � ������ ��������� ����. ������� ����� ���� ������ ���� ���� ���� Window � ���� ���� Popup
    /// </summary>
    public class View : MonoBehaviour
    {
        #region DECLARATIONS

        /// <summary>
        /// ���������� ��� ������ ����������� ����. true ���� ����� ��������  ��������� ����
        /// </summary>
        private static Action<View, bool> onShowView;

        /// <summary>
        /// ��� ����. ������� ����� ���� ������ ���� ���� ���� Window � ���� ���� Popup
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
        /// ����� ���������� �������, ��� ����� ��������
        /// </summary>
        public void DisableGameObject() => gameObject.SetActive(false);

        /// <summary>
        /// ���������� ����, ����� ���������
        /// </summary>
        public void Show() => Show(true);

        /// <summary>
        /// ���������� ����
        /// </summary>
        /// <param name="hideOthers">������ ��������� ���� ����� ����</param>
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
        /// ������ ���� ��� �������� ������� ���� (�� �������)
        /// </summary>
        /// <param name="openedView">����������� ����</param>
        /// <param name="hideOthers">�������� �� ��� ���� ��� �������� �������</param>
        private void Hide(View openedView, bool hideOthers)
        {
            if (this != openedView && Type == openedView.Type && hideOthers) Hide();
        }

        /// <summary>
        /// ������ ����
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