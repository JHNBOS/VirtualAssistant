namespace VirtualAssistentApp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.title = new System.Windows.Forms.Label();
            this.weatherBox = new System.Windows.Forms.PictureBox();
            this.internetBox = new System.Windows.Forms.PictureBox();
            this.selfieBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.mediaBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.weatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internetBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfieBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaBox)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title.Font = new System.Drawing.Font("Quicksand", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(13, 13);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(532, 54);
            this.title.TabIndex = 0;
            this.title.Text = "Virtual Assistent";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // weatherBox
            // 
            this.weatherBox.BackColor = System.Drawing.Color.Transparent;
            this.weatherBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.cloud;
            this.weatherBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.weatherBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("weatherBox.InitialImage")));
            this.weatherBox.Location = new System.Drawing.Point(64, 89);
            this.weatherBox.Name = "weatherBox";
            this.weatherBox.Size = new System.Drawing.Size(90, 90);
            this.weatherBox.TabIndex = 1;
            this.weatherBox.TabStop = false;
            // 
            // internetBox
            // 
            this.internetBox.BackColor = System.Drawing.Color.Transparent;
            this.internetBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.internet;
            this.internetBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.internetBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("internetBox.InitialImage")));
            this.internetBox.Location = new System.Drawing.Point(236, 89);
            this.internetBox.Name = "internetBox";
            this.internetBox.Size = new System.Drawing.Size(90, 90);
            this.internetBox.TabIndex = 2;
            this.internetBox.TabStop = false;
            // 
            // selfieBox
            // 
            this.selfieBox.BackColor = System.Drawing.Color.Transparent;
            this.selfieBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.photo_camera;
            this.selfieBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selfieBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("selfieBox.InitialImage")));
            this.selfieBox.Location = new System.Drawing.Point(407, 89);
            this.selfieBox.Name = "selfieBox";
            this.selfieBox.Size = new System.Drawing.Size(90, 90);
            this.selfieBox.TabIndex = 3;
            this.selfieBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Quicksand", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(112, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start Mary By saying \"Hey Mary\"";
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.settings_4;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(492, 402);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(53, 41);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // mediaBox
            // 
            this.mediaBox.BackColor = System.Drawing.Color.Transparent;
            this.mediaBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.play_button_1;
            this.mediaBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mediaBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("mediaBox.InitialImage")));
            this.mediaBox.Location = new System.Drawing.Point(64, 216);
            this.mediaBox.Name = "mediaBox";
            this.mediaBox.Size = new System.Drawing.Size(90, 90);
            this.mediaBox.TabIndex = 6;
            this.mediaBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(557, 455);
            this.Controls.Add(this.mediaBox);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selfieBox);
            this.Controls.Add(this.internetBox);
            this.Controls.Add(this.weatherBox);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Quicksand", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Virtual Assistent";
            ((System.ComponentModel.ISupportInitialize)(this.weatherBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internetBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfieBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox weatherBox;
        private System.Windows.Forms.PictureBox internetBox;
        private System.Windows.Forms.PictureBox selfieBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.PictureBox mediaBox;
    }
}

