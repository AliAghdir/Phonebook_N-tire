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
    public partial class frmContactDetails : Form
    {
        private readonly ContactService contactService;
        private readonly int contactId;
        public frmContactDetails(int contactId)
        {
            InitializeComponent();
            this.contactId = contactId;
            contactService = new ContactService();
        }

        private void frmContactDetails_Load(object sender, EventArgs e)
        {
            var contactDetails = contactService.GetContactDetails(contactId);
            if (contactDetails.IsSuccess == false)
            {
                MessageBox.Show(contactDetails.Message, "",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblId.Text = contactDetails.Data.Id.ToString();
                lblName.Text = contactDetails.Data.Name;
                lblLastName.Text = contactDetails.Data.LastName;
                lblPhoneNumber.Text = contactDetails.Data.PhoneNumber;
                lblCompany.Text = contactDetails.Data.Company;
                lblCreateAt.Text = contactDetails.Data.CreateAt.ToString();
                lblDescription.Text = contactDetails.Data.Description;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
