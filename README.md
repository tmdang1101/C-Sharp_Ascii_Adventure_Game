asciiadventure
===
Ascii adventure in C#

## Added Features
1. Jumping 2 steps using IJKL
2. Teleporters:
Walking into a teleport will transport you to the other one in the same direction (i.e. from left to right)
3. Weapon and Mine:
I counted these two objects as one feature since they work together. After the Player picks up the Weapon, they have three Mines in their inventory to place wherever they want on the map to kill the mob. Mines can also be automatically placed on the map when the game starts. Player can also be set up to have unlimited Mines.
4. Treasures:
I upgraded Treasure so that Player can store the number of treasures in their inventory to win the game.


Other than those main features, I also added some small, little nuances.
The Game ends when the Player either kills the mob or gathers all the treasures (or gets killed by the mob).


I also fixed some things that I thought were bugs in [screen.cs]. In the original code, resizing the map was an issue, as well as walking into the map boundaries.

## Game files
* [screen.cs](screen.cs)
* [adventure.cs](adventure.cs)
* [gameobjects.cs](gameobjects.cs)
* [player.cs](player.cs)
* [mob.cs](mob.cs)



### Challenges
* Not the greatest class hierarchy
* Mobs can only move when the player moves. There's no timer in the background.
* Not possible for moving gameobjs to move over objects with replacing them
* Really clunky menu options
* Not great MVC for display; the Screen is essentially everything
    * Need to draw moving gameobjs over the top of the underlying stuff
    * Two layers? Basic stuff, plus moving things?
* Not well set up to have multiple screens connected by portals, or something

### Other notes
* Screen is one screen, with numRows and numCols
    * 
* GameObject is an object in the game.
    * Owned by Screen, and does not need to know about screen
    * Some GameObjects include Wall and Treasure
    * Is passable? If it's passable, you can walk over it.
* MovingGameObject extends GameObject, but adds movement as a possibility.
    * Knows about Screen
    * needs to ask Screen about legal moves
