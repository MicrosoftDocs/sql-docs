---
title: "sys.dm_exec_sessions (Transact-SQL)"
description: sys.dm_exec_sessions returns one row per authenticated session on SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_sessions_TSQL"
  - "sys.dm_exec_sessions"
  - "dm_exec_sessions"
  - "sys.dm_exec_sessions_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_sessions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || >=aps-pdw-2016 || =azure-sqldw-latest || =fabric"
---
# sys.dm_exec_sessions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns one row per authenticated session on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. `sys.dm_exec_sessions` is a server-scope view that shows information about all active user connections and internal tasks. This information includes client version, client program name, client login time, login user, current session setting, and more. Use `sys.dm_exec_sessions` to first view the current system load and to identify a session of interest, and then learn more information about that session by using other dynamic management views or dynamic management functions.

The `sys.dm_exec_connections`, `sys.dm_exec_sessions`, and `sys.dm_exec_requests` dynamic management views map to the deprecated [sys.sysprocesses](../system-compatibility-views/sys-sysprocesses-transact-sql.md) system compatibility view.

> [!NOTE]  
> To call this from dedicated SQL pool in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], see [sys.dm_pdw_nodes_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md). For serverless SQL pool or [!INCLUDE [fabric](../../includes/fabric.md)] use `sys.dm_exec_sessions`.

| Column name | Data type | Description and version-specific information |
| --- | --- | --- |
| `session_id` | **smallint** | Identifies the session associated with each active primary connection. Not nullable. |
| `login_time` | **datetime** | Time when session was established. Not nullable. Sessions that haven't completely logged in at the time this DMV is queried, are shown with a login time of `1900-01-01`. |
| `host_name` | **nvarchar(128)** | Name of the client workstation that is specific to a session. The value is `NULL` for internal sessions. Nullable.<br /><br />**Security note:** The client application provides the workstation name and can provide inaccurate data. Don't rely on `HOST_NAME` as a security feature. |
| `program_name` | **nvarchar(128)** | Name of client program that initiated the session. The value is `NULL` for internal sessions. Nullable. |
| `host_process_id` | **int** | Process ID of the client program that initiated the session. The value is `NULL` for internal sessions. Nullable. |
| `client_version` | **int** | TDS protocol version of the interface that is used by the client to connect to the server. The value is `NULL` for internal sessions. Nullable. |
| `client_interface_name` | **nvarchar(32)** | Name of library/driver being used by the client to communicate with the server. The value is `NULL` for internal sessions. Nullable. |
| `security_id` | **varbinary(85)** | Windows security ID associated with the login. Not nullable. |
| `login_name` | **nvarchar(128)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login name under which the session is currently executing. For the original login name that created the session, see `original_login_name`. Can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name or a Windows authenticated domain user name. Not nullable. |
| `nt_domain` | **nvarchar(128)** | Windows domain for the client if the session is using Windows Authentication or a trusted connection. This value is `NULL` for internal sessions and non-domain users. Nullable. |
| `nt_user_name` | **nvarchar(128)** | Windows user name for the client if the session is using Windows Authentication or a trusted connection. This value is `NULL` for internal sessions and non-domain users. Nullable. |
| `status` | **nvarchar(30)** | Status of the session. Possible values:<br /><br />`Running` - Currently running one or more requests<br />`Sleeping` - Currently running no requests<br />`Dormant` - Session was reset because of connection pooling and is now in prelogin state.<br />`Preconnect` - Session is in the Resource Governor classifier.<br /><br />Not nullable. |
| `context_info` | **varbinary(128)** | `CONTEXT_INFO` value for the session. The context information is set by the user by using the [SET CONTEXT_INFO](../../t-sql/statements/set-context-info-transact-sql.md) statement. Nullable. |
| `cpu_time` | **int** | CPU time, in milliseconds, used by this session. Not nullable. |
| `memory_usage` | **int** | Number of 8-KB pages of memory used by this session. Not nullable. |
| `total_scheduled_time` | **int** | Total time, in milliseconds, for which the session (requests within) were scheduled for execution. Not nullable. |
| `total_elapsed_time` | **int** | Time, in milliseconds, since the session was established. Not nullable. |
| `endpoint_id` | **int** | ID of the endpoint associated with the session. Not nullable. |
| `last_request_start_time` | **datetime** | Time at which the last request on the session began. This time includes the currently executing request. Not nullable. |
| `last_request_end_time` | **datetime** | Time of the last completion of a request on the session. Nullable. |
| `reads` | **bigint** | Number of reads performed, by requests in this session, during this session. Not nullable. |
| `writes` | **bigint** | Number of writes performed, by requests in this session, during this session. Not nullable. |
| `logical_reads` | **bigint** | Number of logical reads performed, by requests in this session, during this session. Not nullable. |
| `is_user_process` | **bit** | `0` if the session is a system session. Otherwise, it's `1`. Not nullable. |
| `text_size` | **int** | `TEXTSIZE` setting for the session. Not nullable. |
| `language` | **nvarchar(128)** | `LANGUAGE` setting for the session. Nullable. |
| `date_format` | **nvarchar(3)** | `DATEFORMAT` setting for the session. Nullable. |
| `date_first` | **smallint** | `DATEFIRST` setting for the session. Not nullable. |
| `quoted_identifier` | **bit** | `QUOTED_IDENTIFIER` setting for the session. Not nullable. |
| `arithabort` | **bit** | `ARITHABORT` setting for the session. Not nullable. |
| `ansi_null_dflt_on` | **bit** | `ANSI_NULL_DFLT_ON` setting for the session. Not nullable. |
| `ansi_defaults` | **bit** | `ANSI_DEFAULTS` setting for the session. Not nullable. |
| `ansi_warnings` | **bit** | `ANSI_WARNINGS` setting for the session. Not nullable. |
| `ansi_padding` | **bit** | `ANSI_PADDING` setting for the session. Not nullable. |
| `ansi_nulls` | **bit** | `ANSI_NULLS` setting for the session. Not nullable. |
| `concat_null_yields_null` | **bit** | `CONCAT_NULL_YIELDS_NULL` setting for the session. Not nullable. |
| `transaction_isolation_level` | **smallint** | Transaction isolation level of the session.<br /><br />`0` = `Unspecified`<br />`1` = `ReadUncommitted`<br />`2` = `ReadCommitted`<br />`3` = `RepeatableRead`<br />`4` = `Serializable`<br />`5` = `Snapshot`<br /><br />Not nullable. |
| `lock_timeout` | **int** | `LOCK_TIMEOUT` setting for the session. The value is in milliseconds. Not nullable. |
| `deadlock_priority` | **int** | `DEADLOCK_PRIORITY` setting for the session. Not nullable. |
| `row_count` | **bigint** | Number of rows returned on the session up to this point. Not nullable. |
| `prev_error` | **int** | ID of the last error returned on the session. Not nullable. |
| `original_security_id` | **varbinary(85)** | Windows security ID that is associated with the `original_login_name`. Not nullable. |
| `original_login_name` | **nvarchar(128)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login name that the client used to create this session. Can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name, a Windows authenticated domain user name, or a contained database user. The session could have gone through many implicit or explicit context switches after the initial connection, for example, if [EXECUTE AS](../../t-sql/statements/execute-as-transact-sql.md) is used. Not nullable. |
| `last_successful_logon` | **datetime** | Time of the last successful logon for the `original_login_name` before the current session started. |
| `last_unsuccessful_logon` | **datetime** | Time of the last unsuccessful logon attempt for the `original_login_name` before the current session started. |
| `unsuccessful_logons` | **bigint** | Number of unsuccessful logon attempts for the `original_login_name` between the `last_successful_logon` and `login_time`. |
| `group_id` | **int** | ID of the workload group to which this session belongs. Not nullable. |
| `database_id` | **smallint** | ID of the current database for each session.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `authenticating_database_id` | **int** | ID of the database authenticating the principal. For logins, the value is `0`. For contained database users, the value is the database ID of the contained database.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `open_transaction_count` | **int** | Number of open transactions per session.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. |
| `pdw_node_id` | **int** | The identifier for the node that this distribution is on.<br /><br />**Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]. |
| `page_server_reads` | **bigint** | Number of page server reads performed, by requests in this session, during this session. Not nullable.<br /><br />**Applies to**: Azure SQL Database Hyperscale. |

