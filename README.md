# C# Dungeon Explorer

## Overview
A text-based RPG developed to demonstrate robust **Object-Oriented Programming (OOP)** architecture in C#. Unlike standard console games, this project focuses on modular design patterns, utilizing Interfaces and Manager classes to handle game state, entities, and room navigation dynamically.

## Key Features
* **Advanced OOP Structure:** Uses `Interfaces.cs` and base classes to define shared behaviors across different game entities (Enemies, Player, NPCs).
* **Dynamic Room Navigation:** Implemented a `RoomManager` and `DoorClass` system to link distinct areas logically, allowing for scalable map creation.
* **Combat & Encounters:** The `EncountersClass` handles turn-based battle logic, utilizing RNG for enemy generation and damage scaling.
* **Inventory System:** Dedicated `InventoryLogic` to manage loot, item storage, and equipment stats.
* **State Management:** Tracks player statistics and game progress via `StatisticsClass`.

## Technical Architecture
* **Language:** C# (.NET Core)
* **Design Patterns:**
    * **Managers:** Separation of concerns (e.g., `RoomManager` handles navigation, `InventoryLogic` handles items).
    * **Polymorphism:** Entity definitions in `Entities.cs` allow for diverse enemy types inheriting from base classes.
    * **Interfaces:** defined in `Interfaces.cs` to enforce contract compliance across different game objects.

## How to Run
1. Clone the repository.
2. Open `DungeonExplorer.sln` in Visual Studio.
3. Build the solution (Ctrl+Shift+B).
4. Run `Program.cs` to start the console application.
