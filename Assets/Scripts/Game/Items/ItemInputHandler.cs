using System;
using System.Collections;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ItemInputHandler : MonoBehaviour
    {
        public event Action OnClick;
        public event Action OnEndDrag;
        public event Action OnStartDrag;

        [SerializeField] private bool canDrag = true;
        [SerializeField] private float mouseDragTimeThreshold = 0.3f;

        private float mouseDownTime;
        private Coroutine startDragInvoke;

        private void OnMouseDown()
        {
            mouseDownTime = Time.time;
            startDragInvoke = StartCoroutine(StartDrag());
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
                StopCoroutine(startDragInvoke);
                OnClick?.Invoke();
                return;
            }

            if (!canDrag)
                return;

            mouseDownTime = 0f;
            OnEndDrag?.Invoke();
        }

        private IEnumerator StartDrag()
        {
            yield return new WaitForSeconds(mouseDragTimeThreshold);
            OnStartDrag?.Invoke();
        }
    }
}