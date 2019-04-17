---
title: "Replication Merge Agent | Microsoft Docs"
ms.custom: ""
ms.date: "10/29/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Merge Agent, executables"
  - "Merge Agent, parameter reference"
  - "agents [SQL Server replication], Merge Agent"
  - "command prompt [SQL Server replication]"
ms.assetid: fe1e7f60-b0c8-45e9-a5e8-4fedfa73d7ea
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replication Merge Agent
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Replication Merge Agent is a utility executable that applies the initial snapshot held in the database tables to the Subscribers. It also merges incremental data changes that occurred at the Publisher after the initial snapshot was created, and reconciles conflicts either according to the rules you configure or using a custom resolver you create.  
  
> [!NOTE]  
>  Parameters can be specified in any order. When optional parameters are not specified, values from predefined registry settings on the local computer are used.  
  
## Syntax  
  
```  
  
replmerg [-?]   
-Publisher server_name[\instance_name]  
-PublisherDB publisher_database  
-Publication publication  
-Subscriber server_name[\instance_name]  
-SubscriberDB subscriber_database  
[-AltSnapshotFolder alt_snapshot_folder_path]  
[-Continuous]  
[-DefinitionFile def_path_and_file_name]  
[-DestThreads number_of_destination_threads]  
[-Distributor server_name[\instance_name]]  
[-DistributorLogin distributor_login]  
[-DistributorPassword distributor_password]  
[-DistributorSecurityMode [0|1]]  
[-DownloadGenerationsPerBatch download_generations_per_batch]  
[-DownloadReadChangesPerBatch download_read_changes_per_batch]  
[-DownloadWriteChangesPerBatch download_write_changes_per_batch]  
[-DynamicSnapshotLocation dynamic_snapshot_location]  
[-EncryptionLevel [0|1|2]]  
[-ExchangeType [1|2|3]]  
[-FastRowCount [0|1]]  
[-FileTransferType [0|1]]  
[-ForceConvergenceLevel [0|1|2 (Publisher|Subscriber|Both)]]  
[-FtpAddress ftp_address]  
[-FtpPassword ftp_password]  
[-FtpPort ftp_port]  
[-FtpUserNameftp_user_name]  
[-HistoryVerboseLevel [0|1|2|3]]  
[-Hostname host_name]  
[-InteractiveResolution [0|1]]  
[-InternetLogin internet_login]  
[-InternetPassword internet_password]  
[-InternetProxyLogin internet_proxy_login]  
[–InternetProxyPassword internet_proxy_password]  
[-InternetProxyServer internet_proxy_server]  
[-InternetSecurityMode [0|1]]  
[-InternetTimeout internet_timeout]  
[-InternetURL internet_url]  
[-KeepAliveMessageInterval keep_alive_message_interval_seconds]  
[-LoginTimeOut login_time_out_seconds]  
[-MakeGenerationInterval make_generation_interval_seconds]  
[-MaxBcpThreads number_of_threads]  
[-MaxDownloadChanges number_of_download_changes]  
[-MaxUploadChanges number_of_upload_changes]  
[-MetadataRetentionCleanup [0|1]]  
[-Output]  
[-OutputVerboseLevel [0|1|2]]  
[-ParallelUploadDownload [0|1]]  
[-PacketSize packet_size]   
[-PollingInterval polling_interval]  
[-ProfileName profile_name]  
[-PublisherFailoverPartner server_name[\instance_name] ]  
[-PublisherLogin publisher_login]  
[-PublisherPassword publisher_password]  
[-PublisherSecurityMode [0|1]]  
[-QueryTimeOut query_time_out_seconds]  
[-SrcThreads number_of_source_threads]  
[-StartQueueTimeout start_queue_timeout_seconds]  
[-SubscriberConflictClean [0|1]]  
[-SubscriberDatabasePath subscriber_path]  
[-SubscriberDBAddOption [0|1|2|3]]  
[-SubscriberLogin subscriber_login]  
[-SubscriberPassword subscriber_password   
[-SubscriberSecurityMode [0|1]]  
[-SubscriberType [0|1|2|3|4|5|6|7|8|9]]  
[-SubscriptionType [0|1|2]]  
[-SyncToAlternate [0|1]  
[-UploadGenerationsPerBatch upload_generations_per_batch]  
[-UploadReadChangesPerBatch upload_read_changes_per_batch]  
[-UploadWriteChangesPerBatch upload_write_changes_per_batch]  
[-UseInprocLoader]  
[-Validate [0|1|2|3]]  
[-ValidateInterval validate_interval]  
```  
  
