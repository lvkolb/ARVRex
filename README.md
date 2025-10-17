# UnityProjectsUXDM - Team Git Workflow

This repository contains multiple Unity assignments. This guide explains how to collaborate safely using Git, either via **command-line** or **GitHub Desktop / App**.

---

## 🗂 Repository Structure

UnityProjectsUXDM/ 
├── Assignment 1/ 
│ ├── Assets/ 
│ ├── Packages/ 
│ └── ProjectSettings/ 
├── Assignment 2/ 
├── .gitignore 
└── README.md


- Each `Assignment` folder is a separate Unity project.
- `.gitignore` ensures Unity-generated folders (`Library/`, `Temp/`, `Logs/`, `Obj/`, `Build/`) are not tracked.
- Unity `.meta` files are included — **do not remove them**.

---

## 🗺 Branching Workflow

This diagram illustrates how your individual feature work flows back into the main development branch.

```mermaid
gitGraph
    commit id: "Initial Commit"
    branch assignment1-dev
    checkout assignment1-dev
    commit id: "A1 Setup"
    branch feature/my-new-ui
    checkout feature/my-new-ui
    commit id: "UI Changes"
    commit id: "UI Fixes"
    checkout assignment1-dev
    merge feature/my-new-ui tag: "Feature Merged"
    branch feature/enemy-ai
    checkout feature/enemy-ai
    commit id: "AI Logic"
    checkout assignment1-dev
    merge feature/enemy-ai tag: "AI Merged"
    checkout main
    merge assignment1-dev tag: "A1 Submitted"
Branch	Purpose
main	Stable, submitted versions
assignment1-dev	Development for Assignment 1
feature/...	Individual work/experiments

🌱 Initial Setup
1. Clone the repo

Command-line:

Bash
git clone [https://github.com/lvkolb/UnityProjectsUXDM.git](https://github.com/lvkolb/UnityProjectsUXDM.git)
cd UnityProjectsUXDM
GitHub Desktop / App: Open the app. File → Clone repository → Paste the URL https://github.com/lvkolb/UnityProjectsUXDM.git. Choose a local path.

2. Switch to your development branch

Command-line:

Bash
git checkout assignment1-dev
GitHub Desktop / App: In the branch dropdown, select assignment1-dev. If the branch does not exist locally, fetch from origin first.

🌿 Daily Workflow (Command-Line)
Update your branch

Bash
git checkout assignment1-dev
git pull origin assignment1-dev
Create a feature branch for your work

Bash
git checkout -b feature/<your-feature-name>
Work in Unity Save scenes, prefabs, scripts, and materials. Unity automatically regenerates Library/ and other temporary files (ignored by Git).

Stage and commit your changes

Bash
git add .
git commit -m "Describe your changes here"
Push your branch to GitHub

Bash
git push -u origin feature/<your-feature-name>
Open a Pull Request on GitHub Merge feature branch into assignment1-dev when reviewed.

🌿 Daily Workflow (GitHub Desktop / App)
Open the repo in the app.

Switch to assignment1-dev branch.

Click Fetch origin to get latest changes.

Create a new branch for your feature (e.g., feature/my-ui-fix).

Work in Unity and save changes.

In GitHub Desktop: Stage all changes → Commit with a meaningful message.

Publish Branch (if necessary) → Push to remote → Create Pull Request on GitHub.

⚡ Unity-Specific Notes
Always include .meta files — Unity relies on them.

Do not commit the following folders (they should be in .gitignore):

Library/

Temp/

Logs/

Obj/

Build/ or Builds/

Enable text serialization in Unity for merge-friendly scenes and prefabs:

Edit → Project Settings → Editor → Asset Serialization → Force Text

Version Control → Visible Meta Files

Optional: Consider using Git LFS for very large assets like .fbx, .wav, or .psd.

🧠 Team Tips
Pull from assignment1-dev before starting work.

Commit often with clear messages.

Feature branches help avoid conflicts.

Only merge into assignment1-dev after review.

Avoid committing Library and other temporary files.

📝 Quick Git Command Cheat Sheet
Task	Command-Line
Clone repo	git clone <repo-url>
Switch branch	git checkout <branch>
Create new branch	git checkout -b <branch>
Pull latest changes	git pull origin <branch>
Stage changes	git add .
Commit changes	git commit -m "message"
Push branch	git push -u origin <branch>
Check status	git status
