# Laser CNC Simulator

An interactive virtual training simulator for a CNC laser cutting machine, developed in Unity as a graduation project.

The project was created to demonstrate the operation of a laser CNC machine in an interactive virtual environment and to provide a basic training experience without requiring access to physical equipment.

## Overview

The simulator recreates the main workflow of working with a CNC laser machine in a virtual environment.

The project combines:

* 3D machine visualization;
* interactive machine components;
* operator controls;
* scene-based training tasks;
* visualization of the machine working area;
* laser head movement and operation;
* interactive UI and level selection.

The project was developed with a focus on combining 3D content creation with gameplay and simulation programming.

## Features

* Interactive 3D CNC laser machine.
* Separate machine components prepared for interaction and animation.
* Operator-oriented interface.
* Main menu and level selection system.
* Multiple scenes representing different parts of the training experience.
* Interactive machine controls.
* Visualization of the laser working area.
* Simulation of the laser head movement.
* Training-oriented interaction with the machine.
* Build-in guide.
* Standalone Windows build.

## Technologies

### Programming

* **C#**
* **Unity**

### 3D Content

* **Blender**

### Development Environment

* Unity
* Visual Studio
* Blender

## Machine Model Preparation

The original 3D machine model was prepared in Blender before being imported into Unity.

The model was separated into individual functional components to allow them to be controlled independently inside the simulation.

Examples of prepared components include:

* laser head;
* movement axes;
* machine door;
* working table;
* other interactive machine elements.

The resulting hierarchy was designed with Unity interaction and animation in mind.

## Project Structure

The project follows the standard Unity project structure.

```text
Assets/
├── Blueprints/
├── Scenes/
├── Scripts/
├── Models/
├── Materials/
├── Prefabs/
└── ...

Packages/
ProjectSettings/
Docs/
└── Screenshots/
```

The exact contents of the `Assets` directory may vary depending on the current project version.

## Screenshots

Screenshots of the simulator are available in the [`Docs/`](./Docs/) directory.


## Controls

The simulator uses keyboard and mouse input to interact with the virtual machine and navigate the training environment.

WASD - movement
Mouse - camera control
E - interact
M - menu
J - objectives

## Technical Implementation

The project was developed as an interactive Unity application rather than a static 3D visualization.

The main technical areas include:

### Scene Management

The application is divided into multiple Unity scenes.

A main menu provides access to the available training levels, while individual scenes contain the corresponding simulation environments.

### Interactive Objects

Machine components are represented as separate Unity objects where necessary, allowing their position, state, and behaviour to be controlled by scripts.

### Machine Movement

The laser head and associated machine components are controlled through Unity scripts to reproduce the movement of the CNC mechanism inside the virtual environment.

### 3D Asset Pipeline

Blender was used to prepare the 3D assets before importing them into Unity.

The workflow included:

1. Preparing the machine model.
2. Separating components that require independent movement.
3. Organizing the object hierarchy.
4. Exporting the model for use in Unity.
5. Setting up the imported objects for interaction and animation.

## Project Goals

The main goal of the project was to create a virtual environment that demonstrates the basic principles of operating a CNC laser machine.

The project also served as an opportunity to apply software development and 3D graphics skills in a single application, including:

* C# programming;
* Unity development;
* scene management;
* object interaction;
* 3D modelling and asset preparation;
* UI development;
* animation and machine movement.

## Build

A standalone Windows build is provided separately from the Unity source project.

The build contains the compiled application and required Unity runtime files.

See the **Releases** section of this repository for the latest available build.

## How to Open the Project

1. Clone or download this repository.
2. Open **Unity Hub**.
3. Select **Add project from disk**.
4. Select the project directory.
5. Open the project using a compatible Unity version.
6. Open the main menu scene from the `Assets` directory.

## Graduation Project

This simulator was originally developed as a graduation project.

**Project type:** Graduation / Bachelor Project
**Engine:** Unity
**Programming language:** C#
**3D software:** Blender

The project combines programming, interactive simulation, UI development, and 3D asset preparation into a single application.

## Author

Developed as a graduation project in Information Systems and Technologies by Hevlin A. V.
