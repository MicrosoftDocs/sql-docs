---
description: "sp_resetstatus (Transact-SQL)"
title: "sp_resetstatus (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_resetstatus"
  - "sp_resetstatus_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_resetstatus"
ms.assetid: b892727f-ea3b-4b94-88d9-f2386ad4962c
author: markingmyname
ms.author: maghan
---
# sp_resetstatus (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Resets the status of a suspect database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_resetstatus [ @dbname = ] 'database'  
```  
  
## Arguments  
 [ @dbname= ] '*database*'  
 Is the name of the database to reset. *database* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 sp_resetstatus turns off the suspect flag on a database. This procedure updates the mode and status columns of the named database in sys.databases. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log should be consulted and all problems resolved before running this procedure. Stop and restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] after you execute sp_resetstatus.  
  
 A database can become suspect for several reasons. Possible causes include denial of access to a database resource by the operating system, and the unavailability or corruption of one or more database files.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role.  
  
## Examples  
 The following example resets the status of the `AdventureWorks2012` database.  
  
```  
EXEC sp_resetstatus 'AdventureWorks2012';  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)  
  
  
