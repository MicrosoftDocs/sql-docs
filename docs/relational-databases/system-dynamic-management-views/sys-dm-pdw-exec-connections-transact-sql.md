---
title: "sys.dm_pdw_exec_connections (Transact-SQL)"
description: sys.dm_pdw_exec_connections returns information about the connections established to this instance of Azure Synapse Analytics and the details of each connection.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest"
---
# sys.dm_pdw_exec_connections (Transact-SQL)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Returns information about the connections established to this instance of [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and the details of each connection.

> [!NOTE]  
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] For serverless SQL pool use [sys.dm_exec_connections](sys-dm-exec-connections-transact-sql.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `session_id` | **int** | Identifies the session associated with this connection. Use [SESSION_ID](../../t-sql/functions/session-id-transact-sql.md) to return the `session_id` of the current connection. |
| `connect_time` | **datetime** | Timestamp when connection was established. Not nullable. |
| `encrypt_option` | **nvarchar(40)** | Indicates `TRUE` (connection is encrypted) or `FALSE` (connection isn't encrypted). |
| `auth_scheme` | **nvarchar(40)** | Specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Windows authentication scheme used with this connection. Not nullable. |
| `client_id` | **varchar(48)** | IP address of the client connecting to this server. Nullable. |
| `sql_spid` | **int** | The server process ID of the connection. Use `@@SPID` to return the `sql_spid` of the current connection. For most purposes, use the `session_id` instead. |

## Permissions

Requires `VIEW SERVER STATE` permission on the server.

## Relationship cardinalities

| From | To | Relationship |
| --- | --- | --- |
| `dm_pdw_exec_sessions.session_id` | `dm_pdw_exec_connections.session_id` | One-to-one |
| `dm_pdw_exec_requests.connection_id` | `dm_pdw_exec_connections.connection_id` | Many to one |

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following Transact-SQL example is a typical query to gather information about a query's own connection.

```sql
SELECT
    c.session_id, c.encrypt_option,
    c.auth_scheme, s.client_id, s.login_name,
    s.status, s.query_count
FROM sys.dm_pdw_exec_connections AS c
JOIN sys.dm_pdw_exec_sessions AS s
    ON c.session_id = s.session_id
WHERE c.session_id = SESSION_ID();
```

## Related content

- [SQL and Parallel Data Warehouse Dynamic Management Views](sql-and-parallel-data-warehouse-dynamic-management-views.md)
