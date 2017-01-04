namespace JPS.Demo
{
    partial class Demo
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
            this.SuspendLayout();
            // 
            // btnCreateGrid
            // 
            this.btnCreateGrid.Location = new System.Drawing.Point(12, 12);
            this.btnCreateGrid.Name = "btnCreateGrid";
            this.btnCreateGrid.Size = new System.Drawing.Size(105, 31);
            this.btnCreateGrid.TabIndex = 0;
            this.btnCreateGrid.Text = "Create Grid";
            this.btnCreateGrid.UseVisualStyleBackColor = true;
            this.btnCreateGrid.Click += new System.EventHandler(this.CreateGrid);
            // 
            // btnFindPath
            // 
            this.btnFindPath.Location = new System.Drawing.Point(12, 49);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(105, 31);
            this.btnFindPath.TabIndex = 1;
            this.btnFindPath.Text = "Find Path";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.FindPath);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 292);
            this.Controls.Add(this.btnFindPath);
            this.Controls.Add(this.btnCreateGrid);
            this.Name = "Demo";
            this.Text = "Jump Point Search Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateGrid;
        private System.Windows.Forms.Button btnFindPath;
    }
}

