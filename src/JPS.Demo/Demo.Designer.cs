namespace JPS.Demo
{
    sealed partial class Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateGrid = new System.Windows.Forms.Button();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.ctlMap = new JPS.Demo.Map();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.rdoStart = new System.Windows.Forms.RadioButton();
            this.rdoGoal = new System.Windows.Forms.RadioButton();
            this.rdoWall = new System.Windows.Forms.RadioButton();
            this.grpSetSelect = new System.Windows.Forms.GroupBox();
            this.grpSetSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateGrid
            // 
            this.btnCreateGrid.Location = new System.Drawing.Point(12, 12);
            this.btnCreateGrid.Name = "btnCreateGrid";
            this.btnCreateGrid.Size = new System.Drawing.Size(97, 31);
            this.btnCreateGrid.TabIndex = 0;
            this.btnCreateGrid.Text = "Create Grid";
            this.btnCreateGrid.UseVisualStyleBackColor = true;
            this.btnCreateGrid.Click += new System.EventHandler(this.CreateGrid);
            // 
            // btnFindPath
            // 
            this.btnFindPath.Location = new System.Drawing.Point(12, 49);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(97, 31);
            this.btnFindPath.TabIndex = 1;
            this.btnFindPath.Text = "Find Path";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.FindPath);
            // 
            // ctlMap
            // 
            this.ctlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlMap.Cols = 256;
            this.ctlMap.DrawGrid = false;
            this.ctlMap.Location = new System.Drawing.Point(133, 12);
            this.ctlMap.Name = "ctlMap";
            this.ctlMap.Rows = 256;
            this.ctlMap.SelectedCell = null;
            this.ctlMap.Size = new System.Drawing.Size(764, 640);
            this.ctlMap.TabIndex = 2;
            this.ctlMap.CellMouseDown += new System.EventHandler<JPS.Demo.CellMouseDownEventArgs>(this.CellMouseDown);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(12, 86);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(97, 31);
            this.btnLoadImage.TabIndex = 3;
            this.btnLoadImage.Text = "Load Maze";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.LoadImage);
            // 
            // rdoStart
            // 
            this.rdoStart.AutoSize = true;
            this.rdoStart.Checked = true;
            this.rdoStart.Location = new System.Drawing.Point(16, 29);
            this.rdoStart.Name = "rdoStart";
            this.rdoStart.Size = new System.Drawing.Size(47, 17);
            this.rdoStart.TabIndex = 0;
            this.rdoStart.TabStop = true;
            this.rdoStart.Text = "Start";
            this.rdoStart.UseVisualStyleBackColor = true;
            // 
            // rdoGoal
            // 
            this.rdoGoal.AutoSize = true;
            this.rdoGoal.Location = new System.Drawing.Point(16, 52);
            this.rdoGoal.Name = "rdoGoal";
            this.rdoGoal.Size = new System.Drawing.Size(47, 17);
            this.rdoGoal.TabIndex = 1;
            this.rdoGoal.Text = "Goal";
            this.rdoGoal.UseVisualStyleBackColor = true;
            // 
            // rdoWall
            // 
            this.rdoWall.AutoSize = true;
            this.rdoWall.Location = new System.Drawing.Point(16, 75);
            this.rdoWall.Name = "rdoWall";
            this.rdoWall.Size = new System.Drawing.Size(46, 17);
            this.rdoWall.TabIndex = 2;
            this.rdoWall.Text = "Wall";
            this.rdoWall.UseVisualStyleBackColor = true;
            // 
            // grpSetSelect
            // 
            this.grpSetSelect.Controls.Add(this.rdoStart);
            this.grpSetSelect.Controls.Add(this.rdoWall);
            this.grpSetSelect.Controls.Add(this.rdoGoal);
            this.grpSetSelect.Location = new System.Drawing.Point(12, 123);
            this.grpSetSelect.Name = "grpSetSelect";
            this.grpSetSelect.Size = new System.Drawing.Size(97, 108);
            this.grpSetSelect.TabIndex = 4;
            this.grpSetSelect.TabStop = false;
            this.grpSetSelect.Text = "Setting";
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 663);
            this.Controls.Add(this.grpSetSelect);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.ctlMap);
            this.Controls.Add(this.btnFindPath);
            this.Controls.Add(this.btnCreateGrid);
            this.Name = "Demo";
            this.Text = "Jump Point Search Demo";
            this.grpSetSelect.ResumeLayout(false);
            this.grpSetSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateGrid;
        private System.Windows.Forms.Button btnFindPath;
        private Map ctlMap;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.RadioButton rdoStart;
        private System.Windows.Forms.RadioButton rdoGoal;
        private System.Windows.Forms.RadioButton rdoWall;
        private System.Windows.Forms.GroupBox grpSetSelect;
    }
}

