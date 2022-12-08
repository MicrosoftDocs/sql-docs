---
title: "sys.dm_exec_sessions (Transact-SQL)"
description: sys.dm_exec_sessions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2019"
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
ms.assetid: 2b7e8e0c-eea0-431e-819f-8ccd12ec8cfa
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||>=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_exec_sessions (Transact-SQL)
[!INCLUDE [sql-asdb-asa-pdw](../../includes/applies-to-version/sql-asdb-asa-pdw.md)]

Returns one row per authenticated session on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. `sys.dm_exec_sessions` is a server-scope view that shows information about all active user connections and internal tasks. This information includes client version, client program name, client login time, login user, current session setting, and more. Use `sys.dm_exec_sessions` to first view the current system load and to identify a session of interest, and then learn more information about that session by using other dynamic management views or dynamic management functions.  

  
The `sys.dm_exec_connections`, `sys.dm_exec_sessions`, and `sys.dm_exec_requests` dynamic management views map to the deprecated [sys.sysprocesses](../../relational-databases/system-compatibility-views/sys-sysprocesses-transact-sql.md) system compatibility view.  

  
> [!NOTE]
> To call this from dedicated SQL pool in [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], see [sys.dm_pdw_nodes_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md). For serverless SQL pool use `sys.dm_exec_sessions`.
  
