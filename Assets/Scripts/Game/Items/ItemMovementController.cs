using System.Collections.Generic;
using EpicMergeClone.Game.Mechanics.Grid;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ItemMovementController : MonoBehaviour
    {
        private PolygonCollider2D m_Collider;
        private List<Cell> m_CollidingCells = new List<Cell>();

        private void Awake()
        {
            m_Collider = GetComponent<PolygonCollider2D>();
        }

        private void OnMouseDrag()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            transform.position = mousePosition;
        }

        private void OnMouseUp()
        {
            if (m_CollidingCells.Count == 0)
            {
                Debug.Log("Return to current position");
                // Return to current cell
                return;
            }

            Cell targetCell = m_CollidingCells[m_CollidingCells.Count - 1];

            if (targetCell.State == CellState.Unavailable || targetCell.State == CellState.Locked)
            {
                Debug.Log("Return to current position");
                // Return to current cell
                return;
            }

            if (targetCell.State == CellState.Occupied)
            {
                Debug.Log("Move that occupying cell and put this to that cell");
                // Move the occupying cell and put this to that cell
                return;
            }

            Debug.Log("Add item");
            targetCell.AddItem(GetComponent<ItemBase>());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Cell cell))
            {
                m_CollidingCells.Add(cell);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Cell cell))
            {
                m_CollidingCells.Remove(cell);
            }
        }
    }
}
