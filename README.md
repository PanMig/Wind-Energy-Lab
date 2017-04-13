# Basic info
Welcome to Wind Energy Lab.

Wind energy lab is a serious game where the player gets to control a wind farm to provide electrical energy to a small town. By interacting with the game, the player understands how random changes - in wind speed and power requirement of the town - affect the use of this natural energy resource.

This project is made using Unity 3d as well as Blender 3d and it belongs to the Envisage Project (http://www.envisage-h2020.eu/).

# Game overview
The game is currently under development, however the key elements of the game had been developed.
The player is able to control a wind farm , by adding , and controlling the operation of various wind turbine, with the aim to provide electrical energy to a nearby small town.

However, the amount of electricity generated depends on the strength of the wind - if there is no wind, there is no electricity.
You will see that maintaining the correct amount of power - not too little, or not too much - while both wind and the power requirements of the town are constantly fluctuating can prove a challenge. Sometimes there will be insufficient power generated even with all available turbines in operation. At other times the power generated will be too great. The aim is to maximise the amount of time that only the correct amount of electricity is generated.

# Aesthetics  
The game tries to have as much as possible a relistic look, that resembles a real operating wind farm. The use of a simple GUI system is also availiable to give the ability to even unexpirienced users, to try the game without frustation.

![gamephoto](https://cloud.githubusercontent.com/assets/15057375/24956876/5285e37c-1f92-11e7-913c-2a3aba4c60b0.png)

# Code Overview
The language used is C# (.mono).

There are the classes in the Turbine script folder that simulate the operation of the wind turbines in the game, which is the main object of the game.
Also, there is also a number of other scripts that execute various behaviors of different game objects.

# Turbine classes.
- TurbineController.cs : The main class for the turbine gameObject that controls the behavior of the object. All actions concerning the turbine should be called from this class. That's why this class is having a big number of dependencies.

- TurbineAnimCtrl.cs : Controls the animator component that is attached to the game object.

- TurbineDamage.cs : Calculates the propability in which a turbine can be damaged and damages the game object (stops rotating and becomes semi transparent).

- TurbineInputManager.cs : Controls all the input action on the turbine from the player. More specifically this class gets the mouse clicks on the game object, mouse hover, and highlights the selection of the game object.

- TurbineRepair.cs : When a turbine is damaged, then the player can repair that turbine. That is what the class does. It basically sets the isDamaged boolean to false, and all the rest are handle automatically from the other turbine's classes.

- TurbineSpawnManager.cs : Adds turbine prefabs to the map of the game, and handles the behaviour of the main button in the gui, the button that adds a turbine.

# Demo
You can try and test an early version of the game in the following link: https://www.dropbox.com/s/2kj9hk8izo2fndc/Demo%28v_0.3%29.zip?dl=0

Beware, a zip file will be downloaded. Unzip the file and have both the game file as well as the demo(v_03).data folder in the same directory.
The are three resolution you can play: 1920*1080, 1336*768, 1280*800, and three quality options: Fantastic, Beautiful, good.
Choose based on your system.

# Contact
- Panagiotis migkotzidis (panagiotismigo@gmail.com)
- Spiros Nikolopoulos (nikolopo@iti.gr)
- Giannis Chantas (gchantas@iti.gr)
- Dimitrios Ververidis (ververid@iti.gr)
- Leuteris Anastasovitis (anastasovitis@iti.gr)

