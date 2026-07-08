---
title: "sys.dm_db_external_tables_log_status (Transact-SQL)"
description: The sys.dm_db_external_tables_log_status DMV returns information about the Fabric SQL analytics endpoint metadata sync status.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rakrish, anphil
ms.date: 05/19/2025
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_external_tables_log_status_TSQL"
  - "sys.dm_db_external_tables_log_status"
  - "dm_db_external_tables_log_status_TSQL"
  - "dm_db_external_tables_log_status"
helpviewer_keywords:
  - "sys.dm_db_external_tables_log_status dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=fabric || =fabric-sqldb "
---
# sys.dm_db_external_tables_log_status (Transact-SQL)

[!INCLUDE [fabric-se-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/fabric-se-fabricmirroredsqldb-fabricsqldb.md)]

Returns the details of the most recent update to the current [SQL analytics endpoint in Microsoft Fabric](/fabric/data-engineering/lakehouse-sql-analytics-endpoint). Use this DMV to determine the last successful [SQL analytics endpoint metadata sync](/fabric/data-engineering/sql-analytics-endpoint-metadata-sync).

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `object_id` |**int**|The `object_id` of the current SQL analytics endpoint.|  
| `latest_log_version` |**bigint**| The highest Delta transaction log version of the table that was updated.|
| `latest_checkpoint_version` |**bigint**| The latest Delta checkpoint version that was processed. |
| `last_update_time_utc` |**datetime**| Timestamp for the last time the table was updated with new data, in the UTC time zone.|
| `is_blocked` |**bit**| Indicates whether the last attempt at the table update was blocked (`1`) or successful (`0`).|

## Remarks

As of May 2026, if you are on the new version of the metadata sync, then you can use the `sys.dm_db_external_tables_log_status` dynamic management view (DMV) to get the details of the most recent update to the SQL analytics endpoint.

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) within a Fabric workspace with Contributor or above permissions.

## Examples

Use `sys.dm_db_external_tables_log_status` to check the latest SQL analytics endpoint metadata sync.

```sql
SELECT *
FROM sys.dm_db_external_tables_log_status;
```

## Related content

- [SQL analytics endpoint metadata sync](/fabric/data-engineering/sql-analytics-endpoint-metadata-sync)
- [sys.sp_dw_refresh_ext_table (Transact-SQL)](../system-stored-procedures/sp-dw-refresh-ext-table-transact-sql.md)