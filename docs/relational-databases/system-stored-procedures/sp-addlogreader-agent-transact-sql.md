---
title: "sp_addlogreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addlogreader_agent"
  - "sp_addlogreader_agent_TSQL"
helpviewer_keywords: 
  - "sp_addlogreader_agent"
ms.assetid: d83096b9-96ee-4789-bde0-940d4765b9ed
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addlogreader_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a Log Reader agent for a given database. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addlogreader_agent [ @job_login = ] 'job_login'  
        , [ @job_password = ] 'job_password'  
    [ , [ @job_name = ] 'job_name' ]  
    [ , [ @publisher_security_mode = ] publisher_security_mode ]  
    [ , [ @publisher_login = ] 'publisher_login' ]  
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @job_login = ] 'job_login'`
 Is the login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with a default value of NULL. This Windows account is always used for agent connections to the Distributor.  
  
> [!NOTE]
>  For non- [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this must be the same login specified in [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md).  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with a default value of NULL.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. For best security, login names and passwords should be supplied at runtime.  
  
`[ @job_name = ] 'job_name'`
 Is the name of an existing agent job. *job_name* is **sysname**, with a default value of NULL. This parameter is only specified when the agent is started using an existing job instead of a newly created job (the default).  
  
`[ @publisher_security_mode = ] publisher_security_mode`
 Is the security mode used by the agent when connecting to the Publisher. *publisher_security_mode* is **smallint**, with a default of **1**. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, and **1** specifies Windows Authentication. A value of **0** must be specified for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
`[ @publisher_login = ] 'publisher_login'`
 Is the login used when connecting to the Publisher. *publisher_login* is **sysname**, with a default of NULL. *publisher_login* must be specified when *publisher_security_mode* is **0**. If *publisher_login* is NULL and *publisher_security_mode* is **1**, then the Windows account specified in *job_login* will be used when connecting to the Publisher.  
  
`[ @publisher_password = ] 'publisher_password'`
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. For best security, login names and passwords should be supplied at runtime.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  You should not specify this parameter for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addlogreader_agent** is used in transactional replication.  
  
 You must execute **sp_addlogreader_agent** to add a Log Reader agent if you upgraded a database that was enabled for replication to this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before a publication was created that used the database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_addlogreader_agent**.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranPub](../../relational-databases/replication/codesnippet/tsql/sp-addlogreader-agent-tr_1.sql)]  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_changelogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
