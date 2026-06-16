using UnityEngine;

namespace RelicHaul.Core
{
	public enum PerformanceTier
	{
		Low = 0,
		High = 1
	}

	public class QualitySettingsManager
	{
		const string PrefsKey = "performance_tier";

		public PerformanceTier CurrentTier { get; private set; } = PerformanceTier.High;

		public void ApplyTier(PerformanceTier tier)
		{
			CurrentTier = tier;

			switch (tier)
			{
				case PerformanceTier.Low:
					QualitySettings.SetQualityLevel(0, true);
					Application.targetFrameRate = 30;
					break;
				default:
					QualitySettings.SetQualityLevel(2, true);
					Application.targetFrameRate = 60;
					break;
			}
		}

		public void ApplySavedTier()
		{
			var saved = PlayerPrefs.GetInt(PrefsKey, (int)PerformanceTier.High);
			ApplyTier((PerformanceTier)saved);
		}

		public void SaveTier(PerformanceTier tier)
		{
			PlayerPrefs.SetInt(PrefsKey, (int)tier);
			ApplyTier(tier);
		}
	}
}