namespace Assets.LastDefender.Scripts.Core.Models
{
    public class EnemyInfo
    {
        public EnemyData Data { get; private set; }
        public float CurrentHealth { get; set; }

        public EnemyInfo(EnemyData data)
        {
            Data = data;
            CurrentHealth = data.Health;
        }
    }
}
