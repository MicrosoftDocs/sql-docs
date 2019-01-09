---
title: "Replication Queue Reader Agent | Microsoft Docs"
ms.custom: ""
ms.date: "10/29/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "agents [SQL Server replication], Queue Reader Agent"
  - "command prompt [SQL Server replication]"
  - "Queue Reader Agent, parameter reference"
  - "Queue Reader Agent, executables"
ms.assetid: 8e227793-11f6-47c6-99dc-ffc282f5d4bf
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replication Queue Reader Agent
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Replication Queue Reader Agent is an executable that reads messages stored in a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] queue or a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Message Queue and then applies those messages to the Publisher. Queue Reader Agent is used with snapshot and transactional publications that allow queued updating.  
  
> [!NOTE]  
>  Parameters can be specified in any order. When optional parameters are not specified, predefined values based on the default agent profile are used.  
  
## Syntax  
  
```  
  
qrdrsvc [-?]  
[-Continuous]  
[-DefinitionFile definition_file]  
[-Distributor server_name[\instance_name]]  
[-DistributionDB distribution_database]  
[-DistributorLogin distributor_login]  
[-DistributorPassword distributor_password]  
[-DistributorSecurityMode [0|1]]  
[-EncryptionLevel [0|1|2]]  
[-HistoryVerboseLevel [0|1|2|3]]  
[-LoginTimeOut login_time_out_seconds]  
[-Output output_path_and_file_name]  
[-OutputVerboseLevel [0|1|2]]  
[-PollingInterval polling_interval]  
[-PublisherFailoverPartner server_name[\instance_name] ]  
[-ProfileName agent_profile_name]  
[-QueryTimeOut query_time_out_seconds]  
[-ResolverState [1|2|3]]  
```  
  
## Arguments  
 **-?**  
 Displays usage information.  
  
 **-Continuous**  
 Specifies whether the agent attempts to process queued transactions continuously. If specified, the agent continues execution even if there are no queued transactions pending from any of the subscribers.  
  
 **-DefinitionFile** _def_path_and_file_name_  
 Is the path of the agent definition file. An agent definition file contains command-line arguments for the agent. The content of the file is parsed as an executable file. Use double quotation marks (") to specify argument values containing arbitrary characters.  
  
 **-Distributor** _server_name_[**\\**_instance_name_]  
 Is the Distributor name. Specify *server_name* for the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. Specify *server_name*\\*instance_name* for a named instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that server. If not specified, the name defaults to the name of the default instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on the local computer.  
  
 **-DistributionDB** _distribution_database_  
 Is the distribution database.  
  
 **-DistributorLogin** _distributor_login_  
 Is the Distributor login name.  
  
 **-DistributorPassword** _distributor_password_  
 Is the Distributor password.  
  
 **-DistributorSecurityMode** [ **0**| **1**]  
 Specifies the security mode of the Distributor. A value of **0** indicates [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication Mode (default), and a value of **1** indicates Windows Authentication Mode.  
  
 **-EncryptionLevel** [ **0** | **1** | **2** ]  
 Is the level of Secure Sockets Layer (SSL) encryption used by the Queue Reader Agent when making connections.  
  
|EncryptionLevel value|Description|  
|---------------------------|-----------------|  
|**0**|Specifies that SSL is not used.|  
|**1**|Specifies that SSL is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer.|  
|**2**|Specifies that SSL is used, and that the certificate is verified.|  

 > [!NOTE]  
 >  A valid SSL certificate is defined with a fully qualified domain name of the SQL Server. In order for the agent to connect successfully when setting -EncryptionLevel to 2, create an alias on the local SQL Server. The ‘Alias Name’ parameter should be the server name and the ‘Server’ parameter should be set to the fully qualified name of the SQL Server.
  
 For more information, see [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 **-HistoryVerboseLevel** [ **0**| **1**| **2**| **3**]  
 Specifies the amount of history logged during a queue reader operation. You can minimize the effect of history logging on performance by selecting **1**.  
  
|HistoryVerboseLevel value|Description|  
|-------------------------------|-----------------|  
|**0**|No history logging (not recommended).|  
|**1**|Default. Always update a previous history message of the same status (startup, progress, success, and so on). If no previous record with the same status exists, insert a new record.|  
|**2**|Insert new history records, including idle messages or long-running job messages.|  
|**3**|Insert new history records that include additional details that may be useful for troubleshooting.|  
  
 **-LoginTimeOut** _login_time_out_seconds_  
 Is the number of seconds before the login times out. The default is 15 seconds.  
  
 **-Output** _output_path_and_file_name_  
 Is the path of the agent output file. If the file name is not provided, the output is sent to the console. If the specified file name exists, the output is appended to the file.  
  
 **-OutputVerboseLevel** [ **0**| **1**| **2**]  
 Specifies whether the output should be verbose. If the verbose level is **0**, only error messages are printed. If the verbose level is **1**, all the progress report messages are printed. If the verbose level is **2** (default), all error messages and progress report messages are printed, which is useful for debugging.  
  
 **-PollingInterval** _polling_interval_  
 Is relevant only for updating subscriptions that use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] based queues. Specifies how often, in seconds, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] queue is polled for pending queued transactions. The value can be between 0 and 240 seconds. The default is 5 seconds.  
  
 **-PublisherFailoverPartner** _server_name_[**\\**_instance_name_]  
 Specifies the failover partner instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] participating in a database mirroring session with the publication database. For more information, see [Database Mirroring and Replication &#40;SQL Server&#41;](../../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md).  
  
 **-ProfileName** _agent_profile_name_  
 Is the name of an agent profile used to supply a set of default values to the agent. For information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
 **-QueryTimeOut** _query_time_out_seconds_  
 Is the number of seconds before the query times out. The default is 1800 seconds.  
  
 **-ResolverState** [ **1**| **2**| **3**]  
 Specifies how queued updating conflicts are resolved. A value of **1** indicates the Publisher wins the conflict, and the current conflicting queued transaction will be rolled back on the Publisher and the originating updating Subscriber; the processing of subsequent queued transactions will continue. A value of **2** indicates the Subscriber wins the conflict, and the queued transaction will override the values on the Publisher. A value of **3** indicates that any conflict will result in Subscriber re-initialization; the Publisher wins the conflict, processing of subsequent queued transactions will be terminated, and the subscription will be reinitialized. The default setting is **1** for transactional publications and **3** for snapshot publications.  
  
## Remarks  
 To start the Queue Reader Agent, execute **qrdrsvc.exe** from the command prompt. For information, see [Replication Agent Executables](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md).  
  
## See Also  
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)  
  
  
