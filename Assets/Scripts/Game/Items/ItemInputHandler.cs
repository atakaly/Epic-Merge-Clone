using System;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ItemInputHandler : MonoBehaviour
    {
        public event Action OnClick;
        public event Action OnDragged;

        [SerializeField] private bool canDrag = true;
        [SerializeField] private float mouseDragTimeThreshold = 0.3f;

        private float mouseDownTime;

        private void OnMouseDown()
        {
            mouseDownTime = Time.time;
        }

        private void OnMouseDrag()
        {
            if (!canDrag)
                return;

            if (mouseDownTime + mouseDragTimeThreshold < Time.time)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                transform.position = mousePosition;
            }
        }

        private void OnMouseUp()
        {
            if (Time.time - mouseDownTime < mouseDragTimeThreshold)
            {
                mouseDownTime = 0f;
                OnClick?.Invoke();
                return;
            }

            if (!canDrag)
                return;

            mouseDownTime = 0f;
            OnDragged?.Invoke();
        }
    }
}