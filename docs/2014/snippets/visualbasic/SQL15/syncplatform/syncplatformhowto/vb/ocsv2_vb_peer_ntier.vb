Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.ServiceModel

Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data


Class OCSv2_VB_Peer_NTier

    Shared Sub Main(ByVal args() As String)

        '<snippetOCSv2_VB_Basic_NTier_SyncOrchestrator>
        Dim localProvider As KnowledgeSyncProvider
        Dim remoteProvider As KnowledgeSyncProvider

        Dim localConnection As String = "Data Source = localhost; Initial Catalog = SyncSamplesDb_Peer1; " _
                                      & "Integrated Security = True"
        Dim remoteConnection As String = "http://localhost:8000/Sync/SyncService"
        Dim scopeName As String = "Sales"

        Dim sampleSyncProvider As New SampleSyncProvider()
        localProvider = sampleSyncProvider.SetupSyncProvider(scopeName, localConnection)
        remoteProvider = New SyncProxy(scopeName, remoteConnection)

        Dim syncOrchestrator As New SyncOrchestrator()

        syncOrchestrator.LocalProvider = localProvider
        syncOrchestrator.RemoteProvider = remoteProvider

        syncOrchestrator.Direction = SyncDirectionOrder.Download
        syncOrchestrator.Synchronize()
        '</snippetOCSv2_VB_Basic_NTier_SyncOrchestrator>

    End Sub 'Main 
End Class 'OCSv2_VB_Peer_NTier

'<snippetOCSv2_VB_Basic_NTier_SyncProxy>
Public Class SyncProxy
    Inherits KnowledgeSyncProvider

    Public Sub New(ByVal scopeName As String, ByVal url As String)
        Me.scopeName = scopeName

        Dim binding As New WSHttpBinding()
        binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
        binding.OpenTimeout = New TimeSpan(0, 1, 0)
        'Dim factory As New ChannelFactory(< ISyncContract > binding, New EndpointAddress(url))
        'proxy = factory.CreateChannel()

    End Sub 'New

    'The IdFormat settings for the peer provider.
    Public Overrides ReadOnly Property IdFormats() As SyncIdFormatGroup
        Get
            If idFormatGroup Is Nothing Then
                idFormatGroup = New SyncIdFormatGroup()

                '
                ' 1 byte change unit ID
                '
                idFormatGroup.ChangeUnitIdFormat.IsVariableLength = False
                idFormatGroup.ChangeUnitIdFormat.Length = 1

                '
                ' GUID replica ID
                '
                idFormatGroup.ReplicaIdFormat.IsVariableLength = False
                idFormatGroup.ReplicaIdFormat.Length = 16


                '
                ' Global ID for item IDs
                '
                idFormatGroup.ItemIdFormat.IsVariableLength = True
                idFormatGroup.ItemIdFormat.Length = 10 * 1024
            End If

            Return idFormatGroup
        End Get
    End Property


    Public Overrides Sub BeginSession(ByVal position As SyncProviderPosition, ByVal syncSessionContext As SyncSessionContext)
        proxy.BeginSession(scopeName)

    End Sub


    Public Overrides Sub EndSession(ByVal syncSessionContext As SyncSessionContext)
        proxy.EndSession()

    End Sub


    Public Overrides Sub GetSyncBatchParameters(ByRef batchSize As System.UInt32, ByRef knowledge As SyncKnowledge)  'ToDo: Unsigned Integers not supported
        proxy.GetKnowledge(batchSize, knowledge)

    End Sub


    Public Overrides Function GetChangeBatch(ByVal batchSize As System.UInt32, ByVal destinationKnowledge As SyncKnowledge, ByRef changeDataRetriever As Object) As ChangeBatch  'ToDo: Unsigned Integers not supported
        Return proxy.GetChanges(batchSize, destinationKnowledge, changeDataRetriever)

    End Function


    Public Overrides Sub ProcessChangeBatch(ByVal resolutionPolicy As ConflictResolutionPolicy, ByVal sourceChanges As ChangeBatch, ByVal changeDataRetriever As Object, ByVal syncCallback As SyncCallbacks, ByVal sessionStatistics As SyncSessionStatistics)
        Dim remoteSessionStatistics As New SyncSessionStatistics()
        proxy.ApplyChanges(resolutionPolicy, sourceChanges, changeDataRetriever, remoteSessionStatistics)
        sessionStatistics.ChangesApplied = remoteSessionStatistics.ChangesApplied
        sessionStatistics.ChangesFailed = remoteSessionStatistics.ChangesFailed

    End Sub


    Public Overrides Function GetFullEnumerationChangeBatch(ByVal batchSize As System.UInt32, ByVal lowerEnumerationBound As SyncId, ByVal knowledgeForDataRetrieval As SyncKnowledge, ByRef changeDataRetriever As Object) As FullEnumerationChangeBatch  'ToDo: Unsigned Integers not supported
        'Do nothing.
        '
        Throw New NotImplementedException()

    End Function


    Public Overrides Sub ProcessFullEnumerationChangeBatch(ByVal resolutionPolicy As ConflictResolutionPolicy, ByVal sourceChanges As FullEnumerationChangeBatch, ByVal changeDataRetriever As Object, ByVal syncCallback As SyncCallbacks, ByVal sessionStatistics As SyncSessionStatistics)
        'Do nothing.
        '
        Throw New NotImplementedException()

    End Sub

    Private scopeName As String
    Private proxy As ISyncContract
    Private idFormatGroup As SyncIdFormatGroup = Nothing

