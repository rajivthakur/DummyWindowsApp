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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Button Click Event for getting Pneuron data.
        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("ClaimDetails");
            String connectionString = "Data Source=PSMTNTPNSQD01;Initial Catalog=pneuron_results;Integrated Security=True";
            string claimno1 = "EWR00003397"; 
            string claimno2 = "EWR00004811";
            String sqlStatement = @"    SELECT  TOP 1 [CLAIM_NUMBER] FROM	[pneuron_results].[dbo].[LR_CLAIM_NOTES_SUMMARY]
                                        WHERE	claim_number IS NOT NULL " 
                                        + @"
                                        SELECT  TOP 3 [CLAIM_NUMBER] FROM	[pneuron_results].[dbo].[LR_CLAIM_NOTES_SUMMARY]
                                        WHERE	claim_number IS NOT NULL 
                                        " +@"
                                        ";
            
           
            try
            {
                //(System.Configuration.ConfigurationManager.AppSettings["PSOPNConnStr"]))
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.CommandText = sqlStatement;
                    sqlComm.CommandType = CommandType.Text;
                    sqlComm.Connection = conn;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(ds);
                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = ds.Tables[0];
                    ds.Tables[0].Merge(ds.Tables[1]);
                    dataGridView1.DataSource = ds.Tables[0];
                    
                
                }
            }

                catch(Exception ee)
            {
                    label1.Text = "Exception while Exceuting Sql Statement on Pneuron Dev DB. Error: " + ee.StackTrace;
                }
            finally
            {

            }
            }
            
        }
    }

