namespace MonsterJobs.Admin
{
    partial class JobsAdmin
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
            this.PostJob = new System.Windows.Forms.Button();
            this.JobTypeLabel = new System.Windows.Forms.Label();
            this.JobType = new System.Windows.Forms.ComboBox();
            this.JobDescriptionLabel = new System.Windows.Forms.Label();
            this.JobDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PostJob
            // 
            this.PostJob.Location = new System.Drawing.Point(667, 391);
            this.PostJob.Name = "PostJob";
            this.PostJob.Size = new System.Drawing.Size(121, 47);
            this.PostJob.TabIndex = 0;
            this.PostJob.Text = "Post Job";
            this.PostJob.UseVisualStyleBackColor = true;
            this.PostJob.Click += new System.EventHandler(this.PostJob_Click);
            // 
            // JobTypeLabel
            // 
            this.JobTypeLabel.AutoSize = true;
            this.JobTypeLabel.Location = new System.Drawing.Point(27, 54);
            this.JobTypeLabel.Name = "JobTypeLabel";
            this.JobTypeLabel.Size = new System.Drawing.Size(67, 17);
            this.JobTypeLabel.TabIndex = 1;
            this.JobTypeLabel.Text = "Job Type";
            // 
            // JobType
            // 
            this.JobType.FormattingEnabled = true;
            this.JobType.Items.AddRange(new object[] {
            "FullStack",
            "WebDeveloper",
            "MiddleTierDeveloper",
            "DBA",
            "BA",
            "Testing",
            "Architect"});
            this.JobType.Location = new System.Drawing.Point(143, 54);
            this.JobType.Name = "JobType";
            this.JobType.Size = new System.Drawing.Size(121, 24);
            this.JobType.TabIndex = 2;
            // 
            // JobDescriptionLabel
            // 
            this.JobDescriptionLabel.AutoSize = true;
            this.JobDescriptionLabel.Location = new System.Drawing.Point(25, 97);
            this.JobDescriptionLabel.Name = "JobDescriptionLabel";
            this.JobDescriptionLabel.Size = new System.Drawing.Size(106, 17);
            this.JobDescriptionLabel.TabIndex = 3;
            this.JobDescriptionLabel.Text = "Job Description";
            // 
            // JobDescription
            // 
            this.JobDescription.Location = new System.Drawing.Point(143, 91);
            this.JobDescription.Multiline = true;
            this.JobDescription.Name = "JobDescription";
            this.JobDescription.Size = new System.Drawing.Size(493, 136);
            this.JobDescription.TabIndex = 4;
            // 
            // JobsAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.JobDescription);
            this.Controls.Add(this.JobDescriptionLabel);
            this.Controls.Add(this.JobType);
            this.Controls.Add(this.JobTypeLabel);
            this.Controls.Add(this.PostJob);
            this.Name = "JobsAdmin";
            this.Text = "Jobs Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PostJob;
        private System.Windows.Forms.Label JobTypeLabel;
        private System.Windows.Forms.ComboBox JobType;
        private System.Windows.Forms.Label JobDescriptionLabel;
        private System.Windows.Forms.TextBox JobDescription;
    }
}

