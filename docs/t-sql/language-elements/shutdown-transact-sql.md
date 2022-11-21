---
title: "SHUTDOWN (Transact-SQL)"
description: "SHUTDOWN (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SHUTDOWN_TSQL"
  - "SHUTDOWN"
helpviewer_keywords:
  - "SQL Server, stopping"
  - "shutting down SQL Server"
  - "SHUTDOWN statement"
  - "stopping SQL Server"
  - "immediately stopping SQL Server"
dev_langs:
  - "TSQL"
---
# SHUTDOWN (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Immediately stops SQL Server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SHUTDOWN [ WITH NOWAIT ]   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 WITH NOWAIT  
 Optional. Shuts down [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without performing checkpoints in every database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] exits after attempting to terminate all user processes. When the server restarts, a rollback operation occurs for uncompleted transactions.  
  
## Remarks  
 Unless the WITH NOWAIT option is used, SHUTDOWN shuts down [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by:  
  
1.  Disabling logins (except for members of the **sysadmin** and **serveradmin** fixed server roles).  
  
    > [!NOTE]  
    >  To display a list of all current users, run **sp_who**.  
  
2.  Waiting for currently running Transact-SQL statements or stored procedures to finish. To display a list of all active processes and locks, run **sp_who** and **sp_lock**, respectively.  
  
3.  Inserting a checkpoint in every database.  
  
 Using the SHUTDOWN statement minimizes the amount of automatic recovery work needed when members of the **sysadmin** fixed server role restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Other tools and methods can also be used to stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each of these issues a checkpoint in all databases. You can flush committed data from the data cache and stop the server:  
  
-   By using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   By running **net stop mssqlserver** from a command prompt for a default instance, or by running **net stop mssql$**_instancename_ from a command prompt for a named instance.  
  
-   By using Services in Control Panel.  
  
 If **sqlservr.exe** was started from the command prompt, pressing CTRL+C shuts down [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, pressing CTRL+C does not insert a checkpoint.  
  
> [!NOTE]  
>  Using any of these methods to stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sends the `SERVICE_CONTROL_STOP` message to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Permissions  
 SHUTDOWN permissions are assigned to members of the **sysadmin** and **serveradmin** fixed server roles, and they are not transferable.  
  
## See Also  
 [CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md)   
 [sp_lock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)   
 [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)   
 [sqlservr Application](../../tools/sqlservr-application.md)   
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)  
  
  
