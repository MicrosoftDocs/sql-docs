---
title: "sys.dm_tran_version_store_space_usage (Transact-SQL)"
description: sys.dm_tran_version_store_space_usage (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: maghan
ms.date: 01/06/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_tran_version_store_space_usage_TSQL"
  - "sys.dm_tran_version_store_space_usage"
  - "dm_tran_version_store_space_usage"
  - "dm_tran_version_store_space_usage_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_version_store_space_usage dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_tran_version_store_space_usage (Transact-SQL)

 [!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016sp2-asdb-asdbmi-fabricsqldb.md)]

Returns a table that displays total space in `tempdb` used by version store records for each database. **sys.dm_tran_version_store_space_usage** is efficient and not expensive to run, as it doesn't navigate through individual version store records, and returns aggregated version store space consumed in tempdb per database.

Each versioned record is stored as binary data, together with some tracking or status information. Similar to records in database tables, version-store records are stored in 8192-byte pages. If a record exceeds 8,192 bytes, the record is split across two different records.

Because the versioned record is stored as binary, there are no problems with different collations from different databases. Use **sys.dm_tran_version_store_space_usage** to monitor and plan `tempdb` size based on the version store space usage of databases in a SQL Server instance.

| Column name | Data type | Description |
| --- | --- | --- |
| **database_id** | **int** | Database ID of the database.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| **reserved_page_count** | **bigint** | Total count of the pages reserved in `tempdb` for version store records of the database. |
| **reserved_space_kb** | **bigint** | Total space used in kilobytes in `tempdb` for version store records of the database. |

## Permissions

On [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Examples

The following query can be used to determine space consumed in `tempdb`, by version store of each database in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

```sql
SELECT
  DB_NAME(database_id) as 'Database Name',
  reserved_page_count,
  reserved_space_kb
FROM sys.dm_tran_version_store_space_usage;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```
Database Name            reserved_page_count reserved_space_kb
------------------------ -------------------- -----------
msdb                      0                    0
AdventureWorks2022        10                   80
AdventureWorks2022DW      0                    0
WideWorldImporters        20                   160
```

## Related content

- [Dynamic Management Views and Functions (Transact-SQL)](system-dynamic-management-objects.md)
- [Transaction Related Dynamic Management Views and Functions (Transact-SQL)](transaction-related-dynamic-management-views-and-functions-transact-sql.md)
