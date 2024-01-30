namespace Toolbar
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Validate command line arguments
            // Validate command line arguments
            if (args.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Usage: MyToolbarApp.exe <shortcut_folder_path>");
                return;
            }
            string shortcutFolderPath = args[0];

            Application.Run(new ToolbarFrm(shortcutFolderPath));
        }
    }
}