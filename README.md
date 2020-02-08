# Run_Lolita_Run
My first personal game project 

This is all the code for my personal game project. 

It is a 2D infinite platformer where you control a little dog called Lolita and try to avoid obstacles to get as far as possible. 
There are three lanes that the player can move across, and in eachof them the polayer can jump to avoid the obstacles. On the top lane, the player will also have a super jump that recharges with collected bones. This big jump is used to avoid cars, which are the biggest obstacle.
It uses raycasts to check for collisions to either collect bones or check if the player has lost the game.
Backgrounds will spawn infinitely as the camera moves with the player, so the game will only end when the player hits an obstacle. 
It also has a high-score feature that stores the three highest scores of the player. At the end of each game, it will check if the score is higher than any of the three high-scores and adjust them accordingly.
It uses a meny system to navigate between the main menu, how to play, high scores and game. The game can be paused at any time, unpaused or restarted. The end screen gives the option to restart the game or exit. 
