using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechPoznanNaprawy.Database;
using System.Xml.Serialization;
using TechPoznanNaprawy.Repair_Windows;

namespace TechPoznanNaprawy
{
    public partial class Form1 : Form
    {
        public List<Repair> archiveRepairs = new List<Repair>();
        public List<Repair> repairs = new List<Repair>();
        public List<Client> clients = new List<Client>();
        public Settings settings = new Settings();

        public XmlSerializer archiveSerializer = new XmlSerializer(typeof(List<Repair>));
        public XmlSerializer repairSerializer = new XmlSerializer(typeof(List<Repair>));
        public XmlSerializer clientSerializer = new XmlSerializer(typeof(List<Client>));
        public XmlSerializer settingsSerializer = new XmlSerializer(typeof(Settings));

        public String path;
        private bool isSaved = true;

        // TODO Ustawienia np. częstość backupów, dodawanie nowych firm

        public Form1()
        {
            // Creating controller and taking path to database
            this.readFromRegistry();

            InitializeComponent();

            this.loadDataFromFiles();
            this.loadTables();
        }

        // Loading XML to database
        private void loadDataFromFiles()
        {
            this.repairs.Clear();
            this.clients.Clear();

            try
            {
                // Load clients
                TextReader textReader = new StreamReader(path + @"\klienci.xml");
                clients = (List<Client>)clientSerializer.Deserialize(textReader);

                // Load repairs
                textReader = new StreamReader(path + @"\naprawy.xml");
                repairs = (List<Repair>)repairSerializer.Deserialize(textReader);

                // Load settings
                textReader = new StreamReader(path + @"\ustawienia.xml");
                settings = (Settings)settingsSerializer.Deserialize(textReader);

                // Load archive repairs
                textReader = new StreamReader(path + @"\archiwum.xml");
                archiveRepairs = (List<Repair>)archiveSerializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().Message.ToString() + "\nCzy jesteś pewien, że podana ścieżka jest dobra?");
            }
        }

