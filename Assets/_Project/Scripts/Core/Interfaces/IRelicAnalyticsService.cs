namespace RelicHaul.Core
{
    public interface IRelicAnalyticsService
    {
        void TrackSessionStart();
        void TrackRelicCollected(int totalRelics);
        void TrackPlayerDeath();
        void TrackExtractionSuccess(int relics, float sessionSeconds);
    }
}
