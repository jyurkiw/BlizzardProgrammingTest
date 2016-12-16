using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlizzardProgrammingTest.Backend
{
    /// <summary>
    /// Constant values used by the application.
    /// Pure static constants, because Magic Numbers and Strings
    /// are evil.
    /// </summary>
    public static class BackendConstants
    {
        public static class RaceClassQueryIndicies
        {
            public const int Id = 0;
            public const int Faction = 1;
            public const int Race = 2;
            public const int Class = 3;
        }

        public static class CharacterQueryIndicies
        {
            public const int Id = 0;
            public const int Owner = 1;
            public const int Name = 2;
            public const int Level = 3;
            public const int Race = 4;
            public const int Class = 5;
            public const int Faction = 6;
            public const int Deleted = 7;
        }

        /// <summary>
        /// Catch-all constants dealing with queries and other query-based opperation.
        /// </summary>
        public static class QueryProperties
        {
            public const int RaceClassRowLength = 4;
            public const int CharacterRowLength = 8;
            public const string DeathKnightClassName = "Death Knight";
            public const string RaceClassQueryPath = "bin\\Data\\raceClassJoinQuery.json";
            public const string CharacterPath = "bin\\Data\\charactersQuery.json";
            public const int DeathKnightMinLevelReq = 55;
            public const int LevelTokenLevel = 100;
        }

        public static class RaceClassFieldNames
        {
            public const string Id = "id";
            public const string Race = "race";
            public const string Class = "class";
        }

        public static class CharacterFieldNames
        {
            public const string Id = "id";
            public const string Name = "name";
            public const string Level = "level";
            public const string Race = "race";
            public const string Class = "class";
            public const string Faction = "faction";
            public const string Deleted = "deleted";
        }
    }
}