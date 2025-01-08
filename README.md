# Midnight Warehouse
**Game Description**  
*SECURITY Theme | Limitation: On a Schedule / November 21-24 2024*

## Overview  
Midnight Warehouse is a first-person puzzle and adventure game set in a creepy, dark warehouse. The player must explore the environment, solve various puzzles, and interact with key objects to progress. It features a variety of tasks, including cleaning oil spills, locating hidden codes, and avoiding dangerous entities like a roaming pig. The ultimate goal is to escape the warehouse.

---

## Key Contributions as a Gameplay Engineer  
### **Player Interaction and UI**  
  - Developed intuitive interaction with objects such as cleaning oil spills, picking up items like keys and fire extinguishers, and triggering special events.  
  - Integrated a simple UI system with instructions, interactive prompts, and key status updates (e.g., "Holding key", "Pick up fire extinguisher").

### **Puzzle System**  
  - Designed and implemented a code-entry puzzle where players must find and input the correct sequence to unlock doors.  
  - Allowed interaction with various objects through triggers (e.g., keys, fire extinguishers, and carts) to advance through the game.

### **Physics and Movement**  
  - Integrated character movement and collision handling using Unity’s CharacterController component to allow smooth navigation through tight spaces.  
  - Created object-based interactions (e.g., moving and using the fire extinguisher, interacting with the key) to ensure dynamic gameplay and realistic player interactions.

### **Dynamic Game Events**  
  - Implemented various environmental interactions such as cleaning oil spills, opening secret doors, and handling scripted events like triggering a pig’s movement and death animation.  
  - Enabled complex state changes in the game world based on player actions (e.g., picking up and placing the fire extinguisher, key interactions).

### **Sound and Audio**  
  - Added immersive sound effects such as footsteps, fire extinguisher usage, and background noises to enhance the tense atmosphere of the warehouse.  
  - Integrated multiple sound triggers to indicate events like the pig’s movements or the start of an important cutscene.
  - Incorporated voice lines that played at certain triggers to guide the player along through means of audio.

---

## Challenges Overcome  

### **Complex Interactions**  
  - **Issue**: Multiple items and interactions led to confusion and delayed progress.  
    - **Solution**: Developed clear player prompts and event-specific triggers that guide the player without overwhelming them.

### **Collision and Trigger Management**  
  - **Issue**: Collision detection for objects (like the pig and fire extinguisher) was difficult to manage with frequent changes to state.  
    - **Solution**: Created custom trigger events that track object interactions, ensuring that objects are activated and deactivated appropriately based on player actions.

### **Dynamic Gameplay Flow**  
  - **Issue**: Creating a smooth and continuous progression through puzzle sections with varied objectives was challenging.  
    - **Solution**: Implemented event-driven gameplay with triggers, such as unlocking doors, completing puzzles, and managing scripted events to keep the game flowing smoothly.

---

## Reflection  
Midnight Warehouse was a great exercise in managing a combination of environmental interaction, puzzle-solving, and tense atmospheric moments. By using triggers, custom events, and fluid player control, I was able to create a game that offers a mix of exploration and action, with each puzzle solving rewarding the player with new insights into the story and environment.

---

## Play the Game  
[Play Midnight Warehouse on Itch.io](https://aftertheraingames.itch.io/midnightwarehouse)
