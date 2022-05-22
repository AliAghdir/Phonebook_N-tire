using BLL.Dto;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI_winForm.Forms
{
    public partial class frmAddNewContact : Form
    {
        private readonly ContactService contactService;
        public frmAddNewContact()
        {
            InitializeComponent();
            contactService = new ContactService();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            var resultAddNewContact = contactService.AddNewContact(new AddNewContactDto
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Company = txtCompany.Text,
                Description = txtDescription.Text
            });
            if (resultAddNewContact.IsSuccess==true)
            {
                MessageBox.Show(resultAddNewContact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(resultAddNewContact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
