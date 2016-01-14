using System;
using System.IO;
using System.Collections;
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
    public partial class NewRepairWindow : Form
    {
        private List<Repair> repairs;
        Settings settings;

        // for editing
        private bool isNew = true;
        private Repair newRepair;

        // strings to save in database
        List<Item> newItems = new List<Item>();
        private List<Item> editItems = new List<Item>();

        public NewRepairWindow(ref List<Repair> repairs, List<Client> clients, Settings settings)
        {
            this.newRepair = new Repair();
            this.settings = settings;
            this.newRepair.id = getNextId();
            this.repairs = repairs;

            InitializeComponent();

            this.brandComboBox.Items.AddRange(settings.brands.ToArray());
            this.brandComboBox.SelectedIndex = 0;
            this.typeComboBox.SelectedIndex = 0;
            foreach (Client client in clients)
                this.clientComboBox.Items.Add(client.name);
        }

        private string getNextId()
        {
            string[] id = settings.lastID.Split('/');

            if (int.Parse(id[1]) < DateTime.Today.Year)
            {
                id[0] = "1";
                id[1] = DateTime.Today.Year.ToString();
            }

            id[0] = (int.Parse(id[0]) + 1).ToString();

            return id[0] + "/" + id[1];
        }

        // ADD REPAIR
        private void addNewRepairButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.clientComboBox.Text))
            {
                MessageBox.Show("Pole klienta nie może zostać puste!");
                return;
            }

            if (string.IsNullOrEmpty(this.modelTextBox.Text))
            {
                MessageBox.Show("Pole przedmiotu nie może zostać puste!");
                return;
            }
            Item newItem = new Item();
            newItem.type = this.typeComboBox.Text;
            newItem.item = this.brandComboBox.Text + " " + this.modelTextBox.Text;
            newItem.description = this.descriptionRichTextBox.Text;
            newItem.fabrnum = this.fabrnumTextBox.Text;
            newRepair.client = this.clientComboBox.Text;
            newRepair.date = this.dateTimePicker.Value;
            newRepair.state = "Nowa";
            newRepair.items.Add(newItem);

            this.repairs.Add(newRepair);

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
