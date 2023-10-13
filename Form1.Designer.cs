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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.lvTargetFiles = new System.Windows.Forms.ListView();
            this.btnQuickSwap = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.comboBoxGames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(220, 308);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(305, 25);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(583, 302);
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
            this.lvFileList.Location = new System.Drawing.Point(449, 88);
            this.lvFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(274, 199);
            this.lvFileList.TabIndex = 3;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            this.lvFileList.View = System.Windows.Forms.View.Details;
            this.lvFileList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvFileList_MouseClick);
            // 
            // lvTargetFiles
            // 
            this.lvTargetFiles.HideSelection = false;
            this.lvTargetFiles.Location = new System.Drawing.Point(35, 88);
            this.lvTargetFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvTargetFiles.Name = "lvTargetFiles";
            this.lvTargetFiles.Size = new System.Drawing.Size(274, 199);
            this.lvTargetFiles.TabIndex = 4;
            this.lvTargetFiles.UseCompatibleStateImageBehavior = false;
            this.lvTargetFiles.View = System.Windows.Forms.View.Details;
            // 
            // btnQuickSwap
            // 
            this.btnQuickSwap.Location = new System.Drawing.Point(583, 346);
            this.btnQuickSwap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuickSwap.Name = "btnQuickSwap";
            this.btnQuickSwap.Size = new System.Drawing.Size(114, 31);
            this.btnQuickSwap.TabIndex = 5;
            this.btnQuickSwap.Text = "Quick swap";
            this.btnQuickSwap.UseVisualStyleBackColor = true;
            this.btnQuickSwap.Click += new System.EventHandler(this.btnQuickSwap_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(315, 197);
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
            this.comboBoxGames.Location = new System.Drawing.Point(298, 43);
            this.comboBoxGames.Name = "comboBoxGames";
            this.comboBoxGames.Size = new System.Drawing.Size(161, 23);
            this.comboBoxGames.TabIndex = 10;
            this.comboBoxGames.SelectedIndexChanged += new System.EventHandler(this.comboBoxGames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(299, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select your game";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 399);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxGames);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnQuickSwap);
            this.Controls.Add(this.lvTargetFiles);
            this.Controls.Add(this.lvFileList);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.txtFilePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Souls Save Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ListView lvTargetFiles;
        private System.Windows.Forms.Button btnQuickSwap;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.ComboBox comboBoxGames;
        private System.Windows.Forms.Label label1;
    }
}

