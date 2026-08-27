---
title: "sys.dm_db_missing_index_details (Transact-SQL)"
description: The sys.dm_db_missing_index_details dynamic management view returns detailed information about missing indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/24/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_db_missing_index_details"
  - "dm_db_missing_index_details"
  - "sys.dm_db_missing_index_details_TSQL"
  - "dm_db_missing_index_details_TSQL"
helpviewer_keywords:
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_details dynamic management view"
  - "sys.dm_db_missing_index_details dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_db_missing_index_details (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns detailed information about missing indexes. 
  
 In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], dynamic management views cannot expose information that would affect database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**index_handle**|**int**|Identifies a particular missing index. The identifier is unique across the server. `index_handle` is the key of this table.|  
|**database_id**|**smallint**|Identifies the database where the table with the missing index resides. <br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server.|  
|**object_id**|**int**|Identifies the table where the index is missing.|  
|**equality_columns**|**nvarchar(4000)**|Comma-separated list of columns that contribute to equality predicates of the form:<br /><br /> *table.column* = *constant_value*|  
|**inequality_columns**|**nvarchar(4000)**|Comma-separated list of columns that contribute to inequality predicates, for example, predicates of the form:<br /><br /> *table.column* > *constant_value*<br /><br /> Any comparison operator other than "=" expresses inequality.|  
|**included_columns**|**nvarchar(4000)**|Comma-separated list of columns needed as covering columns for the query. For more information about covering or included columns, see [Create Indexes with Included Columns](../indexes/create-indexes-with-included-columns.md).<br /><br /> For memory-optimized indexes (both hash and memory-optimized nonclustered), ignore `included_columns`. All columns of the table are included in every memory-optimized index.|  
|**statement**|**nvarchar(4000)**|Name of the table where the index is missing.|  
  
## Remarks  
 Information returned by `sys.dm_db_missing_index_details` is updated when a query is optimized by the query optimizer, and is not persisted. Missing index information is kept only until the database engine is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep it after server recycling. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.   

  
 To determine which missing index groups a particular missing index is part of, you can query the `sys.dm_db_missing_index_groups` dynamic management view by equijoining it with `sys.dm_db_missing_index_details` based on the `index_handle` column.  

  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones. 
  
## Using missing index information in CREATE INDEX statements  

To convert the information returned by `sys.dm_db_missing_index_details` into a CREATE INDEX statement for both memory-optimized and disk-based indexes, equality columns should be put before the inequality columns, and together they should make the key of the index. Included columns should be added to the CREATE INDEX statement using the INCLUDE clause. To determine an effective order for the equality columns, order them based on their selectivity: list the most selective columns first (leftmost in the column list). Learn more in [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md), including [Limitations of the missing index feature](../indexes/tune-nonclustered-missing-index-suggestions.md#limitations-of-the-missing-index-feature).
  
 For more information about memory-optimized indexes, see [Indexes for Memory-Optimized Tables](../in-memory-oltp/indexes-for-memory-optimized-tables.md).  
  
## Transaction consistency  

If a transaction creates or drops a table, the rows containing missing index information about the dropped objects are removed from this dynamic management object, preserving transaction consistency. Learn more about [limitations of the missing index feature](../indexes/tune-nonclustered-missing-index-suggestions.md#limitations-of-the-missing-index-feature).

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Examples

The following example returns missing index suggestions for the current database. Missing index suggestions should be combined when possible with one another, and with existing indexes in the current database. Learn how to apply these suggestions in [tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md).

```sql
SELECT
  CONVERT (varchar(30), getdate(), 126) AS runtime,  mig.index_group_handle,  mid.index_handle,
  CONVERT (decimal (28, 1), migs.avg_total_user_cost * migs.avg_user_impact * (migs.user_seeks + migs.user_scans) ) AS improvement_measure,
  'CREATE INDEX missing_index_' + CONVERT (varchar, mig.index_group_handle) + '_' + CONVERT (varchar, mid.index_handle) + ' ON ' + mid.statement + ' (' + ISNULL (mid.equality_columns, '') + CASE
    WHEN mid.equality_columns IS NOT NULL
    AND mid.inequality_columns IS NOT NULL THEN ','
    ELSE ''
  END + ISNULL (mid.inequality_columns, '') + ')' + ISNULL (' INCLUDE (' + mid.included_columns + ')', '') AS create_index_statement,
  migs.*, mid.database_id, mid.[object_id]
FROM sys.dm_db_missing_index_groups mig
	INNER JOIN sys.dm_db_missing_index_group_stats migs ON migs.group_handle = mig.index_group_handle
	INNER JOIN sys.dm_db_missing_index_details mid ON mig.index_handle = mid.index_handle
WHERE CONVERT (decimal (28, 1),migs.avg_total_user_cost * migs.avg_user_impact * (migs.user_seeks + migs.user_scans)) > 10
ORDER BY migs.avg_total_user_cost * migs.avg_user_impact * (migs.user_seeks + migs.user_scans) DESC
```

> [!NOTE]
> The [Index-Creation](https://github.com/microsoft/tigertoolbox/tree/master/Index-Creation) script in Microsoft's [Tiger Toolbox](https://github.com/microsoft/tigertoolbox) examines missing index DMVs and automatically removes any redundant suggested indexes, parses out low impact indexes, and generates index creation scripts for your review. As in the query above, it does NOT execute index creation commands. The [Index-Creation](https://github.com/microsoft/tigertoolbox/tree/master/Index-Creation) script is suitable for SQL Server and  Azure SQL Managed Instance. For Azure SQL Database, consider implementing [automatic index tuning](/azure/azure-sql/database/automatic-tuning-overview).

## Related content

- [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md)
- [sys.dm_db_missing_index_columns (Transact-SQL)](sys-dm-db-missing-index-columns-transact-sql.md)
- [sys.dm_db_missing_index_groups (Transact-SQL)](sys-dm-db-missing-index-groups-transact-sql.md)
- [sys.dm_db_missing_index_group_stats (Transact-SQL)](sys-dm-db-missing-index-group-stats-transact-sql.md)
- [sys.dm_db_missing_index_group_stats_query (Transact-SQL)](sys-dm-db-missing-index-group-stats-query-transact-sql.md)
- [sys.dm_os_sys_info (Transact-SQL)](sys-dm-os-sys-info-transact-sql.md)
