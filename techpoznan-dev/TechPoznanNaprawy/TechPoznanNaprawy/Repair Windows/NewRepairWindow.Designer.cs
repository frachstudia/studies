namespace TechPoznanNaprawy
{
    partial class NewRepairWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewRepairWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.clientComboBox = new System.Windows.Forms.ComboBox();
            this.addNewRepairButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.fabrnumTextBox = new System.Windows.Forms.TextBox();
            this.brandComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pricingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Klient";
            // 
            // clientComboBox
            // 
            this.clientComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clientComboBox.FormattingEnabled = true;
            this.clientComboBox.Location = new System.Drawing.Point(96, 38);
            this.clientComboBox.Name = "clientComboBox";
            this.clientComboBox.Size = new System.Drawing.Size(265, 21);
            this.clientComboBox.Sorted = true;
            this.clientComboBox.TabIndex = 2;
            // 
            // addNewRepairButton
            // 
            this.addNewRepairButton.Location = new System.Drawing.Point(112, 327);
            this.addNewRepairButton.Name = "addNewRepairButton";
            this.addNewRepairButton.Size = new System.Drawing.Size(95, 33);
            this.addNewRepairButton.TabIndex = 8;
            this.addNewRepairButton.Text = "Dodaj naprawę";
            this.addNewRepairButton.UseVisualStyleBackColor = true;
            this.addNewRepairButton.Click += new System.EventHandler(this.addNewRepairButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(239, 327);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 33);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(96, 12);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(149, 20);
            this.dateTimePicker.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Data";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.fabrnumTextBox);
            this.groupBox1.Controls.Add(this.brandComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.descriptionRichTextBox);
            this.groupBox1.Controls.Add(this.modelTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 200);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Przedmiot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Typ naprawy";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Gwarancyjna",
            "Pozagwarancyjna",
            "Reklamacja"});
            this.typeComboBox.Location = new System.Drawing.Point(84, 16);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 31;
            // 
            // fabrnumTextBox
            // 
            this.fabrnumTextBox.Location = new System.Drawing.Point(84, 43);
            this.fabrnumTextBox.Name = "fabrnumTextBox";
            this.fabrnumTextBox.Size = new System.Drawing.Size(159, 20);
            this.fabrnumTextBox.TabIndex = 28;
            // 
            // brandComboBox
            // 
            this.brandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brandComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.brandComboBox.FormattingEnabled = true;
            this.brandComboBox.Location = new System.Drawing.Point(84, 69);
            this.brandComboBox.Name = "brandComboBox";
            this.brandComboBox.Size = new System.Drawing.Size(97, 21);
            this.brandComboBox.Sorted = true;
            this.brandComboBox.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Nr fabr.";
            // 
            // descriptionRichTextBox
            // 
            this.descriptionRichTextBox.Location = new System.Drawing.Point(84, 96);
            this.descriptionRichTextBox.Name = "descriptionRichTextBox";
            this.descriptionRichTextBox.Size = new System.Drawing.Size(335, 98);
            this.descriptionRichTextBox.TabIndex = 29;
            this.descriptionRichTextBox.Text = "";
            // 
            // modelTextBox
            // 
            this.modelTextBox.Location = new System.Drawing.Point(188, 70);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(231, 20);
            this.modelTextBox.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Opis";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Przedmiot";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Data wyceny";
            // 
            // pricingDateTimePicker
            // 
            this.pricingDateTimePicker.Enabled = false;
            this.pricingDateTimePicker.Location = new System.Drawing.Point(136, 292);
            this.pricingDateTimePicker.Name = "pricingDateTimePicker";
            this.pricingDateTimePicker.Size = new System.Drawing.Size(172, 20);
            this.pricingDateTimePicker.TabIndex = 17;
            // 
            // NewRepairWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 383);
            this.Controls.Add(this.pricingDateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addNewRepairButton);
            this.Controls.Add(this.clientComboBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewRepairWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowe zlecenie naprawy";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox clientComboBox;
        private System.Windows.Forms.Button addNewRepairButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker pricingDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox fabrnumTextBox;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox descriptionRichTextBox;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}