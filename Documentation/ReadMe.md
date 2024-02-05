# Quick Launch Toolbar

## Description

This Windows Forms application creates a customizable toolbar that provides quick access to frequently used shortcuts. It offers the following features:

Customizable Shortcut Folder: Users can specify a folder containing their desired shortcuts (executables, files, folders, etc.).
Compact and Intuitive Interface: The toolbar displays a list of shortcuts in a compact window, minimizing screen clutter.
Seamless Taskbar Integration: The toolbar minimizes to the taskbar and can be activated by clicking its icon or using the keyboard shortcut (Win + T).
Smooth Preview and Activation: The toolbar employs techniques to ensure a seamless preview experience, avoiding unwanted flickering or flashing.
Adaptive Form Size: The toolbar dynamically adjusts its size to accommodate the number of shortcuts, ensuring optimal visibility.
Mouse-Driven Navigation: Users can easily select shortcuts using the mouse or keyboard arrows.
Reliable File Execution: The toolbar seamlessly launches selected shortcuts using the ShellExecute method.
## Utilization

1. Requirements:

.NET Framework 4.5 or later
Windows 10 or later
2. Usage:

Building: Compile the C# code using Visual Studio.
Running: Execute the compiled executable (Toolbar.exe).
Specifying Shortcut Folder:
Pass the path to your shortcut folder as a command-line argument when launching the application, e.g., Toolbar.exe "C:\MyShortcuts".
Alternatively, modify the shortcutFolderPath variable within the code to point to your desired folder.
Typically create a shortcut add your toolbar folder as the first argument add the shortcut to your start up launch files and pin to the taskbar.
3. Interaction:

Activate Toolbar: Click the toolbar icon in the taskbar or press Win + T.
Select Shortcut: Use the mouse or arrow keys to navigate the list and press Enter to launch the selected shortcut.
Minimize Toolbar: Click outside the toolbar or press Esc.
## Additional Information:

The toolbar remains minimized to the taskbar, ready for quick access.
It dynamically updates the shortcut list if changes are made to the shortcut folder.
Consider pinning the toolbar icon to the taskbar for easier access.
