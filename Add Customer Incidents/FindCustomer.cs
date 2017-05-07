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
                this.customersTableAdapter.FillBy2(this.techSupport_DataDataSet.Customers, (state));
                if (customersBindingSource.Count <= 0)
                {
                    MessageBox.Show("No customer was found with this state.  Please try again.", "Customer Not Found");
                    //this.customersTableAdapter.FillBy2(this.techSupport_DataDataSet.Customers, stateToolStripTextBox.Text);
                        /*I tried everything I could to make a LIKE querry but could not get it to work.  
                          I spent hours looking up help online and still couldn't solve it.
                          I give up.  This just looks for a matching state code, not a similar one.*/
                }
                /*else
                {
                    MessageBox.Show("No customer was found with this state.  Please try again.", "Customer Not Found");
                }*/
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # "+ex.Number+": "+ex.Message, ex.GetType().ToString());
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Tag = customerID;
            this.DialogResult = DialogResult.OK;    
        }

        private void CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (customersDataGridView.SelectedCells.Count > 0)
            {
                int selectedRowIndex = customersDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow row = customersDataGridView.Rows[selectedRowIndex]; 
                string idString = Convert.ToString(row.Cells[0].Value);
                customerID = int.Parse(idString);
            }
            else if (customersDataGridView.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Please select a row by double clicking or clicking on the arrow to the left of the desired row.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
