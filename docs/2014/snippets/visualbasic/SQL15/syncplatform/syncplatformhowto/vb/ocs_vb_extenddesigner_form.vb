'<snippetOCS_VB_ExtendDesigner_Form>
Public Class Form1

    Private Sub CustomersBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomersBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.CustomersBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.NorthwindDataSet)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'NorthwindDataSet.Customers' table. You can move, or remove it, as needed.
        Me.CustomersTableAdapter.Fill(Me.NorthwindDataSet.Customers)

    End Sub

    Private Sub SynchronizeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeButton.Click
        ' Call the synchronize method to update the local database.
        Dim syncAgent As NorthwindCacheSyncAgent = New NorthwindCacheSyncAgent()

        'Dim clientProvider As NorthwindCacheClientSyncProvider = syncAgent.LocalProvider
        'clientProvider.AddHandlers()

        Dim syncStats As Microsoft.Synchronization.Data.SyncStatistics = syncAgent.Synchronize()

        ' Reload the table from the local database.
        Me.NorthwindDataSet.Merge(Me.CustomersTableAdapter.GetData())

        '<snippetOCS_VB_ExtendDesigner_Form_MessageBoxShow>
        MessageBox.Show("Changes downloaded: " & _
    syncStats.TotalChangesDownloaded.ToString & Environment.NewLine & "Changes uploaded: " & _
    syncStats.TotalChangesUploaded.ToString)
        '</snippetOCS_VB_ExtendDesigner_Form_MessageBoxShow>

    End Sub
End Class
'</snippetOCS_VB_ExtendDesigner_Form>
