---
title: "Replication Snapshot Agent | Microsoft Docs"
ms.custom: ""
ms.date: "10/29/2018"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Snapshot Agent, executables"
  - "agents [SQL Server replication], Snapshot Agent"
  - "command prompt [SQL Server replication]"
  - "Snapshot Agent, parameter reference"
ms.assetid: 2028ba45-4436-47ed-bf79-7c957766ea04
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Snapshot Agent
  The Replication Snapshot Agent is an executable file that prepares snapshot files containing schema and data of published tables and database objects, stores the files in the snapshot folder, and records synchronization jobs in the distribution database.  
  
> [!NOTE]  
>  Parameters can be specified in any order.  
  
## Syntax  
  
```  
  
      snapshot [ -?]   
-Publisherserver_name[\instance_name]   
-Publication publication_name   
[-70Subscribers]   
[-BcpBatchSizebcp_batch_size]  
[-DefinitionFiledef_path_and_file_name]  
[-Distributorserver_name[\instance_name]]  
[-DistributorDeadlockPriority [-1|0|1] ]  
[-DistributorLogindistributor_login]  
[-DistributorPassworddistributor_password]  
[-DistributorSecurityMode [0|1] ]  
[-DynamicFilterHostNamedynamic_filter_host_name]  
[-DynamicFilterLogindynamic_filter_login]  
[-DynamicSnapshotLocationdynamic_snapshot_location]   
[-EncryptionLevel [0|1|2]]  
[-FieldDelimiterfield_delimiter]  
[-HistoryVerboseLevel [0|1|2|3] ]  
[-HRBcpBlocksnumber_of_blocks ]  
[-HRBcpBlockSizeblock_size ]  
[-HRBcpDynamicBlocks ]  
[-KeepAliveMessageIntervalkeep_alive_interval]  
[-LoginTimeOutlogin_time_out_seconds]  
[-MaxBcpThreadsnumber_of_threads ]  
[-MaxNetworkOptimization [0|1]]  
[-Outputoutput_path_and_file_name]  
[-OutputVerboseLevel [0|1|2] ]  
[-PacketSizepacket_size]
[-PrefetchTables [0|1] ]  
[-ProfileNameprofile_name]  
[-PublisherDBpublisher_database]  
[-PublisherDeadlockPriority [-1|0|1] ]  
[-PublisherFailoverPartnerserver_name[\instance_name] ]  
[-PublisherLoginpublisher_login]  
[-PublisherPasswordpublisher_password]   
[-PublisherSecurityMode [0|1] ]  
[-QueryTimeOutquery_time_out_seconds]  
[-ReplicationType [1|2] ]  
[-RowDelimiterrow_delimiter]  
[-StartQueueTimeoutstart_queue_timeout_seconds]  
[-UsePerArticleContentsViewuse_per_article_contents_view]  
```  
  
