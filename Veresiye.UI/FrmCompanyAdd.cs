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
    public partial class FrmCompanyAdd : Form
    {
        private readonly ICompanyService companyService;

        public FrmCompanies MasterForm { get; set; }
        public FrmCompanyAdd(ICompanyService companyService)
        {
            this.companyService = companyService;
            InitializeComponent();
        }

        

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var company = new Company();
            company.Name = txtCompanyName.Text;
            company.Phone = txtPhone.Text;
            company.City = txtCity.Text;
            company.Region = txtRegion.Text;
            companyService.Insert(company);
            MessageBox.Show("Firma başarı ile kaydedildi");
            txtCompanyName.Clear();
            txtPhone.Clear();
            txtRegion.Clear();
            txtCity.Clear();
            this.Close();
            MasterForm.LoadCompanies();

        }

        private void FrmCompanyAdd_Load(object sender, EventArgs e)
        {
           
        }

        private void FrmCompanyAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
