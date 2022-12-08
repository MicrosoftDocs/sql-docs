---
title: "sys.dm_exec_connections (Transact-SQL)"
description: sys.dm_exec_connections returns information about the connections established to this instance of the database engine and the details of each connection.
author: rwestMSFT
ms.author: randolphwest
ms.date: "6/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_connections_TSQL"
  - "sys.dm_exec_connections_TSQL"
  - "sys.dm_exec_connections"
  - "dm_exec_connections"
helpviewer_keywords:
  - "sys.dm_exec_connections dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---
# sys.dm_exec_connections (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns information about the connections established to this instance of the database engine and the details of each connection. Returns server wide connection information for SQL Server and Azure SQL Managed Instance. Returns connection information for the current database in Azure SQL Database. Returns connection information for all databases in the same elastic pool for databases in [elastic pools](/azure/azure-sql/database/elastic-pool-overview) in Azure SQL Database.

> [!NOTE]  
> To call this from dedicated SQL pool in [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], see [sys.dm_pdw_exec_connections (Transact-SQL)](sys-dm-pdw-exec-connections-transact-sql.md). For serverless SQL pool use `sys.dm_exec_connections`.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|session_id|**int**|Identifies the session associated with this connection. Is nullable.|  
|most_recent_session_id|**int**|Represents the session ID for the most recent request associated with this connection. (SOAP connections can be reused by another session.) Is nullable.|  
|connect_time|**datetime**|Timestamp when connection was established. Is not nullable.|  
|net_transport|**nvarchar(40)**|When MARS is used, returns **Session** for each additional connection associated with a MARS logical session.<br /><br /> **Note:** Describes the physical transport protocol that is used by this connection. Is not nullable.|  
|protocol_type|**nvarchar(40)**|Specifies the protocol type of the payload. It currently distinguishes between TDS ("TSQL"), "SOAP", and "Database Mirroring". Is nullable.|  
|protocol_version|**int**|Version of the data access protocol associated with this connection. Is nullable.|  
|endpoint_id|**int**|An identifier that describes what type of connection it is. This `endpoint_id` can be used to query the `sys.endpoints` view. Is nullable.|  
|encrypt_option|**nvarchar(40)**|Boolean value to describe whether encryption is enabled for this connection. Is not nullable.|  
|auth_scheme|**nvarchar(40)**|Specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Windows Authentication scheme used with this connection. Is not nullable.|  
|node_affinity|**smallint**|Identifies the memory node to which this connection has affinity. Is not nullable.|  
|num_reads|**int**|Number of byte reads that have occurred over this connection. Is nullable.|  
|num_writes|**int**|Number of byte writes that have occurred over this connection. Is nullable.|  
|last_read|**datetime**|Timestamp when last read occurred over this connection. Is nullable.|  
|last_write|**datetime**|Timestamp when last write occurred over this connection. Not Is nullable.|  
|net_packet_size|**int**|Network packet size used for information and data transfer. Is nullable.|  
|client_net_address|**varchar(48)**|Host address of the client connecting to this server. Is nullable.|  
|client_tcp_port|**int**|Port number on the client computer that is associated with this connection. Is nullable.<br /><br /> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], this column always returns NULL.|  
|local_net_address|**varchar(48)**|Represents the IP address on the server that this connection targeted. Available only for connections using the TCP transport provider. Is nullable.<br /><br /> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], this column always returns NULL.|  
|local_tcp_port|**int**|Represents the server TCP port that this connection targeted if it were a connection using the TCP transport. Is nullable.<br /><br /> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], this column always returns NULL.|  
|connection_id|**uniqueidentifier**|Identifies each connection uniquely. Is not nullable.|  
|parent_connection_id|**uniqueidentifier**|Identifies the primary connection that the MARS session is using. Is nullable.|  
|most_recent_sql_handle|**varbinary(64)**|The SQL handle of the last request executed on this connection. The `most_recent_sql_handle` column is always in sync with the `most_recent_session_id` column. Is nullable.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On Azure SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Physical joins 
 
:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-exec-connections-1.svg" alt-text="Diagram of physical joins for sys.dm_exec_connections.":::
  
## Relationship cardinalities  
  
| First element | Second element | Relationship |
| --------------| -------------- | ------------ |  
|[sys.dm_exec_sessions](sys-dm-exec-sessions-transact-sql.md).`session_id`|`sys.dm_exec_connections.session_id`|One-to-zero or one-to-many|  
|[sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md).`connection_id`|`sys.dm_exec_connections.connection_id`|Many-to-one|  
|[sys.dm_broker_connections](sys-dm-broker-connections-transact-sql.md).`connection_id`|`sys.dm_exec_connections.connection_id`|One-to-one|  

Most commonly, for each row in `sys.dm_exec_connections` there is a single matching row in `sys.dm_exec_sessions`. However, in some cases such as system internal sessions or [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) activation procedures, there may be a row in `sys.dm_exec_sessions` without a matching row in `sys.dm_exec_connections`.

When MARS is used, there may be multiple rows in `sys.dm_exec_connections` for a row in `sys.dm_exec_sessions`, one row for the parent connection, and one row for each MARS logical session. The latter rows can be identified by the value in the `net_transport` column being set to **Session**. For these connections, the value in the `connection_id` column of `sys.dm_exec_connections` matches the value in the `connection_id` column of `sys.dm_exec_requests` for MARS requests in progress.

## Examples

The following Transact-SQL query gathers information about a query's own connection.  
  
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
  
## Next steps

Learn more about related concepts in the following articles:

- [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)
- [sys.dm_exec_sessions (Transact-SQL)](sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_exec_sql_text (Transact-SQL)](sys-dm-exec-sql-text-transact-sql.md)
- [sys.dm_pdw_exec_connections (Transact-SQL)](sys-dm-pdw-exec-connections-transact-sql.md)
