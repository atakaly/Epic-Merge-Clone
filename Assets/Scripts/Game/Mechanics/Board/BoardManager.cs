using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private CellGrid m_Grid;

        public List<CharacterOrderPair> GetCurrentOrders()
        {
            List<CharacterOrderPair> characterOrderPairs = new List<CharacterOrderPair>();
            List<CharacterItem> characters = FindCharacters();

            for (int i = 0; i < characters.Count; i++)
            {
                characterOrderPairs.Add( new CharacterOrderPair()
                {
                    characterItemSO = characters[i].ItemDataSO as CharacterItemSO,
                    Order = characters[i].GetCurrentOrder()
                });
            }

            return characterOrderPairs;
        }

        public List<CharacterItem> FindCharacters()
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

        [System.Serializable]
        public struct CharacterOrderPair 
        {
            public CharacterItemSO characterItemSO;
            public Order Order;
        }
    }
}