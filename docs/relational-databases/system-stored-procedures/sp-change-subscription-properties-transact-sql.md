---
title: "sp_change_subscription_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_change_subscription_properties_TSQL"
  - "sp_change_subscription_properties"
helpviewer_keywords: 
  - "sp_change_subscription_properties"
ms.assetid: cf8137f9-f346-4aa1-ae35-91a2d3c16f17
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_change_subscription_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Updates information for pull subscriptions. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_subscription_properties [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'  
        , [ @property = ] 'property'  
        , [ @value = ] 'value'  
    [ , [ @publication_type = ] publication_type ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @property = ] 'property'`
 Is the property to be changed. *property* is **sysname**.  
  
`[ @value = ] 'value'`
 Is the new value of the property. *value* is **nvarchar(1000)**, with no default.  
  
`[ @publication_type = ] publication_type`
 Specifies the replication type of the publication. *publication_type* is **int**, and can be one of these values.  
  
|Value|Publication type|  
|-----------|----------------------|  
|**0**|Transactional|  
|**1**|Snapshot|  
|**2**|Merge|  
|NULL (default)|Replication determines the publication type. Because the stored procedure must look through multiple tables, this option is slower than when the exact publication type is provided.|  
  
 This table describes the properties of articles and the values for those properties.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**alt_snapshot_folder**||Specifies the location of the alternate folder for the snapshot. If set to NULL, the snapshot files are picked up from the default location specified by the Publisher.|  
|**distrib_job_login**||Login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs.|  
|**distrib_job_password**||Password for the Windows account under which the agent runs.|  
|**distributor_login**||Distributor login.|  
|**distributor_password**||Distributor password.|  
|**distributor_security_mode**|**1**|Use Windows Authentication when connecting to the Distributor.|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Distributor.|  
|**dts_package_name**||Specifies the name of the SQL Server 2000 Data Transformation Services (DTS) package. This value can be specified only if the publication is transactional or snapshot.|  
|**dts_package_password**||Specifies the password on the package. *dts_package_password* is **sysname** with a default of NULL, which specifies that the password property is to be left unchanged.<br /><br /> Note: A DTS package must have a password.<br /><br /> This value can be specified only if the publication is transactional or snapshot.|  
|**dts_package_location**||Location where the DTS package is stored. This value can be specified only if the publication is transactional or snapshot.|  
|**dynamic_snapshot_location**||Specifies the path to the folder where the snapshot files are saved. This value can be specified only if the publication is a merge publication.|  
|**ftp_address**||For backward compatibility only.|  
|**ftp_login**||For backward compatibility only.|  
|**ftp_password**||For backward compatibility only.|  
|**ftp_port**||For backward compatibility only.|  
|**hostname**||Host name used when connecting to the Publisher.|  
|**internet_login**||Login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication.|  
|**internet_password**||Password that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication.|  
|**internet_security_mode**|**1**|Use Windows Integrated Authentication for Web synchronization. We recommend using Basic Authentication with Web synchronization. For more information, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).|  
||**0**|Use Basic Authentication for Web synchronization.<br /><br /> Note: Web synchronization requires an SSL connection to the Web server.|  
|**internet_timeout**||Length of time, in seconds, before a Web synchronization request expires.|  
|**internet_url**||URL that represents the location of the replication listener for Web synchronization.|  
|**merge_job_login**||Login for the Windows account under which the agent runs.|  
|**merge_job_password**||Password for the Windows account under which the agent runs.|  
|**publisher_login**||Publisher login. Changing *publisher_login* is only supported for subscriptions to merge publications.|  
|**publisher_password**||Publisher password. Changing *publisher_password* is only supported for subscriptions to merge publications.|  
|**publisher_security_mode**|**1**|Use Windows Authentication when connecting to the Publisher. Changing *publisher_security_mode* is only supported for subscriptions to merge publications.|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher.|  
|**use_ftp**|**true**|Use FTP instead of the regular protocol to retrieve snapshots.|  
||**false**|Use the regular protocol to retrieve snapshots.|  
|**use_web_sync**|**true**|Enable Web synchronization.|  
||**false**|Disable Web synchronization.|  
|**working_directory**||Name of the working directory used to temporarily store data and schema files for the publication when File Transfer Protocol (FTP) is used to transfer snapshot files.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_change_subscription_properties** is used in all types of replication.  
  
 **sp_change_subscription_properties** is used for pull subscriptions.  
  
 For Oracle Publishers, the value of *publisher_db* is ignored since Oracle only allows one database per instance of the server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_change_subscription_properties**.  
  
## See Also  
 [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [sp_addmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md)   
 [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md)   
 [sp_addpullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md)   
 [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