## Permissions

Everyone can see their own session information.

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, requires `VIEW SERVER STATE` to see all sessions on the server. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

In [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)], requires `VIEW DATABASE STATE` to see all connections to the current database. `VIEW DATABASE STATE` can't be granted in the `master` database.

## Remarks

When the `common criteria compliance enabled` server configuration option is enabled, logon statistics are displayed in the following columns.

- `last_successful_logon`
- `last_unsuccessful_logon`
- `unsuccessful_logons`

If this option isn't enabled, these columns return null values. For more information about how to set this server configuration option, see [Server configuration: common criteria compliance enabled](../../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md).

The admin connections on Azure SQL Database see one row per authenticated session. The `sa` sessions that appear in the resultset, don't have any effect on the user quota for sessions. The non-admin connections only see information related to their database user sessions.

Because of differences in how they're recorded, `open_transaction_count` might not match `sys.dm_tran_session_transactions`.`open_transaction_count`.

## Relationship cardinalities

| From | To | On/Apply | Relationship |
| --- | --- | --- | --- |
| `sys.dm_exec_sessions` | [sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_exec_connections](sys-dm-exec-connections-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_tran_session_transactions](sys-dm-tran-session-transactions-transact-sql.md) | `session_id` | One-to-zero or one-to-many |
| `sys.dm_exec_sessions` | [sys.dm_exec_cursors](sys-dm-exec-cursors-transact-sql.md) (`session_id` \| `0`) | `session_id CROSS APPLY`<br /><br />`OUTER APPLY` | One-to-zero or one-to-many |
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
GO
```

### C. Find idle sessions that have open transactions

The following example finds sessions that have open transactions and are idle. An idle session is one that has no request currently running.

```sql
SELECT s.*
FROM sys.dm_exec_sessions AS s
WHERE EXISTS (
        SELECT *
        FROM sys.dm_tran_session_transactions AS t
        WHERE t.session_id = s.session_id
    )
    AND NOT EXISTS (
        SELECT *
        FROM sys.dm_exec_requests AS r
        WHERE r.session_id = s.session_id
    );
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

- [System dynamic management views](system-dynamic-management-views.md)
- [Execution Related Dynamic Management Views and Functions (Transact-SQL)](execution-related-dynamic-management-views-and-functions-transact-sql.md)
