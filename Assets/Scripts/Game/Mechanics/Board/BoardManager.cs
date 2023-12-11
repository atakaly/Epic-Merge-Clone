using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace EpicMergeClone.Game.Mechanics.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private CellGrid m_Grid;

        public List<CharacterItem> FindCharacterItems()
        {
            List<CharacterItem> characters = new List<CharacterItem> ();

            for (int i = 0; i < m_Grid.m_Cells.Count; i++)
            {
                if (m_Grid.m_Cells[i].State == CellState.Unavailable || m_Grid.m_Cells[i].State == CellState.Locked)
                    continue;

                if (m_Grid.m_Cells[i].CurrentItem == null)
                    continue;

                if (m_Grid.m_Cells[i].CurrentItem is CharacterItem character && character.gameObject.activeInHierarchy)
                {
                    characters.Add(character);
                } 
            }

            return characters;
        }

        public List<CollectibleItem> FindCollectibleItems()
        {
            List<CollectibleItem> collectibles = new List<CollectibleItem>();

            for (int i = 0; i < m_Grid.m_Cells.Count; i++)
            {
                if (m_Grid.m_Cells[i].State == CellState.Unavailable || m_Grid.m_Cells[i].State == CellState.Locked)
                    continue;

                if (m_Grid.m_Cells[i].CurrentItem == null)
                    continue;

                if (m_Grid.m_Cells[i].CurrentItem is CollectibleItem collectible && collectible.gameObject.activeInHierarchy)
                {
                    collectibles.Add(collectible);
                }
            }

            return collectibles;
        }
    }
}