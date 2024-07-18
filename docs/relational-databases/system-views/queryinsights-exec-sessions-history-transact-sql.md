---
title: "queryinsights.exec_sessions_history (Transact-SQL)"
description: "The queryinsights.exec_sessions_history in Microsoft Fabric provides information about each user session."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali
ms.date: 07/17/2024
ms.service: sql
ms.topic: "reference"
f1_keywords:
  - "queryinsights.exec_sessions_history"
  - "queryinsights.exec_sessions_history_TSQL"
helpviewer_keywords:
  - "queryinsights.exec_sessions_history system view"
  - "queryinsights.exec_sessions_history"
  - "query insights frequently_run_queries"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# queryinsights.exec_sessions_history (Transact-SQL)

[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

  The `queryinsights.exec_sessions_history` in [!INCLUDE [fabric](../../includes/fabric.md)] provides information about each completed session.

| **Column name** | **Data type** | **Description** |
| :-- | :-- | :-- |
| `session_id` | **smallint** | Identifies the session associated with each active primary connection. It is not nullable. |
| `connection_id` | **uniqueidentifier** | Identifies each connection uniquely. It is not nullable. |
| `session_start_time` | **datetime2** | Time when session was established. It is not nullable. |
| `session_end_time` | **datetime2** | Time when the session was disconnected. Sessions that have not completed at the time this view is queried are shown with a value of `1900-01-01`.|
| `program_name` | **varchar(128)** | Name of client program that initiated the session. The value is `NULL` for internal sessions. Is nullable. |
| `login_name` | **varchar(128)** | SQL Server login name under which the session is currently executing. For the original login name that created the session, see `original_login_name`. Can be a SQL Server authenticated login name or a Windows authenticated domain username. It is not nullable. |
| `status` | **varchar(30)** | Status of the session. Values:<br />**Running** - Currently running one or more requests<br />**Sleeping** - Currently running no requests<br />**Dormant** - Session reset because of connection pooling and is now in prelogin state.<br />**Preconnect** - Session is in the Resource Governor classifier.<br />Is not nullable. |
| `context_info` | **varbinary(128)** | `CONTEXT_INFO` value for the session. The context information is set by the user with [SET CONTEXT_INFO](/sql/t-sql/statements/set-context-info-transact-sql?view=azure-sqldw-latest&preserve-view=true). Is nullable. |
| `total_query_elapsed_time_ms` | **int** | Total time, in milliseconds, for which the session (requests within) was scheduled/executed for execution. It is not nullable. |
| `last_request_start_time` | **datetime2** | Time at which the last request on the session began, including the currently executing request. It is not nullable. |
| `last_request_end_time` | **datetime2** | Time of the last completion of a request on the session. Is nullable. |
| `is_user_process` | **bit** | `0` if the session is a system session. Otherwise, it is `1`. It is not nullable. |
| `prev_error` | **int** | ID of the last error returned on the session. It is not nullable. |
| `group_id` | **int** | ID of the workload group to which this session belongs. It is not nullable. |
| `database_id` | **smallint** | ID of the current database for each session. |
| `authenticating_database_id` | **int** | ID of the database authenticating the principal. For logins, the value is `0`. For contained database users, the value is the database ID of the contained database. |
| `open_transaction_count` | **int** | Number of open transactions per session. |
| `text_size` | **int** | `TEXTSIZE` setting for the session. It is not nullable. |
| `language` | **varchar(128)** | `LANGUAGE` setting for the session. Is nullable. |
| `date_format` | **varchar(3)** | `DATEFORMAT` setting for the session. Is nullable. |
| `date_first` | **smallint** | `DATEFIRST` setting for the session. It is not nullable. |
| `quoted_identifier` | **bit** | `QUOTED_IDENTIFIER` setting for the session. It is not nullable. |
| `arithabort` | **bit** | `ARITHABORT` setting for the session. It is not nullable. |
| `ansi_null_dflt_on` | **bit** | `ANSI_NULL_DFLT_ON` setting for the session. It is not nullable. |
| `ansi_defaults` | **bit** | `ANSI_DEFAULTS` setting for the session. It is not nullable. |
| `ansi_warnings` | **bit** | `ANSI_WARNINGS` setting for the session. It is not nullable. |
| `ansi_padding` | **bit** | `ANSI_PADDING` setting for the session. It is not nullable. |
| `ansi_nulls` | **bit** | `ANSI_NULLS` setting for the session. It is not nullable. |
| `concat_null_yields_null` | **bit** | `CONCAT_NULL_YIELDS_NULL` setting for the session. It is not nullable. |
| `transaction_isolation_level` | **smallint** | Transaction isolation level of the session.<br />`0` = Unspecified<br />`1` = ReadUncommitted<br />`2` = ReadCommitted<br />`3` = RepeatableRead<br />`4` = Serializable<br />`5` = Snapshot<br />Is not nullable. |
| `lock_timeout` | **int** | `LOCK_TIMEOUT` setting for the session. The value is in milliseconds. It is not nullable. |
| `deadlock_priority` | **int** | `DEADLOCK_PRIORITY` setting for the session. It is not nullable. |
| `original_security_id` | **varbinary(85)** | [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows security ID that is associated with the `original_login_name`. Not nullable. |
| `database_name` | **varchar(128)** | Name of the current database for each session. |

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or above permissions.

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitoring connections, sessions, and requests using DMVs in Microsoft Fabric](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-requests-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)
