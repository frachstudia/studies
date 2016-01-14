using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TechPoznanNaprawy.Database;

namespace TechPoznanNaprawy.Repair_Windows
{
    public partial class EditRepairWindow : Form
    {
        Repair oldRepair;
        Repair actualRepair;
        Item actualItem;

        public EditRepairWindow(Repair repair, List<Client> clients)
        {
            oldRepair = new Repair(repair.id, repair.date, repair.client, repair.pricingDate, repair.state, repair.archive, repair.pricingComment);
            oldRepair.items = new List<Item>(repair.items.Count);

            foreach (Item item in repair.items)
                oldRepair.items.Add(new Item(item.repairId, item.id, item.type, item.item, item.description,
                    item.price, item.decision, item.decisionDate, item.receiver, item.receiveDate, item.fabrnum));

            actualRepair = repair;
            actualItem = repair.items[0];

            InitializeComponent();
            init(clients);
        }

        // Wprowadzanie danych ze struktury items[i] do formularza
        private void fillItemProperties()
        {
            this.typeComboBox.Text = actualItem.type;
            this.itemTextBox.Text = actualItem.item;
            this.descriptionTextBox.Text = actualItem.description;
            this.fabrnumTextBox.Text = actualItem.fabrnum;

            if (!String.IsNullOrEmpty(actualItem.price) || !String.IsNullOrEmpty(actualItem.decision) ||
                !String.IsNullOrEmpty(actualItem.receiver))
            {
                this.pricingCheckBox.Checked = true;
                this.pricingDatePicker.Value = actualRepair.pricingDate;
                this.pricingTextBox.Text = actualItem.price;
            }
            else
            {
                this.pricingCheckBox.Checked = false;
                this.pricingDatePicker.Value = actualRepair.date;
                this.pricingTextBox.Text = "";

                /*/ If any of items was priced than change date to pricingDate
                DateTime temp = new DateTime();
                foreach (Item item in actualRepair.items)
                {
                    if (!String.IsNullOrEmpty(item.price))
                        temp = actualRepair.pricingDate;
                }

                if (temp > new DateTime(2000, 1, 1))
                    this.pricingDatePicker.Value = temp;*/
            }

            if (!String.IsNullOrEmpty(actualItem.decision) || !String.IsNullOrEmpty(actualItem.receiver))
            {
                this.decisionCheckBox.Checked = true;
                this.decisionDatePicker.Value = actualItem.decisionDate;
                this.decisionComboBox.Text = actualItem.decision;
            }
            else
            {
                this.decisionCheckBox.Checked = false;
                this.decisionDatePicker.Value = this.pricingDatePicker.Value;
                this.decisionComboBox.Text = "";
            }

            if (!String.IsNullOrEmpty(actualItem.receiver))
            {
                this.receiveCheckBox.Checked = true;
                this.receiveDatePicker.Value = actualItem.receiveDate;
                this.receiveTextBox.Text = actualItem.receiver;
            }
            else
            {
                this.receiveCheckBox.Checked = false;
                this.receiveDatePicker.Value = this.decisionDatePicker.Value;
                this.receiveTextBox.Text = "";
            }
        }

        private void updateRepair()
        {
            actualRepair.client = clientComboBox.Text;
            actualRepair.date = repairDatePicker.Value;
            actualRepair.pricingComment = this.pricingCommentRichTextBox.Text;
        }

