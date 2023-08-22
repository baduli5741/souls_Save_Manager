namespace darksouls3_Save_Manager
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
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.lvFileList = new System.Windows.Forms.ListView();
            this.lvTargetFiles = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(86, 347);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(115, 30);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find my save";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(231, 347);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(305, 25);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(616, 348);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(75, 29);
            this.btnDuplicate.TabIndex = 2;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // lvFileList
            // 
            this.lvFileList.HideSelection = false;
            this.lvFileList.Location = new System.Drawing.Point(562, 92);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(184, 204);
            this.lvFileList.TabIndex = 3;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            this.lvFileList.View = System.Windows.Forms.View.Details;
            // 
            // lvTargetFiles
            // 
            this.lvTargetFiles.HideSelection = false;
            this.lvTargetFiles.Location = new System.Drawing.Point(57, 92);
            this.lvTargetFiles.Name = "lvTargetFiles";
            this.lvTargetFiles.Size = new System.Drawing.Size(184, 204);
            this.lvTargetFiles.TabIndex = 4;
            this.lvTargetFiles.UseCompatibleStateImageBehavior = false;
            this.lvTargetFiles.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvTargetFiles);
            this.Controls.Add(this.lvFileList);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnFind);
            this.Name = "Form1";
            this.Text = "ds3 save manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ListView lvTargetFiles;
    }
}

