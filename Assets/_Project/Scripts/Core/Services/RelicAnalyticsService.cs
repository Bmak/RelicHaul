using Unity.Services.Analytics;
using UnityEngine;

namespace RelicHaul.Core
{
    public sealed class RelicAnalyticsService : IRelicAnalyticsService
    {
        public void TrackSessionStart() { LogEvent("session_start"); }

        public void TrackRelicCollected(int totalRelics) { LogEvent("relic_collected", "total_relics", totalRelics); }

        public void TrackPlayerDeath() { LogEvent("player_death"); }

		public void TrackExtractionSuccess(int relics, float sessionSeconds)
        {
            LogEvent("extraction_success", "relics", relics);
            LogEvent("extraction_success", "session_seconds", Mathf.RoundToInt(sessionSeconds));
        }

        static void LogEvent(string eventName, string paramName = null, int paramValue = 0)
        {
            try
            {
                if (paramName != null)
                {
                    AnalyticsService.Instance.RecordEvent(new CustomEvent(eventName)
                    {
                        { paramName, paramValue }
                    });
                }
                else
                {
                    AnalyticsService.Instance.RecordEvent(eventName);
                }
            }
            catch (System.Exception exception)
            {
                Debug.Log($"[Analytics] {eventName} ({exception.Message})");
            }
        }
    }
}
