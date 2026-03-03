# Task Tracker CLI

A simple CLI application to manage the tasks. It has following functions:

- **Add new task:** add "Buy groceries"
- **Update task:** update 1 "Buy groceries and cook dinner"
- **Delete task**
- **List all tasks**
- **List all done tasks**
- **List all in-progress tasks**
- **List all todo tasks**
- **Mark a task as in-progress**
- **Mark a task as done**

## How to run?

```
git clone https://github.com/VuongNguyen2393/TaskTracker.git


dotnet run
```

## Usage Example

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
