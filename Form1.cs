using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace darksouls3_Save_Manager
{
    public partial class Form1 : Form
    {
        private string userDirectory;

        public Form1()
        {
            InitializeComponent();

            userDirectory = GetUserDirectory();
            btnDuplicate.Visible = false;
            InitializeListViewColumns();
            PopulateSaveFileList();
            PopulateTargetFileList(userDirectory);
        }

        private void InitializeListViewColumns()
        {
            lvFileList.Columns.Add("File Name", 150);
            lvFileList.Columns.Add("Last Modified", 150);

            lvTargetFiles.Columns.Add("File Name", 150);
            lvTargetFiles.Columns.Add("Last Modified", 150);
        }

        //first folder would be ur save file path. 이제 스왑 기능/스왑시 파일이름/어떤 파일과 스왑할지 -이건 어차피 기본이랑 바꾸겠지 옆에 걸 두번 클릭하면 그걸로 스왑? 스왑된 파일은 
       
        private void AddListViewItem(ListView listView,FileInfo fileInfo)
        {
            ListViewItem item = new ListViewItem(fileInfo.Name);
            item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
            listView.Items.Add(item); //바보 lvFileList가 아니라 listview에 넣어야지 근데 save쪽은 어떻게 보였던거지.
        }
        private string GetUserDirectory()
        {
            try
            {
                string roamingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DarkSoulsIII");
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
        private void PopulateTargetFileList(string directoryPath)
        {
            lvTargetFiles.Items.Clear(); // 기존 아이템 삭제
            try
            {
                
                if (Directory.Exists(directoryPath))
                {
                    string[] targetFiles = Directory.GetFiles(directoryPath); //오류 
                    foreach (string filePath in targetFiles)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        AddListViewItem (lvTargetFiles, fileInfo);
                    }
                }
                else
                {
                    MessageBox.Show("The 'DarkSouls3' directory does not exist.\nYou should execute DarkSouls3 at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error populating target file list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(userDirectory))
                {
                    MessageBox.Show("The 'DarkSouls3' directory does not exist.\nYou should execute DarkSouls3 at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string targetFileName = "DS30000.sl2";
                string[] files = Directory.GetFiles(userDirectory, targetFileName, SearchOption.AllDirectories);

                if (files.Length > 0)
                {
                    txtFilePath.Text = "File found at:";
                    foreach (string filePath in files)
                    {
                        txtFilePath.Text += Environment.NewLine + filePath;
                    }

                    PopulateTargetFileList(userDirectory);
                    btnDuplicate.Visible=true;
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
                string sourceFile = txtFilePath.Text.Split('\n')[1].Trim(); // 첫 번째 파일 경로 추출
                string targetDirectory = Path.GetDirectoryName(Application.ExecutablePath); // 실행 파일이 있는 폴더

                string newFileName = Path.GetFileNameWithoutExtension(sourceFile) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sourceFile);
                string targetFile = Path.Combine(targetDirectory, "save", newFileName); // 타겟 파일 경로 생성

                Directory.CreateDirectory(Path.Combine(targetDirectory, "save")); // save 폴더 생성
                File.Copy(sourceFile, targetFile); // 파일 복사

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

                string targetFile = Path.Combine(userDirectory, "DS30000.sl2");

                if (File.Exists(targetFile))
                {
                    string newFileName = "swapped_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sl2";
                    string backupPath = Path.Combine(Application.StartupPath, "save", newFileName);

                    File.Copy(targetFile, backupPath);
                    File.Copy(mostRecentSave, targetFile, true);

                    MessageBox.Show("Quick swap completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateTargetFileList(userDirectory);
                }
                else
                {
                    MessageBox.Show("Target file 'DS30000.sl2' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
