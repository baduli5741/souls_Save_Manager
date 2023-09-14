namespace souls_Save_Manager
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.lvTargetFiles = new System.Windows.Forms.ListView();
            this.btnQuickSwap = new System.Windows.Forms.Button();
            this.btnGame1 = new System.Windows.Forms.Button();
            this.btnGame2 = new System.Windows.Forms.Button();
            this.btnGame3 = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.comboBoxGames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(86, 350);
            this.btnFind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(114, 31);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find my save";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(229, 350);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(305, 25);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(606, 350);
            this.btnDuplicate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(114, 31);
            this.btnDuplicate.TabIndex = 2;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // lvFileList
            // 
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(471, 92);
            this.lvFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(274, 199);
            this.lvFileList.TabIndex = 3;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            this.lvFileList.View = System.Windows.Forms.View.Details;
            // 
            // lvTargetFiles
            // 
            this.lvTargetFiles.HideSelection = false;
            this.lvTargetFiles.Location = new System.Drawing.Point(57, 92);
            this.lvTargetFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvTargetFiles.Name = "lvTargetFiles";
            this.lvTargetFiles.Size = new System.Drawing.Size(274, 199);
            this.lvTargetFiles.TabIndex = 4;
            this.lvTargetFiles.UseCompatibleStateImageBehavior = false;
            this.lvTargetFiles.View = System.Windows.Forms.View.Details;
            // 
            // btnQuickSwap
            // 
            this.btnQuickSwap.Location = new System.Drawing.Point(606, 394);
            this.btnQuickSwap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuickSwap.Name = "btnQuickSwap";
            this.btnQuickSwap.Size = new System.Drawing.Size(114, 31);
            this.btnQuickSwap.TabIndex = 5;
            this.btnQuickSwap.Text = "Quick swap";
            this.btnQuickSwap.UseVisualStyleBackColor = true;
            this.btnQuickSwap.Click += new System.EventHandler(this.btnQuickSwap_Click);
            // 
            // btnGame1
            // 
            this.btnGame1.Location = new System.Drawing.Point(25, 29);
            this.btnGame1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGame1.Name = "btnGame1";
            this.btnGame1.Size = new System.Drawing.Size(103, 38);
            this.btnGame1.TabIndex = 6;
            this.btnGame1.Text = "DarkSoulsIII";
            this.btnGame1.UseVisualStyleBackColor = true;
            this.btnGame1.Click += new System.EventHandler(this.btnGame1_Click);
            // 
            // btnGame2
            // 
            this.btnGame2.Location = new System.Drawing.Point(142, 29);
            this.btnGame2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGame2.Name = "btnGame2";
            this.btnGame2.Size = new System.Drawing.Size(103, 38);
            this.btnGame2.TabIndex = 7;
            this.btnGame2.Text = "Sekiro";
            this.btnGame2.UseVisualStyleBackColor = true;
            this.btnGame2.Click += new System.EventHandler(this.btnGame2_Click);
            // 
            // btnGame3
            // 
            this.btnGame3.Location = new System.Drawing.Point(258, 29);
            this.btnGame3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGame3.Name = "btnGame3";
            this.btnGame3.Size = new System.Drawing.Size(103, 38);
            this.btnGame3.TabIndex = 8;
            this.btnGame3.Text = "EldenRing";
            this.btnGame3.UseVisualStyleBackColor = true;
            this.btnGame3.Click += new System.EventHandler(this.btnGame3_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(337, 201);
            this.btnSwap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(130, 39);
            this.btnSwap.TabIndex = 9;
            this.btnSwap.Text = "Swap";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // comboBoxGames
            // 
            this.comboBoxGames.FormattingEnabled = true;
            this.comboBoxGames.Location = new System.Drawing.Point(564, 38);
            this.comboBoxGames.Name = "comboBoxGames";
            this.comboBoxGames.Size = new System.Drawing.Size(121, 23);
            this.comboBoxGames.TabIndex = 10;
            this.comboBoxGames.SelectedIndexChanged += new System.EventHandler(this.comboBoxGames_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxGames);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnGame3);
            this.Controls.Add(this.btnGame2);
            this.Controls.Add(this.btnGame1);
            this.Controls.Add(this.btnQuickSwap);
            this.Controls.Add(this.lvTargetFiles);
            this.Controls.Add(this.lvFileList);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnFind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Souls Save Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ListView lvTargetFiles;
        private System.Windows.Forms.Button btnQuickSwap;
        private System.Windows.Forms.Button btnGame1;
        private System.Windows.Forms.Button btnGame2;
        private System.Windows.Forms.Button btnGame3;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.ComboBox comboBoxGames;
    }
}

