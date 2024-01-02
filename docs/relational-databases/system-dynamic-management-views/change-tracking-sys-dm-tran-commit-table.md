---
title: "sys.dm_tran_commit_table (Transact-SQL)"
description: Displays one row for each transaction that is committed for a table that is tracked by SQL Server change tracking.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_tran_commit_table"
  - "dm_tran_commit_table_TSQL"
  - "sys.dm_tran_commit_table"
  - "sys.dm_tran_commit_table_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_commit_table dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || >=aps-pdw-2016 || =azure-sqldw-latest"
---
# Change tracking - sys.dm_tran_commit_table

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Displays one row for each transaction that is committed for a table that is tracked by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] change tracking. The `sys.dm_tran_commit_table` management view, which is provided for supportability purposes and exposes the transaction-related information that change tracking stores in the `sys.syscommittab` system table. The `sys.syscommittab` table provides an efficient persistent mapping from a database-specific transaction ID to the transaction's commit log sequence number (LSN) and commit timestamp. The data that is stored in the `sys.syscommittab` table and exposed in this management view is subject to cleanup according to the retention period specified when change tracking was configured.

> [!NOTE]  
> To call this from [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_tran_commit_table`. [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

| Column name | Data type | Description |
| --- | --- | --- |
| `commit_ts` | **bigint** | A monotonically increasing number that serves as a database-specific timestamp for each committed transaction. |
| `xdes_id` | **bigint** | A database-specific internal ID for the transaction. |
| `commit_lbn` | **bigint** | The number of the log block that contains the commit log record for the transaction. |
| `commit_csn` | **bigint** | The instance-specific commit sequence number for the transaction. |
| `commit_time` | **datetime** | The time when the transaction was committed. |
| `pdw_node_id` | **int** | **Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />The identifier for the node that this distribution is on. |

> [!NOTE]
> The table `sys.dm_tran_commit_table` will not reflect live changes for read-only users, as `VIEW SERVER STATE` permission is required. The changes remain stored in the rowstore until a `CHECKPOINT` occurs, following which read-only users will see them reflected. This behaviour isn't observed for SA users.

## Related content

- [System dynamic management views](system-dynamic-management-views.md)
- [About Change Tracking (SQL Server)](../track-changes/about-change-tracking-sql-server.md)
