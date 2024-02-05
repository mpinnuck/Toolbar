using System.Diagnostics;

namespace Toolbar
{
    public partial class ToolbarFrm : Form
    {
        const int DelayForSmoothPreview = 50; // Milliseconds

        private List<string> shortcuts = new List<string>();
        private string shortcutFolderPath = ""; // Variable for the shortcut folder path
        private bool isFormActivated = false; // Flag to track whether the form has been activated
        public ToolbarFrm(string shortcutFolderPath)
        {
            InitializeComponent();

            this.shortcutFolderPath = shortcutFolderPath;
            this.Text = Path.GetFileNameWithoutExtension(shortcutFolderPath);
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Activated += ToolbarApp_Click;
            listBox.Click += ListBox_Click;
            this.Shown += ToolbarFrm_Shown; // Handle the Shown event
            this.MouseEnter += ToolbarFrm_MouseEnter; // Handle the MouseEnter event

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
            CalculateFormSize();
            this.Visible = true;
         }

        private void LoadShortcuts()
        {
            if (Directory.Exists(shortcutFolderPath))
            {
                shortcuts.AddRange(Directory.GetFiles(shortcutFolderPath));
            }
        }

        private async void DelayAndMinimize()
        {
            await Task.Delay(DelayForSmoothPreview);
            this.WindowState = FormWindowState.Minimized;
            this.Visible = true; // make form visible so taskbar preview will show items before first click from user
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!isFormActivated)
            {
                this.Visible = false;       // hide form while drawn for the first time
                isFormActivated = true;
                this.WindowState = FormWindowState.Normal; // Optional preview display
                DelayAndMinimize();
            }
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.WindowState = FormWindowState.Minimized;
        }

        // Method to calculate and set the form size based on ListBox items
        private void CalculateFormSize()
        {
            int listBoxHeight = listBox.ItemHeight * (listBox.Items.Count + 1);
            int marginAboveTaskbar = 40;
            int taskbarY = Cursor.Position.Y - listBoxHeight - marginAboveTaskbar;
            taskbarY = Math.Max(taskbarY, 0);

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

            listBox.Width = maxItemWidth;
            this.ClientSize = new System.Drawing.Size(listBox.Width + 5, listBoxHeight);
            listBox.Focus();
        }

        private void ToolbarFrm_Shown(object? sender, EventArgs e)
        {
            // Check if it's the first time the form is shown
            if (!isFormActivated)
            {
                // Calculate and set the form size when it is about to be shown for the first time
                CalculateFormSize();

                // Update the flag to indicate that the form has been activated
                isFormActivated = true;
            }
        }

        private void ToolbarFrm_MouseEnter(object? sender, EventArgs e)
        {
            // Check if the form has been activated, if not, show the preview
            if (!isFormActivated)
            {
                // Calculate and set the form size when the mouse enters for the first time
                CalculateFormSize();

                // Update the flag to indicate that the form has been activated
                isFormActivated = true;
            }
        }

        private void ToolbarApp_Click(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Activate();

            // Calculate and set the form size based on ListBox items
            CalculateFormSize();
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
