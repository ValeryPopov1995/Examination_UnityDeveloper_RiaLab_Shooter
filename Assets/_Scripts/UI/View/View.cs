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

        [field: SerializeField] public ViewType type { get; private set; } = ViewType.Window;
        [SerializeField] private bool hideOnStart = true;

        public UnityEvent<View> OnShow, OnHide;

        private IViewElement[] iViewElements;
        private bool isShown = true;

        #endregion



        private void Awake()
        {
            GetComponent<RectTransform>().localPosition = Vector3.zero;
            iViewElements = GetComponentsInChildren<IViewElement>();

            onShowView += Hide;
        }

        private void Start()
        {
            if (hideOnStart) HideOnStart();
            else Show();
        }

        private void HideOnStart()
        {
            isShown = false;
            gameObject.SetActive(false);
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
            if (this != openedView && type == openedView.type && hideOthers) Hide();
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        public void Hide()
        {
            if (!isShown) return;

            isShown = false;
            for (int i = 0; i < iViewElements.Length; i++) iViewElements[i].OnViewHide();

            OnHide?.Invoke(this);
        }
    }
}