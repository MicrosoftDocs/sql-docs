---
description: "sp_changeqreader_agent (Transact-SQL)"
title: "sp_changeqreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changeqreader_agent_TSQL"
  - "sp_changeqreader_agent"
helpviewer_keywords: 
  - "sp_changeqreader_agent"
ms.assetid: d3fe79c5-31ef-4565-bf38-b476b5fb16f7
author: markingmyname
ms.author: maghan
---
# sp_changeqreader_agent (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes security properties of a Queue Reader agent. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changeqreader_agent [ [ @job_login = ] 'job_login' ]  
    [ , [ @job_password = ] 'job_password' ]  
    [ , [ @frompublisher = ] frompublisher   
```  
  
## Arguments  
`[ @job_login = ] 'job_login'`
 Is the login for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with a default of NULL.  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with a default of NULL.  
  
`[ @frompublisher = ] frompublisher`
 Is if the procedure is being executed at the Publisher. *frompublisher* is bit, with a default value of **0**. A value of **1** means that the procedure is being executed from the Publisher on the publication database.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changeqreader_agent** is used in transactional replication.  
  
 **sp_changeqreader_agent** is used to change the Windows account under which a Queue Reader agent runs. You can change the password of an existing Windows login or supply a new Windows login and password.  
  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changeqreader_agent**.  
  
## See Also  
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [sp_addqreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addqreader-agent-transact-sql.md)  
  
  
