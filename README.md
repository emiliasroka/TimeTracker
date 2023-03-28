# TimeTracker

## Main window
When launching the application, a main window appears with a motivational quote displayed in the center, which changes every time the application is launched. Below the quote, there are three buttons, each with a short note underneath and referring to a different part of the application:
* [To do list](#to-do-list)
* [Pomodoro Timer](#pomodoro-timer)
* [Notepad](#notepad)
By clicking on one of them, the user is taken to the corresponding window.

## To do list

The "To-Do List" is where tasks are added and tracked. Clicking on the "To-Do List" button opens a window where users can add tasks to be completed. At the bottom of the window, users can enter the task's details and add it by clicking the "ADD" button or pressing the "Enter" key. The added tasks are displayed in the center of the window.

To mark a task as done, users can hover over it and double-click. This action will prompt a window to appear, asking if the task has been completed. If yes, clicking "Yes" will display the completion date next to the task, if no, clicking "No" will keep the task on the list.

Right-clicking on a task displays a menu with three options:
* "Edit" - clicking on this option displays a window with the current task, which can be edited. Clicking "OK" or pressing "Enter" saves the changes. To exit the window, users can press the "Cancel" button.
* "Done" - clicking on this option prompts the same window that appears after double-clicking on a task.
* "Delete" - clicking on this option prompts a window asking if the selected task should be deleted. Users can choose "yes" or "no" depending on what they want to do.

At the bottom of the "To-Do List" window, next to the "ADD" button, there is an option button. When this option is selected, completed tasks are displayed on the list along with those that still need to be completed. When the option is deselected, only the tasks that need to be completed are displayed.

At the top of the "To-Do List" window, there are three buttons that allow users to minimize, maximize, and close the window. When the window is closed, the data is saved, and users are taken back to the main window of the application.

## Pomodoro Timer

The Pomodoro app is a time management tool based on the Pomodoro technique. It involves dividing work time into short intervals (usually 25 minutes), followed by a short break (about 5 minutes), and then after three such cycles, a longer break. This method helps to focus on the task for a short period and then take a break before continuing work. It can be used for various purposes, such as studying, writing, working, or designing.

The Pomodoro app allows the user to customize the length of work intervals and breaks in the settings. Additionally, it automatically monitors the number of Pomodoro cycles, short and long breaks. Furthermore, after completing each cycle, the user is notified with a sound about the timer's countdown completion.

To use the program correctly, we can first adjust the length of our work intervals and breaks in the settings (although it is recommended to use the default settings to make the most of the method). Then, by clicking the START button, we can concentrate on our work and start the cycle.

User instructions:
* The START button starts our Pomodoro session with default timer values (i.e., Pomodoro - 25 min, short break - 5 min, long break - 15 min).
* The STOP button stops the timer countdown.
* The SKIP button allows us to skip the timer countdown.
* The RESET button resets the current timer.
* The POMODORO button immediately goes to the Pomodoro cycle.
* The SHORT BREAK button goes to a short break.
* The LONG BREAK button goes to a long break.
* The settings icon allows us to adjust the length of the Pomodoro cycle, short and long breaks. We accept the settings by clicking the SAVE button in the settings window.


## Notepad

The Notepad application allows you to create your own notes. A note has a title and content, and is saved in a list (notebook) that is automatically sorted alphabetically by title, making it easier to find a particular note.

In the main window, you can add a note using the "ADD" button. After entering the title and content and clicking the "SAVE" button, the note is added and displayed in the list. If the user does not provide a title or content for the note, a message appears indicating that these values cannot be empty.

To display a note, you must select an item from the list, which causes its content to appear in the main window. To edit a note, you must select an item from the list and click the "EDIT" button, then proceed as with adding a note. The application also allows you to delete notes using the "DELETE" button.
