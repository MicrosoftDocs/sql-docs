---
title: "SET FORCEPLAN (Transact-SQL)"
description: SET FORCEPLAN (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET_FORCEPLAN_TSQL"
  - "SET FORCEPLAN"
  - "FORCEPLAN"
  - "FORCEPLAN_TSQL"
helpviewer_keywords:
  - "joins [SQL Server], overriding query optimizer process"
  - "FORCEPLAN option"
  - "SET FORCEPLAN statement"
  - "query optimizer [SQL Server], optimizing process"
  - "overriding query optimizer process"
dev_langs:
  - "TSQL"
---
# SET FORCEPLAN (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  When FORCEPLAN is set to ON, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer processes a join in the same order as the tables appear in the FROM clause of a query. In addition, setting FORCEPLAN to ON forces the use of a nested loop join unless other types of joins are required to construct a plan for the query, or they are requested with join hints or query hints.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET FORCEPLAN { ON | OFF }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
 SET FORCEPLAN essentially overrides the logic used by the query optimizer to process a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement. The data returned by the SELECT statement is the same regardless of this setting. The only difference is the way in which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes the tables to satisfy the query.  
  
 Query optimizer hints can also be used in queries to affect how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes the SELECT statement.  
  
 SET FORCEPLAN is applied at execute or run time and not at parse time.  
  
## Permissions  
 SET FORCEPLAN permissions default to all users.  
  
## Examples  
 The following example performs a join of four tables. The `SHOWPLAN_TEXT` setting is enabled, so [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns information about how it is processing the query differently after the `SET FORCE_PLAN` setting is enabled.  
  
```sql
USE AdventureWorks2012;  
GO  
-- Make sure FORCEPLAN is set to OFF.  
SET SHOWPLAN_TEXT OFF;  
GO  
SET FORCEPLAN OFF;  
GO  
SET SHOWPLAN_TEXT ON;  
GO  
-- Example where the query plan is not forced.  
SELECT p.LastName, p.FirstName, v.Name  
FROM Person.Person AS p  
   INNER JOIN HumanResources.Employee AS e  
   ON e.BusinessEntityID = p.BusinessEntityID  
   INNER JOIN Purchasing.PurchaseOrderHeader AS poh  
   ON e.BusinessEntityID = poh.EmployeeID  
   INNER JOIN Purchasing.Vendor AS v  
   ON poh.VendorID = v.BusinessEntityID;  
GO  
-- SET FORCEPLAN to ON.  
SET SHOWPLAN_TEXT OFF;  
GO  
SET FORCEPLAN ON;  
GO  
SET SHOWPLAN_TEXT ON;  
GO  
-- Reexecute inner join to see the effect of SET FORCEPLAN ON.  
SELECT p.LastName, p.FirstName, v.Name  
FROM Person.Person AS p  
   INNER JOIN HumanResources.Employee AS e   
   ON e.BusinessEntityID = p.BusinessEntityID  
   INNER JOIN Purchasing.PurchaseOrderHeader AS poh  
   ON e.BusinessEntityID = poh.EmployeeID  
   INNER JOIN Purchasing.Vendor AS v  
   ON poh.VendorID = v.BusinessEntityID;  
GO  
SET SHOWPLAN_TEXT OFF;  
GO  
SET FORCEPLAN OFF;  
GO  
```  
  
## See Also  
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET SHOWPLAN_ALL &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-all-transact-sql.md)   
 [SET SHOWPLAN_TEXT &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-text-transact-sql.md)  
  
  
