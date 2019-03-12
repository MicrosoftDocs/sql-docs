---
title: "sys.sp_cleanup_temporal_history | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql-database
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 6eff30b4-b261-4f1f-b93c-1f69d754298d
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.sp_cleanup_temporal_history (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

Removes all rows from temporal history table that match configured HISTORY_RETENTION PERIOD within a single transaction.
  
## Syntax  
```  
sp_cleanup_temporal_history [@schema_name = ] schema_name, [@table_name = ] table_name [, [@row_count=] @row_count_var [OUTPUT]]
```  
  
## Arguments  

*@table_name*

The name of the temporal table for which retention cleanup is invoked.

*schema_name*

The name of the schema which current temporal table belongs to

*row_count_var* [OUTPUT]

The output parameter that returns number of deleted rows. If the history table has clustered columnstore index, this parameter will return always 0.
  
## Remarks
This stored procedure can be used only with temporal tables that have finite retention period specified.
Use this stored procedure only if you need to immediately clean all aged rows from the history table. You should know that it can have significant impact on the database log and I/O subsystem as it deletes all eligible rows within the same transaction. 

It is always recommended to rely on an internal background task for cleanup that removes aged rows with the minimal impact on the regular workloads and database in general.

## Permissions  
 Requires db_owner permissions.  

## Example

```
declare @rowcnt int
EXEC sys.sp_cleanup_temporal_history 'dbo', 'Department', @rowcnt output
select @rowcnt
```

## See also

[Temporal tables retention policy](https://docs.microsoft.com/azure/sql-database/sql-database-temporal-tables-retention-policy)
