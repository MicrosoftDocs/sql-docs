---
title: "sp_helppullsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helppullsubscription_TSQL"
  - "sp_helppullsubscription"
helpviewer_keywords: 
  - "sp_helppullsubscription"
ms.assetid: a0d9c3f1-1fe9-497c-8e2f-5b74f47a7346
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helppullsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information about one or more subscriptions at the Subscriber. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helppullsubscription [ [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publisher_db' ]   
    [ , [ @publication = ] 'publication' ]  
    [ , [ @show_push = ] 'show_push' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the remote server. *publisher* is **sysname**, with a default of **%**, which returns information for all Publishers.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with a default of **%**, which returns all the Publisher databases.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**, which returns all the publications. If this parameter equals to ALL, only pull subscriptions with independent_agent = **0** are returned.  
  
`[ @show_push = ] 'show_push'`
 Is whether all push subscriptions are to be returned. *show_push*is **nvarchar(5)**, with a default of FALSE, which does not return push subscriptions.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|Name of the Publisher.|  
|**publisher database**|**sysname**|Name of the Publisher database.|  
|**publication**|**sysname**|Name of the publication.|  
|**independent_agent**|**bit**|Indicates whether there is a stand-alone Distribution Agent for this publication.|  
|**subscription type**|**int**|Subscription type to the publication.|  
|**distribution agent**|**nvarchar(100)**|Distribution Agent handling the subscription.|  
|**publication description**|**nvarchar(255)**|Description of the publication.|  
|**last updating time**|**date**|Time the subscription information was updated. This is a UNICODE string of ISO date (114) + ODBC time (121). The format is yyyymmdd hh:mi:sss.mmm where 'yyyy' is year, 'mm' is month, 'dd' is day, 'hh' is hour, 'mi' is minute, 'sss' is seconds, and 'mmm' is milliseconds.|  
|**subscription name**|**varchar(386)**|Name of the subscription.|  
|**last transaction timestamp**|**varbinary(16)**|Timestamp of the last replicated transaction.|  
|**update mode**|**tinyint**|Type of updates allowed.|  
|**distribution agent job_id**|**int**|Job ID of the Distribution Agent.|  
|**enabled_for_synmgr**|**int**|Whether the subscription can be synchronized through the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Synchronization Manager.|  
|**subscription guid**|**binary(16)**|Global identifier for the version of the subscription on the publication.|  
|**subid**|**binary(16)**|Global identifier for an anonymous subscription.|  
|**immediate_sync**|**bit**|Whether the synchronization files are created or re-created each time the Snapshot Agent runs.|  
|**publisher login**|**sysname**|Login ID used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**publisher password**|**nvarchar(524)**|Password (encrypted) used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**publisher security_mode**|**int**|Security mode implemented at the Publisher:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication<br /><br /> **2** = The synchronization triggers use a static **sysservers** entry to do remote procedure call (RPC), and *publisher* must be defined in the **sysservers** table as a remote server or linked server.|  
|**distributor**|**sysname**|Name of the Distributor.|  
|**distributor_login**|**sysname**|Login ID used at the Distributor for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**distributor_password**|**nvarchar(524)**|Password (encrypted) used at the Distributor for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**distributor_security_mode**|**int**|Security mode implemented at the Distributor:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication|  
|**ftp_address**|**sysname**|For backward compatibility only.|  
|**ftp_port**|**int**|For backward compatibility only.|  
|**ftp_login**|**sysname**|For backward compatibility only.|  
|**ftp_password**|**nvarchar(524)**|For backward compatibility only.|  
|**alt_snapshot_folder**|**nvarchar(255)**|Location where snapshot folder is stored if the location is other than or in addition to the default location.|  
|**working_directory**|**nvarchar(255)**|Fully qualified path to the directory where snapshot files are transferred using File Transfer Protocol (FTP) when that option is specified.|  
|**use_ftp**|**bit**|Subscription is subscribing to Publication over the Internet and FTP addressing properties are configured. If **0**, Subscription is not using FTP. If **1**, subscription is using FTP.|  
|**publication_type**|**int**|Specifies the replication type of the publication:<br /><br /> **0** = Transactional replication<br /><br /> **1** = Snapshot replication<br /><br /> **2** = Merge replication|  
|**dts_package_name**|**sysname**|Specifies the name of the Data Transformation Services (DTS) package.|  
|**dts_package_location**|**int**|Location where the DTS package is stored:<br /><br /> **0** = Distributor<br /><br /> **1** = Subscriber|  
|**offload_agent**|**bit**|Specifies if the agent can be activated remotely. If **0**, the agent cannot be activated remotely.|  
|**offload_server**|**sysname**|Specifies the network name of the server used for remote activation.|  
|**last_sync_status**|**int**|Subscription status:<br /><br /> **0** = All jobs are waiting to start<br /><br /> **1** = One or more jobs are starting<br /><br /> **2** = All jobs have executed successfully<br /><br /> **3** = At least one job is executing<br /><br /> **4** = All jobs are scheduled and idle<br /><br /> **5** = At least one job is attempting to execute after a previous failure<br /><br /> **6** = At least one job has failed to execute successfully|  
|**last_sync_summary**|**sysname**|Description of last synchronization results.|  
|**last_sync_time**|**datetime**|Time the subscription information was updated. This is a UNICODE string of ISO date (114) + ODBC time (121). The format is yyyymmdd hh:mi:sss.mmm where 'yyyy' is year, 'mm' is month, 'dd' is day, 'hh' is hour, 'mi' is minute, 'sss' is seconds, and 'mmm' is milliseconds.|  
|**job_login**|**nvarchar(512)**|Is the Windows account under which the Distribution agent runs, which is returned in the format *domain*\\*username*.|  
|**job_password**|**sysname**|For security reasons, a value of "**\*\*\*\*\*\*\*\*\*\***" is always returned.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helppullsubscription** is used in snapshot and transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_helppullsubscription** .  
  
## See Also  
 [sp_addpullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md)   
 [sp_droppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppullsubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
