namespace TechPoznanNaprawy
{
    partial class NewMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clientTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.textRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Klient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adres E-mail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Temat";
            // 
            // clientTextBox
            // 
            this.clientTextBox.Location = new System.Drawing.Point(94, 23);
            this.clientTextBox.Name = "clientTextBox";
            this.clientTextBox.Size = new System.Drawing.Size(418, 20);
            this.clientTextBox.TabIndex = 3;
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(94, 49);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(418, 20);
            this.emailTextBox.TabIndex = 4;
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Location = new System.Drawing.Point(94, 75);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(418, 20);
            this.subjectTextBox.TabIndex = 5;
            // 
            // textRichTextBox
            // 
            this.textRichTextBox.Location = new System.Drawing.Point(32, 116);
            this.textRichTextBox.Name = "textRichTextBox";
            this.textRichTextBox.Size = new System.Drawing.Size(480, 227);
            this.textRichTextBox.TabIndex = 6;
            this.textRichTextBox.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(143, 349);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(111, 31);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Wyślij";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(295, 349);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(111, 31);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // NewMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 405);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.textRichTextBox);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.clientTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewMail";
            this.Text = "Nowy e-mail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox clientTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.RichTextBox textRichTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button cancelButton;
    }
}