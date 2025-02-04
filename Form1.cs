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
            comboBoxGames.Items.Add("Dark Souls");
            comboBoxGames.Items.Add("DarkSoulsII");
            comboBoxGames.Items.Add("DarkSoulsIII");
            comboBoxGames.Items.Add("DARK SOULS REMASTERED");
            comboBoxGames.Items.Add("Sekiro");
            comboBoxGames.Items.Add("EldenRing");
            comboBoxGames.Items.Add("ArmoredCore6");
            comboBoxGames.Items.Add("WEBFISHING");
            //comboBoxGames.Items.Add("");
            btnDuplicate.Visible = false;
            btnQuickSwap.Visible = false;
            InitializeListViewColumns();
        }

        private void InitializeListViewColumns()
        {
            lvFileList.Columns.Add("File Name", 180);
            lvFileList.Columns.Add("Last Modified", 150);

            lvTargetFiles.Columns.Add("File Name", 180);
            lvTargetFiles.Columns.Add("Last Modified", 150);
        }

        private void AddListViewItem(ListView listView, FileInfo fileInfo)
        {
            ListViewItem item = new ListViewItem(fileInfo.Name);
            item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
            listView.Items.Add(item);
        }

        private string GetUserDirectory(string gameName)
        {
            try
            {
                if (gameName == "DARK SOULS REMASTERED" || gameName == "Dark Souls")
                {
                    // "NBGI" 폴더 안의 해당 게임 폴더 경로를 가져옵니다.
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string nbgiPath = Path.Combine(documentsPath, "NBGI");

                    if (Directory.Exists(nbgiPath))
                    {
                        string[] gameFolders = Directory.GetDirectories(nbgiPath, gameName, SearchOption.TopDirectoryOnly);

                        if (gameFolders.Length > 0)
                        {
                            // "gameName"에 해당하는 폴더 중 첫 번째 폴더 경로 반환
                            return gameFolders[0];
                        }
                    }
                }

                else if (gameName == "WEBFISHING") // WebFishing 경로 탐색 추가
                {
                    string godotPath = Path.Combine(Path.Combine(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Godot"),
                    "app_userdata"),
                    "webfishing_2_newver");


                    if (Directory.Exists(godotPath))
                    {
                        return godotPath;
                    }
                }
                else
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
                    string newFileName = "swapped_" + DateTime.Now.ToString("yyMMddHHmm") + Path.GetExtension(targetFile);//어차피 sl2라서 이젠 아니다... 
                    string saveDirectory = GetSaveDirectory(GetUserGameName(userDirectory)); // 게임별 저장 디렉토리 가져오기
                    string backupPath = Path.Combine(saveDirectory, newFileName);

                    File.Copy(targetFile, backupPath);
                    File.Copy(selectedFilePath, targetFile, true);

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
                    btnQuickSwap.Visible = false;
                    PopulateSaveFileList(); 
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
                // 선택된 파일 가져오기
                if (lvTargetFiles.SelectedItems.Count > 0)
                {
                    string selectedFileName = lvTargetFiles.SelectedItems[0].Text;
                    string sourceFile = Path.Combine(userDirectory, selectedFileName);

                    // 저장 디렉토리와 새 파일 이름 생성
                    string gameSaveFolder = GetSaveDirectory(GetUserGameName(userDirectory));
                    string newFileName = Path.GetFileNameWithoutExtension(sourceFile) + "_" + DateTime.Now.ToString("yyMMddHHmm") + Path.GetExtension(sourceFile);
                    string targetFile = Path.Combine(gameSaveFolder, newFileName);

                    // 복사 실행
                    File.Copy(sourceFile, targetFile);

                    MessageBox.Show("File duplicated successfully.", "Duplicate Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateSaveFileList();
                }
                else
                {
                    MessageBox.Show("Please select a file to duplicate.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                string gameBackupFolder = GetSaveDirectory(GetUserGameName(userDirectory));

                string newFileName = "QS_" + DateTime.Now.ToString("yyMMddHHmm") + Path.GetExtension(targetFile);    //이거
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
            try
            {
                if (lvTargetFiles.SelectedItems.Count > 0 && lvFileList.SelectedItems.Count > 0)
                {
                    // 선택된 대상 파일과 저장 파일 가져오기
                    string targetFileName = lvTargetFiles.SelectedItems[0].Text;
                    string saveFileName = lvFileList.SelectedItems[0].Text;

                    string targetFilePath = Path.Combine(userDirectory, targetFileName);
                    string saveFilePath = Path.Combine(GetSaveDirectory(GetUserGameName(userDirectory)), saveFileName);

                    // 백업 생성 및 스왑 실행
                    if (File.Exists(targetFilePath) && File.Exists(saveFilePath))
                    {
                        string backupPath = Path.Combine(GetSaveDirectory(GetUserGameName(userDirectory)), "backup_" + DateTime.Now.ToString("yyMMddHHmm") + Path.GetExtension(targetFilePath));
                        File.Copy(targetFilePath, backupPath); // 대상 파일 백업

                        File.Copy(saveFilePath, targetFilePath, true); // 저장 파일을 대상 위치로 복사
                        File.Copy(backupPath, saveFilePath, true);     // 백업을 저장 위치로 복사

                        MessageBox.Show("Files swapped successfully.", "Swap Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopulateTargetFileList(userDirectory);
                        PopulateSaveFileList();
                    }
                    else
                    {
                        MessageBox.Show("One or both files do not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a file from both lists to swap.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during swapping files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*
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
        */
        private string GetMostRecentSaveFile()
        {   //user directory 변화 / getusergamename 으로 얻고 / startup으로 가서 겜폴더 찾기
            string gameSaveFolder = GetSaveDirectory(GetUserGameName(userDirectory)); 
            string[] saveFiles = Directory.GetFiles(gameSaveFolder); //, "*.sl2 삭제 "

            if (saveFiles.Length > 0)
            {
                Array.Sort(saveFiles, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));
                return saveFiles[0];
            }

            return null;
        }

        private void lvFileList_MouseClick(object sender, MouseEventArgs e)
        {
            // 마우스 오른쪽 버튼을 클릭한 경우
            if (e.Button == MouseButtons.Right)
            {
                // 선택된 항목이 있는지 확인
                if (lvFileList.SelectedItems.Count > 0)
                {
                    // 선택된 항목의 텍스트 (파일 이름) 가져오기
                    string selectedFileName = lvFileList.SelectedItems[0].Text;

                    // 사용자에게 새 파일 이름을 입력받는 대화 상자 표시
                    string newFileName = PromptForNewFileName(selectedFileName);

                    // 사용자가 입력한 새 파일 이름이 null이 아닌 경우
                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        // 파일 이름 변경
                        RenameFile(selectedFileName, newFileName);

                        // ListView 업데이트
                        lvFileList.SelectedItems[0].Text = newFileName;
                    }
                }
            }
        }

        // 사용자에게 새 파일 이름을 입력받는 대화 상자를 표시합니다.
        private string PromptForNewFileName(string currentFileName)
        {
            // 새 폼 생성
            Form promptForm = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Rename File"
            };

            // 레이블 추가
            Label label = new Label()
            {
                Left = 50,
                Top = 20,
                Text = "Enter new file name:"
            };

            // 텍스트 상자 추가
            TextBox textBox = new TextBox()
            {
                Left = 50,
                Top = 50,
                Width = 300
            };

            // 확인 버튼 추가
            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 250,
                Width = 100,
                Top = 80
            };

            // 확인 버튼 클릭 시 폼을 닫고 입력한 파일 이름을 반환
            confirmation.Click += (sender, e) => { promptForm.Close(); };

            // 폼에 컨트롤들 추가
            promptForm.Controls.Add(confirmation);
            promptForm.Controls.Add(label);
            promptForm.Controls.Add(textBox);

            // 폼 열기
            promptForm.ShowDialog();

            // 사용자가 입력한 새 파일 이름을 반환
            return textBox.Text;
        }

        // 파일 이름 변경
        private void RenameFile(string oldFileName, string newFileName)
        {
            try
            {
                // 파일 경로를 가져옵니다.
                string sourceFilePath = Path.Combine(GetSaveDirectory(GetUserGameName(userDirectory)), oldFileName);
                string targetFilePath = Path.Combine(GetSaveDirectory(GetUserGameName(userDirectory)), newFileName);

                // 파일 이름 변경
                File.Move(sourceFilePath, targetFilePath);

                MessageBox.Show("File renamed successfully.", "Rename Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GetUserGameName(string directory) 
        {


            if (directory.Contains("DarkSoulsIII"))
            {
                return "DarkSoulsIII";
            }
            else if (directory.Contains("Sekiro"))
            {
                return "Sekiro";
            }
            else if (directory.Contains("DarkSoulsII"))
            {
                return "DarkSoulsII";
            }
            else if (directory.Contains("ArmoredCore6"))
            {
                return "ArmoredCore6";
            }
            else if (directory.Contains("EldenRing"))
            {
                return "EldenRing";
            }
            else if (directory.Contains("webfishing_2_newver"))
            {
                return "WEBFISHING";
            }
            return "UnknownGame";
        }
        private string GetTargetFileNameForGame(string gameName)
        {
            switch (gameName)
            {
                case "DarkSoulsIII":
                    return "DS30000.sl2";
                case "Sekiro":
                    return "S0000.sl2";
                case "EldenRing":
                    return "ER0000.sl2";
                case "DarkSoulsII":
                    return "DS20000.sl2";
                case "ArmoredCore6":
                    return "AC60000.sl2";
                case "DARK SOULS REMASTERED":
                    return "DRAKS0005.sl2";
                case "Dark Souls":
                    return "DRAKS0005.sl2";
                case "WEBFISHING":
                    return "webfishing_save_slot_0.sav";
                default:
                    return "";
            }
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

        private void comboBoxGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGame = comboBoxGames.SelectedItem.ToString();
            userDirectory = GetUserDirectory(selectedGame);
            SetTargetFileName(GetTargetFileNameForGame(selectedGame));
            btnFind_Click(sender, e);
        }
    }
}

