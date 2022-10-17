# Project SHMUP

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Brandon Korn
-   Section: 5

## Game Design

-   Camera Orientation: topdown
-   Camera Movement: The background moves behind the player as they move forward
-   Player Health: The player ship has 4 levels of damage, the last being destoryed
-   End Condition: Surviving as long as possible before dying
-   Scoring: Scoring is based on the number of hits and collected orbs

### Game Description

Play as the retired ace pilot from the Space Wars who after a nice vaction at Mars, flys back to see earth blockaded by the old enemy, the Karaxians. Now, to make it home, you must fly and shoot your way past the Karaxian ships and blast through their blockade to make it safely back to your family. 

### Controls

-   Movement
    -   Up: W
    -   Down: S
    -   Left: A
    -   Right: D
-   Fire: SPACE

## You Additions

 -- Custom Enemy types
 -- Special Powers that charge up
 
 -- Boss fight (Stretch goal -- didn't make it in time)

## Sources

* Animated Pixel Ships --- dylestorm (www.livingtheindie.com). Twitter: twitter.com/livingtheindie  PURCHASED 
* Main Ship --- Commissioned from: Baldur (twitter.com/the__baldur).  Distributed by Foozle (www.foozle.io).   CC0 License.

## Known Issues

The webGL build online is a little messed up, but it works better in the Unity browser. The texts are smaller than they should, and the explosion animation doesn't play when the player dies, for a reason I do not know. But there is an explosion animation on player death. 
The activatable powers in the bottom doesn't work on webGL. It worked in the Unity browser. You should be able to press 1, 2, or 3 and use the powerups, and I could in the Unity browser. I don't know why it doesn't work online. 

### Other Notes
The player's health is shown by the player sprite. As the player is hit, the ship gets more and more damage before it explodes. 

