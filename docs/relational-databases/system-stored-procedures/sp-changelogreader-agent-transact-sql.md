---
title: "sp_changelogreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/15/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changelogreader_agent"
  - "sp_changelogreader_agent_TSQL"
helpviewer_keywords: 
  - "sp_changelogreader_agent"
ms.assetid: 929b2fa7-1267-41d0-8b69-e9ab26a62c0f
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changelogreader_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  Changes security properties of a Log Reader agent. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
## Syntax  
  
```  
  
sp_changelogreader_agent [ [ @job_login = ] 'job_login' ]  
    [ , [ @job_password = ] 'job_password' ]  
    [ , [ @publisher_security_mode = ] publisher_security_mode ]  
    [ , [ @publisher_login = ] 'publisher_login' ]  
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@job_login**= ] **'***job_login***'**  
 Is the login for the account under which the agent runs. *job_login* is **nvarchar(257)**, with a default of NULL. On Azure SQL Database Managed Instance, use a SQL Server account. *This cannot be changed for a non-*[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *publisher.*  
  
 [ **@job_password**= ] **'***job_password***'**  
 Is the password for the account under which the agent runs. *job_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@publisher_security_mode**= ] *publisher_security_mode*  
 Is the security mode used by the agent when connecting to the Publisher. *publisher_security_mode* is **smallint**, with a default of NULL. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, and **1** specifies Windows Authentication.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 [ **@publisher_login**= ] **'***publisher_login***'**  
 Is the login used when connecting to the Publisher. *publisher_login* is **sysname**, with a default of NULL. *publisher_login* must be specified when *publisher_security_mode* is **0**. If *publisher_login* is NULL and *publisher_security_mode* is **1**, then the Windows account specified in *job_login* is used when connecting to the Publisher.  
  
 [ **@publisher_password**= ] **'***publisher_password***'**  
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@publisher**= ] **'***publisher***'**  
 Is the name of the Publisher. *publisher* is **sysname**, with a default of NULL. This parameter is only supported for non-SQL Server Publishers.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changelogreader_agent** is used in transactional replication.  
  
 **sp_changelogreader_agent** is used to change the Windows account under which a Log Reader agent runs. You can change the password of an existing Windows login or supply a new Windows login and password.  
  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_changelogreader_agent**.  
  
## See Also  
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [sp_helplogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplogreader-agent-transact-sql.md)   
 [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md)  
  
  
