---
title: "sys.sp_manage_distributed_transaction (Transact-SQL)"
description: sp_manage_distributed_transaction commits, aborts, or forgets a specified transaction.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current"
---
# sys.sp_manage_distributed_transaction (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asmi.md)]

`sp_manage_distributed_transaction` commits, aborts, or forgets a specified transaction.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_manage_distributed_transaction
    @transaction_uow = 'transaction_ID'
    , @operation = 'value'
[ ; ]
```

## Arguments

#### @transaction_uow = '*transaction_id*'

Specifies the MSDTC transaction ID (transaction unit of work). *@transaction_uow* is **uniqueidentifier**.

#### @operation = '*value*'

Specifies operation to perform. Valid values are `commit`, `abort`, or `forget`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Requires **sysadmin** fixed server role, or have CONTROL SERVER permissions.

## Examples

```sql
EXEC sys.sp_manage_distributed_transaction
    @transaction_uow = '1101AD68-43A7-4DC5-B06C-2B4BEF230643',
    @operation = N'commit'
```

## Related content

- [sys.dm_tran_distributed_transaction_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-distributed-transaction-stats.md)
- [sp_reset_dtc_log (Transact-SQL)](sp-reset-dtc-log.md)