|Column name|Data type|Description and version-specific information|  
|-----------------|---------------|-----------------|  
|session_id|**smallint**|Identifies the session associated with each active primary connection. Is not nullable.|  
|login_time|**datetime**|Time when session was established. Is not nullable. Sessions that have not completed logging in at the time this DMV is queried are shown with a a login time of `1900-01-01`.|  
|host_name|**nvarchar(128)**|Name of the client workstation that is specific to a session. The value is NULL for internal sessions. Is nullable.<br /><br /> **Security Note:** The client application provides the workstation name and can provide inaccurate data. Do not rely upon HOST_NAME as a security feature.|  
|program_name|**nvarchar(128)**|Name of client program that initiated the session. The value is NULL for internal sessions. Is nullable.|  
|host_process_id|**int**|Process ID of the client program that initiated the session. The value is NULL for internal sessions. Is nullable.|  
|client_version|**int**|TDS protocol version of the interface that is used by the client to connect to the server. The value is NULL for internal sessions. Is nullable.|  
|client_interface_name|**nvarchar(32)**|Name of library/driver being used by the client to communicate with the server. The value is NULL for internal sessions. Is nullable.|  
|security_id|**varbinary(85)**|Microsoft Windows security ID associated with the login. Is not nullable.|  
|login_name|**nvarchar(128)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name under which the session is currently executing. For the original login name that created the session, see original_login_name. Can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name or a Windows authenticated domain user name. Is not nullable.|  
|nt_domain|**nvarchar(128)**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> Windows domain for the client if the session is using Windows Authentication or a trusted connection. This value is NULL for internal sessions and non-domain users. Is nullable.|  
|nt_user_name|**nvarchar(128)**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> Windows user name for the client if the session is using Windows Authentication or a trusted connection. This value is NULL for internal sessions and non-domain users. Is nullable.|  
|status|**nvarchar(30)**|Status of the session. Possible values:<br /><br /> **Running** - Currently running one or more requests<br /><br /> **Sleeping** - Currently running no requests<br /><br /> **Dormant** - Session has been reset because of connection pooling and is now in prelogin state.<br /><br /> **Preconnect** - Session is in the Resource Governor classifier.<br /><br /> Is not nullable.|  
|context_info|**varbinary(128)**|CONTEXT_INFO value for the session. The context information is set by the user by using the [SET CONTEXT_INFO](../../t-sql/statements/set-context-info-transact-sql.md) statement. Is nullable.|  
|cpu_time|**int**|CPU time, in milliseconds, that was used by this session. Is not nullable.|  
|memory_usage|**int**|Number of 8-KB pages of memory used by this session. Is not nullable.|  
|total_scheduled_time|**int**|Total time, in milliseconds, for which the session (requests within) were scheduled for execution. Is not nullable.|  
|total_elapsed_time|**int**|Time, in milliseconds, since the session was established. Is not nullable.|  
|endpoint_id|**int**|ID of the Endpoint associated with the session. Is not nullable.|  
|last_request_start_time|**datetime**|Time at which the last request on the session began. This includes the currently executing request. Is not nullable.|  
|last_request_end_time|**datetime**|Time of the last completion of a request on the session. Is nullable.|  
|reads|**bigint**|Number of reads performed, by requests in this session, during this session. Is not nullable.|  
|writes|**bigint**|Number of writes performed, by requests in this session, during this session. Is not nullable.|  
|logical_reads|**bigint**|Number of logical reads that have been performed on the session. Is not nullable.|  
|is_user_process|**bit**|0 if the session is a system session. Otherwise, it is 1. Is not nullable.|  
|text_size|**int**|TEXTSIZE setting for the session. Is not nullable.|  
|language|**nvarchar(128)**|LANGUAGE setting for the session. Is nullable.|  
|date_format|**nvarchar(3)**|DATEFORMAT setting for the session. Is nullable.|  
|date_first|**smallint**|DATEFIRST setting for the session. Is not nullable.|  
|quoted_identifier|**bit**|QUOTED_IDENTIFIER setting for the session. Is not nullable.|  
|arithabort|**bit**|ARITHABORT setting for the session. Is not nullable.|  
|ansi_null_dflt_on|**bit**|ANSI_NULL_DFLT_ON setting for the session. Is not nullable.|  
|ansi_defaults|**bit**|ANSI_DEFAULTS setting for the session. Is not nullable.|  
|ansi_warnings|**bit**|ANSI_WARNINGS setting for the session. Is not nullable.|  
|ansi_padding|**bit**|ANSI_PADDING setting for the session. Is not nullable.|  
|ansi_nulls|**bit**|ANSI_NULLS setting for the session. Is not nullable.|  
|concat_null_yields_null|**bit**|CONCAT_NULL_YIELDS_NULL setting for the session. Is not nullable.|  
|transaction_isolation_level|**smallint**|Transaction isolation level of the session.<br /><br /> 0 = Unspecified<br /><br /> 1 = ReadUncommitted<br /><br /> 2 = ReadCommitted<br /><br /> 3 = RepeatableRead<br /><br /> 4 = Serializable<br /><br /> 5 = Snapshot<br /><br /> Is not nullable.|  
|lock_timeout|**int**|LOCK_TIMEOUT setting for the session. The value is in milliseconds. Is not nullable.|  
|deadlock_priority|**int**|DEADLOCK_PRIORITY setting for the session. Is not nullable.|  
|row_count|**bigint**|Number of rows returned on the session up to this point. Is not nullable.|  
|prev_error|**int**|ID of the last error returned on the session. Is not nullable.|  
|original_security_id|**varbinary(85)**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows security ID that is associated with the original_login_name. Is not nullable.|  
|original_login_name|**nvarchar(128)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name that the client used to create this session. Can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login name, a Windows authenticated domain user name, or a contained database user. Note that the session could have gone through many implicit or explicit context switches after the initial connection. For example, if [EXECUTE AS](../../t-sql/statements/execute-as-transact-sql.md) is used. Is not nullable.|  
|last_successful_logon|**datetime**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> Time of the last successful logon for the original_login_name before the current session started.|  
|last_unsuccessful_logon|**datetime**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> Time of the last unsuccessful logon attempt for the original_login_name before the current session started.|  
|unsuccessful_logons|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> Number of unsuccessful logon attempts for the original_login_name between the last_successful_logon and login_time.|  
|group_id|**int**|ID of the workload group to which this session belongs. Is not nullable.|  
|database_id|**smallint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> ID of the current database for each session.|  
|authenticating_database_id|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> ID of the database authenticating the principal. For Logins, the value will be 0. For contained database users, the value will be the database ID of the contained database.|  
|open_transaction_count|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Number of open transactions per session.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
|page_server_reads|**bigint**|**Applies to**: Azure SQL Database Hyperscale<br /><br /> Number of page server reads performed, by requests in this session, during this session. Is not nullable.|  
  
