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
    public partial class frmMain : Form
    {
        private readonly ContactService contactService;

        public frmMain()
        {
            InitializeComponent();
            contactService = new ContactService();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshGridview();
            dataGridView1.Columns[0].HeaderText = "شناسه";
            dataGridView1.Columns[1].HeaderText = "نام";
            dataGridView1.Columns[2].HeaderText = "شماره تلفن";
        }

        private void RefreshGridview(string key = null)
        {
            dataGridView1.DataSource = contactService.GetContactList(key);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            dataGridView1.DataSource = contactService.GetContactList(txtSearchKey.Text);
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                var deleteResult = contactService.DeleteContact(id);
                MessageBox.Show(deleteResult.Message, "", MessageBoxButtons.OK);
                RefreshGridview();
            }
            else
            {
                MessageBox.Show("مخاطبی انتخاب نشده", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnContactDetails_Click(object sender, EventArgs e)
        {
            ShowContactDetails();
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowContactDetails();
        }
        private void ShowContactDetails()
        {
            var id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmContactDetails frmContactDetails = new frmContactDetails(id);
            frmContactDetails.ShowDialog();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            frmAddNewContact frmAddNewContact = new frmAddNewContact();
            frmAddNewContact.ShowDialog();
            RefreshGridview();
        }

        private void btnEditContact_Click(object sender, EventArgs e)
        {
            var id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmEditContact frmEditContact = new frmEditContact(id);
            frmEditContact.ShowDialog();
            RefreshGridview();
        }
    }
}
