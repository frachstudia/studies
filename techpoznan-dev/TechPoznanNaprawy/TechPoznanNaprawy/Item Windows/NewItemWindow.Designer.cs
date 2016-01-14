namespace TechPoznanNaprawy
{
    partial class NewItemWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewItemWindow));
            this.fabrnumTextBox = new System.Windows.Forms.TextBox();
            this.brandComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addItemButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fabrnumTextBox
            // 
            this.fabrnumTextBox.Location = new System.Drawing.Point(79, 51);
            this.fabrnumTextBox.Name = "fabrnumTextBox";
            this.fabrnumTextBox.Size = new System.Drawing.Size(159, 20);
            this.fabrnumTextBox.TabIndex = 17;
            // 
            // brandComboBox
            // 
            this.brandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brandComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.brandComboBox.FormattingEnabled = true;
            this.brandComboBox.Location = new System.Drawing.Point(79, 77);
            this.brandComboBox.Name = "brandComboBox";
            this.brandComboBox.Size = new System.Drawing.Size(72, 21);
            this.brandComboBox.Sorted = true;
            this.brandComboBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Nr fabr.";
            // 
            // descriptionRichTextBox
            // 
            this.descriptionRichTextBox.Location = new System.Drawing.Point(79, 104);
            this.descriptionRichTextBox.Name = "descriptionRichTextBox";
            this.descriptionRichTextBox.Size = new System.Drawing.Size(295, 45);
            this.descriptionRichTextBox.TabIndex = 18;
            this.descriptionRichTextBox.Text = "";
            // 
            // modelTextBox
            // 
            this.modelTextBox.Location = new System.Drawing.Point(162, 78);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(212, 20);
            this.modelTextBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Opis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Przedmiot";
            // 
            // addItemButton
            // 
            this.addItemButton.Location = new System.Drawing.Point(79, 155);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(87, 33);
            this.addItemButton.TabIndex = 20;
            this.addItemButton.Text = "Dodaj";
            this.addItemButton.UseVisualStyleBackColor = true;
            this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(220, 155);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 33);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Gwarancyjna",
            "Pozagwarancyjna",
            "Reklamacja"});
            this.typeComboBox.Location = new System.Drawing.Point(79, 24);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Typ naprawy";
            // 
            // NewItemWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 200);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addItemButton);
            this.Controls.Add(this.fabrnumTextBox);
            this.Controls.Add(this.brandComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptionRichTextBox);
            this.Controls.Add(this.modelTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewItemWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowy przedmiot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fabrnumTextBox;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox descriptionRichTextBox;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label1;
    }
}