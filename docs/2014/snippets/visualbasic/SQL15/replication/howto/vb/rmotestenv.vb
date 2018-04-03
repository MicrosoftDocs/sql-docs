Option Compare Text
Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.Text
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Smo.Agent
Imports Microsoft.SqlServer.Replication

Class RMOTestEnv

    ' Set the global testing defaults
    Private Shared publisherInstance As String = Environment.MachineName
    Private Shared subscriberInstance As String = Environment.MachineName
    Private Shared loginName As String = Settings.Default.LoginName
    Private Shared password As String = Settings.Default.Password
    Private Shared hostName As String = Settings.Default.HostName

    <STAThread()> Shared Sub Main(ByVal args() As String)
        If args.Length < 1 Then
            Throw New ApplicationException("You must specify a command line parameter")
        End If

        args(0) = args(0).ToUpper()

        Try
            ' Create the sub DB if not exists.
            Initialize()

            Select Case args(0)
                Case "-CDP"
                    ConfigDistPublisher()

                Case "-MDP"
                    If args.Length = 2 Then
                        ModifyDistPublisher(args(1))
                    Else
                        Throw New ApplicationException("You must supply the New distributor password.")
                    End If

                Case "-RDP"
                    RemoveDistPublisher()

                Case "-RPF"
                    RemoveDistPublisherForce()

                Case "-CMP"
                    CreateMergePublication(loginName, password)

                Case "-CTP"
                    CreateTranPublication(loginName, password)

                Case "-HMP"
                    ChangeMergePublication()

                Case "-HTP"
                    ChangeTranPublicationCached()

                Case "-RTP"
                    RemoveTranPublication()

                Case "-RMP"
                    RemoveMergePublication()

                Case "-CTA"
                    CreateTranArticle()

                Case "-CMA"
                    CreateMergeArticle()

                Case "-CML"
                    CreateMergePullSub(loginName, password)

                Case "-CMS"
                    CreateMergePushSub(loginName, password)

                Case "-CTS"
                    CreateTranPushSub(loginName, password)

                Case "-CTL"
                    CreateTranPullSub(loginName, password)

                Case "-DTL"
                    RemoveTranPullSub()

                Case "-DTS"
                    RemoveTranPushSub()

                Case "-DML"
                    RemoveMergePullSub()

                Case "-DMS"
                    RemoveMergePushSub()

                Case "-STL"
                    SyncTranPullSub()

                Case "-STS"
                    SyncTranPushSub()

                Case "-SML"
                    SyncMergePullSub()

                Case "-SMS"
                    SyncMergePushSub()

                Case "-SMLJ"
                    SyncMergePullSubWithJob()

                Case "-SMSJ"
                    SyncMergePushSubWithJob()

                Case "-SMLW"
                    SyncMergePullSubNoJobWebSync(loginName, password)

                Case "-STSJ"
                    SyncTranPushSubWithJob()

                Case "-STLJ"
                    SyncTranPullSubWithJob()

                Case "-CWS"
                    ConfigureWebSync(loginName, password)

                Case "-CMLW"
                    CreateMergePullSubWebSync(loginName, password)

                Case "-CMLN"
                    CreateMergePullSubNoJob()

                Case "-GSJ"
                    GenerateSnapshotWithJob()

                Case "-GTS"
                    GenerateTranSnapshot()

                Case "-GMS"
                    GenerateMergeSnapshot()

                Case "-GFS"
                    GenerateFilteredSnapshot(hostName)

                Case "-CP"
                    CreateMergePartition(hostName)

                Case "-RBL"
                    RegisterBLH()

                Case "-HMAB"
                    ChangeMergeArticle_BLH()

                Case "-RTL"
                    ReinitTranPullSub()

                Case "-CSP"
                    ChangeServerPasswords(loginName, password)

                Case "-RML"
                    ReinitMergePullSub_WithUpload()

                Case "-VTP"
                    ValidateTranPublication()

                Case "-VMS"
                    ValidateMergeSubscription()

                Case "-CLR"
                    CreateLogicalRecord()

                Case "-CMLA"
                    CreateMergePullSubWebSyncAnon(loginName, password)

                    'Case "/?"
                    'Case "-?"
                    'Case "?"
                    '    OutputParams()

                Case Else
                    Console.Error.WriteLine("bad parameter passed")
            End Select
        Catch ex As Exception
            Console.Error.WriteLine("The following error occured: " + ex.ToString())
        Finally

        End Try
    End Sub

    Public Shared Sub Initialize()
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim conn As ServerConnection = New ServerConnection(subscriberInstance)
        Dim NewDatabase As Database
        Dim subServer As Server = New Server(conn)

        ' Create the subscription database.
        If subServer.Databases.Contains(subscriptionDbName) = False Then
            NewDatabase = New Database(subServer, subscriptionDbName)
            NewDatabase.Create()
        End If
    End Sub

    Public Shared Sub ConfigDistPublisher()
        '<snippetrmo_vb_AddDistPub>
        ' Set the server and database names
        Dim distributionDbName As String = "distribution"
        Dim publisherName As String = publisherInstance
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim distributionDb As DistributionDatabase
        Dim distributor As ReplicationServer
        Dim publisher As DistributionPublisher
        Dim publicationDb As ReplicationDatabase

        ' Create a connection to the server using Windows Authentication.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the server acting as the Distributor 
            ' and local Publisher.
            conn.Connect()

            ' Define the distribution database at the Distributor,
            ' but do not create it now.
            distributionDb = New DistributionDatabase(distributionDbName, conn)
            distributionDb.MaxDistributionRetention = 96
            distributionDb.HistoryRetention = 120

            ' Set the Distributor properties and install the Distributor.
            ' This also creates the specified distribution database.
            distributor = New ReplicationServer(conn)
            distributor.InstallDistributor((CType(Nothing, String)), distributionDb)

            ' Set the Publisher properties and install the Publisher.
            publisher = New DistributionPublisher(publisherName, conn)
            publisher.DistributionDatabase = distributionDb.Name
            publisher.WorkingDirectory = "\\" + publisherName + "\repldata"
            publisher.PublisherSecurity.WindowsAuthentication = True
            publisher.Create()

            ' Enable AdventureWorks2012 as a publication database.
            publicationDb = New ReplicationDatabase(publicationDbName, conn)

            publicationDb.EnabledTransPublishing = True
            publicationDb.EnabledMergePublishing = True

        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("An error occured when installing distribution and publishing.", ex)

        Finally
            conn.Disconnect()

        End Try
        '</snippetrmo_vb_AddDistPub>
    End Sub
    Public Shared Sub ModifyDistPublisher(ByVal password As String)
        '<snippetrmo_vb_ChangeDistPub>
        ' Set the Distributor and distribution database names.
        Dim distributionDbName As String = "distribution"
        Dim distributorName As String = publisherInstance

        Dim distributor As ReplicationServer
        Dim distributionDb As DistributionDatabase

        ' Create a connection to the Distributor using Windows Authentication.
        Dim conn As ServerConnection = New ServerConnection(distributorName)

        Try
            ' Open the connection. 
            conn.Connect()

            distributor = New ReplicationServer(conn)

            ' Load Distributor properties, if it is installed.
            If distributor.LoadProperties() Then
                ' Password supplied at runtime.
                distributor.ChangeDistributorPassword(password)
                distributor.AgentCheckupInterval = 5

                ' Save changes to the Distributor properties.
                distributor.CommitPropertyChanges()
            Else
                Throw New ApplicationException( _
                    String.Format("{0} is not a Distributor.", publisherInstance))
            End If

            ' Create an object for the distribution database 
            ' using the open Distributor connection.
            distributionDb = New DistributionDatabase(distributionDbName, conn)

            ' Change distribution database properties.
            If distributionDb.LoadProperties() Then
                ' Change maximum retention period to 48 hours and history retention 
                ' period to 24 hours.
                distributionDb.MaxDistributionRetention = 48
                distributionDb.HistoryRetention = 24

                ' Save changes to the distribution database properties.
                distributionDb.CommitPropertyChanges()
            Else
                ' Do something here if the distribution database does not exist.
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here. 
            Throw New ApplicationException("An error occured when changing Distributor " + _
                " or distribution database properties.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ChangeDistPub>
    End Sub
    Public Shared Sub RemoveDistPublisher()
        '<snippetrmo_vb_DropDistPub>
        ' Set the Distributor and publication database names.
        ' Publisher and Distributor are on the same server instance.
        Dim publisherName As String = publisherInstance
        Dim distributorName As String = subscriberInstance
        Dim distributionDbName As String = "distribution"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create connections to the Publisher and Distributor
        ' using Windows Authentication.
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)
        Dim distributorConn As ServerConnection = New ServerConnection(distributorName)

        ' Create the objects we need.
        Dim distributor As ReplicationServer
        distributor = New ReplicationServer(distributorConn)
        Dim publisher As DistributionPublisher
        Dim distributionDb As DistributionDatabase
        distributionDb = New DistributionDatabase(distributionDbName, distributorConn)
        Dim publicationDb As ReplicationDatabase
        publicationDb = New ReplicationDatabase(publicationDbName, publisherConn)

        Try
            ' Connect to the Publisher and Distributor.
            publisherConn.Connect()
            distributorConn.Connect()

            ' Disable all publishing on the AdventureWorks2012 database.
            If publicationDb.LoadProperties() Then
                If publicationDb.EnabledMergePublishing Then
                    publicationDb.EnabledMergePublishing = False
                ElseIf publicationDb.EnabledTransPublishing Then
                    publicationDb.EnabledTransPublishing = False
                End If
            Else
                Throw New ApplicationException( _
                    String.Format("The {0} database does not exist.", publicationDbName))
            End If

            ' We cannot uninstall the Publisher if there are still Subscribers.
            If distributor.RegisteredSubscribers.Count = 0 Then
                ' Uninstall the Publisher, if it exists.
                publisher = New DistributionPublisher(publisherName, distributorConn)
                If publisher.LoadProperties() Then
                    publisher.Remove(False)
                Else
                    ' Do something here if the Publisher does not exist.
                    Throw New ApplicationException(String.Format( _
                        "{0} is not a Publisher for {1}.", publisherName, distributorName))
                End If

                ' Drop the distribution database.
                If distributionDb.LoadProperties() Then
                    distributionDb.Remove()
                Else
                    ' Do something here if the distribition DB does not exist.
                    Throw New ApplicationException(String.Format( _
                     "The distribution database '{0}' does not exist on {1}.", _
                     distributionDbName, distributorName))
                End If

                ' Uninstall the Distributor, if it exists.
                If distributor.LoadProperties() Then
                    ' Passing a value of false means that the Publisher 
                    ' and distribution databases must already be uninstalled,
                    ' and that no local databases be enabled for publishing.
                    distributor.UninstallDistributor(False)
                Else
                    'Do something here if the distributor does not exist.
                    Throw New ApplicationException(String.Format( _
                        "The Distributor '{0}' does not exist.", distributorName))
                End If
            Else
                Throw New ApplicationException("You must first delete all subscriptions.")
            End If

        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The Publisher and Distributor could not be uninstalled", ex)

        Finally
            publisherConn.Disconnect()
            distributorConn.Disconnect()

        End Try
        '</snippetrmo_vb_DropDistPub>
    End Sub
    Public Shared Sub RemoveDistPublisherForce()
        '<snippetrmo_vb_DropDistPubForce>
        ' Set the Distributor and publication database names.
        ' Publisher and Distributor are on the same server instance.
        Dim distributorName As String = publisherInstance

        ' Create connections to the Distributor
        ' using Windows Authentication.
        Dim conn As ServerConnection = New ServerConnection(distributorName)
        conn.DatabaseName = "master"

        ' Create the objects we need.
        Dim distributor As ReplicationServer = New ReplicationServer(conn)

        Try
            ' Connect to the Publisher and Distributor.
            conn.Connect()


            ' Uninstall the Distributor, if it exists.
            ' Use the force parameter to remove everthing.  
            If distributor.IsDistributor And distributor.LoadProperties() Then
                ' Passing a value of true means that the Distributor 
                ' is uninstalled even when publishing objects, subscriptions,
                ' and distribution databases exist on the server.
                distributor.UninstallDistributor(True)
            Else
                'Do something here if the distributor does not exist.
            End If

        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The Publisher and Distributor could not be uninstalled", ex)

        Finally
            conn.Disconnect()

        End Try
        '</snippetrmo_vb_DropDistPubForce>
    End Sub
    Public Shared Sub CreateTranPublication(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateTranPub>
        ' Set the Publisher, publication database, and publication names.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim publisherName As String = publisherInstance

        Dim publicationDb As ReplicationDatabase
        Dim publication As TransPublication

        ' Create a connection to the Publisher using Windows Authentication.
        Dim conn As ServerConnection
        conn = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Enable the AdventureWorks2012 database for transactional publishing.
            publicationDb = New ReplicationDatabase(publicationDbName, conn)

            ' If the database exists and is not already enabled, 
            ' enable it for transactional publishing.
            If publicationDb.LoadProperties() Then
                If Not publicationDb.EnabledTransPublishing Then
                    publicationDb.EnabledTransPublishing = True
                End If

                ' If the Log Reader Agent does not exist, create it.
                If Not publicationDb.LogReaderAgentExists Then
                    ' Specify the Windows account under which the agent job runs.
                    ' This account will be used for the local connection to the 
                    ' Distributor and all agent connections that use Windows Authentication.
                    publicationDb.LogReaderAgentProcessSecurity.Login = winLogin
                    publicationDb.LogReaderAgentProcessSecurity.Password = winPassword

                    ' Explicitly set authentication mode for the Publisher connection
                    ' to the default value of Windows Authentication.
                    publicationDb.LogReaderAgentPublisherSecurity.WindowsAuthentication = True

                    ' Create the Log Reader Agent job.
                    publicationDb.CreateLogReaderAgent()
                End If
            Else
                Throw New ApplicationException(String.Format( _
                 "The {0} database does not exist at {1}.", _
                 publicationDb, publisherName))
            End If

            ' Set the required properties for the transactional publication.
            publication = New TransPublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Specify a transactional publication (the default).
            publication.Type = PublicationType.Transactional

            'Enable push and pull subscriptions and independent Distribition Agents.
            publication.Attributes = _
            publication.Attributes Or PublicationAttributes.AllowPull
            publication.Attributes = _
            publication.Attributes Or PublicationAttributes.AllowPush
            publication.Attributes = _
            publication.Attributes Or PublicationAttributes.IndependentAgent

            ' Activate the publication so that we can add subscriptions.
            publication.Status = State.Active

            ' Specify the Windows account under which the Snapshot Agent job runs.
            ' This account will be used for the local connection to the 
            ' Distributor and all agent connections that use Windows Authentication.
            publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin
            publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword

            ' Explicitly set the security mode for the Publisher connection
            ' Windows Authentication (the default).
            publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = True

            If Not publication.IsExistingObject Then
                ' Create the transactional publication.
                publication.Create()

                ' Create a Snapshot Agent job for the publication.
                publication.CreateSnapshotAgent()
            Else
                Throw New ApplicationException(String.Format( _
                    "The {0} publication already exists.", publicationName))
            End If
        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
                "The publication {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateTranPub>
    End Sub
    Public Shared Sub RemoveTranPublication()
        '<snippetrmo_vb_DropTranPub>
        ' Define the Publisher, publication database, 
        ' and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publication As TransPublication
        Dim publicationDb As ReplicationDatabase

        ' Create a connection to the Publisher 
        ' using Windows Authentication.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            conn.Connect()

            ' Set the required properties for the transactional publication.
            publication = New TransPublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Delete the publication, if it exists and has no subscriptions.
            If publication.LoadProperties() And Not publication.HasSubscription Then
                publication.Remove()
            Else
                ' Do something here if the publication does not exist
                ' or has subscriptions.
                Throw New ApplicationException(String.Format( _
                 "The publication {0} could not be deleted. " + _
                 "Ensure that the publication exists and that all " + _
                 "subscriptions have been deleted.", _
                 publicationName, publisherName))
            End If

            ' If no other transactional publications exists,
            ' disable publishing on the database.
            publicationDb = New ReplicationDatabase(publicationDbName, conn)
            If publicationDb.LoadProperties() Then
                If publicationDb.TransPublications.Count = 0 Then
                    publicationDb.EnabledTransPublishing = False
                End If
            Else
                ' Do something here if the database does not exist.
                Throw New ApplicationException(String.Format( _
                 "The database {0} does not exist on {1}.", _
                 publicationDbName, publisherName))
            End If
        Catch ex As Exception
            ' Implement application error handling here.
            Throw New ApplicationException(String.Format( _
             "The publication {0} could not be deleted.", _
             publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_DropTranPub>
    End Sub
    Public Shared Sub CreateMergePublication(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePub>
        ' Set the Publisher, publication database, and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publicationDb As ReplicationDatabase
        Dim publication As MergePublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Enable the database for merge publication.				
            publicationDb = New ReplicationDatabase(publicationDbName, conn)
            If publicationDb.LoadProperties() Then
                If Not publicationDb.EnabledMergePublishing Then
                    publicationDb.EnabledMergePublishing = True
                End If
            Else
                ' Do something here if the database does not exist. 
                Throw New ApplicationException(String.Format( _
                 "The {0} database does not exist on {1}.", _
                 publicationDb, publisherName))
            End If

            ' Set the required properties for the merge publication.
            publication = New MergePublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Enable precomputed partitions.
            publication.PartitionGroupsOption = PartitionGroupsOption.True

            ' Specify the Windows account under which the Snapshot Agent job runs.
            ' This account will be used for the local connection to the 
            ' Distributor and all agent connections that use Windows Authentication.
            publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin
            publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword

            ' Explicitly set the security mode for the Publisher connection
            ' Windows Authentication (the default).
            publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = True

            ' Enable Subscribers to request snapshot generation and filtering.
            publication.Attributes = publication.Attributes Or _
                PublicationAttributes.AllowSubscriberInitiatedSnapshot
            publication.Attributes = publication.Attributes Or _
                PublicationAttributes.DynamicFilters

            ' Enable pull and push subscriptions
            publication.Attributes = publication.Attributes Or _
                PublicationAttributes.AllowPull
            publication.Attributes = publication.Attributes Or _
                PublicationAttributes.AllowPush

            If Not publication.IsExistingObject Then
                ' Create the merge publication.
                publication.Create()

                ' Create a Snapshot Agent job for the publication.
                publication.CreateSnapshotAgent()
            Else
                Throw New ApplicationException(String.Format( _
                    "The {0} publication already exists.", publicationName))
            End If
        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
                "The publication {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePub>
    End Sub
    Public Shared Sub ChangeMergePublication()
        '<snippetrmo_vb_ChangeMergePub_ddl>
        ' Define the server, database, and publication names
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publication As MergePublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the publication.
            publication = New MergePublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' If we can't get the properties for this merge publication, then throw an application exception.
            If publication.LoadProperties() Then
                ' If DDL replication is currently enabled, disable it.
                If publication.ReplicateDdl = DdlReplicationOptions.All Then
                    publication.ReplicateDdl = DdlReplicationOptions.None
                Else
                    publication.ReplicateDdl = DdlReplicationOptions.All
                End If
            Else
                Throw New ApplicationException(String.Format( _
                 "Settings could not be retrieved for the publication. " + _
                 "Ensure that the publication {0} exists on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException( _
                "The publication property could not be changed.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ChangeMergePub_ddl>
    End Sub
    Public Shared Sub ChangeTranPublicationCached()
        '<snippetrmo_vb_ChangeTranPub_cached>
        ' Define the server, database, and publication names
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publication As TransPublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the publication.
            publication = New TransPublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Explicitly enable caching of property changes on this object.
            publication.CachePropertyChanges = True

            ' If we can't get the properties for this publication, 
            ' throw an application exception.
            If publication.LoadProperties() Then
                ' Enable support for push subscriptions and disable support 
                ' for pull subscriptions.
                If (publication.Attributes And PublicationAttributes.AllowPull) <> 0 Then
                    publication.Attributes = publication.Attributes _
                    Xor PublicationAttributes.AllowPull
                End If
                If (publication.Attributes And PublicationAttributes.AllowPush) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPush
                End If

                ' Send changes to the server.
                publication.CommitPropertyChanges()
            Else
                Throw New ApplicationException(String.Format( _
                 "Settings could not be retrieved for the publication. " + _
                 "Ensure that the publication {0} exists on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException( _
                "The publication property could not be changed.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ChangeTranPub_cached>
    End Sub
    Public Shared Sub RemoveMergePublication()
        '<snippetrmo_vb_DropMergePub>
        ' Define the Publisher, publication database, 
        ' and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publication As MergePublication
        Dim publicationDb As ReplicationDatabase

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the merge publication.
            publication = New MergePublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Delete the publication, if it exists and has no subscriptions.
            If (publication.LoadProperties() And Not publication.HasSubscription) Then
                publication.Remove()
            Else
                ' Do something here if the publication does not exist
                ' or has subscriptions.
                Throw New ApplicationException(String.Format( _
                 "The publication {0} could not be deleted. " + _
                 "Ensure that the publication exists and that all " + _
                 "subscriptions have been deleted.", _
                 publicationName, publisherName))
            End If

            ' If no other merge publications exists,
            ' disable publishing on the database.
            publicationDb = New ReplicationDatabase(publicationDbName, conn)
            If publicationDb.LoadProperties() Then
                If publicationDb.MergePublications.Count = 0 _
                And publicationDb.EnabledMergePublishing Then
                    publicationDb.EnabledMergePublishing = False
                End If
            Else
                ' Do something here if the database does not exist.
                Throw New ApplicationException(String.Format( _
                 "The database {0} does not exist on {1}.", _
                 publicationDbName, publisherName))
            End If
        Catch ex As Exception
            ' Implement application error handling here.
            Throw New ApplicationException(String.Format( _
             "The publication {0} could not be deleted.", _
             publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_DropMergePub>
    End Sub
    Public Shared Sub CreateTranArticle()
        '<snippetrmo_vb_CreateTranArticles>
        ' Define the Publisher, publication, and article names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim articleName As String = "Product"
        Dim schemaOwner As String = "Production"

        Dim article As TransArticle

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        ' Create a filtered transactional articles in the following steps:
        ' 1) Create the  article with a horizontal filter clause.
        ' 2) Add columns to or remove columns from the article.
        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Define a horizontally filtered, log-based table article.
            article = New TransArticle()
            article.ConnectionContext = conn
            article.Name = articleName
            article.DatabaseName = publicationDbName
            article.SourceObjectName = articleName
            article.SourceObjectOwner = schemaOwner
            article.PublicationName = publicationName
            article.Type = ArticleOptions.LogBased
            article.FilterClause = "DiscontinuedDate IS NULL"

            ' Ensure that we create the schema owner at the Subscriber.
            article.SchemaOption = article.SchemaOption Or _
            CreationScriptOptions.Schema

            If Not article.IsExistingObject Then
                ' Create the article.
                article.Create()
            Else
                Throw New ApplicationException(String.Format( _
                 "The article {0} already exists in publication {1}.", _
                 articleName, publicationName))
            End If

            ' Create an array of column names to remove from the article.
            Dim columns() As String = New String(0) {}
            columns(0) = "DaysToManufacture"

            ' Remove the column from the article.
            article.RemoveReplicatedColumns(columns)
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The article could not be created.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateTranArticles>
    End Sub
    Public Shared Sub CreateMergeArticle()
        '<snippetrmo_vb_CreateMergeArticles>
        ' Define the Publisher and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Specify article names.
        Dim articleName1 As String = "Employee"
        Dim articleName2 As String = "SalesOrderHeader"
        Dim articleName3 As String = "SalesOrderDetail"

        ' Specify join filter information.
        Dim filterName12 As String = "SalesOrderHeader_Employee"
        Dim filterClause12 As String = "Employee.EmployeeID = " + _
            "SalesOrderHeader.SalesPersonID"
        Dim filterName23 As String = "SalesOrderDetail_SalesOrderHeader"
        Dim filterClause23 As String = "SalesOrderHeader.SalesOrderID = " + _
            "SalesOrderDetail.SalesOrderID"

        Dim salesSchema As String = "Sales"
        Dim hrSchema As String = "HumanResources"

        Dim article1 As MergeArticle = New MergeArticle()
        Dim article2 As MergeArticle = New MergeArticle()
        Dim article3 As MergeArticle = New MergeArticle()
        Dim filter12 As MergeJoinFilter = New MergeJoinFilter()
        Dim filter23 As MergeJoinFilter = New MergeJoinFilter()

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        ' Create three merge articles that are horizontally partitioned
        ' using a parameterized row filter on Employee.EmployeeID, which is 
        ' extended to the two other articles using join filters. 
        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Create each article. 
            ' For clarity, each article is defined separately. 
            ' In practice, iterative structures and arrays should 
            ' be used to efficiently create multiple articles.

            ' Set the required properties for the Employee article.
            article1.ConnectionContext = conn
            article1.Name = articleName1
            article1.DatabaseName = publicationDbName
            article1.SourceObjectName = articleName1
            article1.SourceObjectOwner = hrSchema
            article1.PublicationName = publicationName
            article1.Type = ArticleOptions.TableBased

            ' Define the parameterized filter clause based on Hostname.
            article1.FilterClause = "Employee.LoginID = HOST_NAME()"

            ' Set the required properties for the SalesOrderHeader article.
            article2.ConnectionContext = conn
            article2.Name = articleName2
            article2.DatabaseName = publicationDbName
            article2.SourceObjectName = articleName2
            article2.SourceObjectOwner = salesSchema
            article2.PublicationName = publicationName
            article2.Type = ArticleOptions.TableBased

            ' Set the required properties for the SalesOrderDetail article.
            article3.ConnectionContext = conn
            article3.Name = articleName3
            article3.DatabaseName = publicationDbName
            article3.SourceObjectName = articleName3
            article3.SourceObjectOwner = salesSchema
            article3.PublicationName = publicationName
            article3.Type = ArticleOptions.TableBased

            ' Create the articles, if they do not already exist.
            If article1.IsExistingObject = False Then
                article1.Create()
            End If
            If article2.IsExistingObject = False Then
                article2.Create()
            End If
            If article3.IsExistingObject = False Then
                article3.Create()
            End If

            ' Select published columns for SalesOrderHeader.
            ' Create an array of column names to vertically filter out.
            ' In this example, only one column is removed.
            Dim columns() As String = New String(0) {}

            columns(0) = "CreditCardApprovalCode"

            ' Remove the column.
            article2.RemoveReplicatedColumns(columns)

            ' Define a merge filter clauses that filter 
            ' SalesOrderHeader based on Employee and 
            ' SalesOrderDetail based on SalesOrderHeader. 

            ' Parent article.
            filter12.JoinArticleName = articleName1
            ' Child article.
            filter12.ArticleName = articleName2
            filter12.FilterName = filterName12
            filter12.JoinUniqueKey = True
            filter12.FilterTypes = FilterTypes.JoinFilter
            filter12.JoinFilterClause = filterClause12

            ' Add the join filter to the child article.
            article2.AddMergeJoinFilter(filter12)

            ' Parent article.
            filter23.JoinArticleName = articleName2
            ' Child article.
            filter23.ArticleName = articleName3
            filter23.FilterName = filterName23
            filter23.JoinUniqueKey = True
            filter23.FilterTypes = FilterTypes.JoinFilter
            filter23.JoinFilterClause = filterClause23

            ' Add the join filter to the child article.
            article3.AddMergeJoinFilter(filter23)

        Catch ex As Exception
            ' Do error handling here and rollback the transaction.
            Throw New ApplicationException( _
                "The filtered articles could not be created", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergeArticles>
    End Sub
    Public Shared Sub CreateTranPushSub(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateTranPushSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        ' Create the objects that we need.
        Dim publication As TransPublication
        Dim subscription As TransSubscription

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports push subscriptions.
            publication = New TransPublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = conn

            If publication.IsExistingObject Then
                If (publication.Attributes And PublicationAttributes.AllowPush) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPush
                End If

                ' Define the push subscription.
                subscription = New TransSubscription()
                subscription.ConnectionContext = conn
                subscription.SubscriberName = subscriberName
                subscription.PublicationName = publicationName
                subscription.DatabaseName = publicationDbName
                subscription.SubscriptionDBName = subscriptionDbName

                ' Specify the Windows login credentials for the Distribution Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword

                ' By default, subscriptions to transactional publications are synchronized 
                ' continuously, but in this case we only want to synchronize on demand.
                subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.OnDemand

                ' Create the push subscription.
                subscription.Create()
            Else
                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If

        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateTranPushSub>
    End Sub
    Public Shared Sub CreateTranPullSub(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateTranPullSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As TransPublication
        Dim subscription As TransPullSubscription

        Try
            ' Connect to the Publisher and Subscriber.
            subscriberConn.Connect()
            publisherConn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports pull subscriptions.
            publication = New TransPublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            If publication.IsExistingObject Then
                If (publication.Attributes And PublicationAttributes.AllowPull) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPull
                End If

                ' Define the pull subscription.
                subscription = New TransPullSubscription()
                subscription.ConnectionContext = subscriberConn
                subscription.PublisherName = publisherName
                subscription.PublicationName = publicationName
                subscription.PublicationDBName = publicationDbName
                subscription.DatabaseName = subscriptionDbName
                subscription.Description = "Pull subscription to " + publicationDbName _
                + " on " + subscriberName + "."

                ' Specify the Windows login credentials for the Distribution Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword

                ' Make sure that the agent job for the subscription is created.
                subscription.CreateSyncAgentByDefault = True

                ' By default, subscriptions to transactional publications are synchronized 
                ' continuously, but in this case we only want to synchronize on demand.
                subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.OnDemand

                ' Create the pull subscription at the Subscriber.
                subscription.Create()

                Dim registered As Boolean = False

                ' Verify that the subscription is not already registered.
                For Each existing As TransSubscription In publication.EnumSubscriptions()
                    If existing.SubscriberName = subscriberName And _
                        existing.SubscriptionDBName = subscriptionDbName Then
                        registered = True
                    End If
                Next existing
                If Not registered Then
                    ' Register the subscription with the Publisher.
                    publication.MakePullSubscriptionWellKnown( _
                     subscriberName, subscriptionDbName, _
                     SubscriptionSyncType.Automatic, _
                     TransSubscriberType.ReadOnly)
                End If
            Else
                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateTranPullSub>
    End Sub
    Public Shared Sub CreateMergePullSub(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePullSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim hostname As String = "adventure-works\garrett1"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As MergePublication
        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            subscriberConn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports pull subscriptions.
            publication = New MergePublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            If publication.LoadProperties() Then
                If (publication.Attributes And PublicationAttributes.AllowPull) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPull
                End If

                ' Define the pull subscription.
                subscription = New MergePullSubscription()
                subscription.ConnectionContext = subscriberConn
                subscription.PublisherName = publisherName
                subscription.PublicationName = publicationName
                subscription.PublicationDBName = publicationDbName
                subscription.DatabaseName = subscriptionDbName
                subscription.HostName = hostname

                ' Specify the Windows login credentials for the Merge Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword

                ' Make sure that the agent job for the subscription is created.
                subscription.CreateSyncAgentByDefault = True

                ' Create the pull subscription at the Subscriber.
                subscription.Create()

                Dim registered As Boolean = False

                ' Verify that the subscription is not already registered.
                For Each existing As MergeSubscription In _
                publication.EnumSubscriptions()
                    If existing.SubscriberName = subscriberName Then
                        registered = True
                    End If
                Next
                If Not registered Then
                    ' Register the local subscription with the Publisher.
                    publication.MakePullSubscriptionWellKnown( _
                     subscriberName, subscriptionDbName, _
                     SubscriptionSyncType.Automatic, _
                     MergeSubscriberType.Local, 0)
                End If
            Else
                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePullSub>
    End Sub
    Public Shared Sub CreateMergePushSub(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePushSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim hostname As String = "adventure-works\garrett1"

        'Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        ' Create the objects that we need.
        Dim publication As MergePublication
        Dim subscription As MergeSubscription

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports push subscriptions.
            publication = New MergePublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = conn

            If publication.IsExistingObject Then
                If (publication.Attributes And PublicationAttributes.AllowPush) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPush
                End If

                ' Define the push subscription.
                subscription = New MergeSubscription()
                subscription.ConnectionContext = conn
                subscription.SubscriberName = subscriberName
                subscription.PublicationName = publicationName
                subscription.DatabaseName = publicationDbName
                subscription.SubscriptionDBName = subscriptionDbName
                subscription.HostName = hostname

                ' Set a schedule to synchronize the subscription every 2 hours
                ' during weekdays from 6am to 10pm.
                subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.Weekly
                subscription.AgentSchedule.FrequencyInterval = Convert.ToInt32("0x003E", 16)
                subscription.AgentSchedule.FrequencyRecurrenceFactor = 1
                subscription.AgentSchedule.FrequencySubDay = ScheduleFrequencySubDay.Hour
                subscription.AgentSchedule.FrequencySubDayInterval = 2
                subscription.AgentSchedule.ActiveStartDate = 20051108
                subscription.AgentSchedule.ActiveEndDate = 20071231
                subscription.AgentSchedule.ActiveStartTime = 60000
                subscription.AgentSchedule.ActiveEndTime = 100000

                ' Specify the Windows login credentials for the Merge Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword

                ' Create the push subscription.
                subscription.Create()
            Else

                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
            "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePushSub>
    End Sub
    Public Shared Sub RemoveMergePullSub()
        '<snippetrmo_vb_DropMergePullSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As MergePublication
        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            subscriberConn.Connect()

            ' Define the pull subscription.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = subscriberConn
            subscription.PublisherName = publisherName
            subscription.PublicationName = publicationName
            subscription.PublicationDBName = publicationDbName
            subscription.DatabaseName = subscriptionDbName

            ' Define the publication.
            publication = New MergePublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            ' Delete the pull subscription, if it exists.
            If subscription.IsExistingObject Then

                ' Delete the pull subscription at the Subscriber.
                subscription.Remove()

                If publication.LoadProperties() Then
                    publication.RemovePullSubscription(subscriberName, subscriptionDbName)
                Else
                    ' Do something here if the publication does not exist.
                    Throw New ApplicationException(String.Format( _
                     "The publication '{0}' does not exist on {1}.", _
                     publicationName, publisherName))
                End If
            Else
                Throw New ApplicationException(String.Format( _
                 "The subscription to {0} does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be deleted.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_DropMergePullSub>
    End Sub
    Public Shared Sub RemoveTranPullSub()
        '<snippetrmo_vb_DropTranPullSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As TransPublication
        Dim subscription As TransPullSubscription

        Try
            ' Connect to the Subscriber.
            subscriberConn.Connect()

            ' Define the pull subscription.
            subscription = New TransPullSubscription()
            subscription.ConnectionContext = subscriberConn
            subscription.PublisherName = publisherName
            subscription.PublicationName = publicationName
            subscription.PublicationDBName = publicationDbName
            subscription.DatabaseName = subscriptionDbName

            ' Define the publication.
            publication = New TransPublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            ' Delete the pull subscription, if it exists.
            If subscription.IsExistingObject Then
               
                If publication.LoadProperties() Then
                    ' Remove the pull subscription registration at the Publisher.
                    publication.RemovePullSubscription(subscriberName, subscriptionDbName)
                Else
                    ' Do something here if the publication does not exist.
                    Throw New ApplicationException(String.Format( _
                     "The publication '{0}' does not exist on {1}.", _
                     publicationName, publisherName))
                End If
                ' Delete the pull subscription at the Subscriber.
                subscription.Remove()
            Else
                Throw New ApplicationException(String.Format( _
                 "The subscription to {0} does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be deleted.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_DropTranPullSub>
    End Sub
    Public Shared Sub RemoveTranPushSub()
        '<snippetrmo_vb_DropTranPushSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim subscription As TransSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New TransSubscription()
            subscription.ConnectionContext = conn
            subscription.SubscriberName = subscriberName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.DatabaseName = publicationDbName

            ' Delete the pull subscription, if it exists.
            If subscription.IsExistingObject Then

                ' Delete the pull subscription at the Subscriber.
                subscription.Remove()
            Else
                Throw New ApplicationException(String.Format( _
                 "The subscription to {0} does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be deleted.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_DropTranPushSub>
    End Sub
    Public Shared Sub RemoveMergePushSub()
        '<snippetrmo_vb_DropMergePushSub>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim subscription As MergeSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New MergeSubscription()
            subscription.ConnectionContext = conn
            subscription.SubscriberName = subscriberName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.DatabaseName = publicationDbName

            ' Delete the pull subscription, if it exists.
            If subscription.IsExistingObject Then
                ' Delete the pull subscription at the Subscriber.
                subscription.Remove()
            Else
                Throw New ApplicationException(String.Format( _
                 "The subscription to {0} does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                "The subscription to {0} could not be deleted.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_DropMergePushSub>
    End Sub
    Public Shared Sub SyncTranPullSub()
        '<snippetrmo_vb_SyncTranPullSub>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As TransPullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New TransPullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription exists, then start the synchronization.
            If subscription.LoadProperties() Then
                ' Check that we have enough metadata to start the agent.
                If Not subscription.PublisherSecurity Is Nothing Then

                    ' Write agent output to a log file.
                    subscription.SynchronizationAgent.Output = "distagent.log"
                    subscription.SynchronizationAgent.OutputVerboseLevel = 2

                    ' Synchronously start the Distribution Agent for the subscription.
                    subscription.SynchronizationAgent.Synchronize()
                Else
                    Throw New ApplicationException("There is insufficent metadata to " + _
                     "synchronize the subscription. Recreate the subscription with " + _
                     "the agent job or supply the required agent properties at run time.")
                End If
            Else
                ' Do something here if the pull subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be " + _
             "synchronized. Verify that the subscription has " + _
             "been defined correctly.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncTranPullSub>
    End Sub
    Public Shared Sub SyncTranPushSub()
        '<snippetrmo_vb_SyncTranPushSub>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Dim subscription As TransSubscription

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Define the push subscription.
            subscription = New TransSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = publicationDbName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.SubscriberName = subscriberName

            ' If the push subscription exists, start the synchronization.
            If subscription.LoadProperties() Then
                ' Check that we have enough metadata to start the agent.
                If Not subscription.SubscriberSecurity Is Nothing Then

                    ' Synchronously start the Distribution Agent for the subscription.
                    subscription.SynchronizationAgent.Synchronize()
                Else
                    Throw New ApplicationException("There is insufficent metadata to " + _
                     "synchronize the subscription. Recreate the subscription with " + _
                     "the agent job or supply the required agent properties at run time.")
                End If
            Else
                ' Do something here if the push subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "The subscription to '{0}' does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncTranPushSub>
    End Sub
    Public Shared Sub SyncTranPullSubWithJob()
        '<snippetrmo_vb_SyncTranPullSub_WithJob>
        ' Define server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As TransPullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define subscription properties.
            subscription = New TransPullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription and the job exists, start the agent job.
            If subscription.LoadProperties() And Not subscription.AgentJobId Is Nothing Then
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exists on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Do appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncTranPullSub_WithJob>
    End Sub
    Public Shared Sub SyncTranPushSubWithJob()
        '<snippetrmo_vb_SyncTranPushSub_WithJob>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Dim subscription As TransSubscription

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Instantiate the push subscription.
            subscription = New TransSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = publicationDbName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.SubscriberName = subscriberName

            ' If the push subscription and the job exists, start the agent job.
            If subscription.LoadProperties() And Not subscription.AgentJobId Is Nothing Then
                ' Start the Distribution Agent asynchronously.
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exists on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncTranPushSub_WithJob>
    End Sub
    Public Shared Sub SyncMergePullSubWithJob()
        '<snippetrmo_vb_SyncMergePullSub_WithJob>
        ' Define server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define subscription properties.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription and the job exists, start the agent job.
            If subscription.LoadProperties() And Not subscription.AgentJobId Is Nothing Then
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exists on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Do appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncMergePullSub_WithJob>
    End Sub
    Public Shared Sub SyncMergePullSub()
        '<snippetrmo_vb_SyncMergePullSub>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription exists, then start the synchronization.
            If subscription.LoadProperties() Then
                ' Check that we have enough metadata to start the agent.
                If Not subscription.PublisherSecurity Is Nothing Or subscription.DistributorSecurity Is Nothing Then

                    ' Output agent messages to the console. 
                    subscription.SynchronizationAgent.OutputVerboseLevel = 1
                    subscription.SynchronizationAgent.Output = ""

                    ' Synchronously start the Merge Agent for the subscription.
                    subscription.SynchronizationAgent.Synchronize()
                Else
                    Throw New ApplicationException("There is insufficent metadata to " + _
                     "synchronize the subscription. Recreate the subscription with " + _
                     "the agent job or supply the required agent properties at run time.")
                End If
            Else
                ' Do something here if the pull subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be " + _
             "synchronized. Verify that the subscription has " + _
             "been defined correctly.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncMergePullSub>
    End Sub
    Public Shared Sub SyncMergePushSub()
        '<snippetrmo_vb_SyncMergePushSub>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Dim subscription As MergeSubscription

        Try
            ' Connect to the Publisher
            conn.Connect()

            ' Define the subscription.
            subscription = New MergeSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = publicationDbName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.SubscriberName = subscriberName

            ' If the push subscription exists, start the synchronization.
            If subscription.LoadProperties() Then
                ' Check that we have enough metadata to start the agent.
                If Not subscription.SubscriberSecurity Is Nothing Then
                    ' Synchronously start the Merge Agent for the subscription.
                    ' Log agent messages to an output file.
                    subscription.SynchronizationAgent.Output = "mergeagent.log"
                    subscription.SynchronizationAgent.OutputVerboseLevel = 2
                    subscription.SynchronizationAgent.Synchronize()
                Else
                    Throw New ApplicationException("There is insufficent metadata to " + _
                     "synchronize the subscription. Recreate the subscription with " + _
                     "the agent job or supply the required agent properties at run time.")
                End If
            Else
                ' Do something here if the push subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "The subscription to '{0}' does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncMergePushSub>
    End Sub
    Public Shared Sub SyncMergePushSubWithJob()
        '<snippetrmo_vb_SyncMergePushSub_WithJob>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Dim subscription As MergeSubscription

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Define push subscription.
            subscription = New MergeSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = publicationDbName
            subscription.PublicationName = publicationName
            subscription.SubscriptionDBName = subscriptionDbName
            subscription.SubscriberName = subscriberName

            ' If the push subscription and the job exists, start the agent job.
            If subscription.LoadProperties() And Not subscription.AgentJobId Is Nothing Then
                ' Start the Merge Agent asynchronously.
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                    "A subscription to '{0}' does not exists on {1}", _
                    publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncMergePushSub_WithJob>
    End Sub
    Public Shared Sub ConfigureWebSync(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePub_WebSync>
        ' Set the Publisher, publication database, and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publicationDb As ReplicationDatabase
        Dim publication As MergePublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Enable the database for merge publication.				
            publicationDb = New ReplicationDatabase(publicationDbName, conn)
            If publicationDb.LoadProperties() Then
                If Not publicationDb.EnabledMergePublishing Then
                    publicationDb.EnabledMergePublishing = True
                End If
            Else
                ' Do something here if the database does not exist. 
                Throw New ApplicationException(String.Format( _
                 "The {0} database does not exist on {1}.", _
                 publicationDb, publisherName))
            End If

            ' Set the required properties for the merge publication.
            publication = New MergePublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' Enable Web synchronization, if not already enabled.
            If (publication.Attributes And PublicationAttributes.AllowWebSynchronization) = 0 Then
                publication.Attributes = publication.Attributes _
                Or PublicationAttributes.AllowWebSynchronization
            End If

            ' Enable pull subscriptions, if not already enabled.
            If (publication.Attributes And PublicationAttributes.AllowPull) = 0 Then
                publication.Attributes = publication.Attributes _
                Or PublicationAttributes.AllowPull
            End If

            ' Enable Subscriber requested snapshot generation. 
            publication.Attributes = publication.Attributes _
                Or PublicationAttributes.AllowSubscriberInitiatedSnapshot

            ' Enable anonymous access for Subscribers that cannot 
            ' make a direct connetion to the Publisher. 
            publication.Attributes = publication.Attributes _
                Or PublicationAttributes.AllowAnonymous

            ' Specify the Windows account under which the Snapshot Agent job runs.
            ' This account will be used for the local connection to the 
            ' Distributor and all agent connections that use Windows Authentication.
            publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin
            publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword

            ' Explicitly set the security mode for the Publisher connection
            ' Windows Authentication (the default).
            publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = True

            If Not publication.IsExistingObject Then
                ' Create the merge publication and the Snapshot Agent job.
                publication.Create()
                publication.CreateSnapshotAgent()
            Else
                Throw New ApplicationException(String.Format( _
                    "The {0} publication already exists.", publicationName))
            End If
        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
                "The publication {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePub_WebSync>
    End Sub
    Public Shared Sub CreateMergePullSubWebSync(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePullSub_WebSync>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim hostname As String = "adventure-works\garrett1"
        Dim webSyncUrl As String = "https://" + publisherInstance + "/WebSync/replisapi.dll"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As MergePublication
        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            subscriberConn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports pull subscriptions and Web synchronization.
            publication = New MergePublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            If publication.LoadProperties() Then
                If (publication.Attributes And PublicationAttributes.AllowPull) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPull
                End If
                If (publication.Attributes And PublicationAttributes.AllowWebSynchronization) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowWebSynchronization
                End If

                ' Define the pull subscription.
                subscription = New MergePullSubscription()
                subscription.ConnectionContext = subscriberConn
                subscription.PublisherName = publisherName
                subscription.PublicationName = publicationName
                subscription.PublicationDBName = publicationDbName
                subscription.DatabaseName = subscriptionDbName
                subscription.HostName = hostname
                subscription.CreateSyncAgentByDefault = True

                ' Specify the Windows login credentials for the Merge Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword

                ' Enable Web synchronization.
                subscription.UseWebSynchronization = True
                subscription.InternetUrl = webSyncUrl

                ' Specify the same Windows credentials to use when connecting to the
                ' Web server using HTTPS Basic Authentication.
                subscription.InternetSecurityMode = AuthenticationMethod.BasicAuthentication
                subscription.InternetLogin = winLogin
                subscription.InternetPassword = winPassword

                ' Create the pull subscription at the Subscriber.
                subscription.Create()

                Dim registered As Boolean = False

                ' Verify that the subscription is not already registered.
                For Each existing As MergeSubscription In _
                publication.EnumSubscriptions()
                    If existing.SubscriberName = subscriberName Then
                        registered = True
                    End If
                Next
                If Not registered Then
                    ' Register the local subscription with the Publisher.
                    publication.MakePullSubscriptionWellKnown( _
                     subscriberName, subscriptionDbName, _
                     SubscriptionSyncType.Automatic, _
                     MergeSubscriberType.Local, 0)
                End If
            Else
                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
             "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePullSub_WebSync>
    End Sub
    Public Shared Sub CreateMergePullSubNoJob()
        '<snippetrmo_vb_CreateMergePullSub_NoJob>
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"

        'Create connections to the Publisher and Subscriber.
        Dim subscriberConn As ServerConnection = New ServerConnection(subscriberName)
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create the objects that we need.
        Dim publication As MergePublication
        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            subscriberConn.Connect()

            ' Ensure that the publication exists and that 
            ' it supports pull subscriptions.
            publication = New MergePublication()
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = publisherConn

            If publication.LoadProperties() Then
                If (publication.Attributes And PublicationAttributes.AllowPull) = 0 Then
                    publication.Attributes = publication.Attributes _
                    Or PublicationAttributes.AllowPull
                End If

                ' Define the pull subscription.
                subscription = New MergePullSubscription()
                subscription.ConnectionContext = subscriberConn
                subscription.PublisherName = publisherName
                subscription.PublicationName = publicationName
                subscription.PublicationDBName = publicationDbName
                subscription.DatabaseName = subscriptionDbName

                ' Specify that an agent job not be created for this subscription. The
                ' subscription can only be synchronized by running the Merge Agent directly.
                ' Subscripition metadata stored in MSsubscription_properties will not
                ' be available and must be specified at run time.
                subscription.CreateSyncAgentByDefault = False

                ' Create the pull subscription at the Subscriber.
                subscription.Create()

                Dim registered As Boolean = False

                ' Verify that the subscription is not already registered.
                For Each existing As MergeSubscription In _
                publication.EnumSubscriptions()
                    If existing.SubscriberName = subscriberName Then
                        registered = True
                    End If
                Next
                If Not registered Then
                    ' Register the local subscription with the Publisher.
                    publication.MakePullSubscriptionWellKnown( _
                     subscriberName, subscriptionDbName, _
                     SubscriptionSyncType.Automatic, _
                     MergeSubscriberType.Local, 0)
                End If
            Else
                ' Do something here if the publication does not exist.
                Throw New ApplicationException(String.Format( _
                 "The publication '{0}' does not exist on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
             "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            subscriberConn.Disconnect()
            publisherConn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePullSub_NoJob>
    End Sub
    Public Shared Sub SyncMergePullSubNoJobWebSync(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_SyncMergePullSub_NoJobWebSync>
        ' Define the server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim hostname As String = "adventure-works\garrett1"
        Dim webSyncUrl As String = "https://" + publisherInstance + "/SalesOrders/replisapi.dll"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As MergePullSubscription
        Dim agent As MergeSynchronizationAgent

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription exists, then start the synchronization.
            If subscription.LoadProperties() Then
                ' Get the agent for the subscription.
                agent = subscription.SynchronizationAgent

                ' Check that we have enough metadata to start the agent.
                If agent.PublisherSecurityMode = Nothing Then
                    ' Set the required properties that could not be returned
                    ' from the MSsubscription_properties table. 
                    agent.PublisherSecurityMode = SecurityMode.Integrated
                    agent.Distributor = publisherInstance
                    agent.DistributorSecurityMode = SecurityMode.Integrated
                    agent.HostName = hostname

                    ' Set optional Web synchronization properties.
                    agent.UseWebSynchronization = True
                    agent.InternetUrl = webSyncUrl
                    agent.InternetSecurityMode = SecurityMode.Standard
                    agent.InternetLogin = winLogin
                    agent.InternetPassword = winPassword
                End If

                ' Enable agent logging to the console.
                agent.OutputVerboseLevel = 1
                agent.Output = ""

                ' Synchronously start the Merge Agent for the subscription.
                agent.Synchronize()
            Else
                ' Do something here if the pull subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exist on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Implement appropriate error handling here.
            Throw New ApplicationException("The subscription could not be " + _
             "synchronized. Verify that the subscription has " + _
             "been defined correctly.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_SyncMergePullSub_NoJobWebSync>
    End Sub
    Public Shared Sub GenerateSnapshotWithJob()
        '<snippetrmo_vb_GenerateSnapshot_WithJob>
        ' Set the Publisher, publication database, and publication names.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim publisherName As String = publisherInstance

        Dim publication As TransPublication

        ' Create a connection to the Publisher using Windows Authentication.
        Dim conn As ServerConnection
        conn = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for an existing publication.
            publication = New TransPublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            If publication.LoadProperties() Then
                ' Start the Snapshot Agent job for the publication.
                publication.StartSnapshotGenerationAgentJob()
            Else
                Throw New ApplicationException(String.Format( _
                 "The {0} publication does not exist.", publicationName))
            End If
        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
             "A snapshot could not be generated for the {0} publication." _
             , publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_GenerateSnapshot_WithJob>
    End Sub
    Public Shared Sub GenerateTranSnapshot()
        '<snippetrmo_vb_GenerateSnapshot>
        ' Set the Publisher, publication database, and publication names.
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim publisherName As String = publisherInstance
        Dim distributorName As String = publisherInstance

        Dim agent As SnapshotGenerationAgent

        Try
            ' Set the required properties for Snapshot Agent.
            agent = New SnapshotGenerationAgent()
            agent.Distributor = distributorName
            agent.DistributorSecurityMode = SecurityMode.Integrated
            agent.Publisher = publisherName
            agent.PublisherSecurityMode = SecurityMode.Integrated
            agent.Publication = publicationName
            agent.PublisherDatabase = publicationDbName
            agent.ReplicationType = ReplicationType.Transactional

            ' Start the agent synchronously.
            agent.GenerateSnapshot()

        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
             "A snapshot could not be generated for the {0} publication." _
             , publicationName), ex)
        End Try
        '</snippetrmo_vb_GenerateSnapshot>
    End Sub
    Public Shared Sub GenerateMergeSnapshot()
        '<snippetrmo_vb_GenerateMergeSnapshot>
        ' Set the Publisher, publication database, and publication names.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim publisherName As String = publisherInstance
        Dim distributorName As String = publisherInstance

        Dim agent As SnapshotGenerationAgent

        Try
            ' Set the required properties for Snapshot Agent.
            agent = New SnapshotGenerationAgent()
            agent.Distributor = distributorName
            agent.DistributorSecurityMode = SecurityMode.Integrated
            agent.Publisher = publisherName
            agent.PublisherSecurityMode = SecurityMode.Integrated
            agent.Publication = publicationName
            agent.PublisherDatabase = publicationDbName
            agent.ReplicationType = ReplicationType.Merge

            ' Start the agent synchronously.
            agent.GenerateSnapshot()

        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
             "A snapshot could not be generated for the {0} publication." _
             , publicationName), ex)
        End Try
        '</snippetrmo_vb_GenerateMergeSnapshot>
    End Sub
    Public Shared Sub GenerateFilteredSnapshot(ByVal hostname As String)
        '<snippetrmo_vb_GenerateFilteredSnapshot>
        ' Set the Publisher, publication database, and publication names.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim publisherName As String = publisherInstance
        Dim distributorName As String = publisherInstance

        Dim agent As SnapshotGenerationAgent

        Try
            ' Set the required properties for Snapshot Agent.
            agent = New SnapshotGenerationAgent()
            agent.Distributor = distributorName
            agent.DistributorSecurityMode = SecurityMode.Integrated
            agent.Publisher = publisherName
            agent.PublisherSecurityMode = SecurityMode.Integrated
            agent.Publication = publicationName
            agent.PublisherDatabase = publicationDbName
            agent.ReplicationType = ReplicationType.Merge

            ' Specify the partition information to generate a 
            ' filtered snapshot based on Hostname.
            agent.DynamicFilterHostName = hostname

            ' Start the agent synchronously.
            agent.GenerateSnapshot()
        Catch ex As Exception
            ' Implement custom application error handling here.
            Throw New ApplicationException(String.Format( _
             "A snapshot could not be generated for the {0} publication." _
             , publicationName), ex)
        End Try
        '</snippetrmo_vb_GenerateFilteredSnapshot>
    End Sub
    Public Shared Sub CreateMergePartition(ByVal hostname As String)
        '<snippetrmo_vb_CreateMergePartition>
        ' Define the server, database, and publication names
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim distributorName As String = publisherInstance

        Dim publication As MergePublication
        Dim partition As MergePartition
        Dim snapshotAgentJob As MergeDynamicSnapshotJob
        Dim schedule As ReplicationAgentSchedule

        ' Create a connection to the Publisher.
        Dim publisherConn As ServerConnection = New ServerConnection(publisherName)

        ' Create a connection to the Distributor to start the Snapshot Agent.
        Dim distributorConn As ServerConnection = New ServerConnection(distributorName)

        Try
            ' Connect to the Publisher.
            publisherConn.Connect()

            ' Set the required properties for the publication.
            publication = New MergePublication()
            publication.ConnectionContext = publisherConn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName


            ' If we can't get the properties for this merge publication, 
            ' then throw an application exception.
            If (publication.LoadProperties() Or publication.SnapshotAvailable) Then
                ' Set a weekly schedule for the filtered data snapshot.
                schedule = New ReplicationAgentSchedule()
                schedule.FrequencyType = ScheduleFrequencyType.Weekly
                schedule.FrequencyRecurrenceFactor = 1
                schedule.FrequencyInterval = Convert.ToInt32("0x001", 16)

                ' Set the value of Hostname that defines the data partition. 
                partition = New MergePartition()
                partition.DynamicFilterHostName = hostname
                snapshotAgentJob = New MergeDynamicSnapshotJob()
                snapshotAgentJob.DynamicFilterHostName = hostname

                ' Create the partition for the publication with the defined schedule.
                publication.AddMergePartition(partition)
                publication.AddMergeDynamicSnapshotJob(snapshotAgentJob, schedule)
            Else
                Throw New ApplicationException(String.Format( _
                 "Settings could not be retrieved for the publication, " + _
                 " or the initial snapshot has not been generated. " + _
                 "Ensure that the publication {0} exists on {1} and " + _
                 "that the Snapshot Agent has run successfully.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException(String.Format( _
             "The partition for '{0}' in the {1} publication could not be created.", _
             hostname, publicationName), ex)
        Finally
            publisherConn.Disconnect()
            If distributorConn.IsOpen Then
                distributorConn.Disconnect()
            End If
        End Try
        '</snippetrmo_vb_CreateMergePartition>
    End Sub
    Public Shared Sub RegisterBLH()
        '<snippetrmo_vb_RegisterBLH>
        ' Specify the Distributor name and business logic properties.
        Dim distributorName As String = publisherInstance
        Dim assemblyName As String = "C:\Program Files\Microsoft SQL Server\90\COM\CustomLogic.dll"
        Dim className As String = "Microsoft.Samples.SqlServer.BusinessLogicHandler.OrderEntryBusinessLogicHandler"
        Dim friendlyName As String = "OrderEntryLogic"

        Dim distributor As ReplicationServer
        Dim customLogic As BusinessLogicHandler

        ' Create a connection to the Distributor.
        Dim distributorConn As ServerConnection = New ServerConnection(distributorName)

        Try
            ' Connect to the Distributor.
            distributorConn.Connect()

            ' Set the Distributor properties.
            distributor = New ReplicationServer(distributorConn)

            ' Set the business logic handler properties.
            customLogic = New BusinessLogicHandler()
            customLogic.DotNetAssemblyName = assemblyName
            customLogic.DotNetClassName = className
            customLogic.FriendlyName = friendlyName
            customLogic.IsDotNetAssembly = True

            Dim isRegistered As Boolean = False

            ' Check if the business logic handler is already registered at the Distributor.
            For Each registeredLogic As BusinessLogicHandler _
            In distributor.RegisteredSubscribers
                If registeredLogic Is customLogic Then
                    isRegistered = True
                End If
            Next

            ' Register the custom logic.
            If Not isRegistered Then
                distributor.RegisterBusinessLogicHandler(customLogic)
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException(String.Format( _
             "The {0} assembly could not be registered.", _
             assemblyName), ex)
        Finally
            distributorConn.Disconnect()
        End Try
        '</snippetrmo_vb_RegisterBLH>
    End Sub
    Public Shared Sub RegisterBLH_10()
        '<snippetrmo_vb_RegisterBLH_10>
        ' Specify the Distributor name and business logic properties.
        Dim distributorName As String = publisherInstance
        Dim assemblyName As String = "C:\Program Files\Microsoft SQL Server\110\COM\CustomLogic.dll"
        Dim className As String = "Microsoft.Samples.SqlServer.BusinessLogicHandler.OrderEntryBusinessLogicHandler"
        Dim friendlyName As String = "OrderEntryLogic"

        Dim distributor As ReplicationServer
        Dim customLogic As BusinessLogicHandler

        ' Create a connection to the Distributor.
        Dim distributorConn As ServerConnection = New ServerConnection(distributorName)

        Try
            ' Connect to the Distributor.
            distributorConn.Connect()

            ' Set the Distributor properties.
            distributor = New ReplicationServer(distributorConn)

            ' Set the business logic handler properties.
            customLogic = New BusinessLogicHandler()
            customLogic.DotNetAssemblyName = assemblyName
            customLogic.DotNetClassName = className
            customLogic.FriendlyName = friendlyName
            customLogic.IsDotNetAssembly = True

            Dim isRegistered As Boolean = False

            ' Check if the business logic handler is already registered at the Distributor.
            For Each registeredLogic As BusinessLogicHandler _
            In distributor.EnumBusinessLogicHandlers
                If registeredLogic Is customLogic Then
                    isRegistered = True
                End If
            Next

            ' Register the custom logic.
            If Not isRegistered Then
                distributor.RegisterBusinessLogicHandler(customLogic)
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException(String.Format( _
             "The {0} assembly could not be registered.", _
             assemblyName), ex)
        Finally
            distributorConn.Disconnect()
        End Try
        '</snippetrmo_vb_RegisterBLH_10>
    End Sub
    Public Shared Sub ChangeMergeArticle_BLH()
        '<snippetrmo_vb_ChangeMergeArticle_BLH>
        ' Define the Publisher, publication, and article names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim articleName As String = "SalesOrderHeader"

        ' Set the friendly name of the business logic handler.
        Dim customLogic As String = "OrderEntryLogic"

        Dim article As MergeArticle = New MergeArticle()

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the article.
            article.ConnectionContext = conn
            article.Name = articleName
            article.DatabaseName = publicationDbName
            article.PublicationName = publicationName

            ' Load the article properties.
            If article.LoadProperties() Then
                article.ArticleResolver = customLogic
            Else
                ' Throw an exception of the article does not exist.
                Throw New ApplicationException(String.Format( _
                 "{0} is not published in {1}", articleName, publicationName))
            End If

        Catch ex As Exception
            ' Do error handling here and rollback the transaction.
            Throw New ApplicationException(String.Format( _
             "The business logic handler {0} could not be associated with " + _
             " the {1} article.", customLogic, articleName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ChangeMergeArticle_BLH>
    End Sub
    Public Shared Sub ReinitTranPullSub()
        '<snippetrmo_vb_ReinitTranPullSub>
        ' Define server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As TransPullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define subscription properties.
            subscription = New TransPullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription and the job exists, mark the subscription
            ' for reinitialization and start the agent job.
            If subscription.LoadProperties() And (Not subscription.AgentJobId Is Nothing) Then
                subscription.Reinitialize()
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exists on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Do appropriate error handling here.
            Throw New ApplicationException("The subscription could not be reinitialized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ReinitTranPullSub>
    End Sub
    Public Shared Sub ReinitMergePullSub_WithUpload()
        '<snippetrmo_vb_ReinitMergePullSub_WithUpload>
        ' Define server, publication, and database names.
        Dim subscriberName As String = subscriberInstance
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"

        ' Create a connection to the Subscriber.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define subscription properties.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = conn
            subscription.DatabaseName = subscriptionDbName
            subscription.PublisherName = publisherName
            subscription.PublicationDBName = publicationDbName
            subscription.PublicationName = publicationName

            ' If the pull subscription and the job exists, mark the subscription
            ' for reinitialization after upload and start the agent job.
            If subscription.LoadProperties() And (Not subscription.AgentJobId Is Nothing) Then
                subscription.Reinitialize(True)
                subscription.SynchronizeWithJob()
            Else
                ' Do something here if the subscription does not exist.
                Throw New ApplicationException(String.Format( _
                 "A subscription to '{0}' does not exists on {1}", _
                 publicationName, subscriberName))
            End If
        Catch ex As Exception
            ' Do appropriate error handling here.
            Throw New ApplicationException("The subscription could not be synchronized.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ReinitMergePullSub_WithUpload>
    End Sub
    Public Shared Sub ChangeServerPasswords(ByVal login As String, ByVal password As String)
        '<snippetrmo_vb_ChangeServerPasswords>
        ' Set the Distributor and distribution database names.
        Dim serverName As String = publisherInstance

        Dim server As ReplicationServer

        ' Create a connection to the Distributor using Windows Authentication.
        Dim conn As ServerConnection = New ServerConnection(serverName)

        Try
            ' Open the connection. 
            conn.Connect()

            server = New ReplicationServer(conn)

            ' Load server properties, if it exists.
            If server.LoadProperties() Then

                ' If the login is in the form string\string, assume we are 
                ' changing the password for a Windows login.
                If login.Split("\").Length = 2 Then

                    ' Change the password for the all connections that use
                    ' the Windows login. 
                    server.ChangeReplicationServerPasswords( _
                    ReplicationSecurityMode.Integrated, login, password)
                Else

                    ' Change the password for the all connections that use
                    ' the SQL Server login. 
                    server.ChangeReplicationServerPasswords( _
                    ReplicationSecurityMode.SqlStandard, login, password)
                End If
            Else
                Throw New ApplicationException(String.Format( _
                 "Properties for {0} could not be retrieved.", publisherInstance))
            End If
        Catch ex As Exception
            ' Implement the appropriate error handling here. 
            Throw New ApplicationException(String.Format( _
             "An error occured when changing agent login " + _
             " credentials on {0}.", serverName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ChangeServerPasswords>

    End Sub
    Public Shared Sub ValidateTranPublication()
        '<snippetrmo_vb_ValidateTranPub>
        ' Define the server, database, and publication names
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksProductTran"
        Dim publicationDbName As String = "AdventureWorks2012"

        Dim publication As TransPublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the publication.
            publication = New TransPublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' If we can't get the properties for this publication, 
            ' throw an application exception.
            If publication.LoadProperties() Then

                ' Initiate validataion for all subscriptions to this publication.
                publication.ValidatePublication(ValidationOption.RowCountOnly, _
                 ValidationMethod.ConditionalFast, False)

                ' If not already running, start the Distribution Agent at each 
                ' Subscriber to synchronize and validate the subscriptions.
            Else
                Throw New ApplicationException(String.Format( _
                 "Settings could not be retrieved for the publication. " + _
                 "Ensure that the publication {0} exists on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException( _
             "Subscription validation could not be initiated.", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ValidateTranPub>
    End Sub
    Public Shared Sub ValidateMergeSubscription()
        '<snippetrmo_vb_ValidateMergeSub>
        ' Define the server, database, and publication names
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"

        Dim publication As MergePublication

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Set the required properties for the publication.
            publication = New MergePublication()
            publication.ConnectionContext = conn
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName

            ' If we can't get the properties for this merge publication, then throw an application exception.
            If publication.LoadProperties() Then
                ' Initiate validation of the specified subscription.
                publication.ValidateSubscription(subscriberName, _
                 subscriptionDbName, ValidationOption.RowCountOnly)

                ' Start the Merge Agent to synchronize and validate the subscription.
            Else
                Throw New ApplicationException(String.Format( _
                 "Settings could not be retrieved for the publication. " + _
                 "Ensure that the publication {0} exists on {1}.", _
                 publicationName, publisherName))
            End If
        Catch ex As Exception
            ' Do error handling here.
            Throw New ApplicationException(String.Format( _
             "The subscription at {0} to the {1} publication could not " + _
             "be validated.", subscriberName, publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_ValidateMergeSub>
    End Sub
    Public Shared Sub CreateLogicalRecord()
        '<snippetrmo_vb_CreateLogicalRecord>
        ' Define the Publisher and publication names.
        Dim publisherName As String = publisherInstance
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publicationDbName As String = "AdventureWorks2012"

        ' Specify article names.
        Dim articleName1 As String = "SalesOrderHeader"
        Dim articleName2 As String = "SalesOrderDetail"

        ' Specify logical record information.
        Dim lrName As String = "SalesOrderHeader_SalesOrderDetail"
        Dim lrClause As String = "[SalesOrderHeader].[SalesOrderID] = " _
                & "[SalesOrderDetail].[SalesOrderID]"

        Dim schema As String = "Sales"

        Dim article1 As MergeArticle = New MergeArticle()
        Dim article2 As MergeArticle = New MergeArticle()
        Dim lr As MergeJoinFilter = New MergeJoinFilter()
        Dim publication As MergePublication = New MergePublication()

        ' Create a connection to the Publisher.
        Dim conn As ServerConnection = New ServerConnection(publisherName)

        Try
            ' Connect to the Publisher.
            conn.Connect()

            ' Verify that the publication uses precomputed partitions.
            publication.Name = publicationName
            publication.DatabaseName = publicationDbName
            publication.ConnectionContext = conn

            ' If we can't get the properties for this merge publication, then throw an application exception.
            If publication.LoadProperties() Then
                ' If precomputed partitions is disabled, enable it.
                If publication.PartitionGroupsOption = PartitionGroupsOption.False Then
                    publication.PartitionGroupsOption = PartitionGroupsOption.True
                End If
            Else
                Throw New ApplicationException(String.Format( _
                    "Settings could not be retrieved for the publication. " _
                    & "Ensure that the publication {0} exists on {1}.", _
                    publicationName, publisherName))
            End If

            ' Set the required properties for the SalesOrderHeader article.
            article1.ConnectionContext = conn
            article1.Name = articleName1
            article1.DatabaseName = publicationDbName
            article1.SourceObjectName = articleName1
            article1.SourceObjectOwner = schema
            article1.PublicationName = publicationName
            article1.Type = ArticleOptions.TableBased

            ' Set the required properties for the SalesOrderDetail article.
            article2.ConnectionContext = conn
            article2.Name = articleName2
            article2.DatabaseName = publicationDbName
            article2.SourceObjectName = articleName2
            article2.SourceObjectOwner = schema
            article2.PublicationName = publicationName
            article2.Type = ArticleOptions.TableBased

            If Not article1.IsExistingObject Then
                article1.Create()
            End If
            If Not article2.IsExistingObject Then
                article2.Create()
            End If

            ' Define a logical record relationship between 
            ' SalesOrderHeader and SalesOrderDetail. 

            ' Parent article.
            lr.JoinArticleName = articleName1
            ' Child article.
            lr.ArticleName = articleName2
            lr.FilterName = lrName
            lr.JoinUniqueKey = True
            lr.FilterTypes = FilterTypes.LogicalRecordLink
            lr.JoinFilterClause = lrClause

            ' Add the logical record definition to the parent article.
            article1.AddMergeJoinFilter(lr)
        Catch ex As Exception
            ' Do error handling here and rollback the transaction.
            Throw New ApplicationException( _
                    "The filtered articles could not be created", ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateLogicalRecord>
    End Sub
    Public Shared Sub CreateMergePullSubWebSyncAnon(ByVal winLogin As String, ByVal winPassword As String)
        '<snippetrmo_vb_CreateMergePullSub_WebSync_Anon>
        ' The publication must support anonymous Subscribers, pull 
        ' subscriptions, and Web synchronization.
        ' Define the Publisher, publication, and databases.
        Dim publicationName As String = "AdvWorksSalesOrdersMerge"
        Dim publisherName As String = publisherInstance
        Dim subscriberName As String = subscriberInstance
        Dim subscriptionDbName As String = "AdventureWorks2012Replica"
        Dim publicationDbName As String = "AdventureWorks2012"
        Dim hostname As String = "adventure-works\\garrett1"
        Dim webSyncUrl As String = "https://" + publisherInstance + "/WebSync/replisapi.dll"

        'Create the Subscriber connection.
        Dim conn As ServerConnection = New ServerConnection(subscriberName)

        ' Create the objects that we need.
        Dim subscription As MergePullSubscription

        Try
            ' Connect to the Subscriber.
            conn.Connect()

            ' Define the pull subscription.
            subscription = New MergePullSubscription()
            subscription.ConnectionContext = conn
            subscription.PublisherName = publisherName
            subscription.PublicationName = publicationName
            subscription.PublicationDBName = publicationDbName
            subscription.DatabaseName = subscriptionDbName
            subscription.HostName = hostname

            ' Specify an anonymous Subscriber type since we can't 
            ' register at the Publisher with a direct connection.
            subscription.SubscriberType = MergeSubscriberType.Anonymous

            ' Specify the Windows login credentials for the Merge Agent job.
            subscription.SynchronizationAgentProcessSecurity.Login = winLogin
            subscription.SynchronizationAgentProcessSecurity.Password = winPassword

            ' Enable Web synchronization.
            subscription.UseWebSynchronization = True
            subscription.InternetUrl = webSyncUrl

            ' Specify the same Windows credentials to use when connecting to the
            ' Web server using HTTPS Basic Authentication.
            subscription.InternetSecurityMode = AuthenticationMethod.BasicAuthentication
            subscription.InternetLogin = winLogin
            subscription.InternetPassword = winPassword

            ' Ensure that we create a job for this subscription.
            subscription.CreateSyncAgentByDefault = True

            ' Create the pull subscription at the Subscriber.
            subscription.Create()
        Catch ex As Exception
            ' Implement the appropriate error handling here.
            Throw New ApplicationException(String.Format( _
                 "The subscription to {0} could not be created.", publicationName), ex)
        Finally
            conn.Disconnect()
        End Try
        '</snippetrmo_vb_CreateMergePullSub_WebSync_Anon>
    End Sub
End Class