        private void updateItem()
        {
            actualItem.repairId = actualRepair.id;
            actualItem.type = this.typeComboBox.Text;
            actualItem.item = itemTextBox.Text;
            actualItem.description = descriptionTextBox.Text;
            actualItem.fabrnum = this.fabrnumTextBox.Text;

            if (this.pricingCheckBox.Checked && pricingTextBox.Text != "")
            {
                actualRepair.pricingDate = this.pricingDatePicker.Value;

                this.pricingTextBox.Text = this.pricingTextBox.Text.Replace('.', ',');

                // Adding zeros if necessary
                if (!this.pricingTextBox.Text.Contains(',') && !String.IsNullOrEmpty(this.pricingTextBox.Text))
                    actualItem.price = this.pricingTextBox.Text + ",00";
                else
                {
                    if (this.pricingTextBox.Text.Length - 1 == this.pricingTextBox.Text.LastIndexOf(',') && 
                            !String.IsNullOrEmpty(this.pricingTextBox.Text))
                        actualItem.price = this.pricingTextBox.Text + "00";
                    else
                    {
                        if (this.pricingTextBox.Text.Length - 2 == this.pricingTextBox.Text.LastIndexOf(',') && 
                                !String.IsNullOrEmpty(this.pricingTextBox.Text))
                            actualItem.price = this.pricingTextBox.Text + "0";
                        else
                            actualItem.price = this.pricingTextBox.Text;
                    }
                }
            }
            else
            {
                actualItem.price = "";
            }

            if (this.decisionCheckBox.Checked)
            {
                actualItem.decisionDate = this.decisionDatePicker.Value;
                actualItem.decision = this.decisionComboBox.Text;
            }
            else
            {
                actualItem.decision = "";
            }

            if (this.receiveCheckBox.Checked)
            {
                actualItem.receiveDate = this.receiveDatePicker.Value;
                actualItem.receiver = this.receiveTextBox.Text;
            }
            else
            {
                actualItem.receiver = "";
            }
        }

        private bool validated()
        {
            int counterPriced = 0;

            foreach (Item item in actualRepair.items)
            {
                if (!String.IsNullOrEmpty(item.price))
                    counterPriced++;

                if (String.IsNullOrEmpty(item.item))
                {
                    MessageBox.Show("Musisz wprowadzić nazwę przedmiotu.");
                    return false;
                }

                if (!String.IsNullOrEmpty(item.decision) && String.IsNullOrEmpty(item.price))
                {
                    MessageBox.Show("Jeśli przedmiot został wyceniony, wprowadź wartość\nwyceny przedmiotu " + item.item + ".");
                    return false;
                }

                if (!String.IsNullOrEmpty(item.receiver) && String.IsNullOrEmpty(item.decision))
                {
                    MessageBox.Show("Wprowadź decyzję dotyczącą naprawy\nprzedmiotu " + item.item + ".");
                    return false;
                }
            }

            if (counterPriced > 0 && counterPriced < actualRepair.items.Count)
            {
                MessageBox.Show("Wprowadź wycenę dla wszystkich przedmiotów.");
                return false;
            }

            return true;
        }

        private void changeState()
        {
            string state = "Nowa";

            foreach (Item item in actualRepair.items)
                if (!String.IsNullOrEmpty(item.price))
                {
                    state = "Wycenione";
                    break;
                }

            int counter = 0;
            foreach (Item item in actualRepair.items)
                if (!String.IsNullOrEmpty(item.decision))
                    counter++;

            if (counter == actualRepair.items.Count)
                state = "Decyzja";

            counter = 0;

            foreach (Item item in actualRepair.items)
                if (!String.IsNullOrEmpty(item.receiver))
                    counter++;

            if (counter == actualRepair.items.Count)
                state = "Zakończone";

            actualRepair.state = state;
        }

        private void init(List<Client> clients)
        {
            foreach (Client client in clients)
                this.clientComboBox.Items.Add(client.name);

            this.repairDatePicker.Value = actualRepair.date;
            this.clientComboBox.Text = actualRepair.client;
            this.pricingCommentRichTextBox.Text = actualRepair.pricingComment;
            //foreach (Item item in actualRepair.items)
            //    this.itemsListBox.Items.Add(item.item);

            this.Text = "Naprawa " + actualRepair.id;

            fillItemProperties();

            //this.itemsListBox.SelectedIndex = 0;

            // MinPricingDate
            this.pricingDatePicker.MinDate = this.actualRepair.date;
        }

        #region [CheckBoxesChanged]