## Arguments  
 **-?**  
 Prints all available parameters.  
  
 **-Publisher** _server_name_[**\\**_instance_name_]  
 Is the name of the Publisher. Specify server_name for the default instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-Publication** _publication_  
 Is the name of the publication. This parameter is only valid if the publication is set to always have a snapshot available for new or reinitialized subscriptions.  
  
 **-70Subscribers**  
 Must be used if any Subscribers are running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] version 7.0.  
  
 **-BcpBatchSize** _bcp_\_ *batch*\_ *size*  
 Is the number of rows to send in a bulk copy operation. When performing a **bcp in** operation, the batch size is the number of rows to send to the server as one transaction, and also the number of rows that must be sent before the Distribution Agent logs a **bcp** progress message. When performing a **bcp out** operation, a fixed batch size of 1000 is used. A value of 0 indicates no message logging.  
  
 **-DefinitionFile** _def_path_and_file_name_  
 Is the path of the agent definition file. An agent definition file contains command line arguments for the agent. The content of the file is parsed as an executable file. Use double quotation marks (") to specify argument values containing arbitrary characters.  
  
 **-Distributor** _server_name_[**\\**_instance_name_]  
 Is the Distributor name. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-DistributorDeadlockPriority** [**-1**|**0**|**1**]  
 Is the priority of the Snapshot Agent connection to the Distributor when a deadlock occurs. This parameter is specified to resolve deadlocks that may occur between the Snapshot Agent and user applications during snapshot generation.  
  
|DistributorDeadlockPriority value|Description|  
|---------------------------------------|-----------------|  
|**-1**|Applications other than the Snapshot Agent have priority when a deadlock occurs at the Distributor.|  
|**0** (Default)|Priority is not assigned.|  
|**1**|Snapshot Agent has priority when a deadlock occurs at the Distributor.|  
  
 **-DistributorLogin** _distributor_login_  
 Is the login used when connecting to the Distributor using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
  
 **-DistributorPassword** _distributor_password_  
 Is the password used when connecting to the Distributor using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication. .  
  
 **-DistributorSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Distributor. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication Mode (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-DynamicFilterHostName** _dynamic_filter_host_name_  
 Is used to set a value for [HOST_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/host-name-transact-sql) in filtering when a dynamic snapshot is created. For example, if the subset filter clause `rep_id = HOST_NAME()` is specified for an article, and you set the **DynamicFilterHostName** property to "FBJones" before calling the Merge Agent, only rows having "FBJones" in the **rep_id** column will be replicated.  
  
 **-DynamicFilterLogin** _dynamic_filter_login_  
 Is used to set a value for [SUSER_SNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/suser-sname-transact-sql)in filtering when a dynamic snapshot is created. For example, if the subset filter clause `user_id = SUSER_SNAME()` is specified for an article, and you set the **DynamicFilterLogin** property to "rsmith" before calling the **Run** method of the **SQLSnapshot** object, only rows having "rsmith" in the **user_id** column will be included in the snapshot.  
  
 **-DynamicSnapshotLocation** _dynamic_snapshot_location_  
 Is the location where the dynamic snapshot should be generated.  
  
 **-EncryptionLevel** [ **0** | **1** | **2** ]  
 Is the level of Secure Sockets Layer (SSL) encryption used by the Snapshot Agent when making connections.  
  
|EncryptionLevel value|Description|  
|---------------------------|-----------------|  
|**0**|Specifies that SSL is not used.|  
|**1**|Specifies that SSL is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer.|  
|**2**|Specifies that SSL is used, and that the certificate is verified.|  

 > [!NOTE]  
 >  A valid SSL certificate is defined with a fully qualified domain name of the SQL Server. In order for the agent to connect successfully when setting -EncryptionLevel to 2, create an alias on the local SQL Server. The 'Alias Name' parameter should be the server name and the 'Server' parameter should be set to the fully qualified name of the SQL Server.
  
 For more information, see [SQL Server Replication Security](../security/view-and-modify-replication-security-settings.md).  
  
 **-FieldDelimiter** _field_delimiter_  
 Is the character or character sequence that marks the end of a field in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] bulk-copy data file. The default is \n\<x$3>\n.  
  
 **-HistoryVerboseLevel** [ **1**| **2**| **3**]  
 Specifies the amount of history logged during a snapshot operation. You can minimize the effect of history logging on performance by selecting **1**.  
  
|HistoryVerboseLevel value|Description|  
|-------------------------------|-----------------|  
|**0**|Progress messages are written either to the console or to an output file. History records are not logged in the distribution database.|  
|**1**|Always update a previous history message of the same status (startup, progress, success, and so on). If no previous record with the same status exists, insert a new record.|  
|**2** (default)|Insert new history records unless the record is for such things as idle messages or long-running job messages, in which case update the previous records.|  
|**3**|Always insert new records, unless it is for idle messages.|  
  
 **-HRBcpBlocks** _number_of_blocks_  
 Is the number of **bcp** data blocks that are queued between the writer and reader threads. The default value is 50. **HRBcpBlocks** is only used with Oracle publications.  
  
> [!NOTE]  
>  This parameter is used for performance tuning of **bcp** performance from an Oracle Publisher.  
  
 -**HRBcpBlockSize**_block_size_  
 Is the size, in kilobytes (KB), of each **bcp** data block. The default value is 64 KB. **HRBcpBlocks** is only used with Oracle publications.  
  
> [!NOTE]  
>  This parameter is used for performance tuning of **bcp** performance from an Oracle Publisher.  
  
 **-HRBcpDynamicBlocks**  
 Is whether or not the size of each **bcp** data block can grow dynamically. **HRBcpBlocks** is only used with Oracle publications.  
  
> [!NOTE]  
>  This parameter is used for performance tuning of **bcp** performance from an Oracle Publisher.  
  
 **-KeepAliveMessageInterval** _keep_alive_interval_  
 Is the amount of time, in seconds, that the Snapshot Agent waits before logging "waiting for backend message" to the [MSsnapshot_history](/sql/relational-databases/system-tables/mssnapshot-history-transact-sql) table. The default value is 300 seconds.  
  
 **-LoginTimeOut** _login_time_out_seconds_  
 Is the number of seconds before the login times out. The default is **15** seconds.  
  
 **-MaxBcpThreads** _number_of_threads_  
 Specifies the number of bulk copy operations that can be performed in parallel. The maximum number of threads and ODBC connections that exist simultaneously is the lesser of **MaxBcpThreads** or the number of bulk copy requests that appear in the synchronization transaction in the distribution database. **MaxBcpThreads** must have a value greater than **0** and has no hard-coded upper limit. The default is **1**.  
  
 \- **MaxNetworkOptimization** [ **0**| **1**]  
 Is if irrelevant deletes are sent to the Subscriber. Irrelevant deletes are DELETE commands that are sent to Subscribers for rows that do not belong to the Subscriber's partition. Irrelevant deletes do not affect data integrity or convergence, but they can result in unnecessary network traffic. The default value of **MaxNetworkOptimization** is **0**. Setting **MaxNetworkOptimization** to **1** minimizing the chances of irrelevant deletes thereby reducing network traffic and maximizing network optimization. Setting this parameter to **1** can also increase the storage of metadata and cause performance to degrade at the Publisher if multiple levels of join filters and complex subset filters are present. You should carefully assess your replication topology and set **MaxNetworkOptimization** to **1** only if network traffic from irrelevant deletes is unacceptably high.  
  
> [!NOTE]
>  Setting this parameter to **1** is useful only when the synchronization optimization option of the merge publication is set to **true** (the **@keep_partition_changes** parameter of [sp_addmergepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql)).  
  
 **-Output** _output_path_and_file_name_  
 Is the path of the agent output file. If the file name is not provided, the output is sent to the console. If the specified file name exists, the output is appended to the file.  
  
 **-OutputVerboseLevel** [ **0**| **1**| **2**]  
 Specifies whether the output should be verbose.  
  
|OutputVerboseLevel value|Description|  
|------------------------------|-----------------|  
|**0**|Only error messages are printed.|  
|**1** (default)|All the progress report messages are printed (default).|  
|**2**|All error messages and progress report messages are printed, which is useful for debugging.|  

 **-PrefetchTables** [ **0**| **1**]  
 Optional parameter that specifies if the table objects will be prefetched and cached.  The default behavior is to prefetch certain table properties using SMO component based on an internal calculation.  This parameter can be helpful in scenarios where SMO prefetch operation takes considerable longer to run. If this parameter is not used, this decision is made at runtime based on the percentage of tables that are added as articles to the publication.  
  
|OutputVerboseLevel value|Description|  
|------------------------------|-----------------|  
|**0**|Call to Prefetch method of SMO component is disabled.|  
|**1**|Snapshot Agent will call Prefetch method to cache some table properties using SMO|  
  
 **-PacketSize** _packet_size_  
 Is the packet size (in bytes) used by the Snapshot Agent when connecting to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The default value is 8192 bytes.  
  
> [!NOTE]  
>  Do not change the packet size unless you are certain that it will improve performance. For most applications, the default packet size is best.  
  
 **-ProfileName** _profile_name_  
 Specifies an agent profile to use for agent parameters. If **ProfileName** is NULL, the agent profile is disabled. If **ProfileName** is not specified, the default profile for the agent type is used. For information, see [Replication Agent Profiles](replication-agent-profiles.md).  
  
 **-PublisherDB** _publisher_database_  
 Is the name of the publication database. *This parameter is not supported for Oracle Publishers*.  
  
 **-PublisherDeadlockPriority** [**-1**|**0**|**1**]  
 Is the priority of the Snapshot Agent connection to the Publisher when a deadlock occurs. This parameter is specified to resolve deadlocks that may occur between the Snapshot Agent and user applications during snapshot generation.  
  
|PublisherDeadlockPriority value|Description|  
|-------------------------------------|-----------------|  
|**-1**|Applications other than the Snapshot Agent have priority when a deadlock occurs at the Publisher.|  
|**0** (Default)|Priority is not assigned.|  
|**1**|Snapshot Agent has priority when a deadlock occurs at the Publisher.|  
  
 **-PublisherFailoverPartner** _server_name_[**\\**_instance_name_]  
 Specifies the failover partner instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] participating in a database mirroring session with the publication database. For more information, see [Database Mirroring and Replication &#40;SQL Server&#41;](../../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md).  
  
 **-PublisherLogin** _publisher_login_  
 Is the login used when connecting to the Publisher using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
  
 **-PublisherPassword** _publisher_password_  
 Is the password used when connecting to the Publisher using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication. .  
  
 **-PublisherSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Publisher. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-QueryTimeOut** _query_time_out_seconds_  
 Is the number of seconds before the query times out. The default is 1800 seconds.  
  
 **-ReplicationType** [ **1**| **2**]  
 Specifies the type of replication. A value of **1** indicates transactional replication, and a value of **2** indicates merge replication.  
  
 **-RowDelimiter** _row_delimiter_  
 Is the character or character sequence that marks the end of a row in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] bulk-copy data file. The default is \n\<,@g>\n.  
  
 **-StartQueueTimeout** _start_queue_timeout_seconds_  
 Is the maximum number of seconds that the Snapshot Agent waits when the number of concurrent dynamic snapshot processes running is at the limit set by the **@max_concurrent_dynamic_snapshots** property of [sp_addmergepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql). If the maximum number of seconds is reached and the Snapshot Agent is still waiting, it will exit. A value of 0 means that the agent waits indefinitely, although it can be canceled.  
  
 \- **UsePerArticleContentsView** _use_per_article_contents_view_  
 This parameter has been deprecated and is supported for backward-compatibility only.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If you have installed [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent to run under a Local System account rather than under a Domain User account (the default), the service can access only the local computer. If the Snapshot Agent that runs under [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent is configured to use Windows Authentication Mode when it logs in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the Snapshot Agent fails. The default setting is [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
  
 To start the Snapshot Agent, execute **snapshot.exe** from the command prompt. For information, see [Replication Agent Executables](../concepts/replication-agent-executables-concepts.md).  
  
## See Also  
 [Replication Agent Administration](replication-agent-administration.md)  
  
  
