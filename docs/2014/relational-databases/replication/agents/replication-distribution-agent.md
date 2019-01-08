---
title: "Replication Distribution Agent | Microsoft Docs"
ms.custom: ""
ms.date: "29/10/2018"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Distribution Agent, executables"
  - "agents [SQL Server replication], Distribution Agent"
  - "Distribution Agent, parameter reference"
  - "command prompt [SQL Server replication]"
ms.assetid: 7b4fd480-9eaf-40dd-9a07-77301e44e2ac
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Distribution Agent
  The Replication Distribution Agent is an executable that moves the snapshot (for snapshot replication and transactional replication) and the transactions held in the distribution database tables (for transactional replication) to the destination tables at the Subscribers.  
  
> [!NOTE]  
>  Parameters can be specified in any order. When optional parameters are not specified, values from predefined registry settings on the local computer are used.  
  
## Syntax  
  
```  
  
      distrib [-?]  
-Publisherserver_name[\instance_name]  
-PublisherDBpublisher_database-Subscriberserver_name[\instance_name]  
-SubscriberDBsubscriber_database   
[-AltSnapshotFolderalt_snapshot_folder_path]   
[-BcpBatchSizebcp_batch_size]  
[-CommitBatchSizecommit_batch_size]  
[-CommitBatchThresholdcommit_batch_threshold]  
[-Continuous]  
[-DefinitionFiledef_path_and_file_name]  
[-Distributordistributor]  
[-DistributorLogindistributor_login]  
[-DistributorPassworddistributor_password]  
[-DistributorSecurityMode [0|1]]  
[-EncryptionLevel [0|1|2]]  
[-ErrorFileerror_path_and_file_name]  
[-ExtendedEventConfigFileconfiguration_path_and_file_name]  
[-FileTransferType [0|1]]  
[-FtpAddressftp_address]  
[-FtpPasswordftp_password]   
[-FtpPortftp_port]  
[-FtpUserNameftp_user_name]  
[-HistoryVerboseLevel [0|1|2|3]]  
[-Hostnamehost_name]  
[-KeepAliveMessageIntervalkeep_alive_message_interval_seconds]  
[-LoginTimeOutlogin_time_out_seconds]  
[-MaxBcpThreads]  
[-MaxDeliveredTransactionsnumber_of_transactions]  
[-MessageIntervalmessage_interval]  
[-OledbStreamThresholdoledb_stream_threshold]  
[-Outputoutput_path_and_file_name]  
[-OutputVerboseLevel [0|1|2]]  
[-PacketSizepacket_size]  
[-PollingIntervalpolling_interval]  
[-ProfileNameprofile_name]  
[-Publicationpublication]  
[-QueryTimeOutquery_time_out_seconds]  
[-QuotedIdentifierquoted_identifier]  
[-SkipErrorsnative_error_id [:...n]]  
[-SubscriberDatabasePathsubscriber_path]  
[-SubscriberLoginsubscriber_login]  
[-SubscriberPasswordsubscriber_password]  
[-SubscriberSecurityMode [0|1]]  
[-SubscriberType [0|1|3]]  
[-SubscriptionStreams [1|2|...64]]  
[-SubscriptionTableNamesubscription_table]  
[-SubscriptionType [0|1|2]]  
[-TransactionsPerHistory [0|1|...10000]]  
[-UseDTS]  
[-UseInprocLoader]  
[-UseOledbStreaming]  
```  
  
