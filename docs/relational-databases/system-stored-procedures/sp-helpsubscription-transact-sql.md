---
title: "sp_helpsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpsubscription_TSQL"
  - "sp_helpsubscription"
helpviewer_keywords: 
  - "sp_helpsubscription"
ms.assetid: ff96bcbf-e2b9-4da8-8515-d80d4ce86c16
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists subscription information associated with a particular publication, article, Subscriber, or set of subscriptions. This stored procedure is executed at a Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpsubscription [ [ @publication = ] 'publication' ]   
    [ , [ @article = ] 'article' ]  
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @destination_db = ] 'destination_db' ]   
    [ , [ @found=] found OUTPUT ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication =** ] **'**_publication_**'**  
 Is the name of the associated publication. *publication* is **sysname**, with a default of **%**, which returns all subscription information for this server.  
  
 [ **@article=** ] **'**_article_**'**  
 Is the name of the article. *article* is **sysname**, with a default of **%**, which returns all subscription information for the selected publications and Subscribers. If **all**, only one entry is returned for the full subscription on a publication.  
  
 [ **@subscriber=** ] **'**_subscriber_**'**  
 Is the name of the Subscriber on which to obtain subscription information. *subscriber* is **sysname**, with a default of **%**, which returns all subscription information for the selected publications and articles.  
  
 [ **@destination_db=** ] **'**_destination_db_**'**  
 Is the name of the destination database. *destination_db* is **sysname**, with a default of **%**.  
  
 [ **@found=** ] **'**_found_**'**OUTPUT  
 Is a flag to indicate returning rows. *found*is **int** and an OUTPUT parameter, with a default of 23456.  
  
 **1** indicates the publication is found.  
  
 **0** indicates the publication is not found.  
  
 [ **@publisher**= ] **'**_publisher_**'**  
 Is the name of the Publisher. *publisher* is **sysname**, and defaults to the name of the current server.  
  
> [!NOTE]  
>  *publisher* should not be specified, except when it is an Oracle Publisher.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subscriber**|**sysname**|Name of the Subscriber.|  
|**publication**|**sysname**|Name of the publication.|  
|**article**|**sysname**|Name of the article.|  
|**destination database**|**sysname**|Name of the destination database in which replicated data is placed.|  
|**subscription status**|**tinyint**|Subscription status:<br /><br /> **0** = Inactive<br /><br /> **1** = Subscribed<br /><br /> **2** = Active|  
|**synchronization type**|**tinyint**|Subscription synchronization type:<br /><br /> **1** = Automatic<br /><br /> **2** = None|  
|**subscription type**|**int**|Type of subscription:<br /><br /> **0** = Push<br /><br /> **1** = Pull<br /><br /> **2** = Anonymous|  
|**full subscription**|**bit**|Whether subscription is to all articles in the publication:<br /><br /> **0** = No<br /><br /> **1** = Yes|  
|**subscription name**|**nvarchar(255)**|Name of the subscription.|  
|**update mode**|**int**|**0** = Read-only<br /><br /> **1** = Immediate-updating subscription|  
|**distribution job id**|**binary(16)**|Job ID of the Distribution Agent.|  
|**loopback_detection**|**bit**|Loopback detection determines whether the Distribution Agent sends transactions originated at the Subscriber back to the Subscriber:<br /><br /> **0** = Sends back.<br /><br /> **1** = Does not send back.<br /><br /> Used with bidirectional transactional replication. For more information, see [Bidirectional Transactional Replication](../../relational-databases/replication/transactional/bidirectional-transactional-replication.md).|  
|**offload_enabled**|**bit**|Specifies whether offload execution of a replication agent has been set to run at the Subscriber.<br /><br /> If **0**, agent is run at the Publisher.<br /><br /> If **1**, agent is run at the Subscriber.|  
|**offload_server**|**sysname**|Name of the server enabled for remote agent activation. If NULL, then the current offload_server listed in [MSdistribution_agents](../../relational-databases/system-tables/msdistribution-agents-transact-sql.md) table is used.|  
|**dts_package_name**|**sysname**|Specifies the name of the Data Transformation Services (DTS) package.|  
|**dts_package_location**|**int**|Location of the DTS package, if one is assigned to the subscription. If there is a package, a value of **0** specifies the package location at the **distributor**. A value of **1** specifies the **subscriber**.|  
|**subscriber_security_mode**|**smallint**|Is the security mode at the Subscriber, where **1** means Windows Authentication, and **0** means [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**subscriber_login**|**sysname**|Is the login name at the Subscriber.|  
|**subscriber_password**||Actual Subscriber password is never returned. The result is masked by a "**&#42;&#42;&#42;&#42;&#42;&#42;**" string.|  
|**job_login**|**sysname**|Name of the Windows account under which the Distribution Agent runs.|  
|**job_password**||Actual job password is never returned. The result is masked by a "**&#42;&#42;&#42;&#42;&#42;&#42;**" string.|  
|**distrib_agent_name**|**nvarchar(100)**|Name of the agent job that synchronizes the subscription.|  
|**subscriber_type**|**tinyint**|Type of Subscriber, which can be one of the following:<br /><br /> **0** = SQL Server Subscriber<br /><br /> **1** = ODBC data source server<br /><br /> **2** = Microsoft JET database (deprecated)<br /><br /> **3** = OLE DB provider|  
|**subscriber_provider**|**sysname**|Unique programmatic identifier (PROGID) with which the OLE DB provider for the non-SQL Server data source is registered.|  
|**subscriber_datasource**|**nvarchar(4000)**|Name of the data source as understood by the OLE DB provider.|  
|**subscriber_providerstring**|**nvarchar(4000)**|OLE DB provider-specific connection string that identifies the data source.|  
|**subscriber_location**|**nvarchar(4000)**|Location of the database as understood by the OLE DB provider|  
|**subscriber_catalog**|**sysname**|Catalog to be used when making a connection to the OLE DB provider.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpsubscription** is used in snapshot and transactional replication.  
  
## Permissions  
 Execute permissions default to the **public** role. Users are only returned information for subscriptions that they created. Information on all subscriptions is returned to members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database.  
  
## See Also  
 [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md)   
 [sp_changesubstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubstatus-transact-sql.md)   
 [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
