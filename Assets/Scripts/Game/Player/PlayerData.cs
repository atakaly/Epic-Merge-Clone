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

        public int CurrentWorkerCount;
        public int MaxWorkerCount;

        public PlayerData()
        {
            Level = 0;
            Experience = 0;
            Coin = 100;
            MaxEnergy = CurrentEnergy = 100;
            MaxWorkerCount = CurrentWorkerCount = 2;
        }
    }
}