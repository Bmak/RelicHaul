using System;
using System.Collections.Generic;

namespace RelicHaul.Core
{
    [Serializable]
    public sealed class PlayerSaveData
    {
        public int BestScore;
        public int TotalExtractions;
        public List<string> UnlockedRelicIds = new();
        public string LastDungeonKey = "dungeon_week1";
        public string LocaleCode = "en";
    }
}
