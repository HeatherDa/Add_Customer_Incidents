using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Add_Customer_Incidents
{
    public partial class CustomerIncidents : Form
    {
        public CustomerIncidents()
        {
            InitializeComponent();
            customerIDTextBox.Focus();
        }

        private void CustomerIncidents_Load(object sender, EventArgs e)
        {

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            if (dataValidation())
            {
                try
                {
                    int customerID = int.Parse(customerIDToolStripTextBox.Text);
                    fillByID(customerID);// this.customersTableAdapter.FillBy1(this.techSupport_DataDataSet.Customers, (customerID));
                    if (customersBindingSource.Count <= 0)
                    {
                        MessageBox.Show("No customer was found with this ID.  If you know the customer's state, try using the Search By State Button", "Customer Not Found");
                        fillByID(customerID);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
                }
            }
        }

        

        private void btnStateSearch_Click(object sender, EventArgs e)
        {
            FindCustomer fc=new FindCustomer();
            DialogResult selectedButton = fc.ShowDialog();
            if(selectedButton == DialogResult.OK)
            {
                int customerID = int.Parse(fc.Tag.ToString());
                fillByID(customerID);
            }
        }
        private void fillByID(int customerID)
        {
            try
            {
                this.customersTableAdapter.FillBy1(this.techSupport_DataDataSet.Customers, (customerID));
                this.incidentsTableAdapter.FillBy(this.techSupport_DataDataSet.Incidents, (customerID));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private bool dataValidation()
        {
            int i = 0;
            if (int.TryParse(customerIDToolStripTextBox.Text, out i) && customerIDToolStripTextBox.TextLength == 4)
            {
                return true;
            }
            MessageBox.Show("CustomerID is required and it must be an integer.");
            return false;
        }

        private void btnAddIncident_Click(object sender, EventArgs e)
        {
            if (dataValidation())
            {
                string info = customerIDTextBox.Text;

                Form addIncident = new AddIncident(info);
                DialogResult selectedButton = addIncident.ShowDialog();
                if (selectedButton == DialogResult.OK)
                {
                    int customerID = int.Parse(addIncident.Tag.ToString());
                    fillByID(customerID);
                }
            }
        }

        

    }
}
