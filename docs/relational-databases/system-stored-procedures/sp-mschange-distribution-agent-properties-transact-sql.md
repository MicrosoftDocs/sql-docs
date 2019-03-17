---
title: "sp_MSchange_distribution_agent_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_MSchange_distribution_agent_properties"
  - "sp_MSchange_distribution_agent_properties_TSQL"
helpviewer_keywords: 
  - "sp_MSchange_distribution_agent_properties"
ms.assetid: 7dac5e68-bf84-433a-a531-66921f35126f
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_MSchange_distribution_agent_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a Distribution Agent job that runs at a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later version Distributor. This stored procedure is used to change properties when the Publisher runs on an instance of [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. This stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_MSchange_distribution_agent_properties [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'   
        , [ @subscriber = ] 'subscriber'   
        , [ @subscriber_db = ] 'subscriber_db'   
        , [ @property = ] 'property'   
        , [ @value = ] 'value' ]  
```  
  
## Arguments  
 [ **@publisher** = ] **'***publisher***'**  
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
 [ **@publisher_db=** ] **'***publisher_db***'**  
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
 [ **@publication =** ] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@subscriber=** ] **'***subscriber***'**  
 Is the name of the Subscriber. *subscriber* is **sysname**, with no default.  
  
 [ **@subscriber_db=** ] **'***subscriber_db***'**  
 Is the name of the subscription database. *subscriber_db* is **sysname**, with no default.  
  
 [ **@property =** ] **'***property***'**  
 Is the publication property to change. *property* is **sysname**, with no default.  
  
 [ **@value =** ] **'***value***'**  
 Is the new property value. *value* is **nvarchar(524)**, with a default of NULL.  
  
 This table describes the properties of the Distribution Agent job that can be changed, and restrictions on the values for those properties.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**distrib_job_login**||Login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs.|  
|**distrib_job_password**||Password for the Windows account under which the agent job runs.|  
|**subscriber_catalog**||Catalog to be used when making a connection to the OLE DB provider. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_datasource**||Name of the data source as understood by the OLE DB provider. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_location**||Location of the database as understood by the OLE DB provider. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_login**||Login to use when connecting to a Subscriber to synchronize the subscription.|  
|**subscriber_password**||Subscriber password.<br /><br /> [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]|  
|**subscriber_provider**||Unique programmatic identifier (PROGID) with which the OLE DB provider for the non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source is registered. *This property is only valid for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers.*|  
|**subscriber_providerstring**||OLE DB provider-specific connection string that identifies the data source. *This property is only valid for non-SQL Server Subscribers.*|  
|**subscriber_security_mode**|**1**|Windows Authentication.<br /><br /> [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]|  
||**0**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**subscriber_type**|**0**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber|  
||**1**|ODBC data source server|  
||**3**|OLE DB provider|  
|**subscriptionstreams**||Denotes the number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber. *Not supported for non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Subscribers, Oracle Publishers, or peer-to-peer subscriptions.*|  
  
> [!NOTE]  
>  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_MSchange_distribution_agent_properties** is used in snapshot replication and transactional replication.  
  
 When the Publisher runs on an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later version, you should use [sp_changesubscription](../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md) to change properties of a Merge Agent job that synchronizes a push subscription that runs at the Distributor.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor can execute **sp_MSchange_distribution_agent_properties**.  
  
## See Also  
 [sp_addpushsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md)   
 [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md)  
  
  
