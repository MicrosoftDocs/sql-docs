'<snippetOCSv3_VB_Basic_SqlPeer> 
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data
Imports Microsoft.Synchronization.Data.SqlServer
Imports Microsoft.Synchronization.Data.SqlServerCe

Namespace Microsoft.Samples.Synchronization
    Class Program
        Public Shared Sub Main(ByVal args As String())

            ' Create the connections over which provisioning and synchronization 
            ' are performed. The Utility class handles all functionality that is not 
            ' directly related to synchronization, such as holding connection 
            ' string information and making changes to the server database. 
            Dim serverConn As New SqlConnection(Utility.ServerConnString)
            Dim clientSqlConn As New SqlConnection(Utility.ClientSqlConnString)
            Dim clientSqlCeConn As New SqlCeConnection(Utility.ClientSqlCeConnString)

            ' Create a scope named "filtered_customer", and add two tables to the scope. 
            ' GetDescriptionForTable gets the schema of each table, so that tracking 
            ' tables and triggers can be created for that table. 
            '<snippetOCSv3_VB_Basic_SqlPeer_ScopeDesc> 
            Dim scopeDesc As New DbSyncScopeDescription("filtered_customer")

            scopeDesc.Tables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("Customer", serverConn))
            scopeDesc.Tables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable("CustomerContact", serverConn))
            '</snippetOCSv3_VB_Basic_SqlPeer_ScopeDesc> 

            ' Create a provisioning object for "filtered_customer" and specify that 
            ' base tables should not be created (They already exist in SyncSamplesDb_SqlPeer1). 
            '<snippetOCSv3_VB_Basic_SqlPeer_ServerConfig> 
            Dim serverConfig As New SqlSyncScopeProvisioning(scopeDesc)
            serverConfig.CreateTableDefault = DbSyncCreationOption.Skip

            ' Specify which column(s) in the Customer table to use for filtering data, 
            ' and the filtering clause to use against the tracking table. 
            ' "[side]" is an alias for the tracking table. 
            serverConfig("Customer").AddFilterColumn("CustomerType")
            serverConfig("Customer").FilterClause = "[side].[CustomerType] = 'Retail'"

            ' Configure the scope and change tracking infrastructure. 
            serverConfig.Apply(serverConn)

            ' Write the configuration script to a file. You can modify 
            ' this script if necessary and run it against the server 
            ' to customize behavior. 
            File.WriteAllText("SampleConfigScript.txt", serverConfig.Script("SyncSamplesDb_SqlPeer1"))
            '</snippetOCSv3_VB_Basic_SqlPeer_ServerConfig> 


            ' Retrieve scope information from the server and use the schema that is retrieved 
            ' to provision the SQL Server and SQL Server Compact client databases. 

            '<snippetOCSv3_VB_Basic_SqlPeer_ClientConfig> 
            ' This database already exists on the server. 
            Dim clientSqlDesc As DbSyncScopeDescription = SqlSyncDescriptionBuilder.GetDescriptionForScope("filtered_customer", serverConn)
            Dim clientSqlConfig As New SqlSyncScopeProvisioning(clientSqlDesc)
            clientSqlConfig.Apply(clientSqlConn)

            ' This database does not yet exist. 
            Utility.CreateSqlCeDatabase()
            Dim clientSqlCeDesc As DbSyncScopeDescription = SqlSyncDescriptionBuilder.GetDescriptionForScope("filtered_customer", serverConn)
            Dim clientSqlCeConfig As New SqlCeSyncScopeProvisioning(clientSqlCeDesc)
            clientSqlCeConfig.Apply(clientSqlCeConn)
            '</snippetOCSv3_VB_Basic_SqlPeer_ClientConfig> 


            ' Initial synchronization sessions. 7 rows are synchronized: 
            ' all rows (4) from CustomerContact, and the 3 rows from Customer 
            ' that satisfy the filtering criteria. 
            '<snippetOCSv3_VB_Basic_SqlPeer_InitialSync> 
            Dim syncOrchestrator As SampleSyncOrchestrator
            Dim syncStats As SyncOperationStatistics

            ' Data is downloaded from the server to the SQL Server client. 
            syncOrchestrator = New SampleSyncOrchestrator(New SqlSyncProvider("filtered_customer", clientSqlConn), New SqlSyncProvider("filtered_customer", serverConn))
            syncStats = syncOrchestrator.Synchronize()
            syncOrchestrator.DisplayStats(syncStats, "initial")

            ' Data is downloaded from the SQL Server client to the 
            ' SQL Server Compact client. 
            syncOrchestrator = New SampleSyncOrchestrator(New SqlCeSyncProvider("filtered_customer", clientSqlCeConn), New SqlSyncProvider("filtered_customer", clientSqlConn))
            syncStats = syncOrchestrator.Synchronize()
            syncOrchestrator.DisplayStats(syncStats, "initial")
            '</snippetOCSv3_VB_Basic_SqlPeer_InitialSync> 


            ' Make changes on the server: 1 insert, 1 update, and 1 delete. 
            Utility.MakeDataChangesOnServer()


            ' Synchronize again. Three changes were made on the server, but 
            ' only two of them applied to rows that are in the "filtered_customer" 
            ' scope. The other row is not synchronized. 
            ' Notice that the order of synchronization is different from the initial 
            ' sessions, but the two changes are propagated to all nodes. 
            syncOrchestrator = New SampleSyncOrchestrator(New SqlCeSyncProvider("filtered_customer", clientSqlCeConn), New SqlSyncProvider("filtered_customer", serverConn))
            syncStats = syncOrchestrator.Synchronize()
            syncOrchestrator.DisplayStats(syncStats, "subsequent")

            syncOrchestrator = New SampleSyncOrchestrator(New SqlSyncProvider("filtered_customer", clientSqlConn), New SqlCeSyncProvider("filtered_customer", clientSqlCeConn))
            syncStats = syncOrchestrator.Synchronize()
            syncOrchestrator.DisplayStats(syncStats, "subsequent")

            serverConn.Close()
            serverConn.Dispose()
            clientSqlConn.Close()
            clientSqlConn.Dispose()
            clientSqlCeConn.Close()
            clientSqlCeConn.Dispose()

            Console.Write(vbLf & "Press any key to exit.")

            Console.Read()
        End Sub

    End Class

    Public Class SampleSyncOrchestrator
        Inherits SyncOrchestrator
        '<snippetOCSv3_VB_Basic_SqlPeer_SampleSyncOrchestrator> 
        Public Sub New(ByVal localProvider As RelationalSyncProvider, ByVal remoteProvider As RelationalSyncProvider)

            Me.LocalProvider = localProvider
            Me.RemoteProvider = remoteProvider
            Me.Direction = SyncDirectionOrder.UploadAndDownload
        End Sub
        '</snippetOCSv3_VB_Basic_SqlPeer_SampleSyncOrchestrator> 

        Public Sub DisplayStats(ByVal syncStatistics As SyncOperationStatistics, ByVal syncType As String)
            Console.WriteLine([String].Empty)
            If syncType = "initial" Then
                Console.WriteLine("****** Initial Synchronization ******")
            ElseIf syncType = "subsequent" Then
                Console.WriteLine("***** Subsequent Synchronization ****")
            End If

            Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
            Console.WriteLine("Total Changes Uploaded: " & syncStatistics.UploadChangesTotal)
            Console.WriteLine("Total Changes Downloaded: " & syncStatistics.DownloadChangesTotal)
            Console.WriteLine("Complete Time: " & syncStatistics.SyncEndTime)
            Console.WriteLine([String].Empty)
        End Sub
    End Class



    Public Class Utility

        'Return connection strings 

        Public Shared ReadOnly Property ServerConnString() As String

            Get
                Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_SqlPeer1; Integrated Security=True"
            End Get
        End Property


        Public Shared ReadOnly Property ClientSqlConnString() As String

            Get
                Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_SqlPeer2; Integrated Security=True"
            End Get
        End Property


        Public Shared ReadOnly Property ClientSqlCeConnString() As String
            Get
                Return "Data Source='SyncSampleClient.sdf'"
            End Get
        End Property


        'Make server changes that are synchronized on the second 
        'synchronization. 
        Public Shared Sub MakeDataChangesOnServer()
            Dim rowCount As Integer = 0

            Using serverConn As New SqlConnection(Utility.ServerConnString)
                Dim sqlCommand As SqlCommand = serverConn.CreateCommand()


                sqlCommand.CommandText = _
                    "INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) " _
                    & "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " _
                    & "UPDATE Customer " & "SET SalesPerson = 'James Bailey' " _
                    & "WHERE CustomerName = 'Tandem Bicycle Store' " _
                    & "DELETE FROM Customer WHERE CustomerName = 'Sharp Bikes'"

                serverConn.Open()
                rowCount = sqlCommand.ExecuteNonQuery()
                serverConn.Close()
            End Using

            ' Row count is divided by 2 because of inserts to tracking tables. 
            Console.WriteLine("Rows inserted, updated, or deleted at the server: " & rowCount / 2)
        End Sub

        Public Shared Sub CreateSqlCeDatabase()
            Using clientConn As New SqlCeConnection(Utility.ClientSqlCeConnString)
                If File.Exists(clientConn.Database) Then
                    File.Delete(clientConn.Database)
                End If
            End Using

            Dim sqlCeEngine As New SqlCeEngine(Utility.ClientSqlCeConnString)
            sqlCeEngine.CreateDatabase()
        End Sub
    End Class
End Namespace
'</snippetOCSv3_VB_Basic_SqlPeer>