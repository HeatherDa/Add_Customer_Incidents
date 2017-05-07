using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Add_Customer_Incidents
{
    public partial class AddIncident : Form
    {
        public AddIncident(string info)
        {
            InitializeComponent();
            customerInfo(info);
        }

        private void AddIncident_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Incidents' table. You can move, or remove it, as needed.
            this.incidentsTableAdapter.Fill(this.techSupport_DataDataSet.Incidents);
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Incidents' table. You can move, or remove it, as needed.
            this.incidentsTableAdapter.Fill(this.techSupport_DataDataSet.Incidents);
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.techSupport_DataDataSet.Products);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productCode=cbProducts.SelectedValue.ToString();
            int custID = int.Parse(txtCustID.Text);
            this.Tag = custID;
            this.incidentsTableAdapter.InsertQuery (custID, productCode, DateTime.Today, txtTitle.Text, txtDescription.Text);
            
            DialogResult = DialogResult.OK;
        }
        private void customerInfo(string customerInfo)
        {
            MessageBox.Show(customerInfo+" "+ customerInfo.Substring(0, 4)+" "+ customerInfo.Substring(4));
            txtCustID.Text = customerInfo.Substring(0, 4);
            txtCustName.Text = customerInfo.Substring(4);


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
