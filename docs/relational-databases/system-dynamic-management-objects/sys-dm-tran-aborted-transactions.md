---
title: "sys.dm_tran_aborted_transactions (Transact-SQL)"
description: sys.dm_tran_aborted_transactions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/03/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_tran_aborted_transactions"
  - "sys.dm_tran_aborted_transactions"
  - "sys.dm_tran_aborted_transactions_TSQL"
  - "dm_tran_aborted_transactions_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_aborted_transactions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15 || =azuresqldb-mi-current || =azuresqldb-current || =fabric-sqldb"
---

# sys.dm_tran_aborted_transactions (Transact-SQL)

[!INCLUDE [SQL Server 2019, ASDB, ASDBMI-fabricsqldb](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-fabricsqldb.md)]

Returns information about unresolved, aborted transactions in the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance.

## Table returned

|Column name|Data type|Description|
|:--|:--|:--|
| `transaction_id` | **int** | The `transaction_id` of the aborted transaction. |
| `database_id` | **int** | The `database_id` of the aborted transaction.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `begin_xact_lsn` | **numeric(25,0)** | The starting LSN of the aborted transaction. |
| `end_xact_lsn` | **numeric(25,0)** | The ending LSN of the aborted transaction. |
| `begin_time` | **datetime** | The begin time of the aborted transaction. |
| `nest_aborted` | **bit** | When 1, indicates that the transaction has a nested aborted transaction. |

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

The `sys.dm_tran_aborted_transactions` DMV shows all aborted transactions in the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance. The `nest_aborted` column indicates that the transaction has been committed or is active, but there are portions (savepoints or nested transactions) that aborted. This can block the PVS cleanup process while the transaction remains active. For more information, see [Monitor and troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md).

Row versions created by unresolved, aborted transactions are removed by persistent version store (PVS) cleanup.

## Related content

- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../accelerated-database-recovery-management.md)
- [Monitor and troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md)

