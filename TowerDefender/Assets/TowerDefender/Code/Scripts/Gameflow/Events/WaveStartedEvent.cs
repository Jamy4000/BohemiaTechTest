namespace TowerDefender
{
    public sealed class WaveStartedEvent 
    {
        public readonly Wave Wave;

        public WaveStartedEvent(Wave wave)
        {
            Wave = wave;
        }
    }
}