        // Saving database to XML
        private void saveDataToFiles()
        {
            try
            {
                // Save clients
                TextWriter textWriter = new StreamWriter(path + @"\klienci.xml");
                clientSerializer.Serialize(textWriter, clients);

                // Save repairs
                textWriter = new StreamWriter(path + @"\naprawy.xml");
                repairSerializer.Serialize(textWriter, repairs);

                // Save settings
                textWriter = new StreamWriter(path + @"\ustawienia.xml");
                settingsSerializer.Serialize(textWriter, settings);

                // Save archive repairs
                textWriter = new StreamWriter(path + @"\archiwum.xml");
                archiveSerializer.Serialize(textWriter, archiveRepairs);

                textWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().Message.ToString() + "\nCzy jesteś pewien, że podana ścieżka jest dobra?");
            }
        }

        // Loading database to grid
        private void loadTables()
        {
            int clientSelectedIndex = 0;
            int repairsSelectedIndex = 0;
            int archiveSelectedIndex = 0;

            int clientFirstRow = 0;
            int repairsFirstRow = 0;
            int archiveFirstRow = 0;

            // Load clients
            if (clientsGrid.Rows.Count > 0)
            {
                clientSelectedIndex = clientsGrid.SelectedRows[0].Index;
                clientFirstRow = clientsGrid.FirstDisplayedScrollingRowIndex;
            }

            this.clientsGrid.Rows.Clear();

            foreach (Client cli in this.clients)
                this.clientsGrid.Rows.Add(cli.name, cli.address, cli.code, cli.city, cli.email, cli.telnum);

            clientSelectedIndex = (clientSelectedIndex == clientsGrid.Rows.Count) ? clientSelectedIndex - 1 : clientSelectedIndex;

            if (clientsGrid.Rows.Count > 0)
            {
                clientsGrid.FirstDisplayedScrollingRowIndex = clientFirstRow;
                clientsGrid.Rows[clientSelectedIndex].Selected = true;
            }

            // Load repairs
            if (repairsGrid.Rows.Count > 0)
            {
                repairsSelectedIndex = repairsGrid.SelectedRows[0].Index;
                repairsFirstRow = repairsGrid.FirstDisplayedScrollingRowIndex;
            }

            if (archiveGrid.Rows.Count > 0)
            {
                archiveSelectedIndex = archiveGrid.SelectedRows[0].Index;
                archiveFirstRow = archiveGrid.FirstDisplayedScrollingRowIndex;
            }

            this.repairsGrid.Rows.Clear();
            this.archiveGrid.Rows.Clear();

            foreach (Repair rep in this.repairs)
            {
                string type = "G";
                switch (rep.items[0].type)
                {
                    case "Reklamacja":
                        type = "R";
                        break;
                    case "Pozagwarancyjna":
                        type = "P";
                        break;
                    default:
                        break;
                }
                if (rep.pricingDate > new DateTime(2000, 1, 1))
                    this.repairsGrid.Rows.Add(type, rep.id, rep.date.ToString().Substring(0, 10), rep.client,
                        rep.getItemsShortcut(), rep.items[0].fabrnum, rep.pricingDate.ToString().Substring(0, 10), rep.state);
                else
                    this.repairsGrid.Rows.Add(type, rep.id, rep.date.ToString().Substring(0, 10), rep.client,
                        rep.getItemsShortcut(), rep.items[0].fabrnum, "", rep.state);
            }

            foreach (Repair rep in this.archiveRepairs)
            {
                string type = "G";
                switch (rep.items[0].type)
                {
                    case "Reklamacja":
                        type = "R";
                        break;
                    case "Pozagwarancyjna":
                        type = "P";
                        break;
                    default:
                        break;
                }
                if (rep.pricingDate > new DateTime(2000, 1, 1))
                    this.archiveGrid.Rows.Add(type, rep.id, rep.date.ToString().Substring(0, 10), rep.client,
                        rep.getItemsShortcut(), rep.items[0].fabrnum, rep.pricingDate.ToString().Substring(0, 10), rep.state);
                else
                    this.archiveGrid.Rows.Add(type, rep.id, rep.date.ToString().Substring(0, 10), rep.client,
                        rep.getItemsShortcut(), rep.items[0].fabrnum, "", rep.state);
            }

            repairsSelectedIndex = (repairsSelectedIndex == repairsGrid.Rows.Count) ? repairsSelectedIndex - 1 : repairsSelectedIndex;
            archiveSelectedIndex = (archiveSelectedIndex == archiveGrid.Rows.Count) ? archiveSelectedIndex - 1 : archiveSelectedIndex;

            if (repairsGrid.Rows.Count > 0)
            {
                repairsGrid.FirstDisplayedScrollingRowIndex = repairsFirstRow;
                repairsGrid.Rows[repairsSelectedIndex].Selected = true;
            }

            if (archiveGrid.Rows.Count > 0)
            {
                archiveGrid.FirstDisplayedScrollingRowIndex = archiveFirstRow;
                archiveGrid.Rows[archiveSelectedIndex].Selected = true;
            }

            this.makeColors();
        }

        // Writing in red font
        private void makeColors()
        {
            foreach (Repair repair in repairs)
            {
                if (repair.state != "Wycenione" && repair.state != "Nowa")
                    continue;

                for (int j = 0; j < this.repairsGrid.Rows.Count; j++)
                {
                    if (repair.id != this.repairsGrid.Rows[j].Cells[1].Value.ToString())
                        continue;

                    if (repair.state == "Nowa" && (DateTime.Now - repair.date).TotalDays > 7)
                        this.repairsGrid.Rows[j].DefaultCellStyle.ForeColor = Color.DarkOrange;

                    if (repair.state == "Nowa" && (DateTime.Now - repair.date).TotalDays > 14)
                        this.repairsGrid.Rows[j].DefaultCellStyle.ForeColor = Color.Red;

                    if (repair.state == "Wycenione" && (DateTime.Now - repair.pricingDate).TotalDays > 7)
                        this.repairsGrid.Rows[j].DefaultCellStyle.ForeColor = Color.MediumOrchid;
                }
            }
        }

        // Ładowanie z rejestru
        private void readFromRegistry()
        {
            this.path = "";

            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE");
                String[] subkeys = rk.GetSubKeyNames();

                if (subkeys.Contains<String>("TechPoznan"))
                    rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\TechPoznan");
                else
                {
                    rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TechPoznan");
                    rk.SetValue("Ścieżka do bazy danych", "");
                }

                if (rk.GetValue("Ścieżka do bazy danych").ToString().Length > 0)
                    this.path = rk.GetValue("Ścieżka do bazy danych").ToString();
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();

                    MessageBox.Show("Rosija");
                    for (; ; )
                    {
                        dialog.ShowDialog();
                        this.path = dialog.SelectedPath;

                        if (!String.IsNullOrEmpty(this.path))
                            break;
                        else
                            MessageBox.Show("Nie wybrano ścieżki.");
                    }

                    rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TechPoznan");
                    rk.SetValue("Ścieżka do bazy danych", this.path);
                }

                rk.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nNastąpi wyjście z programu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        public void SendBackupMail()
        {
            string BackupUsername = "techpoznan@gmail.com";
            string BackupPassword = "tech-poznan-sc";
            string BackupSmtp = "smtp.gmail.com";

            try
            {
                var loginInfo = new NetworkCredential(BackupUsername, BackupPassword);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient(BackupSmtp, 587);

                msg.From = new MailAddress(BackupUsername);
                msg.To.Add(new MailAddress("techpoznan@gmail.com"));
                msg.Subject = "Backup danych serwisowych z dnia " + DateTime.Today.ToString().Substring(0, 10) + ".";
                msg.Attachments.Add(new Attachment(path + "\\naprawy.xml"));
                msg.Attachments.Add(new Attachment(path + "\\klienci.xml"));
                msg.Attachments.Add(new Attachment(path + "\\ustawienia.xml"));
                msg.Attachments.Add(new Attachment(path + "\\archiwum.xml"));

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);

                MessageBox.Show("Kopia zapasowa danych została wysłana.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // MOVING TO ARCHIVE
        private void przenieśDoArchiwumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repair repair = Repair.getRepairFromList(this.repairsGrid.SelectedCells[1].Value.ToString(), repairs);
            archiveRepairs.Add(repair);
            repairs.Remove(repair);

            archiveRepairs = archiveRepairs.OrderBy(rep =>
                int.Parse(rep.id.Substring(rep.id.Length - 4))).ThenBy(rep => int.Parse(rep.id.Substring(0, rep.id.IndexOf('/')))).ToList();

            isSaved = false;
            zapiszZmianyToolStripMenuItem.Enabled = true;

            this.loadTables();
        }

        // MOVING TO REPAIRS
        private void umieśćWNaprawyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repair repair = Repair.getRepairFromList(this.archiveGrid.SelectedCells[1].Value.ToString(), archiveRepairs);
            repairs.Add(repair);
            archiveRepairs.Remove(repair);

            repairs = repairs.OrderBy(rep => 
                int.Parse(rep.id.Substring(rep.id.Length - 4))).ThenBy(rep => int.Parse(rep.id.Substring(0, rep.id.IndexOf('/')))).ToList();

            isSaved = false;
            zapiszZmianyToolStripMenuItem.Enabled = true;

            this.loadTables();
        }

        // PRINT
        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Printer(Client.getClientFromList(this.repairsGrid.SelectedCells[3].Value.ToString(), clients),
                    Repair.getRepairFromList(this.repairsGrid.SelectedCells[1].Value.ToString(), repairs)).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // RIGHT CLICK IN REPAIRS
        private void repairsGrid_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                this.repairContextMenu.Items[i].Enabled = true;

            var hti = repairsGrid.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0)
            {
                repairsGrid.Rows[hti.RowIndex].Selected = true;

                if (e.Button == MouseButtons.Right)
                {
                    repairsGrid.Rows[hti.RowIndex].ContextMenuStrip = this.repairContextMenu;
                }
            }
        }

        // RIGHT CLICK IN CLIENTS
        private void clientsGrid_MouseDown(object sender, MouseEventArgs e)
        {
            var hti = clientsGrid.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0)
            {
                clientsGrid.Rows[hti.RowIndex].Selected = true;

                if (e.Button == MouseButtons.Right)
                    clientsGrid.Rows[hti.RowIndex].ContextMenuStrip = this.clientContextMenu;
            }
        }

        private void archiveGrid_MouseDown(object sender, MouseEventArgs e)
        {
            var hti = archiveGrid.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0)
            {
                archiveGrid.Rows[hti.RowIndex].Selected = true;

                if (e.Button == MouseButtons.Right)
                    archiveGrid.Rows[hti.RowIndex].ContextMenuStrip = this.archiveContextMenu;
            }
        }

        // Delete repair on "Delete" button
        private void repairsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && repairs.Count > 0)
            {
                repairs.Remove(Repair.getRepairFromList(this.repairsGrid.SelectedCells[1].Value.ToString(), repairs));

                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                loadTables();
            }
        }

        private void archiveGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && archiveRepairs.Count > 0)
            {
                archiveRepairs.Remove(Repair.getRepairFromList(this.archiveGrid.SelectedCells[1].Value.ToString(), archiveRepairs));

                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                loadTables();
            }
        }

        // DELETE REPAIR
        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć to zlecenie naprawy?", "Usuń zlecenie naprawy",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            repairs.Remove(Repair.getRepairFromList(this.repairsGrid.SelectedCells[1].Value.ToString(), repairs));

            isSaved = false;
            zapiszZmianyToolStripMenuItem.Enabled = true;

            loadTables();
        }

        // DELETE ARCHIVAL REPAIR
        private void usuńToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć to z archiwum?", "Usuń zlecenie naprawy",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            repairs.Remove(Repair.getRepairFromList(this.archiveGrid.SelectedCells[1].Value.ToString(), repairs));

            isSaved = false;
            zapiszZmianyToolStripMenuItem.Enabled = true;

            loadTables();
        }

        // NEW REPAIR
        private void newRepairButton_Click_1(object sender, EventArgs e)
        {
            DialogResult result = new NewRepairWindow(ref this.repairs, this.clients, this.settings).ShowDialog();

            if (result == DialogResult.OK)
            {
                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                // Updating lastID
                this.settings.lastID = this.repairs.Last().id;
                this.loadTables();
            }
        }

        // EDIT REPAIR
        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repair repair = Repair.getRepairFromList(this.repairsGrid.SelectedCells[1].Value.ToString(), repairs);

            DialogResult result = new EditRepairWindow(repair, clients).ShowDialog();

            if (result == DialogResult.OK)
            {
                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                this.loadTables();
            }
        }

        // EDIT ARCHIVE REPAIR
        private void edytujToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Repair repair = Repair.getRepairFromList(this.archiveGrid.SelectedCells[1].Value.ToString(), archiveRepairs);

            DialogResult result = new EditRepairWindow(repair, clients).ShowDialog();

            if (result == DialogResult.OK)
            {
                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                this.loadTables();
            }
        }

        // CHOOSING PATH
        private void pathButton_Click(object sender, EventArgs e)
        {
            this.path = "";

            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE");
                String[] subkeys = rk.GetSubKeyNames();


                FolderBrowserDialog dialog = new FolderBrowserDialog();

                for (; ; )
                {
                    dialog.ShowDialog();
                    this.path = dialog.SelectedPath;

                    if (!String.IsNullOrEmpty(this.path))
                        break;
                    else
                        MessageBox.Show("Nie wybrano ścieżki.");
                }

                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TechPoznan");
                rk.SetValue("Ścieżka do bazy danych", this.path);

                rk.Close();

                loadDataFromFiles();
                loadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nNastąpi wyjście z programu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CLOSING
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("Zmiany nie zostały zapisane.\nCzy zapisać zmiany?",
                "Zapisz zmiany", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    saveDataToFiles();
                    isSaved = true;
                    return;
                }

                if (result == DialogResult.No)
                    return;

                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }

            // TODO BACKUP
            /*
            if ((DateTime.Today - settings.lastBackup).TotalDays >= 7)
            {
                SendBackupMail();
            }
             * */
        }

        // NEW CLIENT
        private void newClientButton_Click_1(object sender, EventArgs e)
        {
            DialogResult result = new NewClientWindow(ref this.clients, clients.Count.ToString()).ShowDialog();

            if (result == DialogResult.OK)
            {
                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                this.loadTables();
            }
        }

        // EDIT CLIENT
        private void edytujToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Client selectedClient = Client.getClientFromList(this.clientsGrid.SelectedCells[0].Value.ToString(), clients);

            DialogResult result = new NewClientWindow(ref this.clients, ref selectedClient).ShowDialog();

            if (result == DialogResult.OK)
            {
                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;

                this.loadTables();
            }
        }

        // DELETE CLIENT
        private void usuńToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć klienta " +
                this.clientsGrid.SelectedCells[0].Value.ToString() +
                "?", "Usuń klienta.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            clients.Remove(Client.getClientFromList(this.clientsGrid.SelectedCells[0].Value.ToString(), clients));

            isSaved = false;
            zapiszZmianyToolStripMenuItem.Enabled = true;

            loadTables();
        }

        // FAST DELETE CLIENT
        private void clientsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                clients.Remove(Client.getClientFromList(this.clientsGrid.SelectedCells[0].Value.ToString(), clients));

                isSaved = false;
                zapiszZmianyToolStripMenuItem.Enabled = true;
                this.loadTables();
            }
        }

        private void zapiszZmianyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved)
                return;

            DialogResult result = MessageBox.Show("Czy na pewno chcesz zapisać zmiany?\nWszystkie nadpisane dane zostaną utracone.", 
                "Zapisz zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                saveDataToFiles();
                isSaved = true;
                zapiszZmianyToolStripMenuItem.Enabled = false;
            }
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new SettingsWindow().ShowDialog();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
