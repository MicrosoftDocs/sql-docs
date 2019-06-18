---
title: "SET RESULT_SET_CACHING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/03/2019"
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
dev_langs:
  - "TSQL"
helpviewer_keywords: 
author: XiaoyuL-Preview
ms.author: xiaoyul
manager: craigg
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---
# SET RESULT_SET_CACHING (Transact-SQL) Applies to Azure SQL Data Warehouse Gen2 only (preview)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Causes Azure SQL Data Warehouse to cache query result sets.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```
SET RESULT_SET_CACHING { ON | OFF };
```  
  
## Remarks  

> [!Note]
> While this feature is being rolled out to all regions, please check the version deployed to your instance and the latest [Azure SQL DW release notes](/azure/sql-data-warehouse/release-notes-10-0-10106-0) for feature availability.
  
This command must be run while connected to the master database.  Change to this database setting takes effect immediately.  Storage costs are incurred by caching query result sets. After disabling result caching for a database, previously persisted result cache will immediately be deleted from Azure SQL Data Warehouse storage. A new column called is_result_set_caching_on is introduced in [sys.databases](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=azure-sqldw-latest) to show the result caching setting for a database.  

**ON**
Specifies that query result sets returned from this database will be cached in Azure SQL Data Warehouse storage.

**OFF**
Specifies that query result sets returned from this database won't be cached in Azure SQL Data Warehouse storage.

Query the result_cache_hit column in sys.dm_pdw_exec_requests with a query’s request_id to see if this query was executed with a result cache hit or miss.

```sql
Select result_cache_hit 
From sys.dm_pdw_exec_requests
Where request_id = 'QID58286'
;
```

If there's a cache hit, the query result will have a single step with following details:

|**Column name**|**Operator**|**Value**|
|----|----|----|
|operation_type|=|ReturnOperation|
|step_index|=|0|
|location_type|=|Control|
|command|Like|%DWResultCacheDb%|
||||
  
## Permissions

Requires these permissions:

- Server-level principal login (the one created by the provisioning process)
  or
- Member of the dbmanager database role.

A database owner can't alter the database unless the owner is a member of the dbmanager role.
  
## Examples

### Enable `RESULT_SET_CACHING` for a database

```sql
ALTER DATABASE myTestDW  
SET RESULT_SET_CACHING ON;
```

### Disable `RESULT_SET_CACHING` for a database

```sql
ALTER DATABASE myTestDW  
SET RESULT_SET_CACHING OFF;
```

### Check `RESULT_SET_CACHING` setting for a database

```sql
SELECT name, is_result_set_caching_on  
FROM sys.databases
```

## See also

[ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options?view=azure-sqldw-latest)</br>
[ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)</br>
[DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)](/sql/t-sql/database-console-commands/dbcc-showresultcachespaceused-transact-sql)
[DBCC DROPRESULTSETCACHE (Transact-SQL)](/sql/t-sql/database-console-commands/dbcc-dropresultsetcache-transact-sql)