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
            ((System.ComponentModel.ISupportInitialize)(this.weatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internetBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfieBox)).BeginInit();
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
            this.weatherBox.Location = new System.Drawing.Point(31, 93);
            this.weatherBox.Name = "weatherBox";
            this.weatherBox.Size = new System.Drawing.Size(123, 119);
            this.weatherBox.TabIndex = 1;
            this.weatherBox.TabStop = false;
            // 
            // internetBox
            // 
            this.internetBox.BackColor = System.Drawing.Color.Transparent;
            this.internetBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.internet;
            this.internetBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.internetBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("internetBox.InitialImage")));
            this.internetBox.Location = new System.Drawing.Point(220, 93);
            this.internetBox.Name = "internetBox";
            this.internetBox.Size = new System.Drawing.Size(123, 119);
            this.internetBox.TabIndex = 2;
            this.internetBox.TabStop = false;
            // 
            // selfieBox
            // 
            this.selfieBox.BackColor = System.Drawing.Color.Transparent;
            this.selfieBox.BackgroundImage = global::VirtualAssistentApp.Properties.Resources.photo_camera;
            this.selfieBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selfieBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("selfieBox.InitialImage")));
            this.selfieBox.Location = new System.Drawing.Point(407, 93);
            this.selfieBox.Name = "selfieBox";
            this.selfieBox.Size = new System.Drawing.Size(123, 119);
            this.selfieBox.TabIndex = 3;
            this.selfieBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(557, 455);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox weatherBox;
        private System.Windows.Forms.PictureBox internetBox;
        private System.Windows.Forms.PictureBox selfieBox;
    }
}

