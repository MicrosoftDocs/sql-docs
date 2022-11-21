---
title: "SET NOEXEC (Transact-SQL)"
description: SET NOEXEC (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "NOEXEC_TSQL"
  - "SET_NOEXEC_TSQL"
  - "SET NOEXEC"
  - "NOEXEC"
helpviewer_keywords:
  - "queries [SQL Server], compiling"
  - "SET NOEXEC statement"
  - "compiling queries [SQL Server]"
  - "NOEXEC option"
dev_langs:
  - "TSQL"
---
# SET NOEXEC (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Compiles each query but does not execute it.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET NOEXEC { ON | OFF }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks  
 When SET NOEXEC is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parses and compiles each batch of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements but does not execute them. When SET NOEXEC is OFF, all batches are executed after compilation.  NOEXEC supports deferred name resolution; if one or more referenced objects in the batch don't exist, no error will be thrown.
  
 The execution of statements in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has two phases: compilation and execution. This setting is useful for having [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validate the syntax and object names in [!INCLUDE[tsql](../../includes/tsql-md.md)] code when executing. It is also useful for debugging statements that would generally be part of a larger batch of statements.  
  
 The setting of SET NOEXEC is set at execute or run time and not at parse time.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example uses `NOEXEC` with a valid query, a query with an object name that is not valid, and a query with incorrect syntax.  
  
```sql
USE AdventureWorks2012;  
GO  
PRINT 'Valid query';  
GO  
-- SET NOEXEC to ON.  
SET NOEXEC ON;  
GO  
-- Inner join.  
SELECT e.BusinessEntityID, e.JobTitle, v.Name  
FROM HumanResources.Employee AS e   
   INNER JOIN Purchasing.PurchaseOrderHeader AS poh  
   ON e.BusinessEntityID = poh.EmployeeID  
   INNER JOIN Purchasing.Vendor AS v  
   ON poh.VendorID = v.BusinessEntityID;  
GO  
-- SET NOEXEC to OFF.  
SET NOEXEC OFF;  
GO  
  
PRINT 'Invalid object name';  
GO  
-- SET NOEXEC to ON.  
SET NOEXEC ON;  
GO  
-- Function name used is a reserved keyword.  
USE AdventureWorks2012;  
GO  
CREATE FUNCTION dbo.Values(@BusinessEntityID int)  
RETURNS TABLE  
AS  
RETURN (SELECT PurchaseOrderID, TotalDue  
   FROM dbo.PurchaseOrderHeader  
   WHERE VendorID = @BusinessEntityID);  
  
-- SET NOEXEC to OFF.  
SET NOEXEC OFF;  
GO  
  
PRINT 'Invalid syntax';  
GO  
-- SET NOEXEC to ON.  
SET NOEXEC ON;  
GO  
-- Built-in function incorrectly invoked.  
SELECT *  
FROM fn_helpcollations;  
-- Reset SET NOEXEC to OFF.  
SET NOEXEC OFF;  
GO  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET SHOWPLAN_ALL &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-all-transact-sql.md)   
 [SET SHOWPLAN_TEXT &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-text-transact-sql.md)  
  
  
