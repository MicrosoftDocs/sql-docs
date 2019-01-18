---
title: "MSsubscription_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSsubscription_properties"
  - "MSsubscription_properties_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSsubscription_properties system table"
ms.assetid: f96fc1ae-b798-4b05-82a7-564ae6ef23b8
author: stevestein
ms.author: sstein
manager: craigg
---
# MSsubscription_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSsubscription_properties** table contains rows for the parameter information required to run replication agents at the Subscriber. This table is stored in the subscription database at the Subscriber for a pull subscription or in the distribution database at the Distributor for a push subscription.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**publication_type**|**int**|The type of publication:<br /><br /> **0** = Transactional.<br /><br /> **2** = Merge.|  
|**publisher_login**|**sysname**|The Login ID used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**publisher_password**|**nvarchar(524)**|The password (encrypted) used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**publisher_security_mode**|**int**|Security mode implemented at the Publisher:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server Authentication.<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.<br /><br /> **2** = The synchronization triggers use a static **sysservers** entry to do a remote procedure call (RPC), and *publisher* must be defined in the **sysservers** table as a remote server or linked server.|  
|**distributor**|**sysname**|The name of the Distributor.|  
|**distributor_login**|**sysname**|The login ID used at the Distributor for SQL Server Authentication.|  
|**distributor_password**|**nvarchar(524)**|The password (encrypted) used at the Distributor for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**distributor_security_mode**|**int**|The security mode implemented at the Distributor:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.<br /><br /> **1** = Windows Authentication.|  
|**ftp_address**|**sysname**|The network address of the file transfer protocol (FTP) service for the Distributor.|  
|**ftp_port**|**int**|The port number of the FTP service for the Distributor.|  
|**ftp_login**|**sysname**|The username used to connect to the FTP service.|  
|**ftp_password**|**nvarchar(524)**|The u.User password used to connect to the FTP service.|  
|**alt_snapshot_folder**|**nvarchar(255)**|Specifies the location of the alternate folder for the snapshot.|  
|**working_directory**|**nvarchar(255)**|The name of the working directory used to store data and schema files.|  
|**use_ftp**|**bit**|Specifies the use of FTP instead of the regular protocol to retrieve snapshots. If **1**, FTP is used.|  
|**dts_package_name**|**sysname**|Specifies the name of the Data Transformation Services (DTS) package.|  
|**dts_package_password**|**nvarchar(524)**|Specifies the password on the package.|  
|**dts_package_location**|**int**|The location where the DTS package is stored.|  
|**enabled_for_syncmgr**|**bit**|Specifies whether the subscription can be synchronized through the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Synchronization Manager.<br /><br /> **0** = Subscription is not registered with Synchronization Manager.<br /><br /> **1** = Subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|  
|**offload_agent**|**bit**|Specifies whether the agent can be activated remotely or not. If **0**, the agent cannot be activated remotely.|  
|**offload_server**|**sysname**|Specifies the network name of the server used for remote activation.|  
|**dynamic_snapshot_location**|**nvarchar(255)**|Specifies the path to the folder where the snapshot files are saved.|  
|**use_web_sync**|**bit**|Specifies whether the subscription can be synchronized over HTTP or not. A value of **1** means that this feature is enabled.|  
|**internet_url**|**nvarchar(260)**|The URL that represents the location of the replication listener for Web synchronization.|  
|**internet_login**|**sysname**|The login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**internet_password**|**nvarchar(524)**|The password for the login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**internet_security_mode**|**int**|The authentication mode used when connecting to the Web server that is hosting Web synchronization, where a value of **1** means Windows Authentication, and a value of **0** means [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**internet_timeout**|**int**|The length of time, in seconds, before a Web synchronization request expires.|  
|**hostname**|**sysname**|Specifies the value for **HOST_NAME** when this function is used in the **WHERE** clause of a join filter or logical record relationship.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_helppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)   
 [sp_helpsubscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md)  
  
  
