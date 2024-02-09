using System.Runtime.InteropServices;

namespace Toolbar
{
    public partial class ToolbarFrm : Form
    {
        #region Constants
        static readonly Color BackGroundColor = Color.FromArgb(250, 250, 250);
        const int DelayForSmoothPreview = 50; // Milliseconds
        #endregion

        #region Members
        private List<string> shortcuts = new List<string>();
        private string shortcutFolderPath = ""; // Variable for the shortcut folder path
        private bool hasActivated = false; // Flag to track whether the form has been activated
        #endregion

        #region Public Methods
        //Methods
        public ToolbarFrm(string shortcutPath)
        {
            InitializeComponent();

            shortcutFolderPath = shortcutPath;
            Text = Path.GetFileNameWithoutExtension(shortcutFolderPath);
            FormBorderStyle = FormBorderStyle.None;
            // this stops the background erase flicker when the listbox pops up
            BackColor = Color.Beige;
            TransparencyKey = Color.Beige;

            Activated += ToolbarApp_Click;

            listBox.Click += ListBox_Click;
            listBox.MouseMove += ListBox_MouseMove; // Handle the MouseMove event
            listBox.BackColor = BackGroundColor; // Set the background color
            listBox.BorderStyle = BorderStyle.Fixed3D; // Set the border style

            Controls.Add(listBox);
            LoadList();
            Visible = true;
        }
        #endregion

        #region Private methods

        // Load the shortcuts and listbox from the shortcut folder
        private void LoadList()
        {
            LoadShortcuts();
            listBox.Items.Clear();
            foreach (string shortcut in shortcuts)
            {
                listBox.Items.Add(Path.GetFileNameWithoutExtension(shortcut));
            }
        }

        // load the listbox with shortcut file names no extension
        private void LoadShortcuts()
        {
            // empty shortcuts
            shortcuts.Clear();
            // load all shortcuts in the target folder
            if (Directory.Exists(shortcutFolderPath))
            {
                shortcuts.AddRange(Directory.GetFiles(shortcutFolderPath));
            }
        }

        // Once the form is drawn for the first time minimize it nd make it visible
        // the way the taskbar mouse hover preview will show the shortcuts in the listbox
        private async void DelayAndMinimize()
        {
            await Task.Delay(DelayForSmoothPreview);
            WindowState = FormWindowState.Minimized;
            Visible = true; // make form visible so taskbar preview will show items before first click from user
        }

        // if the form is being displayed for the first time
        // make invisble on draw a normal form, start a timer with a small delay allowing time for the form to paint.
        // this way taskbar previews will show a populated listbox before the app has been clicked on for the first time.
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (!hasActivated)
            {
                Visible = false;       // hide form while drawn for the first time
                hasActivated = true;
                WindowState = FormWindowState.Normal; // Optional preview display
                DelayAndMinimize();
            }
        }
        
        /// OnDeactive minimse the form to the taskbar
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            WindowState = FormWindowState.Minimized;
        }

         // Method to calculate and set the form size based on ListBox items
        private void CalculateFormSize()
        {
            int listBoxHeight = listBox.ItemHeight * (listBox.Items.Count + 1);
            int marginAboveTaskbar = 40;
            int taskbarY = Cursor.Position.Y - listBoxHeight - marginAboveTaskbar;
            taskbarY = Math.Max(taskbarY, 0);

            Location = new System.Drawing.Point(Cursor.Position.X, taskbarY);
            // Set the form size based on the ListBox size
            listBox.Height = listBoxHeight;

            // Calculate the maximum width of items in the ListBox
            int maxItemWidth = 0;
            foreach (var item in listBox.Items)
            {
                int itemWidth = TextRenderer.MeasureText(item.ToString(), listBox.Font).Width;
                if (itemWidth > maxItemWidth)
                {
                    maxItemWidth = itemWidth;
                }
            }

            listBox.Width = maxItemWidth;
            ClientSize = new System.Drawing.Size(listBox.Width + 5, listBoxHeight);
            listBox.Focus();
        }

        private void ToolbarApp_Click(object? sender, EventArgs e)
        {
            LoadList();
            // Calculate and set the form size based on ListBox items
            CalculateFormSize();
        }

        // execute the sslected shortcut
        private void ListBox_Click(object? sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender!;
            string? selectedFileName = listBox.SelectedItem?.ToString();
            listBox.SelectedIndex = -1;  // Clear the selected item

            if (!string.IsNullOrEmpty(selectedFileName))
            {
                // Find the corresponding full file path
                string? selectedFilePath = shortcuts.FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == selectedFileName);

                if (!string.IsNullOrEmpty(selectedFilePath))
                {
                    try
                    {
                        Shell32.Shell shell = new Shell32.Shell();
                        shell.ShellExecute(selectedFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error executing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                }
            }
        }
        // highlight shortcut as mouse passes over it
        private void ListBox_MouseMove(object? sender, MouseEventArgs e)
        {
            // Get the index of the item at the current mouse position
            int index = listBox.IndexFromPoint(e.Location);

            // Set the selected index if the mouse is over an item
            if (index >= 0 && index < listBox.Items.Count)
            {
                listBox.SelectedIndex = index;
            }
        }
    }
    #endregion
}
