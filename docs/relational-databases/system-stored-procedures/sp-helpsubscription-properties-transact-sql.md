---
description: "sp_helpsubscription_properties (Transact-SQL)"
title: "sp_helpsubscription_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpsubscription_properties"
  - "sp_helpsubscription_properties_TSQL"
helpviewer_keywords: 
  - "sp_helpsubscription_properties"
ms.assetid: 7a76a645-97eb-47ac-b3ea-e2d75012cbed
author: markingmyname
ms.author: maghan
---
# sp_helpsubscription_properties (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Retrieves security information from the [MSsubscription_properties](../../relational-databases/system-tables/mssubscription-properties-transact-sql.md) table. This stored procedure is executed at the Subscriber.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpsubscription_properties [ [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db =] 'publisher_db' ]   
    [ , [ @publication =] 'publication' ]  
    [ , [ @publication_type = ] publication_type ]   
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default of **%**, which returns information on all Publishers.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with a default of **%**, which returns information on all Publisher databases.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**, which returns information on all publications.  
  
`[ @publication_type = ] publication_type`
 Is the type of publication.*publication_type* is **int**, with a default of NULL. If supplied, *publication_type* must be one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Transactional publication|  
|**1**|Snapshot publication|  
|**2**|Merge publication|  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|Name of the Publisher.|  
|**publisher_db**|**sysname**|Name of the Publisher database.|  
|**publication**|**sysname**|Name of the publication.|  
|**publication_type**|**int**|Type of publication:<br /><br /> **0** = Transactional<br /><br /> **1** = Snapshot<br /><br /> **2** = Merge|  
|**publisher_login**|**sysname**|Login ID used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**publisher_password**|**nvarchar(524)**|Password used at the Publisher for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication (encrypted).|  
|**publisher_security_mode**|**int**|Security mode used at the Publisher:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication|  
|**distributor**|**sysname**|Name of the Distributor.|  
|**distributor_login**|**sysname**|Distributor login.|  
|**distributor_password**|**nvarchar(524)**|Distributor password (encrypted).|  
|**distributor_security_mode**|**int**|Security mode used at the Distributor:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication|  
|**ftp_address**|**sysname**|For backward compatibility only. Network address of the file transfer protocol (FTP) service for the Distributor.|  
|**ftp_port**|**int**|For backward compatibility only. Port number of the FTP service for the Distributor.|  
|**ftp_login**|**sysname**|For backward compatibility only. User name used to connect to the FTP service.|  
|**ftp_password**|**nvarchar(524)**|For backward compatibility only. User password used to connect to the FTP service.|  
|**alt_snapshot_folder**|**nvarchar(255)**|Specifies the location of the alternate folder for the snapshot.|  
|**working_directory**|**nvarchar(255)**|Name of the working directory used to store data and schema files.|  
|**use_ftp**|**bit**|Specifies the use of FTP instead of the regular protocol to retrieve snapshots. If **1**, FTP is used.|  
|**dts_package_name**|**sysame**|Specifies the name of the Data Transformation Services (DTS) package.|  
|**dts_package_password**|**nvarchar(524)**|Specifies the password on the package, if there is one.|  
|**dts_package_location**|**int**|Location where the DTS package is stored.<br /><br /> **0** = the package location is at the Distributor.<br /><br /> **1** = the package location is at the Subscriber.|  
|**offload_agent**|**bit**|Specifies if the agent can be activated remotely. If **0**, the agent cannot be activated remotely.|  
|**offload_server**|**sysname**|Specifies the network name of the server used for remote activation.|  
|**dynamic_snapshot_location**|**nvarchar(255)**|Specifies the path to the folder where the snapshot files are saved.|  
|**use_web_sync**|**bit**|Specifies if the subscription can be synchronized over HTTPS, where a value of **1** means that this feature is enabled.|  
|**internet_url**|**nvarchar(260)**|URL that represents the location of the replication listener for Web synchronization.|  
|**internet_login**|**nvarchar(128)**|Login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication.|  
|**internet_password**|**nvarchar(524)**|Password for the login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication.|  
|**internet_security_mode**|**int**|The authentication mode used when connecting to the Web server that is hosting Web synchronization, where a value of **1** means Windows Authentication, and a value of **0** means Basic Authentication.|  
|**internet_timeout**|**int**|Length of time, in seconds, before a Web synchronization request expires.|  
|**hostname**|**nvarchar(128)**|Specifies the value for HOST_NAME() when this function is used in the WHERE clause parameterized row filter.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpsubscription_properties** is used in snapshot replication, transactional replication, and merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_helpsubscription_properties**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
