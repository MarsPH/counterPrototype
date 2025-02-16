# Rocket Interceptor  

[![Unity](https://img.shields.io/badge/Made%20With-Unity-FF5733)](https://unity.com/)  

---  

## 🎮 Overview  

**Rocket Interceptor** is a Unity prototype developed after completing a **junior programming course on Unity Learn**. In this game, the player must **destroy incoming rockets**—each with unique behaviors—while surviving increasingly challenging waves. The project was created to **test and improve 3D math skills** by implementing realistic **rocket mechanics** without relying on visual effects.  

<p align="center">
  <img src="https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExMGx6bmE3ZDRkM3pldGp1ejM5a2tvenBhNjE4b2YwY2Z4eXZ3MmttdCZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/Kg4QKzJ8WjVGFkGRdr/giphy.gif" alt="Rocket Interceptor Gameplay Preview">
</p>

---  

## 🧩 Key Components  

- **🎯 Artillery.cs**  
  - Manages rapid-fire shooting with **heating and cooldown mechanics**.  
  - Calculates **projectile trajectories** based on camera input and target positions.  

- **💥 ArtilleryBullet.cs**  
  - Implements bullet behavior, including **explosion detection** and damage application within an explosion radius.  

- **🚀 BaseRocket.cs**  
  - Serves as the foundation for **enemy rockets**.  
  - Handles **acceleration, steering, curved flight paths, and damage processing**.  

- **🎥 CameraSwitcher.cs & CamMovement.cs**  
  - Enable **dynamic camera switching** and **edge-based rotation/zooming** for varied gameplay perspectives.  

- **📊 GameManager.cs & Counter.cs**  
  - Oversee **wave progression, score tracking, and health management**.  
  - Dynamically spawn **rockets with increasing difficulty**.  

- **🔫 Gunner.cs & InterceptionLaser.cs**  
  - Provide **offensive capabilities** with multiple missile types (**auto-tracking, manual, multi-target**) and a **laser that destroys rockets**.  

- **🛰️ DroneLauncher.cs & HypersonicLauncher.cs**  
  - Randomly spawn additional **rocket variants** to diversify challenges.  

---  

## 📌 Installation & Running the Game  

```bash
git clone https://github.com/your-username/rocket-interceptor.git
cd rocket-interceptor
```
Run in Unity **2022.x or newer**.

---  

## 🏆 Contributors  
- **[Mahan](https://github.com/MarsPH)** – Developer  

--- 

## Architecture Diagram

```mermaid
graph TD;
    A[Player Input] -->|Controls| B(Gunner.cs)
    B -->|Fires| C(Missile & Laser Systems)
    C --> D(BaseRocket.cs)
    D -->|Movement & Damage| E(GameManager.cs)
    E --> F[Score & Wave Tracking]
    G[Artillery.cs] -->|Rapid Fire| H(ArtilleryBullet.cs)
    I[Camera Input] --> J(CameraSwitcher.cs & CamMovement.cs)
