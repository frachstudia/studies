namespace TechPoznanNaprawy
{
    partial class Printer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Printer));
            this.printerComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // printerComboBox
            // 
            this.printerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printerComboBox.FormattingEnabled = true;
            this.printerComboBox.Location = new System.Drawing.Point(39, 49);
            this.printerComboBox.Name = "printerComboBox";
            this.printerComboBox.Size = new System.Drawing.Size(207, 21);
            this.printerComboBox.TabIndex = 0;
            this.printerComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.printerComboBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zainstalowane drukarki";
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(39, 95);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(87, 33);
            this.printButton.TabIndex = 2;
            this.printButton.Text = "Drukuj";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(159, 95);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 33);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Printer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 142);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.printerComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Printer";
            this.Text = "Drukuj";
            this.Load += new System.EventHandler(this.Printer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox printerComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button cancelButton;
    }
}