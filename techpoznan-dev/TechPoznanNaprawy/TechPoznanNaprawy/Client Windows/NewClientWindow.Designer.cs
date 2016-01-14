namespace TechPoznanNaprawy
{
    partial class NewClientWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewClientWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.streetTextBox = new System.Windows.Forms.TextBox();
            this.post1TextBox = new System.Windows.Forms.TextBox();
            this.post2TextBox = new System.Windows.Forms.TextBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.telTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.addClientButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adres";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(121, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(164, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // streetTextBox
            // 
            this.streetTextBox.Location = new System.Drawing.Point(121, 37);
            this.streetTextBox.Name = "streetTextBox";
            this.streetTextBox.Size = new System.Drawing.Size(164, 20);
            this.streetTextBox.TabIndex = 3;
            // 
            // post1TextBox
            // 
            this.post1TextBox.Location = new System.Drawing.Point(121, 63);
            this.post1TextBox.MaxLength = 5;
            this.post1TextBox.Name = "post1TextBox";
            this.post1TextBox.Size = new System.Drawing.Size(21, 20);
            this.post1TextBox.TabIndex = 4;
            this.post1TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.post1TextBox_KeyDown);
            this.post1TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.post1TextBox_KeyPress);
            // 
            // post2TextBox
            // 
            this.post2TextBox.Location = new System.Drawing.Point(152, 63);
            this.post2TextBox.MaxLength = 5;
            this.post2TextBox.Name = "post2TextBox";
            this.post2TextBox.Size = new System.Drawing.Size(25, 20);
            this.post2TextBox.TabIndex = 6;
            this.post2TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.post2TextBox_KeyPress);
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(183, 63);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(102, 20);
            this.cityTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Poczta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Osoba kontaktowa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Nr telefonu";
            // 
            // telTextBox
            // 
            this.telTextBox.Location = new System.Drawing.Point(121, 115);
            this.telTextBox.MaxLength = 12;
            this.telTextBox.Name = "telTextBox";
            this.telTextBox.Size = new System.Drawing.Size(164, 20);
            this.telTextBox.TabIndex = 11;
            this.telTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.telTextBox_KeyPress);
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(121, 89);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(164, 20);
            this.emailTextBox.TabIndex = 12;
            this.emailTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.emailTextBox_KeyPress);
            // 
            // addClientButton
            // 
            this.addClientButton.Location = new System.Drawing.Point(68, 165);
            this.addClientButton.Name = "addClientButton";
            this.addClientButton.Size = new System.Drawing.Size(87, 33);
            this.addClientButton.TabIndex = 13;
            this.addClientButton.Text = "Dodaj";
            this.addClientButton.UseVisualStyleBackColor = true;
            this.addClientButton.Click += new System.EventHandler(this.addClientButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(171, 165);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 33);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "-";
            // 
            // NewClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 210);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addClientButton);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.telTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.post2TextBox);
            this.Controls.Add(this.post1TextBox);
            this.Controls.Add(this.streetTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewClientWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowy klient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox streetTextBox;
        private System.Windows.Forms.TextBox post1TextBox;
        private System.Windows.Forms.TextBox post2TextBox;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox telTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Button addClientButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label6;
    }
}