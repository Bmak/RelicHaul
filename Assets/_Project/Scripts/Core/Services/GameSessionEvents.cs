using RelicHaul.Core;
using System;
using UnityEngine;

namespace RelicHaul.Core
{
	public class GameSessionEvents : IGameSessionEvents
	{
		public event Action<int> RelicCountChanged;
		public event Action<float, float> HealthChanged;
		public event Action<int> WaveChanged;
		public event Action SessionWon;
		public event Action SessionLost;

		public void PublishHealth(float current, float max)
		{
			HealthChanged?.Invoke(current, max);
		}

		public void PublishLoss()
		{
			SessionLost?.Invoke();
		}

		public void PublishRelicCount(int count)
		{
			RelicCountChanged?.Invoke(count);
		}

		public void PublishWave(int waveIndex)
		{
			WaveChanged?.Invoke(waveIndex);
		}

		public void PublishWin()
		{
			SessionWon?.Invoke();
		}
	}
}