## Arguments  
 **-?**  
 Prints all available parameters.  
  
 **-Publisher** _server_name_[**\\**_instance_name_]  
 Is the name of the Publisher. Specify *server_name* for the default instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-PublisherDB** _publisher_database_  
 Is the name of the Publisher database.  
  
 **-Subscriber** _server_name_[**\\**_instance_name_]  
 Is the name of the Subscriber. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-SubscriberDB** _subscriber_database_  
 Is the name of the Subscriber database.  
  
 **-AltSnapshotFolder** _alt_snapshot_folder_path_  
 Is the path to the folder that contains the initial snapshot for a subscription.  
  
 **-BcpBatchSize** _bcp_batch_size_  
 Is the number of rows to send in a bulk copy operation. When performing a **bcp in** operation, the batch size is the number of rows to send to the server as one transaction, and also the number of rows that must be sent before the Distribution Agent logs a **bcp** progress message. When performing a **bcp out** operation, a fixed batch size of **1000** is used.  
  
 **-CommitBatchSize** _commit_batch_size_  
 Is the number of transactions to be issued to the Subscriber before a COMMIT statement is issued. The default is 100.  
  
 **-CommitBatchThreshold**  _commit_batch_threshold_  
 Is the number of replication commands to be issued to the Subscriber before a COMMIT statement is issued. The default is 1000.  
  
 **-Continuous**  
 Specifies whether the agent attempts to poll replicated transactions continually. If specified, the agent polls replicated transactions from the source at polling intervals, even if there are no transactions pending.  
  
 **-DefinitionFile** _def_path_and_file_name_  
 Is the path of the agent definition file. An agent definition file contains command prompt arguments for the agent. The content of the file is parsed as an executable file. Use double quotation marks (") to specify argument values containing arbitrary characters.  
  
 **-Distributor** _distributor_  
 Is the Distributor name. For Distributor (push) distribution, the name defaults to the name of the local Distributor.  
  
 **-DistributorLogin** _distributor_login_  
 Is the Distributor login name.  
  
 **-DistributorPassword** _distributor_password_  
 Is the Distributor password.  
  
 **-DistributorSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Distributor. A value of 0 indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication Mode, and a value of 1 indicates Windows Authentication Mode (default).  
  
 **-EncryptionLevel** [ **0** | **1** | **2** ]  
 Is the level of Secure Sockets Layer (SSL) encryption used by the Distribution Agent when making connections.  
  
|EncryptionLevel value|Description|  
|---------------------------|-----------------|  
|**0**|Specifies that SSL is not used.|  
|**1**|Specifies that SSL is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer.|  
|**2**|Specifies that SSL is used, and that the certificate is verified.|  
 
 > [!NOTE]  
 >  A valid SSL certificate is defined with a fully qualified domain name of the SQL Server. In order for the agent to connect successfully when setting -EncryptionLevel to 2, create an alias on the local SQL Server. The 'Alias Name' parameter should be the server name and the 'Server' parameter should be set to the fully qualified name of the SQL Server.

 For more information, see [SQL Server Replication Security](../security/view-and-modify-replication-security-settings.md).  
  
 **-ErrorFile** _error_path_and_file_name_  
 Is the path and file name of the error file generated by the Distribution Agent. This file is generated at any point where failure occurred while applying replication transactions at the Subscriber; errors that occur at the Publisher or Distributor are not logged in this file. This file contains the failed replication transactions and associated error messages. When not specified, the error file is generated in the current directory of the Distribution Agent. The error file name is the name of the Distribution Agent with an .err extension. If the specified file name exists, error messages are appended to the file. This parameter can be a maximum of 256 Unicode characters.  
  
 **-ExtendedEventConfigFile** _configuration_path_and_file_name_  
 Specifies the path and file name for the extended events XML configuration file. The extended events configuration file allows you to configure sessions and enable events for tracking.  
  
 **-FileTransferType** [ **0**| **1**]  
 Specifies the file transfer type. A value of **0** indicates UNC (universal naming convention), and a value of **1** indicates FTP (file transfer protocol).  
  
 **-FtpAddress** _ftp_address_  
 Is the network address of the FTP service for the Distributor. When not specified, **DistributorAddress** is used. If **DistributorAddress** is not specified, **Distributor** is used.  
  
 **-FtpPassword** _ftp_password_  
 Is the user password used to connect to the FTP service.  
  
 **-FtpPort** _ftp_port_  
 Is the port number of the FTP service for the Distributor. When not specified, the default port number for FTP service (21) is used.  
  
 **-FtpUserName**  _ftp_user_name_  
 Is the user name used to connect to the FTP service. When not specified, **anonymous** is used.  
  
 **-HistoryVerboseLevel** [ **0** | **1** | **2** | **3** ]  
 Specifies the amount of history logged during a distribution operation. You can minimize the performance effect of history logging by selecting **1**.  
  
|HistoryVerboseLevel value|Description|  
|-------------------------------|-----------------|  
|**0**|Progress messages are written either to the console or to an output file. History records are not logged in the distribution database.|  
|**1**|Default. Always update a previous history message of the same status (startup, progress, success, and so on). If no previous record with the same status exists, insert a new record.|  
|**2**|Insert new history records unless the record is for such things as idle messages or long-running job messages, in which case update the previous records.|  
|**3**|Always insert new records, unless it is for idle messages.|  
  
 **-Hostname** _host_name_  
 Is the host name used when connecting to the Publisher. This parameter can be a maximum of 128 Unicode characters.  
  
 **-KeepAliveMessageInterval** _keep_alive_message_interval_seconds_  
 Is the number of seconds before the history thread checks if any of the existing connections is waiting for a response from the server. This value can be decreased to avoid having the checkup agent mark the Distribution Agent as suspect when executing a long-running batch. The default is **300** seconds.  
  
 **-LoginTimeOut** _login_time_out_seconds_  
 Is the number of seconds before the login times out. The default is **15** seconds.  
  
 **-MaxBcpThreads** _number_of_threads_  
 Specifies the number of bulk copy operations that can be performed in parallel. The maximum number of threads and ODBC connections that exist simultaneously is the lesser of **MaxBcpThreads** or the number of bulk copy requests that appear in the synchronization transaction in the distribution database. **MaxBcpThreads** must have a value greater than **0** and has no hard-coded upper limit. The default is **2** times the number of processors, up to a maximum value of **8**. When applying a snapshot that was generated at the Publisher using the concurrent snapshot option, one thread is used, regardless of the number you specify for **MaxBcpThreads**.  
  
 **-MaxDeliveredTransactions** _number_of_transactions_  
 Is the maximum number of push or pull transactions applied to Subscribers in one synchronization. A value of **0** indicates that the maximum is an infinite number of transactions. Other values can be used by Subscribers to shorten the duration of a synchronization being pulled from a Publisher.  
  
> [!NOTE]  
>  If -MaxDeliveredTransactions and -Continuous are both specified, the Distribution Agent delivers the specified number of transactions and then stops (even though -Continuous is specified). You must restart the Distribution Agent after the job completes.  
  
 **-MessageInterval**  _message_interval_  
 Is the time interval used for history logging. A history event is logged when one of these parameters is reached:  
  
-   The **TransactionsPerHistory** value is reached after the last history event is logged.  
  
-   The **MessageInterval** value is reached after the last history event is logged.  
  
 If there is no replicated transaction available at the source, the agent reports a no-transaction message to the Distributor. This option specifies how long the agent waits before reporting another no-transaction message. Agents always report a no-transaction message when they detect that there are no transactions available at the source after previously processing replicated transactions. The default is 60 seconds.  
  
 **-OledbStreamThreshold** _oledb_stream_threshold_  
 Specifies the minimum size, in bytes, for binary large object data above which the data will be bound as a stream. You must specify **-UseOledbStreaming** to use this parameter. Values can range from 400 to 1048576 bytes, with a default of 16384 bytes.  
  
 **-Output** _output_path_and_file_name_  
 Is the path of the agent output file. If the file name is not provided, the output is sent to the console. If the specified file name exists, the output is appended to the file.  
  
 **-OutputVerboseLevel** [ **0**| **1**| **2**]  
 Specifies whether the output should be verbose. If the verbose level is **0**, only error messages are printed. If the verbose level is **1**, all the progress report messages are printed. If the verbose level is **2** (default), all error messages and progress report messages are printed, which is useful for debugging.  
  
 **-PacketSize** _packet_size_  
 Is the packet size, in bytes. The default is 4096 (bytes).  
  
 **-PollingInterval** _polling_interval_  
 Is how often, in seconds, the distribution database is queried for replicated transactions. The default is 5 seconds.  
  
 **-ProfileName** _profile_name_  
 Specifies an agent profile to use for agent parameters. If **ProfileName** is NULL, the agent profile is disabled. If **ProfileName** is not specified, the default profile for the agent type is used. For information, see [Replication Agent Profiles](replication-agent-profiles.md).  
  
 **-Publication**  _publication_  
 Is the name of the publication. This parameter is only valid if the publication is set to always have a snapshot available for new or reinitialized subscriptions.  
  
 **-QueryTimeOut** _query_time_out_seconds_  
 Is the number of seconds before the query times out. The default is 1800 seconds.  
  
 **-QuotedIdentifier** _quoted_identifier_  
 Specifies the quoted identifier character to use. The first character of the value indicates the value the Distribution Agent uses. If **QuotedIdentifier** is used with no value, the Distribution Agent uses a space. If **QuotedIdentifier** is not used, the Distribution Agent uses whatever quoted identifier the Subscriber supports.  
  
 **-SkipErrors** _native_error_id_ [**:**_...n_]  
 Is a colon-separated list that specifies the error numbers to be skipped by this agent.  
  
 **-SubscriberDatabasePath** _subscriber_database_path_  
 Is the path to the Jet database (.mdb file) if **SubscriberType** is **2** (allows a connection to a Jet database without an ODBC Data Source Name (DSN)).  
  
 **-SubscriberLogin** _subscriber_login_  
 Is the Subscriber login name. If **SubscriberSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-SubscriberPassword** _subscriber_password_  
 Is the Subscriber password. If **SubscriberSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-SubscriberSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Subscriber. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, and a value of **1** indicates Windows Authentication Mode (default).  
  
 **-SubscriberType** [ **0**| **1**| **3**]  
 Specifies the type of Subscriber connection used by the Distribution Agent.  
  
|SubscriberType value|Description|  
|--------------------------|-----------------|  
|**0**|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]|  
|**1**|ODBC data source|  
|**3**|OLE DB data source|  
  
 **-SubscriptionStreams** [**0**|**1**|**2**|...**64**]  
 Is the number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber, while maintaining many of the transactional characteristics present when using a single thread. For a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, a range of values from 1 to 64 is supported. This parameter is only supported when the Publisher and Distributor are running on [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later versions. This parameter is not supported or must be 0 for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers or peer-to-peer subscriptions.  
  
> [!NOTE]  
>  If one of the connections fails to execute or commit, all connections will abort the current batch, and the agent will use a single stream to retry the failed batches. Before this retry phase completes, there can be temporary transactional inconsistencies at the Subscriber. After the failed batches are successfully committed, the Subscriber is brought back to a state of transactional consistency.  
  
> [!IMPORTANT]  
>  When you specify a value of 2 or greater for **-SubscriptionStreams**, the order in which transactions are received at the Subscriber may differ from the order in which they were made at the Publisher. If this behavior causes constraint violations during synchronization, you should use the NOT FOR REPLICATION option to disable the enforcement of constraints during synchronization. For more information, see [Control the Behavior of Triggers and Constraints During Synchronization &#40;Replication Transact-SQL Programming&#41;](../control-behavior-of-triggers-and-constraints-in-synchronization.md).  
  
> [!NOTE]  
>  Subscriptionstreams do not work for articles configured to deliver [!INCLUDE[tsql](../../../includes/tsql-md.md)]. To use subscriptionstreams, configure articles to deliver stored procedure calls instead.  
  
 **-SubscriptionTableName** _subscription_table_  
 Is the name of the subscription table generated or used at the given Subscriber. When not specified, the [MSreplication_subscriptions &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/msreplication-subscriptions-transact-sql) table is used. Use this option for database management systems (DBMS) that do not support long file names.  
  
 **-SubscriptionType** [ **0**| **1**| **2**]  
 Specifies the subscription type for distribution. A value of **0** indicates a push subscription, a value of **1** indicates a pull subscription, and a value of **2** indicates an anonymous subscription.  
  
 **-TransactionsPerHistory** [ **0**| **1**|... **10000**]  
 Specifies the transaction interval for history logging. If the number of committed transactions after the last instance of history logging is greater than this option, a history message is logged. The default is 100. A value of **0** indicates infinite **TransactionsPerHistory**. See the preceding **-MessageInterval**parameter.  
  
 **-UseDTS**  
 Must be specified as a parameter for a publication that allows data transformation.  
  
 **-UseInprocLoader**  
 Improves the performance of the initial snapshot by causing the Distribution Agent to use the BULK INSERT command when applying snapshot files to the Subscriber. This parameter is deprecated because it is not compatible with the XML data type. If you are not replicating XML data, this parameter can be used. This parameter cannot be used with character mode snapshots or non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers. If you use this parameter, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account at the Subscriber must have read permissions on the directory where the snapshot .bcp data files are located. When this parameter is not used, the agent (for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers) or the ODBC driver loaded by the agent (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers) reads from the files, so the security context of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account is not used.  
  
 **-UseOledbStreaming**  
 When specified, enables the binding of binary large object data as a stream. Use **-OledbStreamThreshold** to specify the size, in bytes, above which a stream will be used.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If you have installed [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent to run under a local system account rather than under a domain user account (the default), the service can only access the local computer. If the Distribution Agent that runs under [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent is configured to use Windows Authentication Mode when it logs in to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the Distribution Agent fails. The default setting is [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication. For information on changing security accounts, see [View and Modify Replication Security Settings](../security/view-and-modify-replication-security-settings.md).  
  
 To start the Distribution Agent, execute **distrib.exe** from the command prompt. For information, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Added the **-ExtendedEventConfigFile** parameter.|  
  
## See Also  
 [Replication Agent Administration](replication-agent-administration.md)  
  
  
