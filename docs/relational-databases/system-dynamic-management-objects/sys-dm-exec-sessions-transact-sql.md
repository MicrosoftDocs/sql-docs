---
title: "sys.dm_exec_sessions (Transact-SQL)"
description: sys.dm_exec_sessions returns one row per authenticated session on SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/10/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_exec_sessions_TSQL"
  - "sys.dm_exec_sessions"
  - "dm_exec_sessions"
  - "sys.dm_exec_sessions_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_sessions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || >=aps-pdw-2016 || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# sys.dm_exec_sessions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns one row per authenticated session on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. `sys.dm_exec_sessions` is a server-scope view that shows information about all active user connections and internal tasks. This information includes client version, client program name, client login time, login user, current session setting, and more. Use `sys.dm_exec_sessions` to first view the current system load and to identify a session of interest, and then learn more information about that session by using other dynamic management views or dynamic management functions.

The `sys.dm_exec_connections`, `sys.dm_exec_sessions`, and `sys.dm_exec_requests` dynamic management views map to the deprecated [sys.sysprocesses](../system-compatibility-views/sys-sysprocesses-transact-sql.md) system compatibility view.

> [!NOTE]  
> To call this view from [!INCLUDE [ssazuresynapse_sqlpool_only](../../includes/ssazuresynapse_sqlpool_only.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], see [sys.dm_pdw_nodes_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md). Use `sys.dm_exec_sessions` for [!INCLUDE [ssazuresynapse-svrless-sqlpool-only](../../includes/ssazuresynapse-svrless-sqlpool-only.md)] or [!INCLUDE [fabric](../../includes/fabric.md)].

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `session_id` | **smallint** | No | Identifies the session associated with each active primary connection. |
| `login_time` | **datetime** | No | Time when session was established. Sessions that haven't completely logged in at the time this DMV is queried, are shown with a login time of `1900-01-01`. |
| `host_name` | **nvarchar(128)** | Yes | Name of the client workstation that's specific to a session. The value is `NULL` for internal sessions.<br /><br />**Security note:** The client application provides the workstation name and can provide inaccurate data. Don't rely on `HOST_NAME` as a security feature. |
| `program_name` | **nvarchar(128)** | Yes | Name of client program that initiated the session. The value is `NULL` for internal sessions. |
| `host_process_id` | **int** | Yes | Process ID of the client program that initiated the session. The value is `NULL` for internal sessions. |
| `client_version` | **int** | Yes | TDS protocol version of the interface used by the client to connect to the server. The value is `NULL` for internal sessions. |
| `client_interface_name` | **nvarchar(32)** | Yes | Name of library/driver being used by the client to communicate with the server. The value is `NULL` for internal sessions. |
| `security_id` | **varbinary(85)** | No | Windows security ID associated with the login. |
| `login_name` | **nvarchar(128)** | No | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login name under which the session is currently executing. For the original login name that created the session, see `original_login_name`. Can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name or a Windows authenticated domain user name. |
| `nt_domain` | **nvarchar(128)** | Yes | Windows domain for the client if the session is using Windows Authentication or a trusted connection. This value is `NULL` for internal sessions and non-domain users. |
| `nt_user_name` | **nvarchar(128)** | Yes | Windows user name for the client if the session is using Windows Authentication or a trusted connection. This value is `NULL` for internal sessions and non-domain users. |
| `status` | **nvarchar(30)** | No | Status of the session. Possible values:<br /><br />`Running` - Currently running one or more requests<br />`Sleeping` - Currently running no requests<br />`Dormant` - Session was reset because of connection pooling and is now in prelogin state.<br />`Preconnect` - Session is in the Resource Governor classifier. |
| `context_info` | **varbinary(128)** | Yes | `CONTEXT_INFO` value for the session. The context information is set by the user by using the [SET CONTEXT_INFO](../../t-sql/statements/set-context-info-transact-sql.md) statement. |
| `cpu_time` | **int** | No | CPU time, in milliseconds, used by this session. |
| `memory_usage` | **int** | No | Number of 8-KB pages of memory used by this session. |
| `total_scheduled_time` | **int** | No | Total time, in milliseconds, for which the session (requests within) were scheduled for execution. |
| `total_elapsed_time` | **int** | No | Time, in milliseconds, since the session was established. |
| `endpoint_id` | **int** | No | ID of the endpoint associated with the session. |
| `last_request_start_time` | **datetime** | No | Time at which the last request on the session began. This time includes the currently executing request. |
| `last_request_end_time` | **datetime** | Yes | Time of the last completion of a request on the session. |
| `reads` | **bigint** | No | Number of physical reads performed, by requests in this session, during this session. |
| `writes` <sup>1</sup> | **bigint** | No | Number of physical writes performed, by requests in this session, during this session. |
| `logical_reads` | **bigint** | No | Number of logical reads performed, by requests in this session, during this session. |
| `is_user_process` | **bit** | No | `0` if the session is a system session. Otherwise, it's `1`. |
| `text_size` | **int** | No | `TEXTSIZE` setting for the session. |
| `language` | **nvarchar(128)** | Yes | `LANGUAGE` setting for the session. |
| `date_format` | **nvarchar(3)** | Yes | `DATEFORMAT` setting for the session. |
| `date_first` | **smallint** | No | `DATEFIRST` setting for the session. |
| `quoted_identifier` | **bit** | No | `QUOTED_IDENTIFIER` setting for the session. |
| `arithabort` | **bit** | No | `ARITHABORT` setting for the session. |
| `ansi_null_dflt_on` | **bit** | No | `ANSI_NULL_DFLT_ON` setting for the session. |
| `ansi_defaults` | **bit** | No | `ANSI_DEFAULTS` setting for the session. |
| `ansi_warnings` | **bit** | No | `ANSI_WARNINGS` setting for the session. |
| `ansi_padding` | **bit** | No | `ANSI_PADDING` setting for the session. |
| `ansi_nulls` | **bit** | No | `ANSI_NULLS` setting for the session. |
| `concat_null_yields_null` | **bit** | No | `CONCAT_NULL_YIELDS_NULL` setting for the session. |
| `transaction_isolation_level` | **smallint** | No | Transaction isolation level of the session.<br /><br />`0` = `Unspecified`<br />`1` = `ReadUncommitted`<br />`2` = `ReadCommitted`<br />`3` = `RepeatableRead`<br />`4` = `Serializable`<br />`5` = `Snapshot` |
| `lock_timeout` | **int** | No | `LOCK_TIMEOUT` setting for the session. The value is in milliseconds. |
| `deadlock_priority` | **int** | No | `DEADLOCK_PRIORITY` setting for the session. |
| `row_count` | **bigint** | No | Number of rows returned on the session up to this point. |
| `prev_error` | **int** | No | ID of the last error returned on the session. |
| `original_security_id` | **varbinary(85)** | No | Windows security ID that is associated with the `original_login_name`. |
| `original_login_name` | **nvarchar(128)** | No | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login name that the client used to create this session. Can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name, a Windows authenticated domain user name, or a contained database user. The session might have been through many implicit or explicit context switches after the initial connection, for example, if [EXECUTE AS](../../t-sql/statements/execute-as-transact-sql.md) is used. |
| `last_successful_logon` | **datetime** | Yes | Time of the last successful logon for the `original_login_name` before the current session started. |
| `last_unsuccessful_logon` | **datetime** | Yes | Time of the last unsuccessful logon attempt for the `original_login_name` before the current session started. |
| `unsuccessful_logons` | **bigint** | Yes | Number of unsuccessful logon attempts for the `original_login_name` between the `last_successful_logon` and `login_time`. |
| `group_id` | **int** | No | ID of the workload group to which this session belongs. |
| `database_id` | **smallint** | No | ID of the current database for each session.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `authenticating_database_id` | **int** | Yes | ID of the database authenticating the principal. For logins, the value is `0`. For contained database users, the value is the database ID of the contained database.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `open_transaction_count` | **int** | No | Number of open transactions per session.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `pdw_node_id` | **int** | No | The identifier for the node that this distribution is on.<br /><br />**Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]. |
| `page_server_reads` | **bigint** | No | Number of page server reads performed, by requests in this session, during this session.<br /><br />**Applies to**: Azure SQL Database Hyperscale. |
| `contained_availability_group_id` | **uniqueidentifier** | Yes | ID of the contained availability group.<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. |

