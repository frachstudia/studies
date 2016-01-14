namespace TechPoznanNaprawy.Repair_Windows
{
    partial class EditRepairWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRepairWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.receiveCheckBox = new System.Windows.Forms.CheckBox();
            this.decisionCheckBox = new System.Windows.Forms.CheckBox();
            this.pricingCheckBox = new System.Windows.Forms.CheckBox();
            this.receiveTextBox = new System.Windows.Forms.TextBox();
            this.receiveDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.decisionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.pricingDatePicker = new System.Windows.Forms.DateTimePicker();
            this.decisionComboBox = new System.Windows.Forms.ComboBox();
            this.pricingTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.itemTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clientComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.repairDatePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pricingCommentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fabrnumTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fabrnumTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.receiveCheckBox);
            this.groupBox1.Controls.Add(this.decisionCheckBox);
            this.groupBox1.Controls.Add(this.pricingCheckBox);
            this.groupBox1.Controls.Add(this.receiveTextBox);
            this.groupBox1.Controls.Add(this.receiveDatePicker);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.decisionDatePicker);
            this.groupBox1.Controls.Add(this.pricingDatePicker);
            this.groupBox1.Controls.Add(this.decisionComboBox);
            this.groupBox1.Controls.Add(this.pricingTextBox);
            this.groupBox1.Controls.Add(this.descriptionTextBox);
            this.groupBox1.Controls.Add(this.itemTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(364, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 216);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Przedmiot";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Gwarancyjna",
            "Pozagwarancyjna",
            "Reklamacja"});
            this.typeComboBox.Location = new System.Drawing.Point(77, 15);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(139, 21);
            this.typeComboBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Typ:";
            // 
            // receiveCheckBox
            // 
            this.receiveCheckBox.AutoSize = true;
            this.receiveCheckBox.Enabled = false;
            this.receiveCheckBox.Location = new System.Drawing.Point(6, 190);
            this.receiveCheckBox.Name = "receiveCheckBox";
            this.receiveCheckBox.Size = new System.Drawing.Size(60, 17);
            this.receiveCheckBox.TabIndex = 12;
            this.receiveCheckBox.Text = "Odbiór:";
            this.receiveCheckBox.UseVisualStyleBackColor = true;
            this.receiveCheckBox.CheckedChanged += new System.EventHandler(this.receiveCheckBox_CheckedChanged);
            // 
            // decisionCheckBox
            // 
            this.decisionCheckBox.AutoSize = true;
            this.decisionCheckBox.Enabled = false;
            this.decisionCheckBox.Location = new System.Drawing.Point(6, 165);
            this.decisionCheckBox.Name = "decisionCheckBox";
            this.decisionCheckBox.Size = new System.Drawing.Size(67, 17);
            this.decisionCheckBox.TabIndex = 12;
            this.decisionCheckBox.Text = "Decyzja:";
            this.decisionCheckBox.UseVisualStyleBackColor = true;
            this.decisionCheckBox.CheckedChanged += new System.EventHandler(this.decisionCheckBox_CheckedChanged);
            // 
            // pricingCheckBox
            // 
            this.pricingCheckBox.AutoSize = true;
            this.pricingCheckBox.Location = new System.Drawing.Point(6, 140);
            this.pricingCheckBox.Name = "pricingCheckBox";
            this.pricingCheckBox.Size = new System.Drawing.Size(69, 17);
            this.pricingCheckBox.TabIndex = 12;
            this.pricingCheckBox.Text = "Wycena:";
            this.pricingCheckBox.UseVisualStyleBackColor = true;
            this.pricingCheckBox.CheckedChanged += new System.EventHandler(this.pricingCheckBox_CheckedChanged);
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.Enabled = false;
            this.receiveTextBox.Location = new System.Drawing.Point(227, 188);
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.Size = new System.Drawing.Size(140, 20);
            this.receiveTextBox.TabIndex = 13;
            // 
            // receiveDatePicker
            // 
            this.receiveDatePicker.Enabled = false;
            this.receiveDatePicker.Location = new System.Drawing.Point(79, 188);
            this.receiveDatePicker.Name = "receiveDatePicker";
            this.receiveDatePicker.Size = new System.Drawing.Size(142, 20);
            this.receiveDatePicker.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "zł";
            // 
            // decisionDatePicker
            // 
            this.decisionDatePicker.Enabled = false;
            this.decisionDatePicker.Location = new System.Drawing.Point(79, 163);
            this.decisionDatePicker.Name = "decisionDatePicker";
            this.decisionDatePicker.Size = new System.Drawing.Size(142, 20);
            this.decisionDatePicker.TabIndex = 10;
            this.decisionDatePicker.ValueChanged += new System.EventHandler(this.decisionDatePicker_ValueChanged);
            // 
            // pricingDatePicker
            // 
            this.pricingDatePicker.Enabled = false;
            this.pricingDatePicker.Location = new System.Drawing.Point(79, 138);
            this.pricingDatePicker.Name = "pricingDatePicker";
            this.pricingDatePicker.Size = new System.Drawing.Size(142, 20);
            this.pricingDatePicker.TabIndex = 9;
            this.pricingDatePicker.ValueChanged += new System.EventHandler(this.pricingDatePicker_ValueChanged);
            // 
            // decisionComboBox
            // 
            this.decisionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.decisionComboBox.Enabled = false;
            this.decisionComboBox.FormattingEnabled = true;
            this.decisionComboBox.Items.AddRange(new object[] {
            "",
            "Naprawa",
            "Zwrot"});
            this.decisionComboBox.Location = new System.Drawing.Point(227, 163);
            this.decisionComboBox.Name = "decisionComboBox";
            this.decisionComboBox.Size = new System.Drawing.Size(140, 21);
            this.decisionComboBox.TabIndex = 8;
            // 
            // pricingTextBox
            // 
            this.pricingTextBox.Enabled = false;
            this.pricingTextBox.Location = new System.Drawing.Point(227, 138);
            this.pricingTextBox.Name = "pricingTextBox";
            this.pricingTextBox.Size = new System.Drawing.Size(70, 20);
            this.pricingTextBox.TabIndex = 7;
            this.pricingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pricingTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pricingTextBox_KeyPress);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(77, 94);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(290, 38);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Text = "";
            // 
            // itemTextBox
            // 
            this.itemTextBox.Location = new System.Drawing.Point(77, 68);
            this.itemTextBox.Name = "itemTextBox";
            this.itemTextBox.Size = new System.Drawing.Size(290, 20);
            this.itemTextBox.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Opis:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Przedmiot:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(229, 243);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(129, 36);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Zapisz naprawę";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(394, 243);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(129, 36);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.clientComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.repairDatePicker);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 84);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Naprawa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Klient:";
            // 
            // clientComboBox
            // 
            this.clientComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientComboBox.FormattingEnabled = true;
            this.clientComboBox.Location = new System.Drawing.Point(87, 46);
            this.clientComboBox.Name = "clientComboBox";
            this.clientComboBox.Size = new System.Drawing.Size(244, 21);
            this.clientComboBox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Data naprawy:";
            // 
            // repairDatePicker
            // 
            this.repairDatePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.repairDatePicker.Location = new System.Drawing.Point(87, 20);
            this.repairDatePicker.Name = "repairDatePicker";
            this.repairDatePicker.Size = new System.Drawing.Size(150, 20);
            this.repairDatePicker.TabIndex = 13;
            this.repairDatePicker.ValueChanged += new System.EventHandler(this.repairDatePicker_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pricingCommentRichTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 126);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Komentarz (nie będzie drukowany)";
            // 
            // pricingCommentRichTextBox
            // 
            this.pricingCommentRichTextBox.Location = new System.Drawing.Point(6, 19);
            this.pricingCommentRichTextBox.Name = "pricingCommentRichTextBox";
            this.pricingCommentRichTextBox.Size = new System.Drawing.Size(334, 98);
            this.pricingCommentRichTextBox.TabIndex = 0;
            this.pricingCommentRichTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Nr fabr.";
            // 
            // fabrnumTextBox
            // 
            this.fabrnumTextBox.Location = new System.Drawing.Point(77, 43);
            this.fabrnumTextBox.Name = "fabrnumTextBox";
            this.fabrnumTextBox.Size = new System.Drawing.Size(139, 20);
            this.fabrnumTextBox.TabIndex = 17;
            // 
            // EditRepairWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 295);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditRepairWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Naprawa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditRepairWindow_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox decisionComboBox;
        private System.Windows.Forms.TextBox pricingTextBox;
        private System.Windows.Forms.RichTextBox descriptionTextBox;
        private System.Windows.Forms.TextBox itemTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker receiveDatePicker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker decisionDatePicker;
        private System.Windows.Forms.DateTimePicker pricingDatePicker;
        private System.Windows.Forms.TextBox receiveTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox clientComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker repairDatePicker;
        private System.Windows.Forms.CheckBox receiveCheckBox;
        private System.Windows.Forms.CheckBox decisionCheckBox;
        private System.Windows.Forms.CheckBox pricingCheckBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox pricingCommentRichTextBox;
        private System.Windows.Forms.TextBox fabrnumTextBox;
        private System.Windows.Forms.Label label3;
    }
}