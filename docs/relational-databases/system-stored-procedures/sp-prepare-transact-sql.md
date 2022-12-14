---
title: sp_prepare (Transact SQL)
description: "sp_prepare (Transact SQL)"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_prepare_TSQL"
  - "sp_prepare"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_prepare"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "02/28/2018"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# sp_prepare (Transact SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Prepares a parameterized [!INCLUDE[tsql](../../includes/tsql-md.md)] statement and returns a statement *handle* for execution.  `sp_prepare` is invoked by specifying ID = 11 in a tabular data stream (TDS) packet.  
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql
sp_prepare handle OUTPUT, params, stmt, options  
```  
  
## Arguments  
 *handle*  
 Is a SQL Server-generated *prepared handle* identifier. *handle* is a required parameter with an **int** return value.  
  
 *params*  
 Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a NULL value if the statement is not parameterized.  
  
 *stmt*  
 Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.  
  
 *options*  
 An optional parameter that returns a description of the cursor result set columns. *options* requires the following int input value:  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|RETURN_METADATA|  
  
## Examples  
A. The following example prepares and executes a simple statement.  
  
```sql  
DECLARE @P1 INT;  
EXEC sp_prepare @P1 OUTPUT,   
    N'@P1 NVARCHAR(128), @P2 NVARCHAR(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';  
EXEC sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```

B. The following example prepares a statement in the AdventureWorks2016 database, and later executes it using the handle.

```sql
-- Prepare query
DECLARE @P1 INT;  
EXEC sp_prepare @P1 OUTPUT,   
    N'@Param INT',  
    N'SELECT *
FROM Sales.SalesOrderDetail AS sod
INNER JOIN Production.Product AS p ON sod.ProductID = p.ProductID
WHERE SalesOrderID = @Param
ORDER BY Style DESC;';  

-- Return handle for calling application
SELECT @P1;
GO
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
-----------
1

(1 row affected)
```

Then the application executes the query twice using the handle value 1, before discarding the prepared plan.

```sql
EXEC sp_execute 1, 49879;  
GO

EXEC sp_execute 1, 48766;
GO

EXEC sp_unprepare 1; 
GO
```
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  

