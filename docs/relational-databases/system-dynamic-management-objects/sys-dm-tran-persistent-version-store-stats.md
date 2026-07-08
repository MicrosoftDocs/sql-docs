---
title: "sys.dm_tran_persistent_version_store_stats (Transact-SQL)"
description: sys.dm_tran_persistent_version_store_stats returns information for accelerated database recovery (ADR) persistent version store (PVS) metrics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, dfurman
ms.date: 02/02/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats_TSQL"
  - "dm_tran_persistent_version_store_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_persistent_version_store_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15 || =azuresqldb-mi-current || =azuresqldb-current || =fabric-sqldb"
---

# sys.dm_tran_persistent_version_store_stats (Transact-SQL)

[!INCLUDE [SQL Server 2019, ASDB, ASDBMI-fabricsqldb](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-fabricsqldb.md)]

Returns information for accelerated database recovery (ADR) persistent version store (PVS) metrics.

## Table returned

| Column name | Data type | Description |
|:--|:--|:--|
| `database_id` | **int** | The `database_id` of this row.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `pvs_filegroup_id` | **smallint** | The filegroup that hosts PVS version store. |
| `persistent_version_store_size_kb` | **bigint** | The size of the off-row versions in PVS, in kilobytes. Does not include the size of row versions stored in-row. For more information, see [Space used by the persistent version store (PVS)](../sql-server-transaction-locking-and-row-versioning-guide.md#space-used-by-the-persistent-version-store-pvs). |
| `online_index_version_store_size_kb` | **bigint** | The size of a special version store size used during online index rebuild, in kilobytes. |
| `current_aborted_transaction_count` | **bigint** | The number of aborted transactions in the database. For more details, see `sys.dm_tran_aborted_transactions`. |
| `oldest_active_transaction_id` | **bigint** | The transaction ID of the oldest active transaction. |
| `oldest_aborted_transaction_id` | **bigint** | The transaction ID of the oldest aborted transaction. If the PVS cleaner can't remove the aborted transaction, this value reflects the oldest value. |
| `min_transaction_timestamp` | **bigint** | The minimum useful timestamp in the system from snapshot scans. |
| `online_index_min_transaction_timestamp` | **bigint** | The minimum useful timestamp in the system to hold up the PVS cleanup during online index builds. |
| `secondary_low_water_mark` | **bigint** | The low water mark aggregated for queries on readable secondaries. The value is a transaction ID and can be matched with `oldest_active_transaction_id` and `oldest_aborted_transaction_id`. |
| `offrow_version_cleaner_start_time` | **datetime2(7)** | The start time of the last sweep that cleaned up off-row versions. |
| `offrow_version_cleaner_end_time` | **datetime2(7)** | The end time of the last sweep that cleaned up off-row versions. If start time has value but the end time doesn't, it means PVS cleanup is ongoing on this database. |
| `aborted_version_cleaner_start_time` | **datetime2(7)** | The start timestamp of the last sweep that cleaned up aborted transactions. |
| `aborted_version_cleaner_end_time` | **datetime2(7)** | The end timestamp of last sweep that cleaned up aborted transactions. If start time has value but the end time doesn't, it means PVS cleanup is ongoing on this database. |
| `pvs_off_row_page_skipped_low_water_mark` | **bigint** | The number of pages skipped during cleanup due to hold up from secondary read queries. |
| `pvs_off_row_page_skipped_transaction_not_cleaned` | **bigint** | The number of pages skipped during cleanup due to aborted transactions. Note this value doesn't reflect the PVS hold up from aborted transactions since the version cleaner uses a min threshold for aborted transaction version cleanup. Can be ignored when troubleshooting large PVS issues. |
| `pvs_off_row_page_skipped_oldest_active_xdesid` | **bigint** | The number of pages skipped during cleanup due to the oldest active transaction. |
| `pvs_off_row_page_skipped_min_useful_xts` | **bigint** | The number of pages skipped during cleanup due to a long snapshot scan. |
| `pvs_off_row_page_skipped_oldest_snapshot` | **bigint** | The number of pages skipped during cleanup due to online index rebuild activities. |
| `pvs_off_row_page_skipped_oldest_aborted_xdesid` | **bigint** | The number of pages skipped during cleanup due to oldest aborted transactions. Reflects how many pages were skipped during cleanup because they contained row versions for aborted transactions.<br /><br />**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. |

## Permissions

On [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER PERFORMANCE STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerPerformanceStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE PERFORMANCE STATE` permission on the database, or membership in the `##MS_ServerPerformanceStateReader##` server role is required.

## Related content

- [Best practices for accelerated database recovery](../accelerated-database-recovery-concepts.md#best-practices-for-adr)
- [Monitor and troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md)
- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)
