---
title: "@@LOCK_TIMEOUT (Transact-SQL)"
description: "The @@LOCK_TIMEOUT Transact-SQL function returns the current lock timeout setting in milliseconds for the current session."
author: markingmyname
ms.author: maghan
ms.date: 02/09/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "@@LOCK_TIMEOUT"
  - "@@LOCK_TIMEOUT_TSQL"
helpviewer_keywords:
  - "timeout options [SQL Server], locks"
  - "@@LOCK_TIMEOUT function"
  - "current lock time-out setting"
  - "locking [SQL Server], time-outs"
dev_langs:
  - "TSQL"
---
# @@LOCK_TIMEOUT (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  The `@@LOCK_TIMEOUT` function returns the current lock timeout setting in milliseconds for the current session.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
@@LOCK_TIMEOUT  
```  

## Return types

 **integer**  

## Remarks

 `SET LOCK_TIMEOUT` allows an application to set the maximum time that a statement waits on a blocked resource. When a statement waits longer than the `LOCK_TIMEOUT` setting, the blocked statement is automatically canceled, and an error message is returned to the application.  

 If `SET LOCK_TIMEOUT` hasn't been run in the current session, `@@LOCK_TIMEOUT` returns a value of `-1`.  

## Examples

 This example shows the result set when a `LOCK_TIMEOUT` value is not set.  

```sql  
SELECT @@LOCK_TIMEOUT AS [Lock Timeout];  
GO  
```  

 Here is the result set:  

```  
Lock Timeout  
------------  
-1  
```  

 This example sets `LOCK_TIMEOUT` to 1,800 milliseconds and then calls `@@LOCK_TIMEOUT`.  

```sql  
SET LOCK_TIMEOUT 1800;  
SELECT @@LOCK_TIMEOUT AS [Lock Timeout];  
GO  
```  

 Here is the result set:  

```  
Lock Timeout  
------------  
1800          
```  

## Related content

- [SET LOCK_TIMEOUT (Transact-SQL)](../statements/set-lock-timeout-transact-sql.md)
