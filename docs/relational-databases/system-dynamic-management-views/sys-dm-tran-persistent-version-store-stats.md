---
title: "sys.dm_tran_persistent_version_store_stats (Transact-SQL)"
description: The sys.dm_tran_persistent_version_store_stats dynamic management view returns information for accelerated database recovery (ADR) persistent version store (PVS) metrics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 06/10/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats_TSQL"
  - "dm_tran_persistent_version_store_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_persistent_version_store_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# sys.dm_tran_persistent_version_store_stats (Transact-SQL)
[!INCLUDE [SQL Server 2019, ASDB, ASDBMI](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

  Returns information for accelerated database recovery (ADR) persistent version store (PVS) metrics. 
    
## Table returned  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id  |  int|  The `database_id` of this row.  |
|pvs_filegroup_id  |  smallint |  The filegroup that hosts PVS version store. |
|persistent_version_store_size_kb|    bigint | The PVS size in kilobytes. This value is used to determine current space used by PVS. |
|online_index_version_store_size_kb|    bigint | The special version store size, in kilobytes, used during online index rebuild.|
|current_aborted_transaction_count|    bigint | The number of abort transactions in the database. Detail of the abort transactions can be viewed in `sys.dm_tran_aborted_transactions`. |
|oldest_active_transaction_id|    bigint | The transaction ID of the oldest active transaction.  |
|oldest_aborted_transaction_id|    bigint | The transaction ID of the oldest abort transaction. If the PVS cleaner cannot remove the aborted transaction, this value will reflect the oldest value.|
|min_transaction_timestamp|    bigint | The minimum useful timestamp in the system from snapshot scans.  |
|online_index_min_transaction_timestamp|    bigint | The minimum useful timestamp in the system to hold up the PVS cleanup. This corresponds to `online_index_version_store_size_kb`. |
|secondary_low_water_mark|    bigint | The low water mark aggregated for queries on readable secondaries. It is a transaction ID and can be used to compare with `oldest_active_transaction_id` and `oldest_aborted_transaction_id`. |
|offrow_version_cleaner_start_time   | datetime2(7) |  The start time of the off-row PVS cleanup process. |
|offrow_version_cleaner_end_time   | datetime2(7) | The last end time of the off-row PVS cleanup process. |
|aborted_version_cleaner_start_time   | datetime2(7) | The start timestamp of a full sweep. |
|aborted_version_cleaner_end_time   | datetime2(7) |  The end timestamp of last full sweep. If start time has value but the end time does not, it means PVS cleanup is ongoing on this database. |
|pvs_off_row_page_skipped_low_water_mark|    bigint | The number of pages skipped for reclaim due to hold up from secondary read queries.  |
|pvs_off_row_page_skipped_transaction_not_cleaned|    bigint | The number of pages skipped for reclaim due to aborted transactions. Note this value does not reflect the PVS hold up from aborted transactions since the version cleaner uses a min threshold for aborted transaction version cleanup. This can be ignored for large PVS issue. |
|pvs_off_row_page_skipped_oldest_active_xdesid|    bigint | The number of pages skipped for reclaim due to the oldest active transaction. |
|pvs_off_row_page_skipped_min_useful_xts|    bigint | The number of pages skipped for reclaim due to a long snapshot scan. |
|pvs_off_row_page_skipped_oldest_snapshot|    bigint | The number of pages skipped for reclaim due to online index rebuild activities. This is not common for PVS usage. |
|pvs_off_row_page_skipped_oldest_aborted_xdesid|bigint| **Applies to: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.**<BR />The number of pages skipped for reclaim due to oldest aborted transactions. If the version cleaner is slow or invalidated, this will reflect how many pages must be kept for aborted transactions. |
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks   

Review [Best practices for accelerated database recovery](../accelerated-database-recovery-management.md#best-practices-for-accelerated-database-recovery). If your ADR PVS is growing, see [Troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md).

## See also

- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)
- [Troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md)