End Class 'SyncProxy
'</snippetOCSv2_VB_Basic_NTier_SyncProxy>

'<snippetOCSv2_VB_Basic_NTier_SyncService>
<ServiceBehavior(IncludeExceptionDetailInFaults:=True)> _
Public Class SyncService
    Implements ISyncContract

    Public Sub BeginSession(ByVal scopeName As String) Implements ISyncContract.BeginSession

        Dim localConnection As String = "Data Source = localhost; Initial Catalog = SyncSamplesDb_Peer2; " _
                                      & "Integrated Security = True"
        Dim sampleSyncProvider As New SampleSyncProvider()
        peerProvider = sampleSyncProvider.SetupSyncProvider(scopeName, localConnection)

    End Sub


    Public Sub GetKnowledge(ByRef batchSize As System.UInt32, ByRef knowledge As SyncKnowledge) Implements ISyncContract.GetKnowledge

        peerProvider.GetSyncBatchParameters(batchSize, knowledge)

    End Sub


    Public Function GetChanges(ByVal batchSize As System.UInt32, ByVal destinationKnowledge As SyncKnowledge, ByRef changeData As Object) As ChangeBatch Implements ISyncContract.GetChanges

        Return peerProvider.GetChangeBatch(batchSize, destinationKnowledge, changeData)

    End Function


    Public Sub ApplyChanges(ByVal resolutionPolicy As ConflictResolutionPolicy, ByVal sourceChanges As ChangeBatch, ByVal changeData As Object, ByRef sessionStatistics As SyncSessionStatistics) Implements ISyncContract.ApplyChanges

        Dim syncCallback As New SyncCallbacks()
        peerProvider.ProcessChangeBatch(resolutionPolicy, sourceChanges, changeData, syncCallback, sessionStatistics)

    End Sub


    Public Sub EndSession() Implements ISyncContract.EndSession

        peerProvider = Nothing

    End Sub

    Private peerProvider As DbSyncProvider = Nothing

End Class
'</snippetOCSv2_VB_Basic_NTier_SyncService>

