---
description: "sys.dm_tran_persistent_version_store_stats (Transact-SQL)"
title: "sys.dm_tran_persistent_version_store_stats (Transact-SQL)"
ms.custom: ""
ms.date: "01/21/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats"
  - "sys.dm_tran_persistent_version_store_stats_TSQL"
  - "dm_tran_persistent_version_store_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_tran_persistent_version_store_stats dynamic management view"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# sys.dm_tran_persistent_version_store_stats (Transact-SQL)
[!INCLUDE [SQL Server 2019, ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-asa.md)]

  Returns information for accelerated database recovery (ADR) persistent version store (PVS) metrics. 
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id  |  int|  The database id of this row.  |
|pvs_filegroup_id  |  smallint |  The file group id which hosts PVS version store. By default it is the primary file group ID. On SQL on-premise, we recommend use a separate file group for PVS on a fast storage. |
|persistent_version_store_size_kb|    bigint | The PVS size in kilobytes. This is value used most time to determine current space used by PVS. |
|online_index_version_store_size_kb|    bigint | This is special version store size in kilobytes used during online index rebuild. It should be small at most time. |
|current_aborted_transaction_count|    bigint | The number of abort transactions in the database. Detail of the abort transactions can be viewed in `sys.dm_tran_aborted_transactions`. The number of abort transaction shall be reasonable value. It can be very large if transaction keeps aborting, such as millions.  |
|oldest_active_transaction_id|    bigint | The transaction ID of the oldest active transaction. A long active transaction could hold up the PVS version cleanup. |
|oldest_aborted_transaction_id|    bigint | The transaction ID of the oldest abort transaction. If version cleaner not able to remove aborted transactions, this value will reflect the oldest value. It could hold up PVS version clean up as well. |
|min_transaction_timestamp|    bigint | The minimum useful timestamp in the system from snapshot scans. Note this is not transaction id. Both transaction id and timestamp are used in version cleanup.  |
|online_index_min_transaction_timestamp|    bigint | The minimum useful timestamp in the system to hold up the long term version store cleanup. This is corresponding to online_index_version_store_size_kb. |
|secondary_low_water_mark|    bigint | The low water mark aggregated for queries on readable secondaries. One common reason for PVS cleanup is secondary_low_water_mark is too small which means secondaries are holding up the version. It is a transaction id and can be used to compare with oldest_active_transaction_id and oldest_aborted_transaction_id. |
|offrow_version_cleaner_start_time   | datetime2(7) |  The start time of the off-row PVS clean up process. |
|offrow_version_cleaner_end_time   | datetime2(7) | The last end time of the off-row PVS cleanup process. |
|aborted_version_cleaner_start_time   | datetime2(7) | The start timestamp of a full sweep. |
|aborted_version_cleaner_end_time   | datetime2(7) |  The end timestamp of last full sweep. If start time has value but end time not, it means a sweep is ongoing on this db. |
|pvs_off_row_page_skipped_low_water_mark|    bigint | How many pages skipped for reclaim due to hold up from secondary read queries.  |
|pvs_off_row_page_skipped_transaction_not_cleaned|    bigint | How many pages skipped for reclaim due to it is belong to aborted transactions. Note this value does not reflect the PVS hold up from aborted transactions since the version cleaner uses a min threshold for aborted transaction version cleanup. This can be ignored for large PVS issue. The min threshold hold up from abort transaction will be added later to this DMV. |
|pvs_off_row_page_skipped_oldest_active_xdesid|    bigint | How many pages skipped for reclaim due to there is a long oldest active transaction. The version needs to be kept for oldest active transactions for various reasons.   |
|pvs_off_row_page_skipped_min_useful_xts|    bigint | How many pages skipped for reclaim due to there is a long snapshot scan which can read old versions. |
|pvs_off_row_page_skipped_oldest_snapshot|    bigint | How many pages skipped for reclaim due to online index rebuild activities. This is not common for PVS usage. |
|||
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks   
  
## Examples

For example usage in troubleshooting, see [Troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshooting.md).

## See also

- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)
