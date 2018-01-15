# Basic info
Welcome to Wind Energy Lab.

Wind energy lab is a serious game where the player gets to control a wind farm to provide electrical energy to a small town. Through the process of the game, you will have to select the place where your wind park will be installed, select between a number of different wind turbines types to use, as well as operate the wind farm to produce the correct amount of power

This project is made using Unity 3d as well as Blender 3d and it belongs to the Envisage Project (http://www.envisage-h2020.eu/).

# Game overview
You and your team have undertaken to study the construction of a wind farm.
The wind park should meet the energy needs of a village with the following characteristics.

Population: 4000 || Households: 1800 || Power requested: 6MW

Through the process of the game, you will have to select the place where your wind park will be installed, select between a number of different wind turbines types to use, as well as operate the wind farm to produce the correct amount of power requested.
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

- TurbineInputManager.cs : Controls all the input action on the turbine from the player. More specifically this class gets the mouse clicks on the game object, mouse hover, and highlights the selection of the game object.

- TurbineRepair.cs : When a turbine is damaged, then the player can repair that turbine. That is what the class does. It basically sets the isDamaged boolean to false, and all the rest are handle automatically from the other turbine's classes.


# Other scripts
- Simulation.cs : Simulates the speed of the wind, and the power requirements of the city in the game. By simulation, we mean that it gives to the mentioned variables, random values, that increment and decrement it's valued, in a natural way. Also, the power output that is produced as well as, the usage of the wind farm(under power, correct, over) are calculated. Moreover, time is simulated in this class. All the mentioned variables are not game objects in the architecture of the program, but are just variables that its values are being displayed on the screen as text.

- ObjectPooler.cs : Creates an object pooler to reduce the allocation and deallocations in the memory. Currently, the object pooler is used for the particle effect that is being emitted and destroyed several times in the game when a turbine is damaged. However, the class can be used for any game object.

- ParticlesController.cs : Controls the emission of the particle system in the game. More specifically, this script is attached in the turbine prefab, and emits the particle system prefab (smoke in our case). The particle system is not destroyed, but disabled and enabled upon request using the object pooling technique.


# Demo
You can try and test an early "Windows" version of the game in the following link:
http://www.envisage-h2020.eu/virtual-labs/

# Contact
- Panagiotis migkotzidis (panagiotismigo@gmail.com)
- Spiros Nikolopoulos (nikolopo@iti.gr)
- Dimitrios Ververidis (ververid@iti.gr)

