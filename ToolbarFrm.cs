namespace Toolbar
{
    public partial class ToolbarFrm : Form
    {
        private List<string> shortcuts = new List<string>();
        private string shortcutFolderPath = ""; // Variable for the shortcut folder path
     
        public ToolbarFrm(string shortcutFolderPath)
        {
            InitializeComponent();

            this.shortcutFolderPath = shortcutFolderPath;
            this.Text = Path.GetFileNameWithoutExtension(shortcutFolderPath);
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = true;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Activated += ToolbarApp_Click;
            listBox.Click += ListBox_Click;
            listBox.MouseMove += ListBox_MouseMove; // Handle the MouseMove event
            listBox.BackColor = Color.FromArgb(226, 239, 254); // Set the background color
            listBox.BorderStyle = BorderStyle.Fixed3D; // Set the border style

            this.Controls.Add(listBox);

            this.LoadShortcuts();
            listBox.Items.Clear();
            foreach (string shortcut in shortcuts)
            {
                listBox.Items.Add(Path.GetFileNameWithoutExtension(shortcut));
            }
         }

private void LoadShortcuts()
        {
            if (Directory.Exists(shortcutFolderPath))
            {
                shortcuts.AddRange(Directory.GetFiles(shortcutFolderPath));
            }
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.WindowState = FormWindowState.Minimized;
        }

        private void ToolbarApp_Click(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Activate();

            // Calculate the required height based on the number of items in the ListBox
            int listBoxHeight = listBox.ItemHeight * (listBox.Items.Count+1);

            // Set a margin above the taskbar
            int marginAboveTaskbar = 40;

            // Calculate the Y position, ensuring it's high enough above the taskbar
            int taskbarY = Cursor.Position.Y - listBoxHeight - marginAboveTaskbar;

            if (taskbarY < 0)
            {
                // If the calculated Y position is negative, adjust it to 0
                taskbarY = 0;
            }

            this.Location = new System.Drawing.Point(Cursor.Position.X, taskbarY);
            // Set the form size based on the ListBox size
            this.listBox.Height = listBoxHeight;
            
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

            // Set the ListBox width based on the maximum item width
            listBox.Width = maxItemWidth;

            // Calculate the form size based on the ListBox size
            this.ClientSize = new System.Drawing.Size(listBox.Width+5, listBoxHeight);
            listBox.Focus();



        }

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
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
            }
        }

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
}
