using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veresiye.Model;
using Veresiye.Service;

namespace Veresiye.UI
{
    public partial class FrmCompanyEdit : Form
    {
        private readonly ICompanyService companyService;
        private readonly IActivityService activityService;
        private readonly FrmActivityAdd frmActivityAdd;
        public FrmCompanies MasterForm { get; set; }

        public FrmCompanyEdit(ICompanyService companyService, IActivityService activityService, FrmActivityAdd frmActivityAdd)
        {
            
            this.companyService = companyService;
            this.activityService = activityService;
            this.frmActivityAdd = frmActivityAdd;
            InitializeComponent();
            this.frmActivityAdd.MdiParent = this.MdiParent;
            this.frmActivityAdd.MasterForm = this;
        }

        private void FrmCompanyEdit_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadActivities()
        {
            int companyId = MasterForm.id;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = activityService.GetAllByCompanyId(companyId);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var company = companyService.Get(MasterForm.id);
            company.Name = txtCompanyName.Text;
            company.Phone = txtPhone.Text;
            company.City = txtCity.Text;
            company.Region = txtRegion.Text;
            companyService.Update(company);
            MessageBox.Show("Firma başarı ile güncellendi");
            txtCompanyName.Clear();
            txtPhone.Clear();
            txtRegion.Clear();
            txtCity.Clear();
            this.Close();
            MasterForm.LoadCompanies();
        }

        public void LoadSelectCompany()
        {
            var companyId = this.MasterForm.id;
            var company = companyService.Get(companyId);
            txtCompanyName.Text = company.Name;
            txtPhone.Text = company.Phone;
            txtRegion.Text = company.Region;
            txtCity.Text = company.City;
            LoadActivities();
        }

        private void FrmCompanyEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmActivityAdd.Show();
            frmActivityAdd.LoadForm();
        }
    }
}
