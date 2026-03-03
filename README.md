# Task Tracker CLI

Task Tracker is a simple Command Line Interface (CLI) application built with C# / .NET that helps you manage daily tasks. You can create tasks, update them, change their status, delete them, and view all tasks.
This project is implemented following the specification from roadmap.sh.

## Features:

➕ Add a new task

✏️ Update an existing task

🔄 Change task status
(todo → in-progress → done)

❌ Delete a task

📋 List all tasks

💾 Persist data using a JSON file

## Installation & Setup

### Requirement

- .NET SDK 6.0+
- Windows / macOS / Linux

### Clone the project

```
git clone https://github.com/VuongNguyen2393/TaskTracker

cd TaskTracker
```

### Build & Run

```
dotnet build
dotnet run
```

## Usage Guide

```
# Add new task
task-cli add "Learn JavaScript"

# Update a task
task-cli update 1 "Learn Advanced JavaScript"

# Delete
task-cli delete 1

# Mark task
task-cli mark-in-progress 2
task-cli mark-done 2

# List all tasks
task-cli list

# List by status
task-cli list todo
```
