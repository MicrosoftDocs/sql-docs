---
title: "Database scoped global temporary tables - Azure SQL Database | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "04/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "EDITION SQL Database"
  - "global temporary tables [SQL Server], tempdb database"
  - "temporary stored procedures [SQL Server]"
ms.assetid: 
caps.latest.revision: 66
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# Database scoped blobal temporary tables (Azure SQL Database)

[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Global temporary tables for SQL Server (initiated with ## table name) are stored in tempdb and shared among all users’ sessions across the whole SQL Server instance. For information on SQL table types, see [Tables](tables.md)
.

## Overview

Azure SQL Database supports global temporary tables that are also stored in tempdb and scoped to the database level.  This means that global temporary tables are shared for all users’ sessions within the same Azure SQL database. User sessions from other Azure SQL databases cannot access global temporary tables.

Global temporary tables for Azure SQL DB follow the same syntax and semantics that SQL Server uses for temporary tables.  Similarly, global temporary stored procedures are also scoped to the database level in Azure SQL DB. Local temporary tables (initiated with # table name) are also supported for Azure SQL Database and follow the same syntax and semantics that SQL Server uses.  See [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md).
> [!IMPORTANT]
> This feature is in public preview and is available for Azure SQL Database.
>

## Troubleshooting global temporary tables for Azure SQL DB 

For the troubleshooting the tempdb, see [Troubleshooting Insufficient Disk space in tempdb](https://technet.microsoft.com/library/ms176029%28v=sql.105%29.aspx?f=255&MSPPError=-2147217396). To access the troubleshooting DMVs in Azure SQL Database, you must be a server admin.
  
## Permissions  

 Any user can create global temporary objects. Users can only access their own objects, unless they receive additional permissions. 
 .  
  
## Examples 

- Session A creates a global temp table ##test in Azure SQL Database testdb1 and adds 1 row

```tsql
CREATE TABLE ##test ( a int, b int);
INSERT INTO ##test values (1,1);

--Obtain object ID for temp table ##test 
SELECT OBJECT_ID('tempdb.dbo.##test') AS 'Object ID'; 

---Result
1253579504

---Obtain global temp table name for a given object ID 1253579504 in tempdb (2)
SELECT name FROM tempdb.sys.objects WHERE object_id = 1253579504

---Result
##test
```
- Session B connects to Azure SQL Database testdb1 and can access table ##test created by session A

```tsql
SELECT * FROM ##test
---Results
1,1
```

- Session C connects to another database in Azure SQL Database testdb2 and wants to access ##test created in testdb1. This select fails due to the database scope for the global temp tables 

```tsql
SELECT * FROM ##test
---Results
Msg 208, Level 16, State 0, Line 1
Invalid object name '##test'
```

- Addressing system object in Azure SQL Database tempdb from current user database testdb1

```tsql
SELECT * FROM tempdb.sys.objects
SELECT * FROM tempdb.sys.columns
SELECT * FROM tempdb.sys.database_files
```

  
## See Also  
 
  
  
