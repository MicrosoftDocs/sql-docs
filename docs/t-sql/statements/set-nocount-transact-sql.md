---
title: "SET NOCOUNT (Transact-SQL)"
description: SET NOCOUNT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "12/09/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "NOCOUNT"
  - "SET_NOCOUNT_TSQL"
  - "NOCOUNT_TSQL"
  - "SET NOCOUNT"
helpviewer_keywords:
  - "NOCOUNT option"
  - "number of rows affected by statement"
  - "row affected by statements [SQL Server]"
  - "counting rows"
  - "SET NOCOUNT statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET NOCOUNT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Stops the message that shows the count of the number of rows affected by a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or stored procedure from being returned as part of the result set.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET NOCOUNT { ON | OFF }   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
 When SET NOCOUNT is ON, the count is not returned. When SET NOCOUNT is OFF, the count is returned.  
  
 The @@ROWCOUNT function is updated even when SET NOCOUNT is ON.  
  
 SET NOCOUNT ON prevents the sending of DONEINPROC messages to the client for each statement in a stored procedure. For stored procedures that contain several statements that do not return much actual data, or for procedures that contain [!INCLUDE[tsql](../../includes/tsql-md.md)] loops, setting SET NOCOUNT to ON can provide a significant performance boost, because network traffic is greatly reduced.  
  
 The setting specified by SET NOCOUNT is in effect at execute or run time and not at parse time.  
  
 To view the current setting for this setting, run the following query.  
  
```sql
DECLARE @NOCOUNT VARCHAR(3) = 'OFF';  
IF ( (512 & @@OPTIONS) = 512 ) SET @NOCOUNT = 'ON';  
SELECT @NOCOUNT AS NOCOUNT;  
  
```  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example prevents the message about the number of rows affected from being displayed. In the following example, "(5 rows affected)" will only be returned to clients from the first SELECT statement.
   
```sql
USE AdventureWorks2012;  
GO  
SET NOCOUNT OFF;  
GO  
-- Display the count message.  
SELECT TOP(5) LastName  
FROM Person.Person  
WHERE LastName LIKE 'A%';  
GO  
-- SET NOCOUNT to ON to no longer display the count message.  
SET NOCOUNT ON;  
GO  
SELECT TOP(5) LastName  
FROM Person.Person  
WHERE LastName LIKE 'A%';  
GO  
-- Reset SET NOCOUNT to OFF  
SET NOCOUNT OFF;  
GO  
```  
  
## See Also  

 - [@@ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/rowcount-transact-sql.md)   
 - [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
