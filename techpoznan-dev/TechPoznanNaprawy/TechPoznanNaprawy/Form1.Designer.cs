namespace TechPoznanNaprawy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRepairs = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pathButton = new System.Windows.Forms.Button();
            this.newRepairButton = new System.Windows.Forms.Button();
            this.repairsGrid = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fabrnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pricingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.newClientButton = new System.Windows.Forms.Button();
            this.clientsGrid = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.city = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.archiveGrid = new System.Windows.Forms.DataGridView();
            this.typearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nrArchive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fabrnumarch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateArchive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repairContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.edytujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przenieśDoArchiwumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.drukujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.edytujToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.edytujToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.umieśćWNaprawyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszZmianyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabRepairs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repairsGrid)).BeginInit();
            this.tabClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsGrid)).BeginInit();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.archiveGrid)).BeginInit();
            this.repairContextMenu.SuspendLayout();
            this.clientContextMenu.SuspendLayout();
            this.archiveContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabRepairs);
            this.tabControl1.Controls.Add(this.tabClients);
            this.tabControl1.Controls.Add(this.tabItems);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.MaximumSize = new System.Drawing.Size(1800, 1000);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(971, 419);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRepairs
            // 
            this.tabRepairs.Controls.Add(this.label5);
            this.tabRepairs.Controls.Add(this.label6);
            this.tabRepairs.Controls.Add(this.label3);
            this.tabRepairs.Controls.Add(this.label4);
            this.tabRepairs.Controls.Add(this.label2);
            this.tabRepairs.Controls.Add(this.label1);
            this.tabRepairs.Controls.Add(this.pathButton);
            this.tabRepairs.Controls.Add(this.newRepairButton);
            this.tabRepairs.Controls.Add(this.repairsGrid);
            this.tabRepairs.Location = new System.Drawing.Point(4, 22);
            this.tabRepairs.Name = "tabRepairs";
            this.tabRepairs.Padding = new System.Windows.Forms.Padding(3);
            this.tabRepairs.Size = new System.Drawing.Size(963, 393);
            this.tabRepairs.TabIndex = 0;
            this.tabRepairs.Text = "Naprawy";
            this.tabRepairs.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(550, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "wycenione bez decyzji (1 tydzień)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.MediumOrchid;
            this.label6.Location = new System.Drawing.Point(528, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "   ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(360, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "nie wycenione (2 tydzień)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(338, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "   ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(169, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "nie wycenione (1 tydzień)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(147, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "   ";
            // 
            // pathButton
            // 
            this.pathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pathButton.Location = new System.Drawing.Point(846, 356);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(111, 31);
            this.pathButton.TabIndex = 2;
            this.pathButton.Text = "Zmień ścieżkę";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // newRepairButton
            // 
            this.newRepairButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newRepairButton.Location = new System.Drawing.Point(6, 356);
            this.newRepairButton.Name = "newRepairButton";
            this.newRepairButton.Size = new System.Drawing.Size(111, 31);
            this.newRepairButton.TabIndex = 1;
            this.newRepairButton.Text = "Nowa naprawa";
            this.newRepairButton.UseVisualStyleBackColor = true;
            this.newRepairButton.Click += new System.EventHandler(this.newRepairButton_Click_1);
            // 
            // repairsGrid
            // 
            this.repairsGrid.AllowUserToAddRows = false;
            this.repairsGrid.AllowUserToDeleteRows = false;
            this.repairsGrid.AllowUserToOrderColumns = true;
            this.repairsGrid.AllowUserToResizeRows = false;
            this.repairsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.repairsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.repairsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.repairsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.nr,
            this.date,
            this.client,
            this.item,
            this.fabrnum,
            this.pricingDate,
            this.state});
            this.repairsGrid.Location = new System.Drawing.Point(0, 0);
            this.repairsGrid.MultiSelect = false;
            this.repairsGrid.Name = "repairsGrid";
            this.repairsGrid.ReadOnly = true;
            this.repairsGrid.RowHeadersVisible = false;
            this.repairsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.repairsGrid.Size = new System.Drawing.Size(963, 350);
            this.repairsGrid.TabIndex = 0;
            this.repairsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repairsGrid_KeyDown);
            this.repairsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.repairsGrid_MouseDown);
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.type.HeaderText = " ";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 20;
            // 
            // nr
            // 
            this.nr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nr.HeaderText = "Nr";
            this.nr.Name = "nr";
            this.nr.ReadOnly = true;
            this.nr.Width = 70;
            // 
            // date
            // 
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.date.FillWeight = 18.57013F;
            this.date.HeaderText = "Data";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 75;
            // 
            // client
            // 
            this.client.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.client.FillWeight = 22.81203F;
            this.client.HeaderText = "Klient";
            this.client.Name = "client";
            this.client.ReadOnly = true;
            this.client.Width = 200;
            // 
            // item
            // 
            this.item.FillWeight = 32.81203F;
            this.item.HeaderText = "Przedmiot(y)";
            this.item.Name = "item";
            this.item.ReadOnly = true;
            // 
            // fabrnum
            // 
            this.fabrnum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fabrnum.HeaderText = "Nr fabr.";
            this.fabrnum.Name = "fabrnum";
            this.fabrnum.ReadOnly = true;
            this.fabrnum.Width = 120;
            // 
            // pricingDate
            // 
            this.pricingDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pricingDate.FillWeight = 18.57013F;
            this.pricingDate.HeaderText = "Data wyceny";
            this.pricingDate.Name = "pricingDate";
            this.pricingDate.ReadOnly = true;
            this.pricingDate.Width = 75;
            // 
            // state
            // 
            this.state.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.state.HeaderText = "Status";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.newClientButton);
            this.tabClients.Controls.Add(this.clientsGrid);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(963, 393);
            this.tabClients.TabIndex = 1;
            this.tabClients.Text = "Klienci";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // newClientButton
            // 
            this.newClientButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newClientButton.Location = new System.Drawing.Point(6, 356);
            this.newClientButton.Name = "newClientButton";
            this.newClientButton.Size = new System.Drawing.Size(111, 31);
            this.newClientButton.TabIndex = 1;
            this.newClientButton.Text = "Nowy klient";
            this.newClientButton.UseVisualStyleBackColor = true;
            this.newClientButton.Click += new System.EventHandler(this.newClientButton_Click_1);
            // 
            // clientsGrid
            // 
            this.clientsGrid.AllowUserToAddRows = false;
            this.clientsGrid.AllowUserToDeleteRows = false;
            this.clientsGrid.AllowUserToOrderColumns = true;
            this.clientsGrid.AllowUserToResizeRows = false;
            this.clientsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.clientsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.address,
            this.code,
            this.city,
            this.email,
            this.telnum});
            this.clientsGrid.Location = new System.Drawing.Point(0, 0);
            this.clientsGrid.MultiSelect = false;
            this.clientsGrid.Name = "clientsGrid";
            this.clientsGrid.ReadOnly = true;
            this.clientsGrid.RowHeadersVisible = false;
            this.clientsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsGrid.Size = new System.Drawing.Size(963, 350);
            this.clientsGrid.TabIndex = 0;
            this.clientsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clientsGrid_MouseDown);
            // 
            // name
            // 
            this.name.FillWeight = 200F;
            this.name.HeaderText = "Nazwa";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // address
            // 
            this.address.FillWeight = 120F;
            this.address.HeaderText = "Adres";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // code
            // 
            this.code.FillWeight = 60F;
            this.code.HeaderText = "Kod";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // city
            // 
            this.city.FillWeight = 70F;
            this.city.HeaderText = "Miasto";
            this.city.Name = "city";
            this.city.ReadOnly = true;
            // 
            // email
            // 
            this.email.HeaderText = "Osoba kontaktowa";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // telnum
            // 
            this.telnum.FillWeight = 70F;
            this.telnum.HeaderText = "Numer telefonu";
            this.telnum.Name = "telnum";
            this.telnum.ReadOnly = true;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.archiveGrid);
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Name = "tabItems";
            this.tabItems.Size = new System.Drawing.Size(963, 393);
            this.tabItems.TabIndex = 2;
            this.tabItems.Text = "Archiwum";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // archiveGrid
            // 
            this.archiveGrid.AllowUserToAddRows = false;
            this.archiveGrid.AllowUserToDeleteRows = false;
            this.archiveGrid.AllowUserToOrderColumns = true;
            this.archiveGrid.AllowUserToResizeRows = false;
            this.archiveGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.archiveGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.archiveGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.archiveGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typearch,
            this.nrArchive,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.fabrnumarch,
            this.dataGridViewTextBoxColumn18,
            this.stateArchive});
            this.archiveGrid.Location = new System.Drawing.Point(0, 0);
            this.archiveGrid.MultiSelect = false;
            this.archiveGrid.Name = "archiveGrid";
            this.archiveGrid.ReadOnly = true;
            this.archiveGrid.RowHeadersVisible = false;
            this.archiveGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.archiveGrid.Size = new System.Drawing.Size(963, 350);
            this.archiveGrid.TabIndex = 2;
            this.archiveGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.archiveGrid_KeyDown);
            this.archiveGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.archiveGrid_MouseDown);
            // 
            // typearch
            // 
            this.typearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typearch.HeaderText = "";
            this.typearch.Name = "typearch";
            this.typearch.ReadOnly = true;
            this.typearch.Width = 20;
            // 
            // nrArchive
            // 
            this.nrArchive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nrArchive.HeaderText = "Nr";
            this.nrArchive.Name = "nrArchive";
            this.nrArchive.ReadOnly = true;
            this.nrArchive.Width = 70;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn13.FillWeight = 18.57013F;
            this.dataGridViewTextBoxColumn13.HeaderText = "Data";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 75;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn14.FillWeight = 22.81203F;
            this.dataGridViewTextBoxColumn14.HeaderText = "Klient";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 200;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.FillWeight = 32.81203F;
            this.dataGridViewTextBoxColumn15.HeaderText = "Przedmiot(y)";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // fabrnumarch
            // 
            this.fabrnumarch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fabrnumarch.HeaderText = "Nr fabr.";
            this.fabrnumarch.Name = "fabrnumarch";
            this.fabrnumarch.ReadOnly = true;
            this.fabrnumarch.Width = 120;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn18.FillWeight = 18.57013F;
            this.dataGridViewTextBoxColumn18.HeaderText = "Data wyceny";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Width = 75;
            // 
            // stateArchive
            // 
            this.stateArchive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.stateArchive.HeaderText = "Status";
            this.stateArchive.Name = "stateArchive";
            this.stateArchive.ReadOnly = true;
            // 
            // repairContextMenu
            // 
            this.repairContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edytujToolStripMenuItem,
            this.przenieśDoArchiwumToolStripMenuItem,
            this.usuńToolStripMenuItem,
            this.toolStripSeparator1,
            this.drukujToolStripMenuItem});
            this.repairContextMenu.Name = "repairContextMenu";
            this.repairContextMenu.ShowImageMargin = false;
            this.repairContextMenu.Size = new System.Drawing.Size(121, 98);
            // 
            // edytujToolStripMenuItem
            // 
            this.edytujToolStripMenuItem.Name = "edytujToolStripMenuItem";
            this.edytujToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.edytujToolStripMenuItem.Text = "Edytuj";
            this.edytujToolStripMenuItem.Click += new System.EventHandler(this.edytujToolStripMenuItem_Click);
            // 
            // przenieśDoArchiwumToolStripMenuItem
            // 
            this.przenieśDoArchiwumToolStripMenuItem.Name = "przenieśDoArchiwumToolStripMenuItem";
            this.przenieśDoArchiwumToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.przenieśDoArchiwumToolStripMenuItem.Text = "Do archiwum";
            this.przenieśDoArchiwumToolStripMenuItem.Click += new System.EventHandler(this.przenieśDoArchiwumToolStripMenuItem_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.usuńToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // drukujToolStripMenuItem
            // 
            this.drukujToolStripMenuItem.Name = "drukujToolStripMenuItem";
            this.drukujToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.drukujToolStripMenuItem.Text = "Drukuj";
            this.drukujToolStripMenuItem.Click += new System.EventHandler(this.drukujToolStripMenuItem_Click);
            // 
            // clientContextMenu
            // 
            this.clientContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edytujToolStripMenuItem1,
            this.usuńToolStripMenuItem1});
            this.clientContextMenu.Name = "clientContextMenu";
            this.clientContextMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // edytujToolStripMenuItem1
            // 
            this.edytujToolStripMenuItem1.Name = "edytujToolStripMenuItem1";
            this.edytujToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.edytujToolStripMenuItem1.Text = "Edytuj";
            this.edytujToolStripMenuItem1.Click += new System.EventHandler(this.edytujToolStripMenuItem1_Click);
            // 
            // usuńToolStripMenuItem1
            // 
            this.usuńToolStripMenuItem1.Name = "usuńToolStripMenuItem1";
            this.usuńToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.usuńToolStripMenuItem1.Text = "Usuń";
            this.usuńToolStripMenuItem1.Click += new System.EventHandler(this.usuńToolStripMenuItem1_Click);
            // 
            // archiveContextMenu
            // 
            this.archiveContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edytujToolStripMenuItem2,
            this.usuńToolStripMenuItem2,
            this.umieśćWNaprawyToolStripMenuItem});
            this.archiveContextMenu.Name = "archiveContextMenu";
            this.archiveContextMenu.Size = new System.Drawing.Size(140, 70);
            // 
            // edytujToolStripMenuItem2
            // 
            this.edytujToolStripMenuItem2.Name = "edytujToolStripMenuItem2";
            this.edytujToolStripMenuItem2.Size = new System.Drawing.Size(139, 22);
            this.edytujToolStripMenuItem2.Text = "Edytuj";
            this.edytujToolStripMenuItem2.Click += new System.EventHandler(this.edytujToolStripMenuItem2_Click);
            // 
            // usuńToolStripMenuItem2
            // 
            this.usuńToolStripMenuItem2.Name = "usuńToolStripMenuItem2";
            this.usuńToolStripMenuItem2.Size = new System.Drawing.Size(139, 22);
            this.usuńToolStripMenuItem2.Text = "Usuń";
            this.usuńToolStripMenuItem2.Click += new System.EventHandler(this.usuńToolStripMenuItem2_Click);
            // 
            // umieśćWNaprawyToolStripMenuItem
            // 
            this.umieśćWNaprawyToolStripMenuItem.Name = "umieśćWNaprawyToolStripMenuItem";
            this.umieśćWNaprawyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.umieśćWNaprawyToolStripMenuItem.Text = "Do Naprawy";
            this.umieśćWNaprawyToolStripMenuItem.Click += new System.EventHandler(this.umieśćWNaprawyToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(995, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszZmianyToolStripMenuItem,
            this.ustawieniaToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // zapiszZmianyToolStripMenuItem
            // 
            this.zapiszZmianyToolStripMenuItem.Enabled = false;
            this.zapiszZmianyToolStripMenuItem.Name = "zapiszZmianyToolStripMenuItem";
            this.zapiszZmianyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.zapiszZmianyToolStripMenuItem.Text = "Zapisz zmiany";
            this.zapiszZmianyToolStripMenuItem.Click += new System.EventHandler(this.zapiszZmianyToolStripMenuItem_Click);
            // 
            // ustawieniaToolStripMenuItem
            // 
            this.ustawieniaToolStripMenuItem.Name = "ustawieniaToolStripMenuItem";
            this.ustawieniaToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            this.ustawieniaToolStripMenuItem.Click += new System.EventHandler(this.ustawieniaToolStripMenuItem_Click);
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.zakończToolStripMenuItem.Text = "Zakończ";
            this.zakończToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(946, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "v 1.10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 458);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 300);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tech Poznań Naprawy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabRepairs.ResumeLayout(false);
            this.tabRepairs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repairsGrid)).EndInit();
            this.tabClients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientsGrid)).EndInit();
            this.tabItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.archiveGrid)).EndInit();
            this.repairContextMenu.ResumeLayout(false);
            this.clientContextMenu.ResumeLayout(false);
            this.archiveContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRepairs;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.DataGridView repairsGrid;
        private System.Windows.Forms.DataGridView clientsGrid;
        private System.Windows.Forms.DataGridView archiveGrid;
        private System.Windows.Forms.Button newRepairButton;
        private System.Windows.Forms.Button newClientButton;
        private System.Windows.Forms.ContextMenuStrip repairContextMenu;
        private System.Windows.Forms.ToolStripMenuItem edytujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przenieśDoArchiwumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem drukujToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip clientContextMenu;
        private System.Windows.Forms.ToolStripMenuItem edytujToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip archiveContextMenu;
        private System.Windows.Forms.ToolStripMenuItem umieśćWNaprawyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem2;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszZmianyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawieniaToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem edytujToolStripMenuItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn nr;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn client;
        private System.Windows.Forms.DataGridViewTextBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn fabrnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn pricingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn typearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrArchive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn fabrnumarch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateArchive;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn city;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn telnum;
    }
}

