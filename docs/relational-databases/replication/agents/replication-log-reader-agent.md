---
title: "Replication Log Reader Agent | Microsoft Docs"
ms.custom: ""
ms.date: "10/29/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Log Reader Agent, executables"
  - "Log Reader Agent, parameter reference"
  - "agents [SQL Server replication], Log Reader Agent"
  - "command prompt [SQL Server replication]"
ms.assetid: 5487b645-d99b-454c-8bd2-aff470709a0e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replication Log Reader Agent
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Replication Log Reader Agent is an executable that monitors the transaction log of each database configured for transactional replication and copies the transactions marked for replication from the transaction log into the distribution database.  
  
> [!NOTE]  
>  Parameters can be specified in any order. When optional parameters are not specified, predefined values based on the default agent profile are used.  
  
## Syntax  
  
```  
  
logread [-?]   
-Publisher server_name[\instance_name]   
-PublisherDB publisher_database   
[-Continuous]  
[-DefinitionFile def_path_and_file_name]  
[-Distributor server_name[\instance_name]]  
[-DistributorLogin distributor_login]  
[-DistributorPassword distributor_password]  
[-DistributorSecurityMode [0|1]]  
[-EncryptionLevel [0|1|2]]  
[-ExtendedEventConfigFile configuration_path_and_file_name]  
[-HistoryVerboseLevel [0|1|2]]  
[-KeepAliveMessageInterval keep_alive_message_interval_seconds]  
[-LoginTimeOut login_time_out_seconds]  
[-LogScanThreshold scan_threshold]  
[-MaxCmdsInTran number_of_commands]  
[-MessageInterval message_interval]  
[-Output output_path_and_file_name]  
[-OutputVerboseLevel [0|1|2|3|4]]  
[-PacketSize packet_size]  
[-PollingInterval polling_interval]  
[-ProfileName profile_name]   
[-PublisherFailoverPartner server_name[\instance_name] ]  
[-PublisherSecurityMode [0|1]]  
[-PublisherLogin publisher_login]  
[-PublisherPassword publisher_password]   
[-QueryTimeOut query_time_out_seconds]  
[-ReadBatchSize number_of_transactions]   
[-ReadBatchThreshold read_batch_threshold]  
[-RecoverFromDataErrors]  
```  
  
