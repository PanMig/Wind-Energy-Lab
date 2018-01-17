# Basic info
Welcome to Wind Energy Lab.

Wind energy lab is a serious game where the player gets to control a wind farm to provide electrical energy to a small town. By interacting with the game, the player understands how random changes - in wind speed and power requirement of the town - affect the use of this natural energy resource.

This project is made using Unity 3d as well as Blender 3d and it belongs to the Envisage Project (http://www.envisage-h2020.eu/).

# Game overview
The game is currently under development, however the key elements of the game had been developed.
The player is able to control a wind farm , by adding , and controlling the operation of various wind turbines, with the aim to provide electrical energy to a nearby small town.

However, the amount of electricity generated depends on the strength of the wind - if there is no wind, there is no electricity.
You will see that maintaining the correct amount of power - not too little, or not too much - while both wind and the power requirements of the town are constantly fluctuating can prove a challenge. Sometimes there will be insufficient power generated even with all available turbines in operation. At other times the power generated will be too great. The aim is to maximise the amount of time that only the correct amount of electricity is generated.

# Aesthetics  
The game tries to have as much as possible a relistic look, that resembles a real operating wind farm. The use of a simple GUI system is also availiable to give the ability to even unexpirienced users, to try the game without frustation.

![gamephoto](https://cloud.githubusercontent.com/assets/15057375/24956876/5285e37c-1f92-11e7-913c-2a3aba4c60b0.png)

# Code Overview
The language used is C# (.mono).

There are the classes in the Turbine script folder that simulate the operation of the wind turbines in the game, which is the main object of the game.
Also, there is a number of other scripts that execute various behaviors of different game objects.

# Turbine game object classes.
- TurbineController.cs : The main class for the turbine gameObject that controls the behavior of the object. All actions concerning the turbine should be called from this class. That's why this class is having a big number of dependencies.

- TurbineAnimCtrl.cs : Controls the animator component that is attached to the game object.

- TurbineDamage.cs : Calculates the propability in which a turbine can be damaged and damages the game object (stops rotating and becomes semi transparent).

- TurbineInputManager.cs : Controls all the input action on the turbine from the player. More specifically this class gets the mouse clicks on the game object, mouse hover, and highlights the selection of the game object.

- TurbineRepair.cs : When a turbine is damaged, then the player can repair that turbine. That is what the class does. It basically sets the isDamaged boolean to false, and all the rest are handle automatically from the other turbine's classes.

- TurbineSpawnManager.cs : Adds turbine prefabs to the map of the game, and handles the behaviour of the main button in the gui, the button that adds a turbine.

# Other scripts
- Simulation.cs : Simulates the speed of the wind, and the power requirements of the city in the game. By simulation, we mean that it gives to the mentioned variables, random values, that increment and decrement it's valued, in a natural way. Also, the power output that is produced as well as, the usage of the wind farm(under power, correct, over) are calculated. Moreover, time is simulated in this class. All the mentioned variables are not game objects in the architecture of the program, but are just variables that its values are being displayed on the screen as text.

- ObjectPooler.cs : Creates an object pooler to reduce the allocation and deallocations in the memory. Currently, the object pooler is used for the particle effect that is being emitted and destroyed several times in the game when a turbine is damaged. However, the class can be used for any game object.

- ParticlesController.cs : Controls the emission of the particle system in the game. More specifically, this script is attached in the turbine prefab, and emits the particle system prefab (smoke in our case). The particle system is not destroyed, but disabled and enabled upon request using the object pooling technique.

- DisplayStatistics.cs : This script is used in the end screen of the game. Two functions are present in this script,
  - void ConvertSecondToMin() : that converts the seconds that the player has spent in each power output scenario (under, over,   correct).
  - DisplayPlayerStatistics() : displays a msg that informs the player about the use of the wind farm based on his actions.
  
- PlayerStatistics.cs : The class uses the above methods 
  - CalculatePowerUsageStatistics() : It holds to static variables the seconds that the player has spent in each power output scenario  respectively. These values are later used in the end scene to calculate and display the usage of the wind farm, concerning the time spent in each scenario.
  - EndSimulation() : stops the simulation and loads the end scene in the game. This can be achieved either by clicking on the exit button, or after 24 minutes have passed. 

- ChangeBuildingsMaterial.cs : Switches between different materials for highlighting the buildings in the minimap on the left corner of the screen.

# Demo
You can try and test the web version of the game in the following link in the 3D games TAB: http://www.envisage-h2020.eu/virtual-labs/

Beware, a zip file will be downloaded. Unzip the file and have both the game file as well as the demo(v_03).data folder in the same directory.
The are three resolution you can play: 1920*1080, 1336*768, 1280*800, and three quality options: Fantastic, Beautiful, good.
Choose based on your system.

# Contact
- Panagiotis migkotzidis (panagiotismigo@gmail.com)
- Spiros Nikolopoulos (nikolopo@iti.gr)
- Giannis Chantas (gchantas@iti.gr)
- Dimitrios Ververidis (ververid@iti.gr)

