using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TechPoznanNaprawy.Database;

namespace TechPoznanNaprawy
{
    public partial class NewItemWindow : Form
    {
        private bool isNew = true;

        private Repair actualRepair;
        private Item newItem;

        public NewItemWindow(List<string> brands, Repair repair, string id)
        {
            this.actualRepair = repair;
            this.newItem = new Item();
            this.newItem.id = id;

            InitializeComponent();

            init(brands);
        }

        private void init(List<string> brands)
        {
            this.brandComboBox.Items.AddRange(brands.ToArray());
            this.brandComboBox.SelectedIndex = 0;
            this.typeComboBox.SelectedIndex = 0;
        }

        public void addItem()
        {
            try
            {
                newItem.type = this.typeComboBox.Text;
                newItem.item = this.brandComboBox.Text + " " + this.modelTextBox.Text + " " + this.fabrnumTextBox.Text;
                newItem.description = this.descriptionRichTextBox.Text;

                if (isNew)
                    this.actualRepair.items.Add(newItem);
            }
            catch (Exception e) { MessageBox.Show(e.GetBaseException().Message); }
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            this.addItem();

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
