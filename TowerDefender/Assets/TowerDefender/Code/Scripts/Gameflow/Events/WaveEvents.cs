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

    public readonly struct WaveEndedEvent
    {
        public readonly Wave Wave;

        public WaveEndedEvent(Wave wave)
        {
            Wave = wave;
        }
    }
}