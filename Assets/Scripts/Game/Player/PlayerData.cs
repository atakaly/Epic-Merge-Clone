namespace EpicMergeClone.Game.Player
{
    [System.Serializable]
    public class PlayerData
    {
        public int Level;
        public float Experience;
        public int Coin;

        public int CurrentEnergy;
        public int MaxEnergy;

        public PlayerData()
        {
            Level = 1;
            Experience = 0;
            Coin = 100;
            MaxEnergy = CurrentEnergy = 100;
        }
    }
}