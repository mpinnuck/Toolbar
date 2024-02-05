# Quick Launch Toolbar

## Description

This Windows Forms application creates a customizable toolbar that provides quick access to frequently used shortcuts.

**Features:**

- **Customizable Shortcut Folder:** Specify a folder containing your desired shortcuts (executables, files, folders, etc.).
- **Compact and Intuitive Interface:** Displays shortcuts in a compact window, minimizing screen clutter.
- **Seamless Taskbar Integration:** Minimizes to the taskbar and can be activated by clicking its icon or using the keyboard shortcut (Win + T).
- **Smooth Preview and Activation:** Ensures a seamless preview experience, avoiding unwanted flickering or flashing.
- **Adaptive Form Size:** Dynamically adjusts its size to accommodate the number of shortcuts, ensuring optimal visibility.
- **Mouse-Driven Navigation:** Easily select shortcuts using the mouse or keyboard arrows.
- **Reliable File Execution:** Seamlessly launches selected shortcuts using the ShellExecute method.

## Utilization

**Requirements:**

- .NET Framework 4.5 or later
- Windows 10 or later

**Usage:**

1. **Building:** Compile the C# code using Visual Studio.
2. **Running:** Execute the compiled executable (Toolbar.exe).
3. **Specifying Shortcut Folder:**
   - Pass the path to your shortcut folder as a command-line argument, e.g., `Toolbar.exe "C:\MyShortcuts"`.
   - Alternatively, modify the `shortcutFolderPath` variable within the code.
4. **Typical Setup:**
   - Create a shortcut for Toolbar.exe, adding your shortcut folder as the first argument.
   - Add the shortcut to your startup launch files.
   - Pin the shortcut to the taskbar.

**Interaction:**

- **Activate Toolbar:** Click the toolbar icon in the taskbar or press Win + T.
- **Select Shortcut:** Use the mouse or arrow keys to navigate the list and press Enter to launch the selected shortcut.
- **Minimize Toolbar:** Click outside the toolbar or press Esc.

**Additional Information:**

- The toolbar remains minimized to the taskbar for quick access.
- It dynamically updates the shortcut list if changes are made to the shortcut folder.
- Consider pinning the toolbar icon to the taskbar for easier access.