<sup>1</sup> Specifies when a page is marked dirty in the buffer pool. This value doesn't directly equate to actual writes, because the same page can be marked more than once. These counters are aggregated at the end of the batch.

## Permissions

Everyone can see their own session information.

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, requires `VIEW SERVER STATE` to see all sessions on the server. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

In [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)], requires `VIEW DATABASE STATE` to see all connections to the current database. `VIEW DATABASE STATE` can't be granted in the `master` database.

## Remarks

When the `common criteria compliance enabled` server configuration option is enabled, logon statistics are displayed in the following columns.

- `last_successful_logon`
- `last_unsuccessful_logon`
- `unsuccessful_logons`

If this option isn't enabled, these columns return null values. For more information about how to set this server configuration option, see [Enable common criteria compliance configuration](../../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md).

The admin connections on Azure SQL Database see one row per authenticated session. The `sa` sessions that appear in the resultset, don't have any effect on the user quota for sessions. The non-admin connections only see information related to their database user sessions.

Because of differences in how they're recorded, `open_transaction_count` might not match `sys.dm_tran_session_transactions`.`open_transaction_count`.

## Relationship cardinalities

| From | To | On/Apply | Relationship |
| --- | --- | --- | --- |
| `sys.dm_exec_sessions` | [sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_exec_connections](sys-dm-exec-connections-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_tran_session_transactions](sys-dm-tran-session-transactions-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_exec_cursors](sys-dm-exec-cursors-transact-sql.md) (`session_id` \| `0`) | `session_id CROSS APPLY`<br />`OUTER APPLY` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_db_session_space_usage](sys-dm-db-session-space-usage-transact-sql.md) | `session_id` | One-to-one |

