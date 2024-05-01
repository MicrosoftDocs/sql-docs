---
title: "sp_reset_dtc_log (Transact-SQL)"
description: "sp_reset_dtc_log (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current"
---
# sp_reset_dtc_log (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asmi.md)]

Clears the Microsoft Distributed Transaction Coordinator (MSDTC) log.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_reset_dtc_log
[ ; ]
```

## Arguments

There are no arguments for this stored procedure.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Requires **sysadmin** or have CONTROL SERVER permissions.

## Examples

```sql
EXEC sp_reset_dtc_log;
```

## Related content

- [sys.sp_manage_distributed_transaction (Transact-SQL)](sys-sp-manage-distributed-transaction.md)
- [sys.dm_tran_distributed_transaction_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-distributed-transaction-stats.md)
