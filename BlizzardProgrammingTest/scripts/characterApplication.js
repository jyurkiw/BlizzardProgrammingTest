characterApp = angular.module('character-app', ['UserServices', 'CharacterServices', 'RaceServices']);

// Application constants
characterApp.constant('CLASS_COLUMNS', 3);
characterApp.constant('MIN_CLASS_PER_COL', 4);
characterApp.constant('ALLIANCE', 'alliance');
characterApp.constant('HORDE', 'horde');
characterApp.constant('DEFAULT_FACTION', 'alliance');
characterApp.constant('DEFAULT_RACE', 'Human');
characterApp.constant('DEFAULT_CLASS', 'Warrior');
characterApp.constant('DEATHKNIGHT_CLASS', 'Death Knight');
characterApp.constant('DEATHKNIGHT_START_LEVEL', 55);