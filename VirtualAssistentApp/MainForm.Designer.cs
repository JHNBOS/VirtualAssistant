namespace VirtualAssistentApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.title = new System.Windows.Forms.Label();
            this.weatherBox = new System.Windows.Forms.PictureBox();
            this.internetBox = new System.Windows.Forms.PictureBox();
            this.selfieBox = new System.Windows.Forms.PictureBox();
            this.explainLabel = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.mediaBox = new System.Windows.Forms.PictureBox();
            this.btnAddCommand = new System.Windows.Forms.Button();
            this.appBox = new System.Windows.Forms.PictureBox();
            this.officeBox = new System.Windows.Forms.PictureBox();
            this.systemBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.weatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internetBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfieBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemBox)).BeginInit();
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
            // explainLabel
            // 
            this.explainLabel.BackColor = System.Drawing.Color.Transparent;
            this.explainLabel.Font = new System.Drawing.Font("Quicksand", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.explainLabel.ForeColor = System.Drawing.Color.White;
            this.explainLabel.Location = new System.Drawing.Point(58, 402);
            this.explainLabel.Name = "explainLabel";
            this.explainLabel.Size = new System.Drawing.Size(439, 33);
            this.explainLabel.TabIndex = 4;
            this.explainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.settings_4;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(620, 402);
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
            // btnAddCommand
            // 
            this.btnAddCommand.BackColor = System.Drawing.Color.Transparent;
            this.btnAddCommand.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.add;
            this.btnAddCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddCommand.FlatAppearance.BorderSize = 0;
            this.btnAddCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCommand.Location = new System.Drawing.Point(561, 402);
            this.btnAddCommand.Name = "btnAddCommand";
            this.btnAddCommand.Size = new System.Drawing.Size(53, 41);
            this.btnAddCommand.TabIndex = 7;
            this.btnAddCommand.UseVisualStyleBackColor = true;
            this.btnAddCommand.Click += new System.EventHandler(this.btnAddCommand_Click);
            // 
            // appBox
            // 
            this.appBox.BackColor = System.Drawing.Color.Transparent;
            this.appBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.settings_3;
            this.appBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("appBox.InitialImage")));
            this.appBox.Location = new System.Drawing.Point(236, 216);
            this.appBox.Name = "appBox";
            this.appBox.Size = new System.Drawing.Size(90, 90);
            this.appBox.TabIndex = 8;
            this.appBox.TabStop = false;
            // 
            // officeBox
            // 
            this.officeBox.BackColor = System.Drawing.Color.Transparent;
            this.officeBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.compose;
            this.officeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.officeBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("officeBox.InitialImage")));
            this.officeBox.Location = new System.Drawing.Point(407, 216);
            this.officeBox.Name = "officeBox";
            this.officeBox.Size = new System.Drawing.Size(90, 90);
            this.officeBox.TabIndex = 9;
            this.officeBox.TabStop = false;
            // 
            // systemBox
            // 
            this.systemBox.BackColor = System.Drawing.Color.Transparent;
            this.systemBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.television_1;
            this.systemBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.systemBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("systemBox.InitialImage")));
            this.systemBox.Location = new System.Drawing.Point(561, 160);
            this.systemBox.Name = "systemBox";
            this.systemBox.Size = new System.Drawing.Size(90, 90);
            this.systemBox.TabIndex = 10;
            this.systemBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(685, 455);
            this.Controls.Add(this.systemBox);
            this.Controls.Add(this.officeBox);
            this.Controls.Add(this.appBox);
            this.Controls.Add(this.btnAddCommand);
            this.Controls.Add(this.mediaBox);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.explainLabel);
            this.Controls.Add(this.selfieBox);
            this.Controls.Add(this.internetBox);
            this.Controls.Add(this.weatherBox);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Quicksand", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Virtual Assistent";
            ((System.ComponentModel.ISupportInitialize)(this.weatherBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internetBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfieBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox weatherBox;
        private System.Windows.Forms.PictureBox internetBox;
        private System.Windows.Forms.PictureBox selfieBox;
        private System.Windows.Forms.Label explainLabel;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.PictureBox mediaBox;
        private System.Windows.Forms.Button btnAddCommand;
        private System.Windows.Forms.PictureBox appBox;
        private System.Windows.Forms.PictureBox officeBox;
        private System.Windows.Forms.PictureBox systemBox;
    }
}

