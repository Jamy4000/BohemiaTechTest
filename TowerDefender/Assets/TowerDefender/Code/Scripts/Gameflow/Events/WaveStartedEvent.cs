namespace TowerDefender
{
    public readonly struct WaveStartedEvent 
    {
        public readonly Wave Wave;

        public WaveStartedEvent(Wave wave)
        {
            Wave = wave;
        }
    }
}