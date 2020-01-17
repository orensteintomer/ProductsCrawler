namespace ProductsCrawler
{
    partial class ProductsStatsForm
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
            this.btnRefreshStats = new System.Windows.Forms.Button();
            this.dtgPoductsStats = new System.Windows.Forms.DataGridView();
            this.btnLoadNewFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPoductsStats)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefreshStats
            // 
            this.btnRefreshStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnRefreshStats.Location = new System.Drawing.Point(368, 439);
            this.btnRefreshStats.Name = "btnRefreshStats";
            this.btnRefreshStats.Size = new System.Drawing.Size(201, 29);
            this.btnRefreshStats.TabIndex = 0;
            this.btnRefreshStats.Text = "Refresh Statistics!";
            this.btnRefreshStats.UseVisualStyleBackColor = true;
            this.btnRefreshStats.Click += new System.EventHandler(this.btnRefreshStats_Click);
            // 
            // dtgPoductsStats
            // 
            this.dtgPoductsStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgPoductsStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPoductsStats.Location = new System.Drawing.Point(36, 102);
            this.dtgPoductsStats.Name = "dtgPoductsStats";
            this.dtgPoductsStats.Size = new System.Drawing.Size(856, 292);
            this.dtgPoductsStats.TabIndex = 1;
            // 
            // btnLoadNewFile
            // 
            this.btnLoadNewFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnLoadNewFile.Location = new System.Drawing.Point(36, 12);
            this.btnLoadNewFile.Name = "btnLoadNewFile";
            this.btnLoadNewFile.Size = new System.Drawing.Size(131, 29);
            this.btnLoadNewFile.TabIndex = 2;
            this.btnLoadNewFile.Text = "LoadNewFile";
            this.btnLoadNewFile.UseVisualStyleBackColor = true;
            this.btnLoadNewFile.Click += new System.EventHandler(this.btnLoadNewFile_Click);
            // 
            // ProductsStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 511);
            this.Controls.Add(this.btnLoadNewFile);
            this.Controls.Add(this.dtgPoductsStats);
            this.Controls.Add(this.btnRefreshStats);
            this.Name = "ProductsStatsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductsStats";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductsStats_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPoductsStats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshStats;
        private System.Windows.Forms.DataGridView dtgPoductsStats;
        private System.Windows.Forms.Button btnLoadNewFile;
    }
}