using System;
using System.IO;
using System.Windows.Forms;

namespace souls_Save_Manager
{
    public partial class Form1 : Form
    {
        private string userDirectory;
        private string targetFileName;

        public Form1()
        {
            InitializeComponent();

            //userDirectory = GetUserDirectory("DarkSoulsIII");
            //SetTargetFileName("DS30000.sl2");
            btnDuplicate.Visible = false;
            btnFind.Visible = false;
            btnQuickSwap.Visible = false;
            InitializeListViewColumns();
            //PopulateSaveFileList();
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

        private string GetSaveDirectory(string gameName)
        {
            try
            {
                string savePath = Path.Combine(Application.StartupPath, gameName + "_Save");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                return savePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting save directory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //일단 save 파일 버튼 클릭마다 변경되게 해야하고
        private void PopulateSaveFileList()
        {
            lvFileList.Items.Clear();

            string gameSaveFolder = GetSaveDirectory(GetUserGameName(userDirectory));
            if (Directory.Exists(gameSaveFolder))
            {
                string[] files = Directory.GetFiles(gameSaveFolder);

                foreach (string filePath in files)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    AddListViewItem(lvFileList, fileInfo);
                }
            }
        }

        private void SwapSelectedFileWithTarget()
        {
            try
            {
                
                if (GetUserGameName(userDirectory) == null)
                { 
                    MessageBox.Show("The directory does not exist.\nYou should execute the game at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                string selectedFileName = lvFileList.SelectedItems[0].Text; //save폴더에서 내생각에 path라서 문제인가. file자체가아니라
                string selectedFilePath = Path.Combine(GetSaveDirectory(GetUserGameName(userDirectory)), selectedFileName);
                string targetFile = Path.Combine(userDirectory, targetFileName); // 게임폴더

                Console.WriteLine("Selected File Path: " + selectedFilePath);
                Console.WriteLine("Target File Path: " + targetFile);

                if (File.Exists(targetFile))
                {
                    string newFileName = "swapped_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(targetFile);//어차피 sl2라서 그냥 해도됨 sl2
                    string saveDirectory = GetSaveDirectory(GetUserGameName(userDirectory)); // 게임별 저장 디렉토리 가져오기
                    string backupPath = Path.Combine(saveDirectory, newFileName);

                    File.Copy(targetFile, backupPath); //그래 이건 ok
                    File.Copy(selectedFilePath, targetFile, true); //이게 문제일지도.

                    MessageBox.Show("File swapped successfully.", "Swap Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Error during swapping files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            lvTargetFiles.Items.Clear();
            lvFileList.Items.Clear();
            try
            {
                if (!Directory.Exists(userDirectory))
                {
                    btnQuickSwap.Visible = false;
                    btnDuplicate.Visible = false;
                    MessageBox.Show("The directory does not exist.\nYou should execute the game at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
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
                    btnQuickSwap.Visible = true;
                    PopulateSaveFileList(); //여기서 찾아서 그런듯. 
                }
                else
                {
                    txtFilePath.Text = "File not found.";
                    PopulateSaveFileList();
                }
            }
            catch (Exception ex)
            {
                txtFilePath.Text = "File not found.";  //txtFilePath.Text = "Error: " + ex.Message;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = txtFilePath.Text.Split('\n')[1].Trim();
                string gameSaveFolder = GetSaveDirectory(GetUserGameName(userDirectory));
                string newFileName = Path.GetFileNameWithoutExtension(sourceFile) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sourceFile);
                string targetFile = Path.Combine(gameSaveFolder, newFileName);
 
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

                string targetFile = Path.Combine(userDirectory, targetFileName);//외부 내부 차이? 생각 아니 if를 없애도 되었나?
                string gameBackupFolder = GetSaveDirectory(GetUserGameName(userDirectory) + "_Backup");

                string newFileName = "QS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sl2";
                string backupPath = Path.Combine(gameBackupFolder, newFileName);

                File.Copy(targetFile, backupPath);
                File.Copy(mostRecentSave, targetFile, true);

                MessageBox.Show("Quick swap completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateTargetFileList(userDirectory);
                PopulateSaveFileList();             
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error during quick swap: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (lvFileList.SelectedItems.Count > 0)
            {
                SwapSelectedFileWithTarget();
                PopulateSaveFileList();
                PopulateTargetFileList(userDirectory);
            }
            else
            {
                MessageBox.Show("Please select a file to swap.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private string GetMostRecentSaveFile()
        {   //user directory 변화 / getusergamename 으로 얻고 / startup으로 가서 겜폴더 찾기
            string gameSaveFolder = GetSaveDirectory(GetUserGameName(userDirectory)); 
            string[] saveFiles = Directory.GetFiles(gameSaveFolder, "*.sl2");

            if (saveFiles.Length > 0)
            {
                Array.Sort(saveFiles, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));
                return saveFiles[0];
            }

            return null;
        }

        private string GetUserGameName(string directory) //예외설정 필요. 없는데 duplicate 하기/ select swap 하기 + 없는데 QS하기.
        {


            if (directory.Contains("DarkSoulsIII"))
            {
                return "DarkSoulsIII";
            }
            else if (directory.Contains("Sekiro"))
            {
                return "Sekiro";
            }
            else if (directory.Contains("EldenRing"))
            {
                return "EldenRing";
            }
            return "UnknownGame";
        }
        private void btnGame1_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("DarkSoulsIII");
            SetTargetFileName("DS30000.sl2");
            string gameSaveFolder = GetSaveDirectory("DarkSoulsIII");
            btnFind_Click(sender, e);
        }

        private void btnGame2_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("Sekiro");
            SetTargetFileName("S0000.sl2");
            string gameSaveFolder = GetSaveDirectory("Sekiro");
            btnFind_Click(sender, e);
        }

        private void btnGame3_Click(object sender, EventArgs e)
        {
            userDirectory = GetUserDirectory("EldenRing");
            SetTargetFileName("ER0000.sl2");
            string gameSaveFolder = GetSaveDirectory("EldenRing");
            btnFind_Click(sender, e);
        }
    }
}

