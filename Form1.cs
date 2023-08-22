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

            userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DarkSouls3");
            btnDuplicate.Visible = false;
            lvFileList.Columns.Add("File Name", 150);
            lvFileList.Columns.Add("Last Modified", 150);

            lvTargetFiles.Columns.Add("File Name", 150);
            lvTargetFiles.Columns.Add("Last Modified", 150);

            PopulateSaveFileList();
            PopulateTargetFileList(userDirectory);
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
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    lvFileList.Items.Add(item);
                }
            }
        }
        private void PopulateTargetFileList(string directoryPath)
        {
            lvTargetFiles.Items.Clear(); // 기존 아이템 삭제
            try
            {
                string[] targetFiles = Directory.GetFiles(directoryPath); //오류 
                if (Directory.Exists(directoryPath))
                {
                    foreach (string filePath in targetFiles)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);

                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        lvTargetFiles.Items.Add(item);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("The 'DarkSouls3' directory does not exist.\nYou should execute DarkSouls3 at least once", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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

                string targetFileName = "DS30000.txt";
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
    }
}
