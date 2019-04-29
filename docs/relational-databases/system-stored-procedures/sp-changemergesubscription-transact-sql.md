---
title: "sp_changemergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changemergesubscription_TSQL"
  - "sp_changemergesubscription"
helpviewer_keywords: 
  - "sp_changemergesubscription"
ms.assetid: fd820f35-c189-4e2d-884d-b60c1c469f58
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changemergesubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes selected properties of a merge push subscription. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
## Syntax  
  
```  
  
sp_changemergesubscription [ [ @publication= ] 'publication' ]  
    [ , [ @subscriber= ] 'subscriber'  
    [ , [ @subscriber_db= ] 'subscriber_db' ]  
    [ , [ @property= ] 'property' ]  
    [ , [ @value= ] 'value' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to change. *publication* is **sysname**, with a default of NULL. The publication must already exist and must conform to the rules for identifiers.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscriber_db*is **sysname**, with a default of NULL.  
  
`[ @property = ] 'property'`
 Is the property to change for the given publication. *property* is **sysname**, and can be one of the values in the table.  
  
`[ @value = ] 'value'`
 Is the new value for the specified *property*. *value* is **nvarchar(255)**, and can be one of the values in the table.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**description**||Description of this merge subscription.|  
|**priority**||Is the subscription priority. The priority is used by the default resolver to pick a winner when conflicts are detected.|  
|**merge_job_login**||Login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs.|  
|**merge_job_password**||Password for the Windows account under which the agent runs.|  
|**publisher_security_mode**|**1**|Use Windows Authentication when connecting to the Publisher.|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher.|  
|**publisher_login**||Login name at the Publisher.|  
|**publisher_password**||Strong password for the supplied Publisher login.|  
|**subscriber_security_mode**|**1**|Use Windows Authentication when connecting to the Subscriber.|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Subscriber.|  
|**subscriber_login**||Login name at the Subscriber.|  
|**subscriber_password**||Strong password for the supplied Subscriber login.|  
|**sync_type**|**automatic**|Schema and initial data for published tables are transferred to the Subscriber first.|  
||**none**|Subscriber already has the schema and initial data for published tables; system tables and data are always transferred.|  
|**use_interactive_resolver**|**true**|Allows conflicts to be resolved interactively for all articles that allow interactive resolution.|  
||**false**|Conflicts are resolved automatically using a default resolver or custom resolver.|  
|NULL (default)|NULL (default)||  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changemergesubscription** is used in merge replication.  
  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changemergesubscription**.  
  
## See Also  
 [sp_addmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md)   
 [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md)   
 [sp_helpmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
