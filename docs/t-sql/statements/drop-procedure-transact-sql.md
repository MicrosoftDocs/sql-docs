---
title: "DROP PROCEDURE (Transact-SQL)"
description: DROP PROCEDURE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP PROCEDURE"
  - "DROP_PROCEDURE_TSQL"
helpviewer_keywords:
  - "removing stored procedures"
  - "dropping procedure groups"
  - "deleting stored procedures"
  - "deleting procedure groups"
  - "DROP PROCEDURE statement"
  - "dropping stored procedures"
  - "stored procedures [SQL Server], removing"
  - "removing procedure groups"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP PROCEDURE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes one or more stored procedures or procedure groups from the current database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
DROP { PROC | PROCEDURE } [ IF EXISTS ] { [ schema_name. ] procedure } [ ,...n ]  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
DROP { PROC | PROCEDURE } { [ schema_name. ] procedure_name }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the procedure only if it already exists.  
  
 *schema_name*  
 The name of the schema to which the procedure belongs. A server name or database name cannot be specified.  
  
 *procedure*  
 The name of the stored procedure or stored procedure group to be removed. Individual procedures within a numbered procedure group cannot be dropped; the whole procedure group is dropped.  
  
## Best Practices  
 Before removing any stored procedure, check for dependent objects and modify these objects accordingly. Dropping a stored procedure can cause dependent objects and scripts to fail when these objects are not updated. For more information, see [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)  
  
## Metadata  
 To display a list of existing procedures, query the **sys.objects** catalog view. To display the procedure definition, query the **sys.sql_modules** catalog view.  
  
## Security  
  
### Permissions  
 Requires **CONTROL** permission on the procedure, or **ALTER** permission on the schema to which the procedure belongs, or membership in the **db_ddladmin** fixed server role.  
  
## Examples  
 The following example removes the `dbo.uspMyProc` stored procedure in the current database.  
  
```sql  
DROP PROCEDURE dbo.uspMyProc;  
GO  
```  
  
 The following example removes several stored procedures in the current database.  
  
```sql  
DROP PROCEDURE dbo.uspGetSalesbyMonth, dbo.uspUpdateSalesQuotes, dbo.uspGetSalesByYear;  
```  
  
 The following example removes the `dbo.uspMyProc` stored procedure if it exists but does not cause an error if the procedure does not exist. This syntax is new in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
```sql  
DROP PROCEDURE IF EXISTS dbo.uspMyProc;  
GO  
```  
  
  
## See Also  
 [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [Delete a Stored Procedure](../../relational-databases/stored-procedures/delete-a-stored-procedure.md)  
