---
title: "sys.dm_tran_active_transactions (Transact-SQL)"
description: sys.dm_tran_active_transactions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_tran_active_transactions"
  - "sys.dm_tran_active_transactions_TSQL"
  - "dm_tran_active_transactions_TSQL"
  - "dm_tran_active_transactions"
helpviewer_keywords:
  - "sys.dm_tran_active_transactions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.dm_tran_active_transactions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  The `sys.dm_tran_active_transactions` dynamic management view returns information about transactions for the instance.

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
| `transaction_id` |**bigint**|ID of the transaction at the instance level, not the database level. It is only unique across all databases within an instance but not unique across all server instances.|  
| `name` |**nvarchar(32)**|Transaction name. This is overwritten if the transaction is marked and the marked name replaces the transaction name.|  
| `transaction_begin_time` |**datetime**|Time that the transaction started.|  
| `transaction_type` |**int**|Type of transaction.<br /><br /> 1 = Read/write transaction<br /><br /> 2 = Read-only transaction<br /><br /> 3 = System transaction<br /><br /> 4 = Distributed transaction|  
| `transaction_uow` |**uniqueidentifier**|Transaction unit of work (UOW) identifier for distributed transactions. The Microsoft distributed transaction coordinator (MS DTC) uses the UOW identifier to work with the distributed transaction.|
| `transaction_state` |**int**|0 = The transaction has not been completely initialized yet.<br /><br /> 1 = The transaction has been initialized but is not started.<br /><br /> 2 = The transaction is active.<br /><br /> 3 = The transaction has ended. Used for read-only transactions.<br /><br /> 4 = The commit process has been initiated on the distributed transaction. For distributed transactions only. The distributed transaction is still active but further processing cannot take place.<br /><br /> 5 = The transaction is in a prepared state and waiting resolution.<br /><br /> 6 = The transaction has been committed.<br /><br /> 7 = The transaction is being rolled back.<br /><br /> 8 = The transaction has been rolled back.|  
| `transaction_status` |**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
| `transaction_status2` |**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
| `dtc_state` |**int**|**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].<br /><br /> 1 = ACTIVE<br /><br /> 2 = PREPARED<br /><br /> 3 = COMMITTED<br /><br /> 4 = ABORTED<br /><br /> 5 = RECOVERED|  
| `dtc_status` |**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
| `dtc_isolation_level` |**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
| `filestream_transaction_id` |**varbinary(128)**|**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
| `pdw_node_id` |**int**|**Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

In Microsoft Fabric, membership in the **Contributor** [workspace role](/fabric/fundamentals/roles-workspaces#-workspace-roles) or more privileged role is needed to query `sys.dm_tran_active_transactions`.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Remarks

To call this dynamic management view from [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_tran_active_transactions` [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 

## Examples

<a id="a-using-sysdm_tran_active_transactions-with-other-dmvs-to-find-information-about-active-transactions"></a>

### A. Use sys.dm_tran_active_transactions with other DMVs to find information about active transactions

 The following example shows any active transactions on the system. The query provides detailed information about the transaction, the user session, the application that submitted, and the query that started it and many others.  

```sql  
SELECT
  GETDATE() as now,
  DATEDIFF(SECOND, transaction_begin_time, GETDATE()) as tran_elapsed_time_seconds,
  st.session_id,
  txt.text, 
  *
FROM
  sys.dm_tran_active_transactions at
  INNER JOIN sys.dm_tran_session_transactions st ON st.transaction_id = at.transaction_id
  LEFT OUTER JOIN sys.dm_exec_sessions sess ON st.session_id = sess.session_id
  LEFT OUTER JOIN sys.dm_exec_connections conn ON conn.session_id = sess.session_id
    OUTER APPLY sys.dm_exec_sql_text(conn.most_recent_sql_handle)  AS txt
ORDER BY
  tran_elapsed_time_seconds DESC;
```

## Related content

- [sys.dm_tran_session_transactions (Transact-SQL)](sys-dm-tran-session-transactions-transact-sql.md)
- [sys.dm_tran_database_transactions (Transact-SQL)](sys-dm-tran-database-transactions-transact-sql.md)
- [Dynamic Management Views and Functions (Transact-SQL)](system-dynamic-management-objects.md)
- [Transaction Related Dynamic Management Views and Functions (Transact-SQL)](transaction-related-dynamic-management-views-and-functions-transact-sql.md)