## Arguments  
 **-?**  
 Displays usage information.  
  
 **-Publisher** _server_name_[**\\**_instance_name_]  
 Is the name of the Publisher. Specify *server_name* for the default instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-PublisherDB** _publisher_database_  
 Is the name of the Publisher database.  
  
 **-Continuous**  
 Specifies whether the agent tries to poll replicated transactions continually. If specified, the agent polls replicated transactions from the source at polling intervals even if there are no transactions pending.  
  
 **-DefinitionFile** _def_path_and_file_name_  
 Is the path of the agent definition file. An agent definition file contains command-line arguments for the agent. The content of the file is parsed as an executable file. Use double quotation marks (") to specify argument values that contain arbitrary characters.  
  
 **-Distributor** _server_name_[**\\**_instance_name_]  
 Is the Distributor name. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify _server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server.  
  
 **-DistributorLogin** _distributor_login_  
 Is the Distributor login name.  
  
 **-DistributorPassword** _distributor_password_  
 Is the Distributor password.  
  
 **-DistributorSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Distributor. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication Mode (default), and a value of **1** indicates [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Authentication Mode.  
  
 **-EncryptionLevel** [ **0** | **1** | **2** ]  
 Is the level of Secure Sockets Layer (SSL) encryption that is used by the Log Reader Agent when making connections.  
  
|EncryptionLevel value|Description|  
|---------------------------|-----------------|  
|**0**|Specifies that SSL is not used.|  
|**1**|Specifies that SSL is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer.|  
|**2**|Specifies that SSL is used, and that the certificate is verified.|  

 > [!NOTE]  
 >  A valid SSL certificate is defined with a fully qualified domain name of the SQL Server. In order for the agent to connect successfully when setting -EncryptionLevel to 2, create an alias on the local SQL Server. The ‘Alias Name’ parameter should be the server name and the ‘Server’ parameter should be set to the fully qualified name of the SQL Server.
 
 For more information, see [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 **-ExtendedEventConfigFile** _configuration_path_and_file_name_  
 Specifies the path and file name for the extended events XML configuration file. The extended events configuration file allows you to configure sessions and enable events for tracking.  
  
 **-HistoryVerboseLevel** [ **0**| **1**| **2**]  
 Specifies the amount of history logged during a log reader operation. You can minimize the performance effect of history logging by selecting **1**.  
  
|HistoryVerboseLevel value|Description|  
|-------------------------------|-----------------|  
|**0**||  
|**1**|Default. Always update a previous history message of the same status (startup, progress, success, and so on). If no previous record with the same status exists, insert a new record.|  
|**2**|Insert new history records unless the record is for such things as idle messages or long-running job messages, in which case update the previous records.|  
  
 **-KeepAliveMessageInterval** _keep_alive_message_interval_seconds_  
 Is the number of seconds before the history thread checks if any of the existing connections is waiting for a response from the server. This value can be decreased to avoid having the checkup agent mark the Log Reader Agent as suspect when executing a long-running batch. The default is 300 seconds.  
  
 **-LoginTimeOut** _login_time_out_seconds_  
 Is the number of seconds before the login times out. The default is 15 seconds.  
  
 **-LogScanThreshold** _scan_threshold_  
 Internal use only.  
  
 **-MaxCmdsInTran** _number_of_commands_  
 Specifies the maximum number of statements grouped into a transaction as the Log Reader writes commands to the distribution database. Using this parameter allows the Log Reader Agent and Distribution Agent to divide large transactions (consisting of many commands) at the Publisher into several smaller transactions when applied at the Subscriber. Specifying this parameter can reduce contention at the Distributor and reduce latency between the Publisher and Subscriber. Because the original transaction is applied in smaller units, the Subscriber can access rows of a large logical Publisher transaction prior to the end of the original transaction, breaking strict transactional atomicity. The default is **0**, which preserves the transaction boundaries of the Publisher.  
  
> [!NOTE]
>  This parameter is ignored for non- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] publications. For more information, see the section "Configuring the Transaction Set Job" in [Performance Tuning for Oracle Publishers](../../../relational-databases/replication/non-sql/performance-tuning-for-oracle-publishers.md).  
  
 **-MessageInterval** _message_interval_  
 Is the time interval used for history logging. A history event is logged when the **MessageInterval** value is reached after the last history event is logged.  
  
 If there is no replicated transaction available at the source, the agent reports a no-transaction message to the Distributor. This option specifies how long the agent waits before reporting another no-transaction message. Agents always report a no-transaction message when they detect that there are no transactions available at the source after previously processing replicated transactions. The default is 60 seconds.  
  
 **-Output** _output_path_and_file_name_  
 Is the path of the agent output file. If the file name is not provided, the output is sent to the console. If the specified file name exists, the output is appended to the file.  
  
 **-OutputVerboseLevel** [ **0**| **1**| **2** | **3** | **4** ]  
 Specifies whether the output should be verbose.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Only error messages are printed.|  
|**1**|All agent progress report messages are printed.|  
|**2** (default)|All error messages and agent progress report messages are printed.|  
|**3**|The first 100 bytes of each replicated command are printed.|  
|**4**|All replicated commands are printed.|  
  
 Values 2-4 are useful when debugging.  
  
 **-PacketSize** _packet_size_  
 Is the packet size, in bytes. The default is 4096 (bytes).  
  
 **-PollingInterval** _polling_interval_  
 Is how often, in seconds, the log is queried for replicated transactions. The default is 5 seconds.  
  
 **-ProfileName** _profile_name_  
 Specifies an agent profile to use for agent parameters. If **ProfileName** is NULL, the agent profile is disabled. If **ProfileName** is not specified, the default profile for the agent type is used. For information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
 **-PublisherFailoverPartner** _server_name_[**\\**_instance_name_]  
 Specifies the failover partner instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] participating in a database mirroring session with the publication database. For more information, see [Database Mirroring and Replication &#40;SQL Server&#41;](../../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md).  
  
 **-PublisherSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Publisher. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-PublisherLogin** _publisher_login_  
 Is the Publisher login name.  
  
 **-PublisherPassword** _publisher_password_  
 Is the Publisher password.  
  
 **-QueryTimeOut** _query_time_out_seconds_  
 Is the number of seconds before the query times out. The default is 1800 seconds.  
  
 **-ReadBatchSize** _number_of_transactions_  
 Is the maximum number of transactions read out of the transaction log of the publishing database per processing cycle, with a default of 500. The agent will continue to read transactions in batches until all transactions are read from the log. This parameter is not supported for Oracle Publishers.  
  
 **-ReadBatchThreshold** _number_of_commands_  
 Is the number of replication commands to be read from the transaction log before being issued to the Subscriber by the Distribution Agent. The default is 0. If this parameter is not specified, the Log Reader Agent will read to the end of the log or to the number specified in **-ReadBatchSize** (number of transactions).  
  
 **-RecoverFromDataErrors**  
 Specifies that the Log Reader Agent continue to run when it encounters errors in column data published from a non-SQL Server Publisher. By default, such errors cause the Log Reader Agent to fail. When you use **-RecoverFromDataErrors**, erroneous column data is replicated either as NULL or an appropriate nonnull value, and warning messages are logged to the [MSlogreader_history](../../../relational-databases/system-tables/mslogreader-history-transact-sql.md) table. This parameter is only supported for Oracle Publishers.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If you installed [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent to run under a local system account instead of under a domain user account (the default), the service can access only the local computer. If the Log Reader Agent that runs under [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent is configured to use Windows Authentication Mode when it logs in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the Log Reader Agent fails. The default setting is [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication. For information about changing security accounts, see [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 To start the Log Reader Agent, execute **logread.exe** from the command prompt. For information, see [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md).  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Added the **-ExtendedEventConfigFile** parameter.|  
  
## See Also  
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)  
  
  
