using System;
using System.IO;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechPoznanNaprawy.Database;

namespace TechPoznanNaprawy
{
    public partial class Printer : Form
    {
        private string _header = "TECH POZNAŃ\nSz. Orszulak, G. Bręcz Spółka Jawna\nul. Głuchowska 1\n60-101 Poznań\ntel./fax 61 6616050\ntel./fax 61 8833869\nNIP: 7792333375\nbiuro@tech-poznan.pl";
        private string _footer = "W przypadku rezygnacji z naprawy przez zlecającego nie wykonujemy montażu, " +
            "maszynę wydajemy w częściach. Nie odebranie sprzętu w ciągu trzech miesięcy od daty przyjęcia do naprawy, " +
            "zgodnie z art. 180 K.C. w związku z art. 60 K.C. Serwis Tech potraktuje jako wolę wyzbycia się własności, " +
            "co skutkuje uznaniem sprzętu za porzucony.";

        private Client client;
        private List<Item> itemsToPrint = new List<Item>();
        private Repair repair;

        public Printer(Client client, Repair repair)
        {
            this.client = client;
            this.repair = repair;

            InitializeComponent();
        }

        private void print()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerComboBox.SelectedItem.ToString();
            pd.PrintPage += new PrintPageEventHandler (pd_PrintPage);
            pd.Print();
        }

        private void printItems(Graphics g, Font font, Brush brush, int height)
        {
            for (int i = 0; i < repair.items.Count; i++)
            {
                this.doublePrint(g, repair.items[i].type, font, brush, new Rectangle(160, 260 + i * 17, 130, 15), height);
                this.doublePrint(g, repair.items[i].item, font, brush, new Rectangle(300, 260 + i * 17, 270, 15), height);
                this.doublePrint(g, repair.items[i].description, font, brush, new Rectangle(580, 260 + i * 17, 200, 15), height);
            }

            this.doublePrint(g, this.repair.date.ToString().Substring(0,10), font, brush, 
                new Rectangle(50, 260, 100, 15), height);
        }

        private void doublePrint(Graphics g, string text, Font font, Brush brush, Rectangle rect, int height)
        {
            g.DrawString(text, font, brush, rect);
            g.DrawString(text, font, brush, new Rectangle(rect.Location.X, rect.Location.Y + (int) height/2, rect.Width, rect.Height));
        }

        public void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int height = ev.PageBounds.Height;
            int width = ev.PageBounds.Width;

            Graphics g = ev.Graphics;
            
            Font headerFont = new Font("Arial", 10);
            Font headerBoldFont = new Font("Arial", 10, FontStyle.Bold);
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            Font textFont = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            string contactPerson = "";

            if (!string.IsNullOrEmpty(client.email) || !string.IsNullOrEmpty(client.telnum))
                contactPerson = client.email + " tel.: " + client.telnum;

            // drawing horizontal line
            g.DrawString("------------------------------------------------------------------------------------------------" + 
                "------------------------------------------------------------------------------------", headerFont, brush, -5, (int)height / 2);

            // header
            this.doublePrint(g, this._header, headerFont, brush, new Rectangle(40, 40, 250, 140), height);

            string monthFilling = (DateTime.Today.Month > 9) ? "" : "0";

            // city and date
            this.doublePrint(g, "Poznań, " + DateTime.Today.Day + "." + monthFilling + DateTime.Today.Month + "." + DateTime.Today.Year + "r.", 
                headerFont, brush, new Rectangle(width - 170, 20, 150, 30), height);

            // page title
            this.doublePrint(g, "Zlecenie naprawy nr " + this.repair.id, titleFont, brush, 
                new Rectangle((int)width / 2 - 140, 190, 350, 30), height);

            // client's data
            this.doublePrint(g, "Zleceniodawca:\n" + this.client.name + "\nul. " + this.client.address + "\n" + this.client.code + " " +
                this.client.city + "\n" + this.client.telnum, 
                headerFont, brush, new Rectangle(width - 300, 50, 260, 110), height);

            // items headers
            this.doublePrint(g, "Data przyjęcia:", headerBoldFont, brush, new Rectangle(83, 230, 110, 20), height);
            this.doublePrint(g, "Rodzaj naprawy:", headerBoldFont, brush, new Rectangle(70, 250, 130, 20), height);
            this.doublePrint(g, "Osoba kontaktowa:", headerBoldFont, brush, new Rectangle(52, 270, 150, 20), height);
            this.doublePrint(g, "Przedmiot:", headerBoldFont, brush, new Rectangle(108, 290, 270, 20), height);
            this.doublePrint(g, "Nr fabr.:", headerBoldFont, brush, new Rectangle(124, 310, 270, 20), height);
            this.doublePrint(g, "Opis:", headerBoldFont, brush, new Rectangle(144, 330, 230, 20), height);

            //items
            //this.printItems(g, headerFont, brush, height);
            this.doublePrint(g, repair.date.ToString().Substring(0, 10), headerFont, brush, new Rectangle(195, 231, 510, 20), height);
            this.doublePrint(g, repair.items[0].type, headerFont, brush, new Rectangle(195, 251, 600, 20), height);
            this.doublePrint(g, contactPerson, headerFont, brush, new Rectangle(195, 271, 600, 20), height);
            this.doublePrint(g, repair.items[0].item, headerFont, brush, new Rectangle(195, 292, 600, 20), height);
            this.doublePrint(g, repair.items[0].fabrnum, headerFont, brush, new Rectangle(195, 311, 600, 20), height);
            this.doublePrint(g, repair.items[0].description, headerFont, brush, new Rectangle(195, 331, 600, 80), height);

            this.doublePrint(g, this._footer, headerFont, brush, new Rectangle(50, 412, width - 100, 80), height);
            this.doublePrint(g, "..........................................\n    Podpis zleceniodawcy", headerFont, brush,
                new Rectangle(width - 260, 510, 200, 50), height);

            this.Close();
        }

        private void printerComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.print();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            this.print();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Printer_Load(object sender, EventArgs e)
        {
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Nie znaleziono żadnej drukarki!");
                return;
            }

            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                printerComboBox.Items.Add(printer.ToString());
            }

            this.printerComboBox.SelectedIndex = 0;
        }
    }
}
