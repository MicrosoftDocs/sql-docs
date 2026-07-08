---
title: "In-Memory OLTP System Views (Transact-SQL)"
description: Learn about dynamic management views and object catalog views used with In-Memory OLTP.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - build-2025
---

# In-Memory OLTP System Views (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

For more information, see [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md).

### Dynamic management views

The following dynamic management views (DMVs) are used with In-Memory OLTP:

- [sys.dm_xtp_gc_queue_stats](sys-dm-xtp-gc-queue-stats-transact-sql.md)
- [sys.dm_xtp_gc_stats](sys-dm-xtp-gc-stats-transact-sql.md)
- [sys.dm_xtp_system_memory_consumers](sys-dm-xtp-system-memory-consumers-transact-sql.md)
- [sys.dm_xtp_transaction_stats](sys-dm-xtp-transaction-stats-transact-sql.md)
- [sys.dm_db_xtp_checkpoint_files](sys-dm-db-xtp-checkpoint-files-transact-sql.md)
- [sys.dm_db_xtp_checkpoint_stats](sys-dm-db-xtp-checkpoint-stats-transact-sql.md)
- [sys.dm_db_xtp_gc_cycle_stats](sys-dm-db-xtp-gc-cycle-stats-transact-sql.md)
- [sys.dm_db_xtp_hash_index_stats](sys-dm-db-xtp-hash-index-stats-transact-sql.md)
- [sys.dm_db_xtp_index_stats](sys-dm-db-xtp-index-stats-transact-sql.md)
- [sys.dm_db_xtp_memory_consumers](sys-dm-db-xtp-memory-consumers-transact-sql.md)
- [sys.dm_db_xtp_merge_requests](system-dynamic-management-objects.md)
- [sys.dm_db_xtp_nonclustered_index_stats](sys-dm-db-xtp-nonclustered-index-stats-transact-sql.md)
- [sys.dm_db_xtp_object_stats](sys-dm-db-xtp-object-stats-transact-sql.md)
- [sys.dm_db_xtp_table_memory_stats](sys-dm-db-xtp-table-memory-stats-transact-sql.md)
- [sys.dm_db_xtp_transactions](sys-dm-db-xtp-transactions-transact-sql.md)
- [sys.dm_db_xtp_undeploy_status](sys-dm-db-xtp-undeploy-status.md)

### Object catalog views

The following object catalog views are used with In-Memory OLTP.

- [sys.hash_indexes](../system-catalog-views/sys-hash-indexes-transact-sql.md)
- [sys.memory_optimized_tables_internal_attributes](../system-catalog-views/sys-memory-optimized-tables-internal-attributes-transact-sql.md)

### Internal DMVs

There are additional DMVs that are intended for internal use only, and for which we provide no direct documentation. In the area of memory-optimized tables, undocumented DMVs include the following:

- `sys.dm_xtp_threads`
- `sys.dm_xtp_transaction_recent_rows`
