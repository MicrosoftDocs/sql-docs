---
description: "sp_cycle_agent_errorlog (Transact-SQL)"
title: "sp_cycle_agent_errorlog (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cycle_agent_errorlog"
  - "sp_cycle_agent_errorlog_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cycle_agent_errorlog"
ms.assetid: 8aa96182-60b7-4d7b-b2a7-ccce70378c6e
author: markingmyname
ms.author: maghan
---
# sp_cycle_agent_errorlog (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Closes the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log file and cycles the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log extension numbers just like a server restart. The new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log contains a line indicating that the new log has been created.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_cycle_agent_errorlog  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is started, the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log is renamed to **SQLAgent.1**; **SQLAgent.1** becomes **SQLAgent.2**, **SQLAgent.2** becomes **SQLAgent.3**, and so on. **sp_cycle_agent_errorlog** enables you to cycle the error log files without stopping and starting the server.  
  
 This stored procedure must be run from the **msdb** database.  
  
## Permissions  
 Execute permissions for **sp_cycle_agent_errorlog** are restricted to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example cycles the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_cycle_agent_errorlog ;  
GO  
```  
  
## See Also  
 [sp_cycle_errorlog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cycle-errorlog-transact-sql.md)  
  
  
