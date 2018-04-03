//<snippetOCS_CS_ExtendDesigner_Form>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OCSWalkthroughCS1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);

        }

        private void SynchronizeButton_Click(object sender, EventArgs e)
        {
            // Call the synchronize method to synchronize
            // data between local and remote databases.
            //<snippetOCS_CS_ExtendDesigner_Form_AddHandlers>
            NorthwindCacheSyncAgent syncAgent = new NorthwindCacheSyncAgent();

            NorthwindCacheClientSyncProvider clientSyncProvider =
                (NorthwindCacheClientSyncProvider)syncAgent.LocalProvider;
            clientSyncProvider.AddHandlers();

            Microsoft.Synchronization.Data.SyncStatistics syncStats = 
                syncAgent.Synchronize();
            //</snippetOCS_CS_ExtendDesigner_Form_AddHandlers>
            
            // After synchronizing the data, refill the
            // table in the dataset.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);

            //<snippetOCS_CS_ExtendDesigner_Form_MessageBoxShow>
            MessageBox.Show("Changes downloaded: " +
                syncStats.TotalChangesDownloaded.ToString() + 
                Environment.NewLine +
                "Changes uploaded: " + syncStats.TotalChangesUploaded.ToString());
            //</snippetOCS_CS_ExtendDesigner_Form_MessageBoxShow>


        }
    }
}
//</snippetOCS_CS_ExtendDesigner_Form>
