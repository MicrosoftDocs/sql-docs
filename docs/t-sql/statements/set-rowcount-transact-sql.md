---
title: "SET ROWCOUNT (Transact-SQL)"
description: SET ROWCOUNT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET_ROWCOUNT_TSQL"
  - "ROWCOUNT_TSQL"
  - "SET ROWCOUNT"
  - "ROWCOUNT"
helpviewer_keywords:
  - "row return limitations [SQL Server]"
  - "SET ROWCOUNT statement"
  - "number of rows affected by statement"
  - "ROWCOUNT option"
  - "counting rows"
  - "stopping queries"
  - "limiting rows returned"
  - "queries [SQL Server], stopping"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET ROWCOUNT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to stop processing the query after the specified number of rows are returned.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SET ROWCOUNT { number | @number_var }   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *number* | @*number_var*  
 Is the number, an integer, of rows to be processed before stopping the specific query.  
  
## Remarks  
  
> [!IMPORTANT]  
>  Using SET ROWCOUNT will not affect DELETE, INSERT, and UPDATE statements in a future release of SQL Server. Avoid using SET ROWCOUNT with DELETE, INSERT, and UPDATE statements in new development work, and plan to modify applications that currently use it. For a similar behavior, use the TOP syntax. For more information, see [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
  
 To set this option off so that all rows are returned, specify SET ROWCOUNT 0.  
  
 Setting the SET ROWCOUNT option causes most [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to stop processing when they have been affected by the specified number of rows. This includes triggers. The ROWCOUNT option does not affect dynamic cursors, but it does limit the rowset of keyset and insensitive cursors. This option should be used with caution.  
  
 SET ROWCOUNT overrides the SELECT statement TOP keyword if the rowcount is the smaller value.  
  
 The setting of SET ROWCOUNT is set at execute or run time and not at parse time.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 SET ROWCOUNT stops processing after the specified number of rows. In the following example, note that over 500 rows meet the criteria of `Quantity` less than `300`. However, after applying SET ROWCOUNT, you can see that not all rows were returned.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT count(*) AS Count  
FROM Production.ProductInventory  
WHERE Quantity < 300;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Count 
 ----------- 
 537 
 
 (1 row(s) affected)
 ```  
  
 Now, set `ROWCOUNT` to `4` and return all rows to demonstrate that only 4 rows are returned.  
  
```sql
SET ROWCOUNT 4;  
SELECT *  
FROM Production.ProductInventory  
WHERE Quantity < 300;  
GO  
  
-- (4 row(s) affected)
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 SET ROWCOUNT stops processing after the specified number of rows. In the following example, note that more than 20 rows meet the criteria of `AccountType = 'Assets'`. However, after applying SET ROWCOUNT, you can see that not all rows were returned.  
  
```sql
-- Uses AdventureWorks  
  
SET ROWCOUNT 5;  
SELECT * FROM [dbo].[DimAccount]  
WHERE AccountType = 'Assets';  
```  
  
 To return all rows, set ROWCOUNT to 0.  
  
```sql
-- Uses AdventureWorks  
  
SET ROWCOUNT 0;  
SELECT * FROM [dbo].[DimAccount]  
WHERE AccountType = 'Assets';  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  

