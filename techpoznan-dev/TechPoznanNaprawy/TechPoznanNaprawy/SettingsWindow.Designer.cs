namespace TechPoznanNaprawy
{
    partial class SettingsWindow
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
            this.autoBackupCheckBox = new System.Windows.Forms.CheckBox();
            this.backupPeriodComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // autoBackupCheckBox
            // 
            this.autoBackupCheckBox.AutoSize = true;
            this.autoBackupCheckBox.Location = new System.Drawing.Point(12, 12);
            this.autoBackupCheckBox.Name = "autoBackupCheckBox";
            this.autoBackupCheckBox.Size = new System.Drawing.Size(228, 17);
            this.autoBackupCheckBox.TabIndex = 0;
            this.autoBackupCheckBox.Text = "Automatyczne tworzenie kopii zapasowych";
            this.autoBackupCheckBox.UseVisualStyleBackColor = true;
            // 
            // backupPeriodComboBox
            // 
            this.backupPeriodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.backupPeriodComboBox.FormattingEnabled = true;
            this.backupPeriodComboBox.Items.AddRange(new object[] {
            "Co trzy dni",
            "Co tydzień",
            "Co 10 dni",
            "Co miesiąc"});
            this.backupPeriodComboBox.Location = new System.Drawing.Point(246, 10);
            this.backupPeriodComboBox.Name = "backupPeriodComboBox";
            this.backupPeriodComboBox.Size = new System.Drawing.Size(121, 21);
            this.backupPeriodComboBox.TabIndex = 1;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 197);
            this.Controls.Add(this.backupPeriodComboBox);
            this.Controls.Add(this.autoBackupCheckBox);
            this.Name = "SettingsWindow";
            this.Text = "SettingsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autoBackupCheckBox;
        private System.Windows.Forms.ComboBox backupPeriodComboBox;
    }
}