namespace TransferX_GUI
{
    public partial class Main : Form
    {
        private TreeView treeViewDirectories;
        private TreeView treeViewFiles;
        public Main()
        {
            InitializeComponent();
            treeViewDirectories = new TreeView { Dock = DockStyle.Left, Width = 300 };
            treeViewFiles = new TreeView { Dock = DockStyle.Right, Width = 300 };
            this.Controls.Add(treeViewDirectories);
            this.Controls.Add(treeViewFiles);
            LoadRootDirectories();

            // Add event handler to populate the files when a directory is selected
            treeViewDirectories.AfterSelect += TreeViewDirectories_AfterSelect;
        }

        private void LoadRootDirectories()
        {
            try
            {
                // Get all the drives on the system (e.g., C:\, D:\)
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady) // Ensure the drive is ready
                    {
                        var rootNode = new TreeNode(drive.Name)
                        {
                            Tag = drive.Name // Save the full path in the Tag property
                        };

                        treeViewDirectories.Nodes.Add(rootNode);

                        // Optionally, load subdirectories immediately or wait until expanded
                        LoadSubDirectories(rootNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading drives: {ex.Message}");
            }
        }




    }
}