'<snippetOCSv2_VB_Basic_NTier_ISyncContract>
<ServiceContract(SessionMode:=SessionMode.Required), _
ServiceKnownType(GetType(SyncIdFormatGroup)), _
ServiceKnownType(GetType(DbSyncContext))> _
Public Interface ISyncContract

    <OperationContract(IsInitiating:=True, IsTerminating:=False)> _
    Sub BeginSession(ByVal scopeName As String)

    <OperationContract(IsInitiating:=False, IsTerminating:=False)> _
    Sub GetKnowledge(ByRef batchSize As System.UInt32, ByRef knowledge As SyncKnowledge)  'ToDo: Unsigned Integers not supported

    <OperationContract(IsInitiating:=False, IsTerminating:=False)> _
    Function GetChanges(ByVal batchSize As System.UInt32, ByVal destinationKnowledge As SyncKnowledge, ByRef changeData As Object) As ChangeBatch  'ToDo: Unsigned Integers not supported

    <OperationContract(IsInitiating:=False, IsTerminating:=False)> _
    Sub ApplyChanges(ByVal resolutionPolicy As ConflictResolutionPolicy, ByVal sourceChanges As ChangeBatch, ByVal changeData As Object, ByRef sessionStatistics As SyncSessionStatistics)

    <OperationContract(IsInitiating:=False, IsTerminating:=True)> _
    Sub EndSession()

End Interface
'</snippetOCSv2_VB_Basic_NTier_ISyncContract>

