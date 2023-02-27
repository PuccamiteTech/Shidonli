namespace Shidonli
{
    partial class frmSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetup));
            this.btnInstall = new System.Windows.Forms.Button();
            this.lblHeading = new System.Windows.Forms.Label();
            this.grpVersion = new System.Windows.Forms.GroupBox();
            this.radV2 = new System.Windows.Forms.RadioButton();
            this.radV1 = new System.Windows.Forms.RadioButton();
            this.grpModifications = new System.Windows.Forms.GroupBox();
            this.chkRegister = new System.Windows.Forms.CheckBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.picAnimal = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.chkFresh = new System.Windows.Forms.CheckBox();
            this.grpVersion.SuspendLayout();
            this.grpModifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.White;
            this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstall.Location = new System.Drawing.Point(126, 200);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 33);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(121, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(383, 29);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = "Local Shidonni Server AutoTool";
            // 
            // grpVersion
            // 
            this.grpVersion.Controls.Add(this.radV2);
            this.grpVersion.Controls.Add(this.radV1);
            this.grpVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVersion.Location = new System.Drawing.Point(126, 74);
            this.grpVersion.Name = "grpVersion";
            this.grpVersion.Size = new System.Drawing.Size(170, 90);
            this.grpVersion.TabIndex = 2;
            this.grpVersion.TabStop = false;
            this.grpVersion.Text = "Game Version";
            // 
            // radV2
            // 
            this.radV2.AutoSize = true;
            this.radV2.Checked = true;
            this.radV2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radV2.Location = new System.Drawing.Point(7, 43);
            this.radV2.Name = "radV2";
            this.radV2.Size = new System.Drawing.Size(110, 20);
            this.radV2.TabIndex = 1;
            this.radV2.TabStop = true;
            this.radV2.Text = "v2 (2010-2015)";
            this.radV2.UseVisualStyleBackColor = true;
            // 
            // radV1
            // 
            this.radV1.AutoSize = true;
            this.radV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radV1.Location = new System.Drawing.Point(7, 20);
            this.radV1.Name = "radV1";
            this.radV1.Size = new System.Drawing.Size(110, 20);
            this.radV1.TabIndex = 0;
            this.radV1.Text = "v1 (2008-2010)";
            this.radV1.UseVisualStyleBackColor = true;
            // 
            // grpModifications
            // 
            this.grpModifications.Controls.Add(this.chkFresh);
            this.grpModifications.Controls.Add(this.chkRegister);
            this.grpModifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpModifications.Location = new System.Drawing.Point(334, 74);
            this.grpModifications.Name = "grpModifications";
            this.grpModifications.Size = new System.Drawing.Size(170, 90);
            this.grpModifications.TabIndex = 3;
            this.grpModifications.TabStop = false;
            this.grpModifications.Text = "Modifications";
            // 
            // chkRegister
            // 
            this.chkRegister.AutoSize = true;
            this.chkRegister.Checked = true;
            this.chkRegister.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRegister.Location = new System.Drawing.Point(6, 20);
            this.chkRegister.Name = "chkRegister";
            this.chkRegister.Size = new System.Drawing.Size(143, 20);
            this.chkRegister.TabIndex = 2;
            this.chkRegister.Text = "Custom RegisterLib";
            this.chkRegister.UseVisualStyleBackColor = true;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.Black;
            this.txtStatus.Location = new System.Drawing.Point(196, 269);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(416, 158);
            this.txtStatus.TabIndex = 4;
            // 
            // picAnimal
            // 
            this.picAnimal.Image = global::Shidonli.Properties.Resources.animal;
            this.picAnimal.Location = new System.Drawing.Point(12, 269);
            this.picAnimal.Name = "picAnimal";
            this.picAnimal.Size = new System.Drawing.Size(170, 158);
            this.picAnimal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnimal.TabIndex = 5;
            this.picAnimal.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(277, 200);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 33);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.White;
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(428, 200);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 33);
            this.btnQuit.TabIndex = 7;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // chkFresh
            // 
            this.chkFresh.AutoSize = true;
            this.chkFresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFresh.Location = new System.Drawing.Point(6, 43);
            this.chkFresh.Name = "chkFresh";
            this.chkFresh.Size = new System.Drawing.Size(98, 20);
            this.chkFresh.TabIndex = 3;
            this.chkFresh.Text = "Fresh Setup";
            this.chkFresh.UseVisualStyleBackColor = true;
            // 
            // frmSetup
            // 
            this.AcceptButton = this.btnInstall;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.picAnimal);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.grpModifications);
            this.Controls.Add(this.grpVersion);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btnInstall);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSetup";
            this.Text = "Shidonli Setup";
            this.Load += new System.EventHandler(this.frmSetup_Load);
            this.grpVersion.ResumeLayout(false);
            this.grpVersion.PerformLayout();
            this.grpModifications.ResumeLayout(false);
            this.grpModifications.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.GroupBox grpVersion;
        private System.Windows.Forms.RadioButton radV2;
        private System.Windows.Forms.RadioButton radV1;
        private System.Windows.Forms.GroupBox grpModifications;
        private System.Windows.Forms.CheckBox chkRegister;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.PictureBox picAnimal;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.CheckBox chkFresh;
    }
}

