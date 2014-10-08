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
    public partial class Form1 : Form
    {
        private PersonDAO personDAO;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            personDAO = new PersonDAOImpl();
            this.loadListOfPerson();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in this.listOfPersons.Items)
            {
                if (item.SubItems[1].Text == tbFullName.Text)
                {
                    this.lblInfo.Text = "Person already exist. Try to update.";
                    return;
                }
            }


            if (!string.IsNullOrWhiteSpace(tbFullName.Text))
            {
                Person person = new Person(tbFullName.Text, tbAddress.Text, tbPhone.Text, tbEmail.Text);
                personDAO.insertPerson(person);
                this.loadListOfPerson();
            }
            else
            {
                this.lblInfo.Text = "Please enter a valid name.";
            }
        }

        private void loadListOfPerson()
        {
            this.listOfPersons.Items.Clear();
            List<Person> list = personDAO.getAllPersons();
            ListViewItem lvi;
            int i = 1;
            foreach (Person person in list)
            {
                lvi = new ListViewItem(i.ToString());
                lvi.SubItems.Add(person.getFullName());
                lvi.SubItems.Add(person.getAddress());
                lvi.SubItems.Add(person.getPhone());
                lvi.SubItems.Add(person.getEmail());
                this.listOfPersons.Items.Add(lvi);
                i++;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.listOfPersons.SelectedItems.Count > 0)
            {
                string fullName = listOfPersons.SelectedItems[0].SubItems[1].Text;
                personDAO.deletePerson(fullName);
                this.loadListOfPerson();
                this.lblInfo.Text = "Successfully removed.";
                this.tbFullName.Text = "";
                this.tbAddress.Text = "";
                this.tbPhone.Text = "";
                this.tbEmail.Text = "";
            } 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.listOfPersons.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.listOfPersons.SelectedItems[0];
                string oldPersonsFullName = lvi.SubItems[1].Text;
                personDAO.updatePerson(oldPersonsFullName, new Person(tbFullName.Text, tbAddress.Text, tbPhone.Text, tbEmail.Text));
                this.loadListOfPerson();
                this.lblInfo.Text = "Successfully updated.";
            }
            else
            {
                this.lblInfo.Text = "Please select person from list before update.";
            }
        }

        private void listOfPersons_Click(object sender, EventArgs e)
        {
            if (this.listOfPersons.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listOfPersons.SelectedItems[0];
                this.tbFullName.Text = item.SubItems[1].Text;
                this.tbAddress.Text = item.SubItems[2].Text;
                this.tbPhone.Text = item.SubItems[3].Text;
                this.tbEmail.Text = item.SubItems[4].Text;
            }
        }

        private void listOfPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listOfPersons.SelectedItems.Count < 1)
            {
                this.tbFullName.Text = "";
                this.tbAddress.Text = "";
                this.tbPhone.Text = "";
                this.tbEmail.Text = "";
            }
        }
    }
}
