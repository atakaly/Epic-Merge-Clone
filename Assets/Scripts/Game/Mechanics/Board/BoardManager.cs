using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using System.Collections.Generic;
using UnityEngine;

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
                if (m_Grid.m_Cells[i].CurrentItem == null)
                    continue;

                if (m_Grid.m_Cells[i].CurrentItem is CharacterItem character && character.gameObject.activeInHierarchy)
                {
                    characters.Add(character);
                } 
            }

            return characters;
        }

    }
}