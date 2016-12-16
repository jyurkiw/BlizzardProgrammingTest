# BlizzardProgrammingTest

By Jeffrey Yurkiw

This is a solution to the Blizzard .Net Engineer Take Home Test.

It is an application that allows managment of a World of Warcraft player's characters.

When the application starts, the user will take on the role of a new player with a clean sheet of characters.

* The player can add characters to their account.
* The player can delete characters from their account.
* The player can un-delete characters from their account.

New characters can be added to the account.

* The player can select the character's race (race decided faction).
* The selected race can limit the available class options.
* The player can give the new character a name.

The available factions are: Alliance or Horde.

The Alliance has the following races:
* Human
* Gnome
* Worgen

The Horde has the following races:
* Orc
* Tauren
* Blood Elf

The following classes are available:
* Warrior
* Mage
* Druid
* Death Knight

* Humans and Gnomes can be Warriors, Mages, and Death Knights.
* Worgen can be Warriors, Mages, Druids, and Death Knights.
* Orcs can be Warriors, Mages, and Death Knights.
* Tauren can be Warriors, Mages, Druids, and Death Knights.
* Blood Elves can be Mages, and Death Knights.

To create a Death Knight a player must already have an active (not-deleted) character at level 55 or above.

# Technical Requirements
The application runs on a clean build directly from a git clone operation out of the primary repo master branch provided Nuget isn't blocked.

The project uses AngularJS for the front-end, and ASP.Net for the back-end. The front-end is an angularjs single-page application while the back-end API uses Microsoft's .NET Web Api 2.

Data persistence is in-memory and on-disk (see issue #1).

Authentication is handled through Windows Authentication. If you are signed into your computer, you are signed into the application (see issue #2).

Unit testing was performed on the front and back-ends where we would not simply be testing Microsoft's and Google's code. (see issue #3)

# Technical Issues

1. The disk-persistance is slow. I attempted to isolate the actual writes from the main thread of execution using tasks, but was unsuccessful and simply ran out of time. The choice for disk-persistence was to either disable or remove the feature, or to leave it in but have it impact application performance. I chose to leave it in to demonstrate that it works. In a production environment, additional steps could have been taken with the time provided (like colaboration with teammates who were more familair with the fine points of .NET file IO performance tuning), and additional tools would have been available (like a database).

2. There is no backup plan for authentication besides windows auth, and anon auth is not enabled. The application does not have a sign-in page so if Windows auth is disabled (because, say, you're using Mac or 'nix without some sort of LDAP auth token set up for AD) you'll probably see the application blow up. This limitation is purely time-constrained.

3. Test coverage is light, however I feel the big areas were hit. The overall application structure was very straightforward, and so the more complex data-transforming methods were made a testing priority. The tests written for the class columnization in the front-end character creation controller test file actually uncovered a number of issues with the columnization that were then fixed. Particularly, while the columnization routine was dividing the data up into columns, the exact behavior at the edges was wrong. Especially with a large number of classes.

# Assumptions

1. Characters are created at level 1. This does mean that Death Knights cannot be created without shutting the application down, and changing the data that has been written to file.

2. I'm assuming my awesome orc drawing in the character creator is adequate for all race and class combinations. He's just that awesome.
