using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechPoznanNaprawy
{
    public partial class NewClientWindow : Form
    {
        private List<Client> clients;
        private Client actualClient = new Client();

        public NewClientWindow(ref List<Client> clients, ref Client client)
        {
            this.clients = clients;
            this.actualClient = client;

            InitializeComponent();

            this.nameTextBox.Text = client.name;
            this.streetTextBox.Text = client.address;
            if (client.code.Length > 5)
            {
                this.post1TextBox.Text = client.code.Substring(0, 2);
                this.post2TextBox.Text = client.code.Substring(3, 3);
            }
            this.cityTextBox.Text = client.city;
            this.telTextBox.Text = client.telnum;
            this.emailTextBox.Text = client.email;

            this.addClientButton.Text = "Zmień";
        }

        public NewClientWindow(ref List<Client> clients, string id)
        {
            this.clients = clients;
            this.actualClient.id = id;

            InitializeComponent();
        }

        public NewClientWindow(ref List<Client> clients, string id, string name, string address, string post, 
            string city, string mail, string telnum)
        {
            this.clients = clients;
            this.actualClient.id = id;

            InitializeComponent();

            this.nameTextBox.Text = name;
            this.streetTextBox.Text = address;
            if (post.Length > 5)
            {
                this.post1TextBox.Text = post.Substring(0, 2);
                this.post2TextBox.Text = post.Substring(3, 3);
            }
            this.cityTextBox.Text = city;
            this.telTextBox.Text = telnum;
            this.emailTextBox.Text = mail;
        }

        private void addClient()
        {
            if (this.nameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Musisz podać przynajmniej nazwę klienta.");
                return;
            }

            if (!valid())
            {
                MessageBox.Show("Źle wypełniono dane.");
                return;
            }

            if (this.addClientButton.Text == "Zmień")
            {
                // Edycja klienta
                actualClient.name = this.nameTextBox.Text;
                actualClient.address = this.streetTextBox.Text;
                actualClient.code = this.post1TextBox.Text + "-" + this.post2TextBox.Text;
                actualClient.city = this.cityTextBox.Text;
                actualClient.telnum = this.telTextBox.Text;
                actualClient.email = this.emailTextBox.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                actualClient.name = this.nameTextBox.Text;
                actualClient.address = this.streetTextBox.Text;
                actualClient.code = this.post1TextBox.Text + "-" + this.post2TextBox.Text;
                actualClient.city = this.cityTextBox.Text;
                actualClient.telnum = this.telTextBox.Text;
                actualClient.email = this.emailTextBox.Text;

                clients.Add(actualClient);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool valid()
        {
            // Post code
            if (this.post1TextBox.TextLength != 0 || this.post2TextBox.TextLength !=0)
                if (!Char.IsDigit(this.post1TextBox.Text, 0) || !Char.IsDigit(this.post1TextBox.Text, 1) ||
                    !Char.IsDigit(this.post2TextBox.Text, 0) || !Char.IsDigit(this.post2TextBox.Text, 1) ||
                    !Char.IsDigit(this.post2TextBox.Text, 2))
                    return false;

            // Tel number
            foreach (Char c in this.telTextBox.Text.ToCharArray())
            {
                if (!Char.IsDigit(c) && c != '+')
                    return false;
            }

            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            this.addClient();
        }

        // FUNCTIONS FOR PRESSING ENTER BEHAVIOUR
        private void telTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.addClient();
        }

        private void emailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.addClient();
        }

        // FUNCTIONS FOR VALIDATING POSTAL CODE
        private void post1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.OemMinus && this.post1TextBox.TextLength == 2)
                SendKeys.Send("{TAB}");
        }

        private void post1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            
            if (this.post1TextBox.TextLength == 2 && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void post2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;

            if (this.post2TextBox.TextLength == 3 && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
