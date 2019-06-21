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
    public partial class FrmCompanies : Form
    {
        public int id;
        private readonly FrmCompanyAdd frmCompanyAdd;
        private readonly FrmCompanyEdit frmCompanyEdit;
        private readonly ICompanyService companyService;
        public FrmCompanies(ICompanyService companyService, FrmCompanyAdd frmCompanyAdd, FrmCompanyEdit frmCompanyEdit)
        {
            this.companyService = companyService;
            this.frmCompanyAdd = frmCompanyAdd;
            this.frmCompanyEdit = frmCompanyEdit;
            InitializeComponent();
            this.frmCompanyAdd.MdiParent = this.MdiParent;
            this.frmCompanyEdit.MdiParent = this.MdiParent;
            this.frmCompanyAdd.MasterForm = this;
            this.frmCompanyEdit.MasterForm = this;
        }

        private void FrmCompanies_Load(object sender, EventArgs e)
        {
            LoadCompanies();
        }

        public void LoadCompanies()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = companyService.GetAll();
        }

        private void Button1_Click(object sender, EventArgs e) //Add Company event
        {
            frmCompanyAdd.Show();
            
        }

        private void Button2_Click(object sender, EventArgs e) //Update event
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                id = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frmCompanyEdit.Show();
                frmCompanyEdit.LoadSelectCompany();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz firmayı seçiniz.");
            }
        }
        
        private void Button3_Click(object sender, EventArgs e) //Delete event
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult karar = new DialogResult();
                int selectCompanyId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                karar = MessageBox.Show("Seçili firmayı silmek istediğinizden emin misiniz?", "Silme işlemi", MessageBoxButtons.YesNo);
                if (karar == DialogResult.Yes)
                {
                    companyService.Delete(selectCompanyId);                    
                    LoadCompanies();
                    MessageBox.Show("Firma başarı ile silindi");
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kaydı seçin.");
            }
        }
    }
}
