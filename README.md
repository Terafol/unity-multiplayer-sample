 Unity Multiplayer Music Platform - Code Sample

Code sample from a collaborative music creation platform developed during my internship at Emotionwave (Seoul, South Korea, Summer 2024).

**Note:** This repository contains only parts of the code I personally wrote. The full project source code is proprietary to Emotionwave and cannot be shared.

---

## Project Overview

Real-time 3D multiplayer music creation platform built in Unity with C#. The system enables multiple users to collaborate on procedural music generation in a shared virtual environment.

**Duration:** 3 months (July - September 2024)  
**Role:** R&D Engineer Intern  
**First Unity project** - learned the engine while building this system

The code shown is taken from the internal beta build released in September 2024

---

## Technical Highlights

### 1. Mobile Lobby System

Built a mobile-first lobby experience with automatic connection and social features.

**Architecture Decision:**  
All UI text strings are externalized in JSON localization files, making language switching instant and allowing non-developers to add translations without touching code.
I came up with this idea because the API links were constantly changing at that time and looking in each files if the link was still correct was time consumming.

### 2. Multiplayer Core

Real-time networking implementation using Photon Unity Networking.

**Key Features:**
- Room creation and matchmaking
- Player state synchronization
- Network event handling
- Latency compensation

**Challenge:**  
Reverse-engineered existing multiplayer foundation and understand how to share the interaction among each players.

### 3. Procedural Music System

Fixed and enhanced the procedural music generation system.

**What I Did:**
- Debugged existing music generation asset
- Synchronized music state across network
- Integrated with Photon for real-time collaboration

### 4. Configuration System

JSON-based modular configuration for all runtime parameters.

**Benefits:**
- Environment switching (dev/staging/production)
- Designer-friendly parameter tuning
- No recompilation for config changes
- Localization without code changes
---

## What I Learned

Building this as my first Unity project taught me:
- **Architecture first:** Planning extensible systems from the start pays off
- **Network programming:** State synchronization and latency handling
- **Internationalization:** Designing for multi-language from day one
- **Debugging:** Reverse-engineering and fixing existing complex systems
- **Internationnal collaboration:** Working with people from a different country understanding how to share ideas with different communication styles

The modular configuration approach I implemented here has influenced how I design systems in subsequent projects.

---

## Context

This code represents my contribution to a larger collaborative project.
---

**Contact:** louplangard@gmail.com  
**LinkedIn:** [in/loup-langard](https://linkedin.com/in/loup-langard)
