---
title: "sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)"
description: sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)
author: dimitri-furman
ms.author: dfurman
ms.date: 10/29/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_tran_active_snapshot_database_transactions_TSQL"
  - "dm_tran_active_snapshot_database_transactions"
  - "sys.dm_tran_active_snapshot_database_transactions"
  - "dm_tran_active_snapshot_database_transactions_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_active_snapshot_database_transactions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

This dynamic management view returns information about transactions registered for row versioning and assigned a transaction sequence number (XSN). Transactions can be included under one or more of the following conditions:

- When either or both `ALLOW_SNAPSHOT_ISOLATION` and `READ_COMMITTED_SNAPSHOT` database options are `ON`:

    - Transactions that establish a snapshot under snapshot isolation or read committed isolation using row versioning.

    - Transactions that generate row versions, for example by updating or deleting rows in a database with these options enabled.

- Transactions that generate or access row versions for triggers.

- Transactions that use row versioning during online index operations.

- Transactions that use row versioning in a Multiple Active Result Sets (MARS) session.

- In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later versions, transactions that modify data in a database with accelerated database recovery (ADR) enabled, even when both snapshot database options are `OFF`.

This dynamic management view doesn't include system transactions. Completed transactions can appear briefly during concurrent enumeration or transaction cleanup.

> [!NOTE]  
>  To call this from [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], use the name **sys.dm_pdw_nodes_tran_active_snapshot_database_transactions**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
## Syntax  
  
```
sys.dm_tran_active_snapshot_database_transactions  
```  
  
## Table Returned  
  
| Column name | Data type | Description |
| --- | --- | --- |
| `transaction_id` | `bigint` | Unique identification number assigned to the transaction. The transaction ID is primarily used to identify the transaction in locking operations. |
| `transaction_sequence_num` | `bigint` | Unique sequence number assigned when the transaction first requires row versioning. Transactions that neither generate row versions nor use snapshot scans aren't assigned a transaction sequence number. |
| `commit_sequence_num` | `bigint` | Always NULL in this view. |
| `session_id` | `int` | ID of the session associated with the transaction. |
| `is_snapshot` | `bit` | 0 = Is not a snapshot isolation transaction.<br /><br />1 = Is a snapshot isolation transaction. |
| `first_snapshot_sequence_num` | `bigint` | Lowest transaction sequence number recorded as active in the snapshots currently held by the transaction. Includes transaction-level and statement-level snapshots. NULL if no such sequence number is recorded. |
| `max_version_chain_traversed` | `int` | Maximum number of row versions traversed in a single version chain by the transaction. Returns 0 if no row versions are traversed. |
| `average_version_chain_traversed` | `float` | Average number of row versions traversed per version-chain traversal by the transaction. Traversals of length 0 are excluded. Returns 0 if no row versions are traversed. |
| `elapsed_time_seconds` | `bigint` | Elapsed time, in seconds, since the transaction obtained its transaction sequence number. |
| `pdw_node_id` | `int` | **Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]<br /><br /> The identifier for the node that this distribution is on. |
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

`sys.dm_tran_active_snapshot_database_transactions` reports transactions assigned a transaction sequence number (XSN) for row versioning. An XSN is assigned when the transaction first requires row versioning, not necessarily when it starts or physically accesses the version store.

In a database with `ALLOW_SNAPSHOT_ISOLATION` or `READ_COMMITTED_SNAPSHOT` enabled, examples include:

- A transaction running under serializable isolation is assigned an XSN when it first generates row versions, for example through an `UPDATE` or `DELETE` operation.

- A transaction running under snapshot isolation is assigned an XSN when it first establishes a snapshot for data access, for example through a `SELECT` that reads a user table. `BEGIN TRANSACTION` alone does not assign an XSN.

XSNs are allocated from an instance-wide increasing counter. The same counter is used for statement-level snapshots, so transaction XSNs need not be consecutive.

## Examples

The following example illustrates four concurrent transactions in a database with `ALLOW_SNAPSHOT_ISOLATION` and `READ_COMMITTED_SNAPSHOT` set to `ON`:

- XSN-57 executes an `UPDATE` under serializable isolation and remains uncommitted.
- XSN-58 executes another `UPDATE` under serializable isolation and remains uncommitted.
- XSN-59 executes a `SELECT` under snapshot isolation and remains open.
- XSN-60 executes another `SELECT` under snapshot isolation and remains open.

Both snapshot transactions establish their snapshots while XSN-57 and XSN-58 are active. Their `SELECT` statements read earlier versions of rows modified by the uncommitted updates.

The following query is executed:

```sql
SELECT transaction_id,
       transaction_sequence_num,
       commit_sequence_num,
       is_snapshot,
       session_id,
       first_snapshot_sequence_num,
       max_version_chain_traversed,
       average_version_chain_traversed,
       elapsed_time_seconds
FROM sys.dm_tran_active_snapshot_database_transactions
ORDER BY transaction_sequence_num;
```

The following output is illustrative. Actual identifiers, sequence numbers, statistics, and elapsed times vary.

```output
transaction_id  transaction_sequence_num  commit_sequence_num  is_snapshot  session_id  first_snapshot_sequence_num  max_version_chain_traversed  average_version_chain_traversed  elapsed_time_seconds
--------------  ------------------------  -------------------  -----------  ----------  ---------------------------  ---------------------------  -------------------------------  --------------------
9295            57                        NULL                 0            54          NULL                         0                            0                                419
9324            58                        NULL                 0            53          NULL                         0                            0                                397
9387            59                        NULL                 1            52          57                           1                            1                                359
9400            60                        NULL                 1            51          57                           1                            1                                333
```

The results show the following:

- XSN-57 and XSN-58 use serializable isolation, so `is_snapshot = 0`. The `UPDATE` statements executed within these transactions generate row versions because the database's snapshot options are `ON`. This version generation causes transaction sequence numbers to be assigned. Neither transaction holds a snapshot in this example, so `first_snapshot_sequence_num` is NULL.

- XSN-59 and XSN-60 have `is_snapshot = 1`. Their `first_snapshot_sequence_num` value of `57` identifies the oldest transaction recorded as active when their snapshots were established.

- For XSN-59 and XSN-60, the maximum and average version-chain traversal lengths are both `1`. Each recorded traversal visited one row version. The average is calculated over traversals that visit at least one row version.

- XSN-59 and XSN-60 read earlier committed versions of the rows modified by XSN-57 and XSN-58, which were still uncommitted when both snapshots were established.
  
## Related content

- [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Transaction Related Dynamic Management Views and Functions (Transact-SQL)](transaction-related-dynamic-management-views-and-functions-transact-sql.md)