'<snippetOCSv2_VB_Basic_NTier_SampleSyncProvider>
Public Class SampleSyncProvider

    Public Function SetupSyncProvider(ByVal scopeName As String, ByVal peerConnString As String) As DbSyncProvider

        Const MetadataAgingInHours As Integer = 100

        Dim peerProvider As New DbSyncProvider()

        Dim peerConnection As New SqlConnection(peerConnString)
        peerProvider.Connection = peerConnection
        peerProvider.ScopeName = scopeName


        Dim adapterCustomer As New DbSyncAdapter("Customer")
        adapterCustomer.RowIdColumns.Add("CustomerId")


        Dim chgsCustomerCmd As New SqlCommand()
        chgsCustomerCmd.CommandType = CommandType.StoredProcedure
        chgsCustomerCmd.CommandText = "Sales.sp_Customer_SelectChanges"
        chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMetadataOnly, SqlDbType.Int)
        chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
        chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncInitialize, SqlDbType.Int)

        adapterCustomer.SelectIncrementalChangesCommand = chgsCustomerCmd


        Dim insCustomerCmd As New SqlCommand()
        insCustomerCmd.CommandType = CommandType.StoredProcedure
        insCustomerCmd.CommandText = "Sales.sp_Customer_ApplyInsert"
        insCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        insCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar)
        insCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
        insCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar)
        insCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        adapterCustomer.InsertCommand = insCustomerCmd


        Dim updCustomerCmd As New SqlCommand()
        updCustomerCmd.CommandType = CommandType.StoredProcedure
        updCustomerCmd.CommandText = "Sales.sp_Customer_ApplyUpdate"
        updCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        updCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar)
        updCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
        updCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar)
        updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
        updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int)

        adapterCustomer.UpdateCommand = updCustomerCmd


        Dim delCustomerCmd As New SqlCommand()
        delCustomerCmd.CommandType = CommandType.StoredProcedure
        delCustomerCmd.CommandText = "Sales.sp_Customer_ApplyDelete"
        delCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
        delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        adapterCustomer.DeleteCommand = delCustomerCmd


        Dim selRowCustomerCmd As New SqlCommand()
        selRowCustomerCmd.CommandType = CommandType.StoredProcedure
        selRowCustomerCmd.CommandText = "Sales.sp_Customer_SelectRow"
        selRowCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)

        adapterCustomer.SelectRowCommand = selRowCustomerCmd


        Dim insMetadataCustomerCmd As New SqlCommand()
        insMetadataCustomerCmd.CommandType = CommandType.StoredProcedure
        insMetadataCustomerCmd.CommandText = "Sales.sp_Customer_InsertMetadata"
        insMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int)
        insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        adapterCustomer.InsertMetadataCommand = insMetadataCustomerCmd


        Dim updMetadataCustomerCmd As New SqlCommand()
        updMetadataCustomerCmd.CommandType = CommandType.StoredProcedure
        updMetadataCustomerCmd.CommandText = "Sales.sp_Customer_UpdateMetadata"
        updMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
        updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        adapterCustomer.UpdateMetadataCommand = updMetadataCustomerCmd


        Dim delMetadataCustomerCmd As New SqlCommand()
        delMetadataCustomerCmd.CommandType = CommandType.StoredProcedure
        delMetadataCustomerCmd.CommandText = "Sales.sp_Customer_DeleteMetadata"
        delMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
        delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
        delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        adapterCustomer.DeleteMetadataCommand = delMetadataCustomerCmd


        Dim selMetadataCustomerCmd As New SqlCommand()
        selMetadataCustomerCmd.CommandType = CommandType.StoredProcedure
        selMetadataCustomerCmd.CommandText = "Sales.sp_Customer_SelectMetadata"
        selMetadataCustomerCmd.Parameters.Add("@metadata_aging_in_hours", SqlDbType.Int).Value = MetadataAgingInHours

        adapterCustomer.SelectMetadataForCleanupCommand = selMetadataCustomerCmd

        peerProvider.SyncAdapters.Add(adapterCustomer)

        Dim selectNewTimestampCommand As New SqlCommand()
        Dim newTimestampVariable As String = "@" + DbSyncSession.SyncNewTimestamp
        selectNewTimestampCommand.CommandText = "SELECT " + newTimestampVariable + " = min_active_rowversion() - 1"
        selectNewTimestampCommand.Parameters.Add(newTimestampVariable, SqlDbType.Timestamp)
        selectNewTimestampCommand.Parameters(newTimestampVariable).Direction = ParameterDirection.Output

        peerProvider.SelectNewTimestampCommand = selectNewTimestampCommand


        Dim selReplicaInfoCmd As New SqlCommand()
        selReplicaInfoCmd.CommandType = CommandType.Text
        selReplicaInfoCmd.CommandText = "SELECT " _
                         & "@" + DbSyncSession.SyncScopeId + " = scope_id, " _
                         & "@" + DbSyncSession.SyncScopeKnowledge + " = scope_sync_knowledge, " _
                         & "@" + DbSyncSession.SyncScopeCleanupKnowledge + " = scope_tombstone_cleanup_knowledge, " _
                         & "@" + DbSyncSession.SyncScopeTimestamp + " = scope_timestamp " _
                         & "FROM Sales.ScopeInfo " _
                         & "WHERE scope_name = @" + DbSyncSession.SyncScopeName
        selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
        selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeId, SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output
        selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000).Direction = ParameterDirection.Output
        selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000).Direction = ParameterDirection.Output
        selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt).Direction = ParameterDirection.Output

        peerProvider.SelectScopeInfoCommand = selReplicaInfoCmd


        Dim updReplicaInfoCmd As New SqlCommand()
        updReplicaInfoCmd.CommandType = CommandType.Text
        updReplicaInfoCmd.CommandText = "UPDATE  Sales.ScopeInfo SET " _
                         & "scope_sync_knowledge = @" + DbSyncSession.SyncScopeKnowledge + ", " _
                         & "scope_tombstone_cleanup_knowledge = @" + DbSyncSession.SyncScopeCleanupKnowledge + " " _
                         & "WHERE scope_name = @" + DbSyncSession.SyncScopeName + " AND " _
                         & " ( @" + DbSyncSession.SyncCheckConcurrency + " = 0 or scope_timestamp = @" + DbSyncSession.SyncScopeTimestamp + "); " _
                         & "SET @" + DbSyncSession.SyncRowCount + " = @@rowcount"
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000)
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000)
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt)
        updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output

        peerProvider.UpdateScopeInfoCommand = updReplicaInfoCmd

        Return peerProvider

    End Function 'SetupSyncProvider 
End Class 'SampleSyncProvider
'</snippetOCSv2_VB_Basic_NTier_SampleSyncProvider>