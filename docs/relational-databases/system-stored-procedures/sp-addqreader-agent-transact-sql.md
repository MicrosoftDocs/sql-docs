---
title: "sp_addqreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addqreader_agent_TSQL"
  - "sp_addqreader_agent"
helpviewer_keywords: 
  - "sp_addqreader_agent"
ms.assetid: dc9f591a-e67e-4ba8-bf47-defd5eda0822
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addqreader_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a Queue Reader agent for a given Distributor. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addqreader_agent [ @job_login = ] 'job_login'  
        , [ @job_password = ] 'job_password'  
    [ , [ @job_name = ] 'job_name'  
    [ , [ @frompublisher = ] frompublisher   
```  
  
## Arguments  
 [ **@job_login**= ] **'***job_login***'**  
 Is the login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Distributor.  
  
 [ **@job_password**= ] **'***job_password***'**  
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. For best security, login names and passwords should be supplied at runtime.  
  
 [ **@job_name**= ] **'***job_name***'**  
 Is the name of an existing agent job. *job_name* is **sysname**, with a default value of NULL. This parameter is only specified when the agent is created using an existing job instead of a newly created job (the default).  
  
 [ **@frompublisher=** ] *frompublisher*  
 Specifies whether the procedure is being executed at the Publisher. *frompublisher* is bit, with a default value of **0**. A value of **1** means that the procedure is being executed from the Publisher on the publication database.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_addqreader_agent** is used in transactional replication.  
  
 **sp_addqreader_agent** must be executed at least once at a Distributor that supports queued updating after [sp_adddistributiondb](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md) but before [sp_addpublication](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md).  
  
 The Queue Reader Agent job is removed when you execute [sp_dropdistributiondb](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_addqreader_agent**.  
  
## See Also  
 [Enable Updating Subscriptions for Transactional Publications](../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md)   
 [Upgrade Replication Scripts &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/upgrade-replication-scripts-replication-transact-sql-programming.md)   
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
 [sp_changeqreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changeqreader-agent-transact-sql.md)   
 [sp_helpqreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpqreader-agent-transact-sql.md)  
  
  