        private void pricingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.pricingCheckBox.Checked)
            {
                this.pricingDatePicker.Enabled = true;
                this.pricingTextBox.Enabled = true;

                this.decisionCheckBox.Enabled = true;
            }
            else
            {
                this.pricingDatePicker.Enabled = false;
                this.pricingTextBox.Enabled = false;

                this.decisionCheckBox.Checked = false;
                this.decisionCheckBox.Enabled = false;
            }
        }

        private void decisionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.decisionCheckBox.Checked)
            {
                this.decisionDatePicker.Enabled = true;
                this.decisionComboBox.Enabled = true;

                this.receiveCheckBox.Enabled = true;
            }
            else
            {
                this.decisionDatePicker.Enabled = false;
                this.decisionComboBox.Enabled = false;

                this.receiveCheckBox.Checked = false;
                this.receiveCheckBox.Enabled = false;
            }
        }

        private void receiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.receiveCheckBox.Checked)
            {
                this.receiveDatePicker.Enabled = true;
                this.receiveTextBox.Enabled = true;
            }
            else
            {
                this.receiveDatePicker.Enabled = false;
                this.receiveTextBox.Enabled = false;
            }
        }

        #endregion

        #region [DatePickersChanged]

        private void pricingDatePicker_ValueChanged(object sender, EventArgs e)
        {
            this.decisionDatePicker.MinDate = this.pricingDatePicker.Value;
        }

        private void repairDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (this.actualRepair.pricingDate > new DateTime(2000, 1, 1) && this.repairDatePicker.Value > this.actualRepair.pricingDate)
            {
                this.repairDatePicker.Value = actualRepair.date;
                this.pricingDatePicker.MinDate = actualRepair.date;

                MessageBox.Show("Nie możesz zmienić daty naprawy\nna późniejszą niż data wyceny.");
                return;
            }

            this.pricingDatePicker.MinDate = this.repairDatePicker.Value;
        }

        private void decisionDatePicker_ValueChanged(object sender, EventArgs e)
        {
            this.receiveDatePicker.MinDate = this.decisionDatePicker.Value;
        }

        #endregion

        #region [ButtonsClicked]

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // First update item, than validate it
                updateItem();
                fillItemProperties();

                if (!validated())
                    return;

                updateRepair();

                // Checking for changing repair's state
                this.changeState();

                // If pricing unchecked, update actualRepair
                foreach (Item item in actualRepair.items)
                {
                    if (item.price == "")
                        actualRepair.pricingDate = DateTime.MinValue;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //this.actualRepair = new Repair(oldRepair.id, oldRepair.date, oldRepair.client, oldRepair.pricingDate, oldRepair.state, oldRepair.archive);
            //this.actualRepair.items.Clear();
            
            //foreach (Item item in oldRepair.items)
            //    actualRepair.items.Add(new Item(item));
            /*
            actualRepair = new Repair(oldRepair.id, oldRepair.date, oldRepair.client, oldRepair.pricingDate, oldRepair.state, oldRepair.archive);
            actualRepair.items = new List<Item>();

            foreach (Item item in oldRepair.items)
                actualRepair.items.Add(new Item(item.repairId, item.id, item.type, item.item, item.description,
                    item.price, item.decision, item.decisionDate, item.receiver, item.receiveDate));
            */

            actualRepair.id = oldRepair.id;
            actualRepair.date = oldRepair.date;
            actualRepair.client = oldRepair.client;
            actualRepair.archive = oldRepair.archive;
            actualRepair.pricingDate = oldRepair.pricingDate;
            actualRepair.state = oldRepair.state;
            actualRepair.items = oldRepair.items;
            actualRepair.pricingComment = oldRepair.pricingComment;

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        private void pricingTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && this.pricingTextBox.Text.Contains(',') &&
                (this.pricingTextBox.Text.Length - this.pricingTextBox.Text.IndexOf(',') == 3))
                e.Handled = true;

            if (!char.IsControl(e.KeyChar) && this.pricingTextBox.Text.Contains('.') &&
                (this.pricingTextBox.Text.Length - this.pricingTextBox.Text.IndexOf('.') == 3))
                e.Handled = true;

            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as TextBox).Text.IndexOf('.') > -1 || (sender as TextBox).Text.IndexOf(',') > -1))
                e.Handled = true;
        }

        private void EditRepairWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
