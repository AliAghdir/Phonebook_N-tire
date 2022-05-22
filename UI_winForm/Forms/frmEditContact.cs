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
    public partial class frmEditContact : Form
    {
        private readonly ContactService contactService;
        private readonly int contactId;

        public frmEditContact(int id)
        {
            InitializeComponent();
            contactService = new ContactService();
            this.contactId = id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveEditContact_Click(object sender, EventArgs e)
        {
            var resultEditContact = contactService.EditContact(new EditContactDto
            {
                Id = contactId,
                Name = txtName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Company = txtCompany.Text,
                Description = txtDescription.Text
            });
            if (resultEditContact.IsSuccess == true)
            {
                MessageBox.Show(resultEditContact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(resultEditContact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmEditContact_Load(object sender, EventArgs e)
        {
            var contactDetails = contactService.GetContactDetails(contactId);
            if (contactDetails.IsSuccess == false)
            {
                MessageBox.Show(contactDetails.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtName.Text = contactDetails.Data.Name;
                txtLastName.Text = contactDetails.Data.LastName;
                txtPhoneNumber.Text = contactDetails.Data.PhoneNumber;
                txtCompany.Text = contactDetails.Data.Company;
                txtDescription.Text = contactDetails.Data.Description;
            }
        }
    }
}
