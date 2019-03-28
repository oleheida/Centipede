Terrifying Centipede is a clone of Centipede 1980, a vertically oriented shooter arcade game.

Implemented:
	-Player can move his plane using either keyboard arrows or WASD;
	-Player can shoot projectiles using either Spacebar or Left Ctrl;
	-Killing mushrooms gives points and randomly generates pickup buffs for players;
	-There are 2 buff types for now: Movement Speed buff and Shooting Speed buff;
	-Centipede moves horizontally and goes one segment down and changes move direction everytime it collides with mushroom or side of a screen;
	-Killing any part of the centipede will generate mushroom at that point;
	
Newly implemented:
	-When there are 20 or less mushrooms left, spiders will begin to spawn at the top of a screen;
	-When there are 10 or less mushrooms left, mushrooms will start to generate randomly on the screen, until there are 30 of them presented;
	-Spiders spawned at the top of the screen are falling down;
	-Spiders spawn mushrooms;
	-Spiders have animation;
	-While falling spiders generate additional mushrooms;
	-Spiders now also damage the Player;
	-Killing all the segment will create additional centipede at the top of a screen;
	-Difficulty;
	-After killing a centipede difficulty level raises by spawning new Centipede with 15% increased speed relatively to previous one and with 1 additional head;
	-Health system for player;
	-Health buff for player;
	-When centipede collides with a player, player loses 1 health;
	-When centipede reaches the bottom side of a screen, player loses the game;
	
To be implemented:
	-Lizards spawning at the bottom of the screen and moving diagonally with random factor; 
	-Lizards damage player;
	-??? Lizards eats any mushroom which it collides with; (makes the game easier in my opinion)
	-Killing one segment of a centipede creates a new head in the next one;