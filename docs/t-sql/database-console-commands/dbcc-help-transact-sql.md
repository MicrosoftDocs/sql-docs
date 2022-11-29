---
title: "DBCC HELP (Transact-SQL)"
description: "DBCC HELP (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC HELP"
  - "DBCC_HELP_TSQL"
helpviewer_keywords:
  - "DBCC statement syntax information"
  - "DBCC HELP statement"
dev_langs:
  - "TSQL"
---
# DBCC HELP (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns syntax information for the specified DBCC command.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC HELP ( 'dbcc_statement' | @dbcc_statement_var | '?' )  
[ WITH NO_INFOMSGS ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *dbcc_statement* | *\@dbcc_statement_var*  
 Is the name of the DBCC command for which to receive syntax information. Provide only the part of the DBCC command that follows DBCC, for example, CHECKDB instead of DBCC CHECKDB.  
  
 ?  
 Returns all DBCC commands for which Help is available.  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
DBCC HELP returns a result set displaying the syntax for the specified DBCC command.
  
## Permissions  
Requires membership in the **sysadmin** fixed server role.
  
## Examples  
### A. Using DBCC HELP with a variable  
The following example returns syntax information for DBCC `CHECKDB`.
  
```sql  
DECLARE @dbcc_stmt sysname;  
SET @dbcc_stmt = 'CHECKDB';  
DBCC HELP (@dbcc_stmt);  
GO  
```  
  
### B. Using DBCC HELP with the ? option  
The following example returns all DBCC statements for which Help is available.
  
```sql  
DBCC HELP ('?');  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  
