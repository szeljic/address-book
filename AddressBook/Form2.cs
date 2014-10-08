using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class Form2 : Form
    {
        private string fullName;
        private string address;
        private string phone;
        private string email;

        public Form2(string fullName, string address, string phone, string email)
        {
            InitializeComponent();
            this.fullName = fullName;
            this.address = address;
            this.phone = phone;
            this.email = email;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = this.fullName;
            this.lblFullName.Text = this.fullName;
            this.lblAddress.Text = this.address;
            this.lblPhone.Text = this.phone;
            this.lblEmail.Text = this.email;
        }
    }
}
