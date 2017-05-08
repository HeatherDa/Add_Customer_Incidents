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
    public partial class AddIncident : Form
    {
        int customerID = 0;
        public AddIncident(string info)
        {
            InitializeComponent();
            customerInfo(info);
        }

        private void AddIncident_Load(object sender, EventArgs e)
        {
            try
            {
                //this.productsTableAdapter.FillByCustomerCode(this.techSupport_DataDataSet.Products, customerID); //fill Products listbox
                /* This would fill the Products list box with software that is registered to the customer, 
                 * but there is a problem with the data in the database.  Specifically, there are customer 
                 * incidents for software for which the customer is not registered.  I have verified this 
                 * problem by comparing specific records.  One of these records is for customer 1010, who 
                 * is only registered for one product, but has incident reports on file for 3.
                 * For this reason, I decided to put all the products into the combobox, so that the form 
                 * would still be useful inspite of the issues with the data in the database.
                 * If this problem is resolved in future, we can switch to the above query, if desired.  
                 * However, I would like to also point out that issues with registering a product could be 
                 * considered Incidents, so this may not be a good idea anyway.*/
                this.customersTableAdapter.FillBy1(this.techSupport_DataDataSet.Customers, customerID);//fill customerID and customerName
                this.productsTableAdapter.Fill(this.techSupport_DataDataSet.Products);//fill product's list box
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database error # "+ex.Number+": " + ex.Message, ex.GetType().ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataValidation())
                {
                    string productCode = cbProducts.SelectedValue.ToString();//cbProducts displays name but returns code 
                    int custID = int.Parse(txtCustomerID.Text);
                    this.Tag = custID;
                    this.incidentsTableAdapter.InsertQuery(custID, productCode, DateTime.Today, txtTitle.Text, txtDescription.Text);

                    DialogResult = DialogResult.OK;
                }
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("The row could not be added.  There may be a concurrency error.");
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                customersBindingSource.CancelEdit();//I think this is right, but I can't get this to come up so not sure.
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            }
        }
        private void customerInfo(string customerInfo)
        {
            customerID = int.Parse(customerInfo);//validated in previous form before passing
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private bool dataValidation()
        {
            if (txtTitle.TextLength <= 0 || txtDescription.TextLength <= 0)
            {
                MessageBox.Show("Title and Description are required fields.");
                return false;
            }
            return true;
        }

       

        
    }
}
