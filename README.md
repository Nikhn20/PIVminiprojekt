# Portal "but worse" Copy

The idea behind this game was to make a game like Portal. The player can freely see in any direction, move, and can jump in a 3D-world and can use either left or right mouse buttons to shoot either red or blue portals. The player must move the block to two specific points to advance in the level, which can be done in either way you like. This block can be moved by using the middle mouse button and using X or Z to move it towards the player or away from the player.

## Parts of the Game

- **Player:** can be moved using WASD and jump using Spacebar
- **Portals:** can be used by pressing the left or right mouse button, two portals must be instantiated to use them
- **Camera:** the camera is sitting on the player and can freely see in any direction using the mouse.
- **Moveable Block:** can be moved using middle mouse button to drag the movable block and moved towards or away from you with using the keys X or Z
- **Level:** a level designed in ProBuild that contains 2 easy puzzle elements, the level restart if the player hits the bottom of the level.

The game can be further increased in difficulty by making new levels that require more thinking and more intricate positions of the portal, however for this case an easy map was made to show the functionality of the game features.

## Project Parts

### Scripts

- **Movement:** used for the movement of the player using the input system, moving up stairs and rigid body physics for jumping and used for checking colliders, and the code for grabbing the object and moving it around.
- **CubeScriptGoal:** used for collision checks and then triggering animations for the different pathways.
- **MouseLook:** used for allowing the player to freely see around the level with the use of their mouse.
- **Portals:** used for making sure the cube gets teleported with the same portals.
- **PortalGun:** used for shooting portals and making sure that only a maximum of 2 portals can be active at the same time, and the teleportation of the player.
## Time Usage for different parts

| Task                                                | Time taken in hours |
| --------------------------------------------------- | -------------------- |
| Setting up Unity                                    | 0.25                 |
| Portal ideas and research                           | 2                    |
| Building level using ProBuilder                     | 1.5                  |
| Making a movement script with jumping and walking stairs    | 3                    |
| Making portals with particle systems                | 1.5                  |
| Making shootable portals script                     | 5                    |
| Making pressure plates for the cube to collide with and animations that trigger accordingly.                | 2                    |
| Making the cube teleport                             | 0.5                  |
| Making the cube moveable using middle mouse button  | 2                    |
| Making UI based on teleportation and crosshair      | 0.5                  |
| Creating GitHub repository and uploading files      | 1                    |
| All      | 19.25                    |

## Additional Information

The GitHub repository for this project is available [here](https://github.com/Nikhn20/PIVminiprojekt/tree/main).

## References

- Reference for looking around with the mouse [[link](https://www.youtube.com/watch?v=_QajrabyTJc&t=567s)]
- Reference for walking up stairs [[link](https://www.youtube.com/watch?v=DrFk5Q_IwG0)]
