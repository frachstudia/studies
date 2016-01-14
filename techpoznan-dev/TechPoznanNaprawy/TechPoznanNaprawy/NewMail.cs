using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechPoznanNaprawy.Database;

namespace TechPoznanNaprawy
{
    public partial class NewMail : Form
    {
        private String techLogin = "biuro@tech-poznan.pl";
        private String techPass = "tech-poznan-sc";
        private String techSmtp = "tech-poznan.home.pl";

        public NewMail(String name, String email, List<Item> items, string repairId)
        {
            InitializeComponent();

            this.clientTextBox.Text = name;
            this.emailTextBox.Text = email;
            this.subjectTextBox.Text = "TECH-POZNAŃ - Wycena przedmiotów";
            this.textRichTextBox.Text = "Dzień dobry,\n\ninformujemy, że serwis dokonał wyceny Państwa przedmiotu/ów:\n\n";

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].repairId == repairId)
                    this.textRichTextBox.Text += "\t" + items[i].item + " - " + items[i].description + ", wycena: " + items[i].price + " zł.\n";
            }
            this.textRichTextBox.Text += "\nProsimy o odpowiedź zawierającą decyzję o naprawie.\n\n";
            this.textRichTextBox.Text += "--\nPozdrawiam\nKatarzyna Klopś\n\n...........................................................................\n" +
                "TECH Sz. Orszulak, G. Bręcz Spółka Jawna\nul. Głuchowska 1\n60-101 Poznań\ntel./fax 61 6616050\ntel./fax 61 8833869\n" +
                "NIP: 7792333375\nbiuro@tech-poznan.pl";
        }

        private void send(String clientEmail, String subject, String text)
        {
            var loginInfo = new NetworkCredential(this.techLogin, this.techPass);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(techSmtp, 587);

            msg.From = new MailAddress(this.techLogin);
            msg.To.Add(new MailAddress(clientEmail));
            msg.Subject = subject;
            msg.Body = text;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.send(this.emailTextBox.Text, this.subjectTextBox.Text, this.textRichTextBox.Text);
                this.Close();
                MessageBox.Show("Email został wysłany.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
