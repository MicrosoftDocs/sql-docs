---
description: "sp_changesubscription (Transact-SQL)"
title: "sp_changesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/28/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "changesubscription"
  - "sp_changesubscription"
  - "changesubscription_TSQL"
  - "sp_changesubscription_TSQL"
helpviewer_keywords: 
  - "sp_changesubscription"
ms.assetid: f9d91fe3-47cf-4915-b6bf-14c9c3d8a029
author: markingmyname
ms.author: maghan
---
# sp_changesubscription (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the properties of a snapshot or transactional push subscription or a pull subscription involved in queued updating transactional replication. To change properties of all other types of pull subscriptions, use [sp_change_subscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md). **sp_changesubscription** is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changesubscription [ @publication = ] 'publication'  
        , [ @article = ] 'article'  
        , [ @subscriber = ] 'subscriber'  
        , [ @destination_db = ] 'destination_db'  
        , [ @property = ] 'property'  
        , [ @value = ] 'value'  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to change. *publication*is **sysname**, with no default  
  
`[ @article = ] 'article'`
 Is the name of the article to change. *article* is **sysname**, with no default.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with no default.  
  
`[ @destination_db = ] 'destination_db'`
 Is the name of the subscription database. *destination_db* is **sysname**, with no default.  
  
`[ @property = ] 'property'`
 Is the property to change for the given subscription. *property* is **nvarchar(30)**, and can be one of the values in the table.  
  
`[ @value = ] 'value'`
 Is the new value for the specified *property*. *value* is **nvarchar(4000)**, and can be one of the values in the table.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**distrib_job_login**||Login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs.|  
|**distrib_job_password**||Password for the Windows account under which the agent runs.|  
|**subscriber_catalog**||Catalog to be used when making a connection to the OLE DB provider. This property is only valid for non-[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.|  
|**subscriber_datasource**||Name of the data source as understood by the OLE DB provider. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_location**||Location of the database as understood by the OLE DB provider. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_login**||Login name at the Subscriber.|  
|**subscriber_password**||Strong password for the supplied login.|  
|**subscriber_security_mode**|**1**|Use Windows Authentication when connecting to the Subscriber.|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Subscriber.|  
|**subscriber_provider**||Unique programmatic identifier (PROGID) with which the OLE DB provider for the non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source is registered. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_providerstring**||OLE DB provider-specific connection string that identifies the data source. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriptionstreams**||Is the number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber. A range of values from **1** to **64** is supported for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. This property must be **0** for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, Oracle Publishers or peer-to-peer subscriptions.|  
|**subscriber_type**|**1**|ODBC data source server|  
||**3**|OLE DB provider|  
|**memory_optimized**|**bit**|Indicates that  the subscription supports memory optimized tables. *memory_optimized* is **bit**, where 1 equals true (the subscription supports memory optimized tables).|  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changesubscription** is used in snapshot and transactional replication.  
  
 **sp_changesubscription** can only be used to modify the properties of push subscriptions or pull subscriptions involved in queued updating transactional replication. To change properties of all other types of pull subscriptions, use [sp_change_subscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md).  
  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changesubscription**.  
  
## See Also  
 [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md)   
 [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md)  
  
  
