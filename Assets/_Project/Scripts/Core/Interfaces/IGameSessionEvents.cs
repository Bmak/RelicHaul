using System;

namespace RelicHaul.Core
{
    public interface IGameSessionEvents
    {
        event Action<int> RelicCountChanged;
        event Action<float, float> HealthChanged;
        event Action<int> WaveChanged;
        event Action SessionWon;
        event Action SessionLost;

        void PublishRelicCount(int count);
        void PublishHealth(float current, float max);
        void PublishWave(int waveIndex);
        void PublishWin();
        void PublishLoss();
    }
}
