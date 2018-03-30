'<snippetOCS_VB_NTier_ProvidersTwoTier>
Me.LocalProvider = New SampleClientSyncProvider()

Me.RemoteProvider = New SampleServerSyncProvider()
'</snippetOCS_VB_NTier_ProvidersTwoTier>

'<snippetOCS_VB_NTier_ProvidersNTier>
Me.LocalProvider = New SampleClientSyncProvider()

Dim serviceProxy As New ServiceReference.ServiceForSyncClient()
Me.RemoteProvider = New ServerSyncProviderProxy(serviceProxy)
'</snippetOCS_VB_NTier_ProvidersNTier>

'<snippetOCS_VB_NTier_SampleClientSyncProvider>
Public Class SampleClientSyncProvider
    Inherits SqlCeClientSyncProvider

    Public Sub New()
        'Specify a connection string for the sample client database.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

    End Sub
End Class
'</snippetOCS_VB_NTier_SampleClientSyncProvider>

'<snippetOCS_VB_NTier_IServiceForSync>
<ServiceContract()> _
Public Interface IServiceForSync
    <OperationContract()> _
    Function ApplyChanges(ByVal groupMetadata As SyncGroupMetadata, ByVal dataSet As DataSet, ByVal syncSession As SyncSession) As SyncContext

    <OperationContract()> _
    Function GetChanges(ByVal groupMetadata As SyncGroupMetadata, ByVal syncSession As SyncSession) As SyncContext

    <OperationContract()> _
    Function GetSchema(ByVal tableNames As Collection(Of String), ByVal syncSession As SyncSession) As SyncSchema

    <OperationContract()> _
    Function GetServerInfo(ByVal syncSession As SyncSession) As SyncServerInfo
End Interface
'</snippetOCS_VB_NTier_IServiceForSync>

'<snippetOCS_VB_NTier_ServiceForSync>
Public Class ServiceForSync
    Implements IServiceForSync

    Private _serverSyncProvider As SampleServerSyncProvider


    Public Sub New()
        Me._serverSyncProvider = New SampleServerSyncProvider()
    End Sub


    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Overridable Function ApplyChanges(ByVal groupMetadata As SyncGroupMetadata, ByVal dataSet As DataSet, ByVal syncSession As SyncSession) As SyncContext
        Return Me._serverSyncProvider.ApplyChanges(groupMetadata, dataSet, syncSession)
    End Function


    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Overridable Function GetChanges(ByVal groupMetadata As SyncGroupMetadata, ByVal syncSession As SyncSession) As SyncContext
        Return Me._serverSyncProvider.GetChanges(groupMetadata, syncSession)

    End Function


    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Overridable Function GetSchema(ByVal tableNames As Collection(Of String), ByVal syncSession As SyncSession) As SyncSchema
        Return Me._serverSyncProvider.GetSchema(tableNames, syncSession)
    End Function


    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Overridable Function GetServerInfo(ByVal syncSession As SyncSession) As SyncServerInfo
        Return Me._serverSyncProvider.GetServerInfo(syncSession)
    End Function
End Class
'</snippetOCS_VB_NTier_ServiceForSync>

'<snippetOCS_VB_NTier_SampleServerSyncProvider>
Public Class SampleServerSyncProvider
    Inherits DbServerSyncProvider

    Public Sub New()
        'Create a connection to the sample server database.
        Dim util As New Utility()
        Dim serverConn As New SqlConnection(util.ServerConnString)
        Me.Connection = serverConn

        'Create a command to retrieve a new anchor value from
        'the server.
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() – 1"
        selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
        selectNewAnchorCommand.Parameters(newAnchorVariable).Direction = ParameterDirection.Output
        selectNewAnchorCommand.Connection = serverConn
        Me.SelectNewAnchorCommand = selectNewAnchorCommand

        'Create a SyncAdapter for the Customer table manually, or
        'by using the SqlSyncAdapterBuilder as in the following
        'code.
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)

        customerBuilder.TableName = "Sales.Customer"
        customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone"
        customerBuilder.SyncDirection = SyncDirection.DownloadOnly
        customerBuilder.CreationTrackingColumn = "InsertTimestamp"
        customerBuilder.UpdateTrackingColumn = "UpdateTimestamp"
        customerBuilder.DeletionTrackingColumn = "DeleteTimestamp"

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)

    End Sub
End Class
'</snippetOCS_VB_NTier_SampleServerSyncProvider>