## Arguments  
 **-?**  
 Prints all available parameters.  
  
 **-Publisher** _server_name_[**\\**_instance_name_]  
 Is the name of the Publisher. Specify *server_name* for the default instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-PublisherDB** _publisher_database_  
 Is the name of the Publisher database.  
  
 **-Publication** _publication_  
 Is the name of the publication. This parameter is only valid if the publication is set to always have a snapshot available for new or reinitialized subscriptions.  
  
 **-Subscriber** _server_name_[**\\**_instance_name_]  
 Is the name of the Subscriber. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-SubscriberDB** _subscriber_database_  
 Is the name of the Subscriber database.  
  
 **-AltSnapshotFolder** _alt_snapshot_folder_path_  
 Is the path to the folder that contains the initial snapshot for a subscription.  
  
 **-Continuous**  
 Specifies whether the agent attempts to poll replicated transactions continually. If specified, the agent polls replicated transactions from the source at polling intervals, even if there are no transactions pending.  
  
 **-DestThreads** _number_of_destination_threads_  
 Specifies the number of destination threads that the Merge Agent uses to apply changes at the destination. The destination is the Publisher during upload and the Subscriber during download. The default is 4.  
  
 **-DefinitionFile** _def_path_and_file_name_  
 Is the path of the agent definition file. An agent definition file contains command prompt arguments for the agent. The content of the file is parsed as an executable file. Use double quotation marks (") to specify argument values containing arbitrary characters.  
  
 **-Distributor** _server_name_[**\\**_instance_name_]  
 Is the Distributor name. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. For Distributor (push) distribution, the name defaults to the name of the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on the local computer.  
  
 **-DistributorLogin** _distributor_login_  
 Is the Distributor login name.  
  
 **-DistributorPassword** _distributor_password_  
 Is the Distributor password.  
  
 **-DistributorSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Distributor. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication Mode (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-DownloadGenerationsPerBatch** _download_generations_per_batch_  
 Is the number of generations to be processed in a single batch while downloading changes from the Publisher to the Subscriber. A generation is defined as a logical group of changes per article. The default for a reliable communication link is 100. The default for an unreliable communication link is 10.  
  
 **-DownloadReadChangesPerBatch** _download_read_changes_per_batch_  
 Is the number of changes to be read in a single batch while downloading changes from the Publisher to the Subscriber. The default is 100.  
  
 **-DownloadWriteChangesPerBatch** _download_write_changes_per_batch_  
 Is the number of changes to be applied in a single batch while downloading changes from the Publisher to the Subscriber. The default is 100.  
  
 **-DynamicSnapshotLocation** _dynamic_snapshot_location_  
 Is the location of the filtered data snapshot files when the publication uses parameterized row filters.  
  
 **-EncryptionLevel** [ **0** | **1** | **2** ]  
 Is the level of Secure Sockets Layer (SSL) encryption used by the Merge Agent when making connections.  
  
|EncryptionLevel value|Description|  
|---------------------------|-----------------|  
|**0**|Specifies that SSL is not used.|  
|**1**|Specifies that SSL is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer.|  
|**2**|Specifies that SSL is used, and that the certificate is verified.|  

 > [!NOTE]  
 >  A valid SSL certificate is defined with a fully qualified domain name of the SQL Server. In order for the agent to connect successfully when setting -EncryptionLevel to 2, create an alias on the local SQL Server. The ‘Alias Name’ parameter should be the server name and the ‘Server’ parameter should be set to the fully qualified name of the SQL Server.

 For more information, see [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 **-ExchangeType** [ **1**| **2**| **3**]  
> [!WARNING]
>  [!INCLUDE[ssNoteDepFutureDontUse](../../../includes/ssnotedepfuturedontuse-md.md)] To restrict uploading, use the **@subscriber_upload_options** of **sp_addmergearticle** instead.  
  
 Specifies the type of data exchange during synchronization, which can be one of the following:  
  
|ExchangeType value|Description|  
|------------------------|-----------------|  
|**1**|Agent should upload data changes from the Subscriber to the Publisher.|  
|**2**|Agent should download data changes from the Publisher to the Subscriber.|  
|**3** (default)|Agent should first upload data changes from the Subscriber to the Publisher and then download data changes from the Publisher to the Subscriber. You must use this option with Web synchronization.|  
  
 Download-only articles enable you to control the synchronization behavior of individual articles in a publication, and they can provide a performance benefit. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
 If using **ExchangeType** to separate the upload and download phase of merge replication into separate sessions, you must run the merge agent with **ExchangeType** set to 1 first and then run the merge agent again with the value 2. Failure to run the merge agent with both parameters will cause metadata to be deleted and require you to reinitialize the subscription (without upload).  
  
 **-FastRowCount** [**0**|**1**]  
 Specifies what type of rowcount calculation method should be used for rowcount validation. A value of **1** (default) indicates the fast method. A value of **0** indicates the full rowcount method.  
  
 **-FileTransferType** [**0**|**1**]  
 Specifies the file transfer type. A value of **0** indicates UNC (universal naming convention), and a value of **1** indicates FTP (file transfer protocol).  
  
 **-ForceConvergenceLevel** [**0**|**1**|**2** ( **Publisher**| **Subscriber**| **Both**)]  
 Specifies the level of convergence the Merge Agent should use, and can be one of the following:  
  
|ForceConvergenceLevel value|Description|  
|---------------------------------|-----------------|  
|**0** (default)|Default. Perform a standard merge without additional convergence.|  
|**1**|Force convergence for all generations.|  
|**2**|Force convergence for all generations and correct corrupt lineages. When specifying this value, specify where lineages should be corrected: the Publisher, the Subscriber, or both the Publisher and the Subscriber.|  
  
 **-FtpAddress** _ftp_address_  
 Is the network address of the FTP service for the Distributor. When not specified, **Distributor** is used.  
  
 **-FtpPassword** _ftp_password_  
 Is the user password used to connect to the FTP service.  
  
 **-FtpPort** _ftp_port_  
 Is the port number of the FTP service for the Distributor. When not specified, the default port number for FTP service (21) is used.  
  
 **-FtpUserName** _ftp_user_name_  
 Is the user name used to connect to the FTP service. When not specified, anonymous is used.  
  
 **-HistoryVerboseLevel** [**1**|**2**|**3**]  
 Specifies the amount of history logged during a merge operation. You can minimize the effect of history logging on performance by selecting **1**.  
  
|HistoryVerboseLevel value|Description|  
|-------------------------------|-----------------|  
|**0**|Log the final agent status message, final session details, and any errors.|  
|**1**|Log incremental session details at each session status, including percent complete, in addition to the final agent status message, final session details, and any errors.|  
|**2**|Default. Log both incremental session details at each session status and article level session details, including percent complete, in addition to the final agent status message, final session details, and any errors. Agent status messages are also logged.|  
|**3**|The same as **-HistoryVerboseLevel** = **2**, except that more agent progress messages are logged.|  
  
 **-Hostname** _host_name_  
 Is the network name of the local computer. The default is the local computer name.  
  
 **-InteractiveResolution** [**0**|**1**]  
 Specifies whether interactive conflict resolution is used when a conflict occurs during synchronization. The default is **0**, indicating that interactive conflict resolution is not used.  
  
 **-InternetLogin** _internet_login_  
 Specifies the login name used when connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication listener ISAPI DLL that requires authentication.  
  
 **-InternetPassword** _internet_password_  
 Specifies the password used when connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication listener ISAPI DLL that requires authentication.  
  
 **-InternetProxyLogin**  *internet_proxy_login*  
 Specifies the login name used when connecting to a proxy server, defined in *internet_proxy_server*, that requires authentication.  
  
 **–InternetProxyPassword**  *internet_proxy_password*  
 Specifies the password used when connecting to a proxy server, defined in *internet_proxy_server*, that requires authentication.  
  
 **-InternetProxyServer**  *internet_proxy_server*  
 Specifies the proxy server to use when accessing the HTTP resource specified in *internet_url*.  
  
 **-InternetSecurityMode** [**0**|**1**]  
 Specifies the IIS security mode used when connecting to the Web server during Web synchronization. A value of **0** indicates Basic Authentication, and a value of **1** indicates Windows Integrated Authentication (default).  
  
 **-InternetTimeout** _internet_timeout_  
 Is the number of seconds before a connection to the to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication listener ISAPI DLL times out.  
  
 **-InternetURL** _internet_url_  
 Specifies the URL used to connect to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication listener ISAPI DLL. This property must be specified.  
  
 **-KeepAliveMessageInterval** _keep_alive_message_interval_seconds_  
 Is the number of seconds before the history thread checks if any of the existing connections is waiting for a response from the server. This value can be decreased to avoid having the checkup agent mark the Merge Agent as suspect when executing a long-running batch. The default is **300** seconds.  
  
 **-LoginTimeOut** _login_time_out_seconds_  
 Is the number of seconds before the login times out. The default is **15** seconds.  
  
 **-MakeGenerationInterval** _make_generation_interval_seconds_  
 Is the number of seconds to wait between creating generations, or batches of changes, to download to the client. The default is **1** second.  
  
 Makegeneration is the process that prepares Publisher changes to be downloaded to Subscribers, and it can be a performance bottleneck during downloads. If the makegeneration process already ran within the interval specified by **-MakeGenerationInterval**, the process is skipped for the current synchronization session. This can benefit synchronization concurrency and is especially helpful if Subscribers do not expect to download changes.  
  
 **-MaxBcpThreads** _number_of_threads_  
 Specifies the number of bulk copy operations that can be performed in parallel. The maximum number of threads and ODBC connections that exist simultaneously is the lesser of **MaxBcpThreads** or the number of bulk copy requests that appear in the system table **sysmergeschemachange** in the publication database. **MaxBcpThreads** must have a value greater than 0 and has no hard-coded upper limit. The default is **1**.  
  
 **-MaxDownloadChanges** _number_of_download_changes_  
 Specifies the maximum number of changed rows that should be downloaded from the Publisher to the Subscriber. The number of rows downloaded may be higher than the specified maximum because: complete generations are processed; and parallel destination threads may run, each of which processes at least 100 changes in its first pass. By default all changes that are ready to be downloaded are sent.  
  
 **-MaxUploadChanges** _number_of_upload_changes_  
 Specifies the maximum number of changed rows that should be uploaded from the Subscriber to the Publisher. The number of rows uploaded may be higher than the specified maximum because: complete generations are processed; and parallel destination threads may run, each of which processes at least 100 changes in its first pass. By default all changes that are ready to be uploaded are sent.  
  
 **-MetadataRetentionCleanup** [**0**|**1**]  
 Specifies if metadata is removed from [MSmerge_genhistory](../../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../../relational-databases/system-tables/msmerge-contents-transact-sql.md), [MSmerge_tombstone](../../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md), [MSmerge_past_partition_mappings](../../../relational-databases/system-tables/msmerge-past-partition-mappings-transact-sql.md), and [MSmerge_current_partition_mappings](../../../relational-databases/system-tables/msmerge-current-partition-mappings.md) based on the publication retention period. The default is **1**, indicating that cleanup should occur. A value of **0** indicates that cleanup should not occur automatically.  
  
 **-Output** _output_path_and_file_name_  
 Is the path of the agent output file. If the file name is not provided, the output is sent to the console. If the specified file name exists, the output is appended to the file.  
  
 **-OutputVerboseLevel** [**0**|**1**|**2**]  
 Specifies whether the output should be verbose. If the verbose level is **0**, only error messages are printed. If the verbose level is **1**, all of the progress report messages are printed. If the verbose level is **2** (default), all error messages and progress report messages are printed, which is useful for debugging.  
  
 **-ParallelUploadDownload** [**0**|**1**]  
 Specifies whether the Merge Agent should process in parallel the changes uploaded to the Publisher and those downloaded to the Subscriber, which is useful in high volume environments with high network bandwidth. If **ParallelUploadDownload** is **1**, then parallel processing is enabled.  
  
 **-PacketSize**  
 Is the packet size, in bytes. The default is 4096 (bytes).  
  
 **-PollingInterval** _polling_interval_  
 Is how often, in seconds, the Publisher or Subscriber is queried for data changes. The default is 60 seconds.  
  
 **-ProfileName** _profile_name_  
 Specifies an agent profile to use for agent parameters. If **ProfileName** is NULL, the agent profile is disabled. If **ProfileName** is not specified, the default profile for the agent type is used. For information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
 **-PublisherFailoverPartner** _server_name_[**\\**_instance_name_]  
 Specifies the failover partner instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] participating in a database mirroring session with the publication database. For more information, see [Database Mirroring and Replication &#40;SQL Server&#41;](../../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md).  
  
 **-PublisherLogin** _publisher_login_  
 Is the Publisher login name. If **PublisherSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-PublisherPassword** _publisher_password_  
 Is the Publisher password. If **PublisherSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-PublisherSecurityMode** [**0**|**1**]  
 Specifies the security mode of the Publisher. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-QueryTimeOut** _query_time_out_seconds_  
 Is the number of seconds before the query times out. The default is 300 seconds. The Merge Agent also uses the value of **QueryTimeout** to determine how long to wait for generation of a partitioned snapshot when this value is greater than 1800.  
  
 **-SrcThreads** _number_of_source_threads_  
 Specifies the number of source threads that the Merge Agent uses to enumerate changes from the source. The source is the Subscriber during upload and the Publisher during download. The default is **3**.  
  
 **-StartQueueTimeout** _start_queue_timeout_seconds_  
 Is the maximum number of seconds that the Merge Agent waits when the number of concurrent merge processes running is at the limit set by the **@max_concurrent_merge** property of **sp_addmergepublication**. If the maximum number of seconds is reached and the Merge Agent is still waiting, it will exit. A value of 0 means that the agent waits indefinitely, although it can be cancelled.  
  
 **-SubscriberDatabasePath** _subscriber_database_path_  
 Is the path to the Jet database (.mdb file) if **SubscriberType** is **2** (allows a connection to a Jet database without an ODBC Data Source Name (DSN)).  
  
 **-SubscriberDBAddOption** [**0**| **1**| **2**| **3**]  
 Specifies whether there is an existing Subscriber database.  
  
|SubscriberDBAddOption value|Description|  
|---------------------------------|-----------------|  
|**0**|Use the existing database (default).|  
|**1**|Create a new, empty Subscriber database.|  
|**2**|Create a new database and attach it to the specified file.|  
|**3**|Create a new database, attach the database, and enable all subscriptions that might exist at the file.|  
  
> [!NOTE]  
>  When you use values **2** and **3**, the database path for the Subscriber must be specified in the **SubscriberDatabasePath** option.  
  
 **-SubscriberLogin** _subscriber_login_  
 Is the Subscriber login name. If **SubscriberSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-SubscriberPassword** _subscriber_password_  
 Is the Subscriber password. If **SubscriberSecurityMode** is **0** (for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication), this parameter must be specified.  
  
 **-SubscriberSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Subscriber. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-SubscriberConflictClean** [ **0**| **1**]  
 Is if conflict tables are cleaned-up at the Subscriber during the synchronization process, where a value of **1** indicates that conflict tables at the Subscriber are cleaned-up. This parameter is used only for subscriptions to publications with decentralized conflict logging.  
  
 **-SubscriberType** [ **0**| **1**| **3**| **4**| **5**| **6**| **7**| **8**]  
 Specifies the type of Subscriber connection used by the Merge Agent. Only the default value of **0** is supported for this parameter.  
  
 **-SubscriptionType**[ **0**| **1**| **2**]  
 Specifies the subscription type for distribution. A value of **0** indicates a push subscription (default), a value of **1** indicates a pull subscription, and a value of **2** indicates an anonymous subscription.  
  
 **-SyncToAlternate** [ **0|1**]  
 Specifies whether the Merge Agent is synchronizing between a Subscriber and an alternate Publisher. A value of **1** indicates that it is an alternate Publisher. The default is **0**.  
  
 **-UploadGenerationsPerBatch** _upload_generations_per_batch_  
 Is the number of generations to be processed in a single batch while uploading changes from the Subscriber to the Publisher. A generation is defined as a logical group of changes per article. The default for a reliable communication link is **100**. The default for an unreliable communication link is **1**.  
  
 **-UploadReadChangesPerBatch** _upload_read_changes_per_batch_  
 Is the number of changes to be read in a single batch while uploading changes from the Subscriber to the Publisher. The default is **100**.  
  
 **-UploadWriteChangesPerBatch** _upload_write_changes_per_batch_  
 Is the number of changes to be applied in a single batch while uploading changes from the Subscriber to the Publisher. The default is **100**.  
  
 **-UseInprocLoader**  
 Improves the performance of the initial snapshot by causing the Merge Agent to use the BULK INSERT command when applying snapshot files to the Subscriber. This parameter is deprecated because it is not compatible with the XML data type. If you are not replicating XML data, this parameter can be used. This parameter cannot be used with character mode snapshots. If you use this parameter, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account at the Subscriber must have read permissions on the directory where the snapshot .bcp data files are located. When this parameter is not used, the ODBC driver loaded by the agent reads from the files, so the security context of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account is not used.  
  
 **-Validate** [**0**|**1**|**2**|**3**]  
 Specifies whether validation should be done at the end of the merge session, and, if so, what type of validation. The value of **3** is the recommended value.  
  
|Validate value|Description|  
|--------------------|-----------------|  
|**0** (default)|No validation.|  
|**1**|Rowcount-only validation.|  
|**2**|Rowcount and checksum validation.|  
|**3**|Rowcount and binary checksum validation.|  
  
> [!NOTE]  
>  Validation by using binary checksum or checksum can incorrectly report a failure if data types are different at the Subscriber than they are at the Publisher. For more information, see the section "Considerations for Data Validation" in [Validate Replicated Data](../../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
 **-ValidateInterval** _validate_interval_  
 Is how often, in minutes, the subscription is validated in continuous mode. The default is **60** minutes.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If you have installed [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent to run under a local system account rather than under a domain user account (the default), the service can access only the local computer. If the Merge Agent that runs under [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent is configured to use Windows Authentication Mode when it logs in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the Merge Agent fails. The default setting is [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
  
 To start the Merge Agent, execute **replmerg.exe** from the command prompt. For information, see [Replication Agent Executables](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md).  
  
 The merge agent history for the current session is not removed while running in continuous mode. A long running agent can result in a large number of entries in the merge history tables which could impact performance. To resolve this problem switch to scheduled mode, or continue to use continuous mode but create a dedicated job to periodically restart the merge agent, or reduce the verbosity of the history level to reduce the number of rows and therefor reduce the performance impact.  
  
## See Also  
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)  
  
  
