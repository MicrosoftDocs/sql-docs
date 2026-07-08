---
title: "sys.dm_db_xtp_undeploy_status (Transact-SQL)"
description: sys.dm_db_xtp_undeploy_status reports the status of the In-Memory OLTP database engine when removing the engine from a database.
author: dimitri-furman
ms.author: dfurman
ms.date: 04/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_undeploy_status"
  - "sys.dm_db_xtp_undeploy_status_TSQL"
  - "dm_db_xtp_undeploy_status"
  - "dm_db_xtp_undeploy_status_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_undeploy_status dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17"
ms.custom:
  - build-2025
---

# sys.dm_db_xtp_undeploy_status (Transact-SQL)

[!INCLUDE [sqlserver2025-and-later](../../includes/applies-to-version/sqlserver2025-and-later.md)]

Returns a single row reflecting the status of the [!INCLUDE [inmemory](../../includes/inmemory-md.md)] (XTP) database engine when removing the engine from a database.

XTP engine removal, or undeployment, is a multi-step process initiated by the `ALTER DATABASE ... REMOVE FILE` statement that removes the last remaining memory-optimized container from the [memory-optimized filegroup](../in-memory-oltp/the-memory-optimized-filegroup.md). The `sys.dm_db_xtp_undeploy_status` view reports the current step in the process. It can be used to monitor and troubleshoot memory-optimized container and filegroup removal.

For more information and to review a step-by-step process, see [Memory-optimized container and filegroup removal](../in-memory-oltp/memory-optimized-container-filegroup-removal.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `deployment_state` | **int** | The current state of the XTP engine:<br /><br />`0` - XTP engine is not deployed.<br /><br />The XTP engine has never been deployed in this database or has already been removed.<br /><br />`1` - XTP engine is ready (version-deployed).<br /><br />Version-deployed means that a memory-optimized filegroup and container exist, memory-optimized tables or other XTP objects don't exist but can be created, and XTP checkpoints aren't running. `ALTER DATABASE ... REMOVE FILE` can be executed to remove all memory-optimized containers including the last remaining container.<br /><br />`2` - XTP engine is ready (checkpoint-deployed).<br /><br />Checkpoint-deployed means that memory-optimized tables or other XTP objects exist (or existed in the past), and XTP checkpoints are running. `ALTER DATABASE ... REMOVE FILE` can be executed to start removing the last remaining memory-optimized container.<br /><br />`3` - Waiting for the start of log to advance past undeploy LSN.<br /><br />XTP undeployment is in progress. Manual checkpoints using `CHECKPOINT` and backups using `BACKUP DATABASE` and `BACKUP LOG` can be executed to advance the `start_of_log_lsn` value past the `undeploy_lsn` value.<br /><br />`4` - Waiting for the final undeployment log record.<br /><br />XTP undeployment is in progress. After the start of log LSN advances past the undeploy LSN, including on all availability group replicas, the primary replica creates the final undeploy log record. After the final log record is applied, the subsequent checkpoint will undeploy the XTP engine.<br /><br />`5` - Waiting for an XTP checkpoint to complete XTP undeployment.<br /><br />XTP undeployment is in progress. The final XTP checkpoint can be started manually using `CHECKPOINT`, or will occur automatically when the transaction log grows over a certain threshold. For more information, see [Checkpoint operation for memory-optimized tables](../in-memory-oltp/checkpoint-operation-for-memory-optimized-tables.md).<br /><br />`6` - Ready to remove the last memory-optimized container.<br /><br />XTP undeployment is in progress. This state can be reached if the `ALTER DATABASE ... REMOVE FILE` statement to remove the last memory-optimized container has been aborted before XTP undeployment completed. The statement can be executed again to remove the last container and complete XTP undeployment. |
| `undeploy_lsn` | **numeric(25,0)** | The log sequence number (LSN) indicating the start of XTP engine removal from the database.<br /><br />A log record with this LSN is logged after a `ALTER DATABASE ... REMOVE FILE` statement is executed for the first time for the last remaining memory-optimized container to start the XTP engine undeployment process, and an [XTP checkpoint](../in-memory-oltp/checkpoint-operation-for-memory-optimized-tables.md) occurs. Prior to the checkpoint, the reported value is 0. |
| `start_of_log_lsn` | **numeric(25,0)** | The start LSN of the active portion of the transaction log. |
| `deployment_state_desc` | **nvarchar(60)** | The description of the current state of the XTP engine. |

## Permissions

Requires `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Related content

- [Memory-optimized container and filegroup removal](../in-memory-oltp/memory-optimized-container-filegroup-removal.md)
- [In-Memory OLTP System Views (Transact-SQL)](memory-optimized-table-dynamic-management-views-transact-sql.md)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