## Permissions  
Everyone can see their own session information.  
**[!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]:** Requires `VIEW SERVER STATE` permission on [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] to see all sessions on the server.  
**[!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]:** Requires `VIEW DATABASE STATE` to see all connections to the current database. `VIEW DATABASE STATE` cannot be granted in the `master` database. 
  
  
## Remarks  
 When the **common criteria compliance enabled** server configuration option is enabled, logon statistics are displayed in the following columns.  
  
-   last_successful_logon  
  
-   last_unsuccessful_logon  
  
-   unsuccessful_logons  
  
 If this option is not enabled, these columns will return null values. For more information about how to set this server configuration option, see [common criteria compliance enabled Server Configuration Option](../../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md).  
 
 
 The admin connections on Azure SQL Database will see one row per authenticated session. The "sa" sessions that appear in the resultset, do not have any impact on the user quota for sessions. The non-admin connections will only see information related to their database user sessions.
 
  
## Relationship Cardinalities  
  
|From|To|On/Apply|Relationship|  
|----------|--------|---------------|------------------|  
|sys.dm_exec_sessions|[sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)|session_id|One-to-zero or one-to-many|  
|sys.dm_exec_sessions|[sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)|session_id|One-to-zero or one-to-many|  
|sys.dm_exec_sessions|[sys.dm_tran_session_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-session-transactions-transact-sql.md)|session_id|One-to-zero or one-to-many|  
|sys.dm_exec_sessions|[sys.dm_exec_cursors](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cursors-transact-sql.md)(session_id &#124; 0)|session_id CROSS APPLY<br /><br /> OUTER APPLY|One-to-zero or one-to-many|  
|sys.dm_exec_sessions|[sys.dm_db_session_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md)|session_id|One-to-one|  
  
## Examples  
  
### A. Finding users that are connected to the server  
 The following example finds the users that are connected to the server and returns the number of sessions for each user.  
  
```sql  
SELECT login_name ,COUNT(session_id) AS session_count   
FROM sys.dm_exec_sessions   
GROUP BY login_name;  
```  
  
### B. Finding long-running cursors  
 The following example finds the cursors that have been open for more than a specific period of time, who created the cursors, and what session the cursors are on.  
  
```sql  
USE master;  
GO  
SELECT creation_time ,cursor_id   
    ,name ,c.session_id ,login_name   
FROM sys.dm_exec_cursors(0) AS c   
JOIN sys.dm_exec_sessions AS s   
   ON c.session_id = s.session_id   
WHERE DATEDIFF(mi, c.creation_time, GETDATE()) > 5;  
```  
  
### C. Finding idle sessions that have open transactions  
 The following example finds sessions that have open transactions and are idle. An idle session is one that has no request currently running.  
  
```sql  
SELECT s.*   
FROM sys.dm_exec_sessions AS s  
WHERE EXISTS   
    (  
    SELECT *   
    FROM sys.dm_tran_session_transactions AS t  
    WHERE t.session_id = s.session_id  
    )  
    AND NOT EXISTS   
    (  
    SELECT *   
    FROM sys.dm_exec_requests AS r  
    WHERE r.session_id = s.session_id  
    );  
```  
  
### D. Finding information about a queries own connection  
 Typical query to gather information about a queries own connection.  
  
```sql  
SELECT   
    c.session_id, c.net_transport, c.encrypt_option,   
    c.auth_scheme, s.host_name, s.program_name,   
    s.client_interface_name, s.login_name, s.nt_domain,   
    s.nt_user_name, s.original_login_name, c.connect_time,   
    s.login_time   
FROM sys.dm_exec_connections AS c  
JOIN sys.dm_exec_sessions AS s  
    ON c.session_id = s.session_id  
WHERE c.session_id = @@SPID;  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  


