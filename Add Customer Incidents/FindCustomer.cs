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
    public partial class FindCustomer : Form
    {
        int customerID = 0;
        public FindCustomer()
        {
            InitializeComponent();
            stateToolStripTextBox.Focus();
        }

        private void FindCustomer_Load(object sender, EventArgs e)
        {
        }

        private void fillBy2ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string state = stateToolStripTextBox.Text;
              //this.customersTableAdapter.FillBy3(this.techSupport_DataDataSet.Customers, stateToolStripTextBox.Text);
                /*I tried everything I could to make a LIKE querry but could not get it to work.  
                 *I spent hours looking up help online and still couldn't solve it.
                 *I have no more time.  */
                this.customersTableAdapter.FillBy2(this.techSupport_DataDataSet.Customers, (state));//This just looks for a matching state code, not a similar one.
                if (customersBindingSource.Count <= 0)
                {
                    MessageBox.Show("No customer was found with this state.  Please try again.", "Customer Not Found");
                    
                }                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # "+ex.Number+": "+ex.Message, ex.GetType().ToString());
            }

        }

        private void btnOK_Click(object sender, EventArgs e)//handles ok button and double click
        {
            if(customersBindingSource.Count <= 0|| stateToolStripTextBox.Text=="")//exit without results is like cancel
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else if (customersDataGridView.SelectedCells.Count > 0)//some row is selected
            {
                this.Tag = customerID;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void customerIDSet()//sets the value of customerID to the value from the selected row
        {
            int selectedRowIndex = customersDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow row = customersDataGridView.Rows[selectedRowIndex];
            string idString = Convert.ToString(row.Cells[0].Value);
            customerID = int.Parse(idString);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            customerIDSet();
            MessageBox.Show(customerID.ToString());
        }
    }
}
