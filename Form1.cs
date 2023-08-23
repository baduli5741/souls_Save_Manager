using System;
using System.IO;
using System.Windows.Forms;

namespace darksouls3_Save_Manager
{
    public partial class Form1 : Form
    {
        private string userDirectory;
        private string targetFileName;

        public Form1()
        {
            InitializeComponent();

            userDirectory = GetUserDirectory("DarkSoulsIII");
            SetTargetFileName("DS30000.sl2");
            btnDuplicate.Visible = false;
            InitializeListViewColumns();
            PopulateSaveFileList();
        }

        private void InitializeListViewColumns()
        {
            lvFileList.Columns.Add("File Name", 150);
            lvFileList.Columns.Add("Last Modified", 150);

            lvTargetFiles.Columns.Add("File Name", 150);
            lvTargetFiles.Columns.Add("Last Modified", 150);
        }

        private void AddListViewItem(ListView listView, FileInfo fileInfo)
        {
            ListViewItem item = new ListViewItem(fileInfo.Name);
            item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
            listView.Items.Add(item);
        }

        private string GetUserDirectory(string gameName)
        {
            try
            {
                string roamingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName);
                if (Directory.Exists(roamingPath))
                {
                    string[] subDirectories = Directory.GetDirectories(roamingPath);
                    if (subDirectories.Length > 0)
                    {
                        return subDirectories[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting user directory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private void SetTargetFileName(string fileName)
        {
            targetFileName = fileName;
        }

        private void PopulateTargetFileList(string directoryPath)
        {
            lvTargetFiles.Items.Clear(); // 기존 아이템 삭제
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] targetFiles = Directory.GetFiles(directoryPath);
                    foreach (string filePath in targetFiles)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        AddListViewItem(lvTargetFiles, fileInfo);
                    }
                }
                else
                {
                    MessageBox.Show("The directory does not exist.\nYou should execute the game at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating target file list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateSaveFileList()
        {
            lvFileList.Items.Clear();

            string saveDirectory = Path.Combine(Application.StartupPath, "save");

            if (Directory.Exists(saveDirectory))
            {
                string[] files = Directory.GetFiles(saveDirectory);

                foreach (string filePath in files)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    AddListViewItem(lvFileList, fileInfo);
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(userDirectory))
                {
                    MessageBox.Show("The directory does not exist.\nYou should execute the game at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string[] files = Directory.GetFiles(userDirectory, targetFileName, SearchOption.AllDirectories);

                if (files.Length > 0)
                {
                    txtFilePath.Text = "File found at:";
                    foreach (string filePath in files)
                    {
                        txtFilePath.Text += Environment.NewLine + filePath;
                    }

                    PopulateTargetFileList(userDirectory);
                    btnDuplicate.Visible = true;
                    PopulateSaveFileList();
                }
                else
                {
                    txtFilePath.Text = "File not found.";
                }
            }
            catch (Exception ex)
            {
                txtFilePath.Text = "Error: " + ex.Message;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = txtFilePath.Text.Split('\n')[1].Trim();
                string targetDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                string newFileName = Path.GetFileNameWithoutExtension(sourceFile) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sourceFile);
                string targetFile = Path.Combine(targetDirectory, "save", newFileName);

                Directory.CreateDirectory(Path.Combine(targetDirectory, "save"));
                File.Copy(sourceFile, targetFile);

                MessageBox.Show("File duplicated successfully.", "Duplicate Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateSaveFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error duplicating file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuickSwap_Click(object sender, EventArgs e)
        {
            try
            {
                string mostRecentSave = GetMostRecentSaveFile();

                if (mostRecentSave == null)
                {
                    MessageBox.Show("No save files found in the 'save' directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string targetFile = Path.Combine(userDirectory, targetFileName);

                if (File.Exists(targetFile))
                {
                    string newFileName = "swapped_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sl2";
                    string backupPath = Path.Combine(Application.StartupPath, "save", newFileName);

                    File.Copy(targetFile, backupPath);
                    File.Copy(mostRecentSave, targetFile, true);

                    MessageBox.Show("Quick swap completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateTargetFileList(userDirectory);
                    PopulateSaveFileList();
                }
                else
                {
                    MessageBox.Show("Target file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during quick swap: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMostRecentSaveFile()
        {
            string saveDirectory = Path.Combine(Application.StartupPath, "save");
            string[] saveFiles = Directory.GetFiles(saveDirectory, "*.sl2");

            if (saveFiles.Length > 0)
            {
                Array.Sort(saveFiles, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));
                return saveFiles[0];
            }

            return null;
        }

        private void btnGame1_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("DarkSoulsIII");
            SetTargetFileName("DS30000.sl2");
        }

        private void btnGame2_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("Sekiro");
            SetTargetFileName("S0000.sl2");
        }

        private void btnGame3_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("EldenRing");
            SetTargetFileName("ER0000.sl2");
        }
    }
}

