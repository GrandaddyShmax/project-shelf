## Project Shelf
In order to run the project:
1. Open up the Unity project
2. Select "File" in the top menu bar and then select "Build and Run"

*Alternatively*, you can try the built version on [Itch.io](https://grandaddyshmax.itch.io/project-shelf)

![image](https://github.com/user-attachments/assets/33321fdf-11c3-49ec-9236-bd0db65807b1)

All assets were made by me. No third party plugins were used.

## Design
My design principle is based on `Managers` and `Controllers`:<br/>
`Managers` hold the main logic of each element of the game.<br/>
`Controllers` are component scripts attached to the controlled object to modify all relevant parts through one script.<br/>

For example:<br/>
`ProductManager` Manages and instantiates all of the Products, it holds their data and modifies them when needed. `ProductManagers` holds a list of `ProductControllers`
`ProductController` Controlls and modifies the object itself, like the name and price that is displayed.

`SceneryManager` That holds all the `Managers`. It initiates them and updates them in the desired order.<br/>
Each `Manager` is a singleton, allowing `Managers` to easily comminicate with each other.

## Code Overview
Abstract:
- `Controller`: The abstract class for every controller based script. It is updated and managed by the `ControllerManager` if the manager set to default.
- `Manager`: The abstract class for every manager based script. This script is updated and managed by the `SceneryManager`. It's main goal is to wait until all the other managers are initiated before letting it run.

Controllers:
- `FPSCounterController`: Short script to display the FPS of the game.
- `ProductController`: Handles what the board displays.
- `StateObserverController`: Observes the current state of the game. There are 3 states: `Shelf` - Shelf view, `Transition` - During animation, `Product` - Focused on the product.
- `UIController`: Simple script for displaying certain UI elements on specific states.

Game:
- `GameManager`: Due to having only one scene, this script is irrelevant.
- `SceneryManager`: Initiates and updates every manager attached to it in order, also handles the state of the current scene.

Global:
- `Enums`: Simple script that holds every enum.

Managers:
- `ControllerManager`: Updates every controller script that attaches to it.
- `KeyboardManager`: (Not actual Manager) Roundabout solution to keyboard not showing up when running on mobile.
- `PlayerManager`: Handles interactions with the products, including animating them.
- `ProductManager`: Fetches products from the server and initiates them.
- `UIManager`: Handles the UI elements, including animating them.

Object:
- `Product`: Simple class for the product.