## Examples

### A. Find users that are connected to the server

The following example finds the users that are connected to the server and returns the number of sessions for each user.

```sql
SELECT login_name,
       COUNT(session_id) AS session_count
FROM sys.dm_exec_sessions
GROUP BY login_name;
```

### B. Find long-running cursors

The following example finds the cursors that were open for more than a specific period of time, who created the cursors, and what session the cursors are on.

```sql
USE master;
GO

SELECT creation_time,
       cursor_id,
       name,
       c.session_id,
       login_name
FROM sys.dm_exec_cursors(0) AS c
     INNER JOIN sys.dm_exec_sessions AS s
         ON c.session_id = s.session_id
WHERE DATEDIFF(mi, c.creation_time, GETDATE()) > 5;
```

### C. Find idle sessions that have open transactions

The following example finds sessions that have open transactions and are idle. An idle session is one that has no request currently running.

```sql
SELECT s.*
FROM sys.dm_exec_sessions AS s
WHERE EXISTS (SELECT *
              FROM sys.dm_tran_session_transactions AS t
              WHERE t.session_id = s.session_id)
      AND NOT EXISTS (SELECT *
                      FROM sys.dm_exec_requests AS r
                      WHERE r.session_id = s.session_id);
```

### D. Find information about a query's own connection

The following example gathers information about a query's own connection:

```sql
SELECT c.session_id,
       c.net_transport,
       c.encrypt_option,
       c.auth_scheme,
       s.host_name,
       s.program_name,
       s.client_interface_name,
       s.login_name,
       s.nt_domain,
       s.nt_user_name,
       s.original_login_name,
       c.connect_time,
       s.login_time
FROM sys.dm_exec_connections AS c
     INNER JOIN sys.dm_exec_sessions AS s
         ON c.session_id = s.session_id
WHERE c.session_id = @@SPID;
```

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Execution Related Dynamic Management Views and Functions (Transact-SQL)](execution-related-dynamic-management-views-and-functions-transact-sql.md)
