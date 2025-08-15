# Space Invaders Clone

A modern implementation of the classic Space Invaders arcade game, built with real-time and data-driven software architecture principles using C# and the Azul game engine.

## Overview

This Space Invaders clone demonstrates advanced software engineering concepts through a complete game implementation. The project showcases real-time game development patterns, data-driven design, and clean architecture principles while delivering an authentic arcade experience.

## Architecture Highlights

### Real-Time Architecture
- **Frame-based game loop** with consistent 60 FPS performance
- **Real-time collision detection** using observer pattern
- **Time-driven events** and animations
- **Responsive input handling** with immediate feedback

### Data-Driven Design
- **Component-based game objects** for flexible entity composition
- **Strategy pattern** for dynamic behavior switching
- **Factory pattern** for object creation and management
- **State machine** for scene and game object lifecycle management

### Design Patterns Implemented
- **Observer Pattern**: Collision detection and event handling
- **Composite Pattern**: Hierarchical game object management
- **Strategy Pattern**: Movement and behavior algorithms
- **Factory Pattern**: Object creation and resource management
- **Proxy Pattern**: Resource management and optimization
- **State Pattern**: Scene and game object state management

## Features

### Core Gameplay
- Classic Space Invaders mechanics
- Multiple invader types (Crab, Octopus, Squid, UFO)
- Player ship with movement and shooting
- Destructible shields
- Progressive difficulty scaling
- Score tracking system

### Technical Features
- **Scene Management**: Select, Play, and Game Over states
- **Collision System**: Efficient collision detection and response
- **Audio System**: Sound effects and background music
- **Sprite Management**: Optimized rendering with sprite batching
- **Timer System**: Event-driven timing for animations and spawns
- **Input System**: Keyboard input handling with observer pattern

## Technology Stack

- **Language**: C# (.NET Framework 4.7.2)
- **Game Engine**: Azul Engine
- **Audio**: irrKlang
- **Architecture**: Real-time, data-driven design
- **Platform**: Windows (x86)

## Project Structure

```
SpaceInvaders/
├── Collision/          # Collision detection system
├── Composite/          # Composite pattern implementation
├── Font/              # Text rendering system
├── GameObject/        # Game entities and components
│   ├── Birds/         # Bird entities
│   ├── Bomb/          # Enemy projectiles
│   ├── Bumper/        # Screen boundaries
│   ├── Explosion/     # Explosion effects
│   ├── Invaders/      # Enemy invaders
│   ├── Missile/       # Player projectiles
│   ├── Shield/        # Destructible shields
│   ├── Ship/          # Player ship
│   └── Walls/         # Game boundaries
├── Image/             # Image management
├── Input/             # Input handling system
├── Manager/           # Core management systems
├── Observer/          # Observer pattern implementations
├── Scene/             # Scene management
├── Sprite/            # Sprite rendering system
├── Texture/           # Texture management
├── Timer/             # Timer and event system
└── _Diagrams/         # Architecture diagrams
```

## Getting Started

### Prerequisites
- Visual Studio 2017 or later
- .NET Framework 4.7.2
- Windows x86 platform

### Building the Project
1. Clone the repository
2. Open `SpaceInvaders.sln` in Visual Studio
3. Build the solution (Debug or Release configuration)
4. Run the application

### Controls
- **Arrow Keys**: Move ship left/right
- **Spacebar**: Shoot
- **1**: Switch to Select scene
- **2**: Switch to Play scene
- **3**: Switch to Game Over scene

## Architecture Diagrams

The project includes comprehensive architecture diagrams in the `_Diagrams/` folder covering:
- Animation system
- Collision detection
- Composite pattern
- Font rendering
- GameObject management
- Input handling
- Proxy pattern
- Scene management
- Simulation flow
- Sprite management
- Strategy pattern
- Texture management
- Timer system

## Key Systems

### Collision System
Real-time collision detection using the Observer pattern with efficient bounding box calculations and collision response.

### GameObject Management
Component-based architecture allowing flexible composition of game objects with different behaviors and properties.

### Scene Management
State pattern implementation for managing different game scenes (Select, Play, Game Over) with clean transitions.

### Timer System
Event-driven timing system for managing animations, spawns, and game events with precise timing control.

### Input System
Observer-based input handling providing immediate response to user actions with clean separation of concerns.

## Development Notes

This project serves as an excellent example of:
- Real-time game development principles
- Data-driven design patterns
- Clean architecture in game development
- Performance optimization techniques
- Scalable game object systems

## License

Copyright 2025, Jessa Gillespie, all rights reserved.

## Contributing

This is an educational project demonstrating software architecture principles. The codebase is structured to be easily understood and extended for learning purposes.

---
