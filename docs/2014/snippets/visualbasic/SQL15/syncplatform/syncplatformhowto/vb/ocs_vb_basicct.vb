'<snippetOCS_VB_BasicCT>
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data
Imports Microsoft.Synchronization.Data.Server
Imports Microsoft.Synchronization.Data.SqlServerCe

Class Program

    Shared Sub Main(ByVal args() As String)
        'The Utility class handles all functionality that is not
        'directly related to synchronization, such as holding connection 
        'string information and making changes to the server database.
        Dim util As New Utility()

        'The SampleStats class handles information from the SyncStatistics
        'object that the Synchronize method returns.
        Dim sampleStats As New SampleStats()

        'Delete and re-create the database. The client synchronization
        'provider also enables you to create the client database 
        'if it does not exist.
        util.SetClientPassword()
        util.RecreateClientDatabase()

        'Initial synchronization. Instantiate the SyncAgent
        'and call Synchronize.
        '<snippetOCS_VB_BasicCT_Synchronize>
        Dim sampleSyncAgent As New SampleSyncAgent()
        Dim syncStatistics As SyncStatistics = sampleSyncAgent.Synchronize()
        '</snippetOCS_VB_BasicCT_Synchronize>
        sampleStats.DisplayStats(syncStatistics, "initial")

        'Make changes on the server.
        util.MakeDataChangesOnServer()

        'Subsequent synchronization.
        syncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "subsequent")

        'Return server data back to its original state.
        util.CleanUpServer()

        'Exit.
        Console.Write(vbLf + "Press Enter to close the window.")
        Console.ReadLine()

    End Sub 'Main
End Class 'Program

'Create a class that is derived from 
'Microsoft.Synchronization.SyncAgent.
Public Class SampleSyncAgent
    Inherits SyncAgent

    Public Sub New()
        'Instantiate a client synchronization provider and specify it
        'as the local provider for this synchronization agent.
        Me.LocalProvider = New SampleClientSyncProvider()

        'Instantiate a server synchronization provider and specify it
        'as the remote provider for this synchronization agent.
        Me.RemoteProvider = New SampleServerSyncProvider()

        'Add the Customer table: specify a synchronization direction of
        'DownloadOnly.
        '<snippetOCS_VB_BasicCT_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.DownloadOnly
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_BasicCT_CustomerSyncTable>

    End Sub 'New 
End Class 'SampleSyncAgent

'Create a class that is derived from 
'Microsoft.Synchronization.Server.DbServerSyncProvider.
Public Class SampleServerSyncProvider
    Inherits DbServerSyncProvider

    Public Sub New()
        'Create a connection to the sample server database.
        Dim util As New Utility()
        Dim serverConn As New SqlConnection(util.ServerConnString)
        Me.Connection = serverConn

        'Create a command to retrieve a new anchor value from
        'the server. In this case, we use a timestamp value
        'that is retrieved and stored in the client database.
        'During each synchronization, the new anchor value and
        'the last anchor value from the previous synchronization
        'are used: the set of changes between these upper and
        'lower bounds is synchronized.
        '
        'SyncSession.SyncNewReceivedAnchor is a string constant; 
        'you could also use @sync_new_received_anchor directly in 
        'your queries.
        '<snippetOCS_VB_BasicCT_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = _
                "SELECT " + newAnchorVariable + " = change_tracking_current_version()"
            .Parameters.Add(newAnchorVariable, SqlDbType.BigInt)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_BasicCT_NewAnchorCommand>

        'Create a SyncAdapter for the Customer table by using 
        'the SqlSyncAdapterBuilder:
        '  * Specify the base table names.
        '  * Specify that the server uses SQL Server change tracking.
        '  * Specify download-only synchronization.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).
        '<snippetOCS_VB_BasicCT_CustomerAdapterBuilder>
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)

        customerBuilder.TableName = "Sales.Customer"
        customerBuilder.ChangeTrackingType = ChangeTrackingType.SqlServerChangeTracking

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)
        '</snippetOCS_VB_BasicCT_CustomerAdapterBuilder>

    End Sub 'New
End Class 'SampleServerSyncProvider

'Create a class that is derived from 
'Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
'You can just instantiate the provider directly and associate it
'with the SyncAgent, but you could use this class to handle client 
'provider events and other client-side processing.
Public Class SampleClientSyncProvider
    Inherits SqlCeClientSyncProvider


    Public Sub New()
        'Specify a connection string for the sample client database.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

    End Sub 'New
End Class 'SampleClientSyncProvider

'Handle the statistics that are returned by the SyncAgent.
Public Class SampleStats

    Public Sub DisplayStats(ByVal syncStatistics As SyncStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization ****")
        End If

        '<snippetOCS_VB_BasicCT_Statistics>
        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)
        '</snippetOCS_VB_BasicCT_Statistics>

    End Sub 'DisplayStats 
End Class 'SampleStats


Public Class Utility

    Private Shared _clientPassword As String

    'Get and set the client database password.
    Public Shared Property Password() As String
        Get
            Return _clientPassword
        End Get
        Set(ByVal value As String)
            _clientPassword = value
        End Set
    End Property

    'Have the user enter a password for the client database file.
    Public Sub SetClientPassword()
        Console.WriteLine("Type a strong password for the client")
        Console.WriteLine("database, and then press Enter.")
        Utility.Password = Console.ReadLine()

    End Sub 'SetClientPassword

    'Return the client connection string with the password.
    Public ReadOnly Property ClientConnString() As String
        Get
            Return "Data Source='SyncSampleClient.sdf'; Password=" + Utility.Password
        End Get
    End Property

    'Return the server connection string. 
    Public ReadOnly Property ServerConnString() As String

        Get
            Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_ChangeTracking; Integrated Security=True"
        End Get
    End Property


    'Make server changes that are synchronized on the second 
    'synchronization.
    Public Sub MakeDataChangesOnServer()
        Dim rowCount As Integer = 0

        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim sqlCommand As SqlCommand = serverConn.CreateCommand()
            sqlCommand.CommandText = _
                "INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) " _
              & "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " _
              & "UPDATE Sales.Customer " _
              & "SET  SalesPerson = 'James Bailey' " _
              & "WHERE CustomerName = 'Tandem Bicycle Store' " _
              & "DELETE FROM Sales.Customer WHERE CustomerName = 'Sharp Bikes'"
            serverConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

        Console.WriteLine("Rows inserted, updated, or deleted at the server: " & rowCount)

    End Sub 'MakeDataChangesOnServer


    'Revert changes that were made during synchronization.
    Public Sub CleanUpServer()
        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim sqlCommand As SqlCommand = serverConn.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = "usp_InsertSampleData"

            serverConn.Open()
            sqlCommand.ExecuteNonQuery()
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

    End Sub 'CleanUpServer


    'Delete the client database.
    Public Sub RecreateClientDatabase()
        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try
            If File.Exists(clientConn.Database) Then
                File.Delete(clientConn.Database)
            End If
        Finally
            clientConn.Dispose()
        End Try

        Dim sqlCeEngine As New SqlCeEngine(Me.ClientConnString)
        sqlCeEngine.CreateDatabase()

    End Sub 'RecreateClientDatabase
End Class 'Utility
'</snippetOCS_VB_BasicCT>