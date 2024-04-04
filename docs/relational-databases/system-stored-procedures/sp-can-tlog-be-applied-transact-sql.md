---
title: "sp_can_tlog_be_applied (Transact-SQL)"
description: "sp_can_tlog_be_applied (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_can_tlog_be_applied_TSQL"
  - "sp_can_tlog_be_applied"
helpviewer_keywords:
  - "sp_can_tlog_be_applied"
dev_langs:
  - "TSQL"
---
# sp_can_tlog_be_applied (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Verifies whether a transaction log backup can be applied to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. **sp_can_tlog_be_applied** requires that the database be in the Restoring state.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_can_tlog_be_applied [ @backup_file_name = ] 'backup_file_name'   
        , [ @database_name = ] 'database_name'   
        , [ @result = ] result OUTPUT  
```  
  
## Arguments  
`[ @backup_file_name = ] 'backup_file_name'`
 Is the name of a backup file. *backup_file_name* is **nvarchar(128)**.  
  
`[ @database_name = ] 'database_name'`
 Is the name of the database. *database_name* is **sysname**.  
  
`[ @result = ] _result_ OUTPUT`
 Indicates whether the transaction log can be applied to the database. *result* is **bit**.  
  
 1 = The log can be applies  
  
 0= The log cannot be applied.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_can_tlog_be_applied**.  
  
## Examples  
 The following example declares a local variable, `@MyBitVar`, to store the result.  
  
```  
USE master;  
GO  
DECLARE @MyBitVar BIT;  
EXEC sp_can_tlog_be_applied  
     @backup_file_name =   
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\AdventureWorks2022.bak',  
     @database_name = N'AdventureWorks2022',  
     @result = @MyBitVar OUTPUT;  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
