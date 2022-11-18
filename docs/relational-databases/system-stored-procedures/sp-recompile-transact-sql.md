---
description: "The sp_recompile system stored procedure causes stored procedures, triggers, and user-defined functions to be recompiled the next time that they are run."
title: "sp_recompile (Transact-SQL)"
ms.custom: ""
ms.date: "08/01/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_recompile_TSQL"
  - "sp_recompile"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_recompile"
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_recompile (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Causes stored procedures, triggers, and user-defined functions to be recompiled the next time that they are run. It does this by dropping the existing plan from the procedure cache forcing a new plan to be created the next time that the procedure or trigger is run. In a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] collection, the event `SP:CacheInsert` is logged instead of the event `SP:Recompile`.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
sp_recompile [ @objname = ] 'object'  
```  
  
## Arguments  

#### [ @objname= ] '*object*'  
 The qualified or unqualified name of a stored procedure, trigger, table, view, or user-defined function in the current database. *object* is **nvarchar(776)**, with no default. If *object* is the name of a stored procedure, trigger, or user-defined function, the stored procedure, trigger, or function will be recompiled the next time that it is run. If *object* is the name of a table or view, all the stored procedures, triggers, or user-defined functions that reference the table or view will be recompiled the next time that they are run.  
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
  
## Remarks  
 `sp_recompile` looks for an object in the current database only.  
  
 The queries used by stored procedures, or triggers, and user-defined functions are optimized only when they are compiled. As indexes or other changes that affect statistics are made to the database, compiled stored procedures, triggers, and user-defined functions may lose efficiency. By recompiling stored procedures and triggers that act on a table, you can reoptimize the queries.  
   
 Proactive execution of this stored procedure is usually unnecessary. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically recompiles stored procedures, triggers, and user-defined functions when it is advantageous. There are a variety of reasons the database engine may choose to recompile objects. Most commonly, automatic recompilation follows changes to the underlying cardinality estimate because of automatic or manual statistics updates.
 
 Recompiling a stored procedure with every execution is one of the less efficient ways to combat query plan issues caused by parameterization. The feature [Parameter Sensitive Plan optimization](../performance/parameter-sensitivity-plan-optimization.md) introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] attempts to mitigate this problem automatically. In prior versions, instead of calling `sp_recompile` with each execution, consider:
 
 - Append the [WITH RECOMPILE option](../stored-procedures/recompile-a-stored-procedure.md) to the query, requiring a code change.
 - Apply the `WITH RECOMPILE` option with a [plan guide](../performance/plan-guides.md).
 - Apply the `WITH RECOMPILE` option with a [Query Store hint](../performance/query-store-hints.md) without making code changes.
 - For more issues and discussion on this topic, see [Resolving queries with parameter sensitive plan problems](/azure/azure-sql/managed-instance/identify-query-performance-issues#resolving-queries-with-suboptimal-query-execution-plans).
 
## Permissions  
 Requires ALTER permission on the specified object.  
  
## Examples  
 The following example causes stored procedures, triggers, and user-defined functions that act on the `Sales.Customer` table to be recompiled the next time that they are run.  
  
```sql
USE AdventureWorks2012;  
GO  
EXEC sp_recompile N'Sales.Customer';  
GO  
```  
  
## See also
 - [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 - [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 - [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
 - [SQL:StmtRecompile Event Class](../event-classes/sql-stmtrecompile-event-class.md)
  
## Next steps
 - [Recompile a Stored Procedure](../stored-procedures/recompile-a-stored-procedure.md)
  
