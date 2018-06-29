'<snippetOCS_VB_ExtendDesigner_PartialClasses>
'<snippetOCS_VB_ExtendDesigner_PartialClasses_NorthwindCacheSyncAgent>
Partial Public Class NorthwindCacheSyncAgent

    Partial Sub OnInitialized()

        Me.Customers.SyncDirection = Microsoft.Synchronization.Data.SyncDirection.Bidirectional

    End Sub

End Class
'</snippetOCS_VB_ExtendDesigner_PartialClasses_NorthwindCacheSyncAgent>

'<snippetOCS_VB_ExtendDesigner_PartialClasses_NorthwindCacheServerSyncProvider>
Partial Public Class NorthwindCacheServerSyncProvider

    Private Sub NorthwindCacheServerSyncProvider_ApplyChangeFailed( _
        ByVal sender As Object, ByVal e As  _
        Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs) _
        Handles Me.ApplyChangeFailed

        If e.Conflict.ConflictType = _
            Microsoft.Synchronization.Data.ConflictType.ClientUpdateServerUpdate Then

            'Resolve a client update / server update conflict by force writing
            'the client change to the server database.
            MessageBox.Show("A client update / server update conflict was detected at the server.")
            e.Action = Microsoft.Synchronization.Data.ApplyAction.RetryWithForceWrite

        End If

    End Sub

End Class

Partial Public Class NorthwindCacheClientSyncProvider

    Private Sub NorthwindCacheClientSyncProvider_ApplyChangeFailed( _
        ByVal sender As Object, ByVal e As  _
        Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs) _
        Handles Me.ApplyChangeFailed

        If e.Conflict.ConflictType = _
            Microsoft.Synchronization.Data.ConflictType.ClientUpdateServerUpdate Then

            'Resolve a client update / server update conflict by keeping the 
            'client change.
            e.Action = Microsoft.Synchronization.Data.ApplyAction.Continue

        End If

    End Sub

End Class
'</snippetOCS_VB_ExtendDesigner_PartialClasses_NorthwindCacheServerSyncProvider>

'<snippetOCS_VB_ExtendDesigner_PartialClasses_CustomersSyncAdapter>
Partial Public Class CustomersSyncAdapter
    Private Sub OnInitialized()

        'Redefine the insert command so that it does not insert values 
        'into the CreationDate and LastEditDate columns.
        Dim insertCommand As New System.Data.SqlClient.SqlCommand
        With insertCommand
            .CommandText = "INSERT INTO dbo.Customers ([CustomerID], [CompanyName], " & _
                "[ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], " & _
                "[Country], [Phone], [Fax] )" & _
                "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, " & _
                "@Region, @PostalCode, @Country, @Phone, @Fax) SET @sync_row_count = @@rowcount"
            .CommandType = System.Data.CommandType.Text
            .Parameters.Add("@CustomerID", System.Data.SqlDbType.NChar)
            .Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@ContactName", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@ContactTitle", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Address", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@City", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Region", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@PostalCode", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Country", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Fax", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@sync_row_count", System.Data.SqlDbType.Int)
            .Parameters("@sync_row_count").Direction = ParameterDirection.Output
        End With

        Me.InsertCommand = insertCommand


        'Redefine the update command so that it does not update values 
        'in the CreationDate and LastEditDate columns.
        Dim updateCommand As New System.Data.SqlClient.SqlCommand
        With updateCommand
            .CommandText = "UPDATE dbo.Customers SET [CompanyName] = @CompanyName, [ContactName] " & _
                "= @ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, [City] " & _
                "= @City, [Region] = @Region, [PostalCode] = @PostalCode, [Country] = @Country, " & _
                "[Phone] = @Phone, [Fax] = @Fax " & _
                "WHERE ([CustomerID] = @CustomerID) AND (@sync_force_write = 1 " & _
                "OR ([LastEditDate] <= @sync_last_received_anchor)) SET @sync_row_count = @@rowcount"
            .CommandType = System.Data.CommandType.Text
            .Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@ContactName", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@ContactTitle", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Address", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@City", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Region", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@PostalCode", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Country", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@Fax", System.Data.SqlDbType.NVarChar)
            .Parameters.Add("@CustomerID", System.Data.SqlDbType.NChar)
            .Parameters.Add("@sync_force_write", System.Data.SqlDbType.Bit)
            .Parameters.Add("@sync_last_received_anchor", System.Data.SqlDbType.DateTime)
            .Parameters.Add("@sync_row_count", System.Data.SqlDbType.Int)
            .Parameters("@sync_row_count").Direction = ParameterDirection.Output
        End With

        Me.UpdateCommand = updateCommand

    End Sub

End Class
'</snippetOCS_VB_ExtendDesigner_PartialClasses_CustomersSyncAdapter>
'</snippetOCS_VB_ExtendDesigner_PartialClasses>