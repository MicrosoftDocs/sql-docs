---
title: "sp_change_feed_configure_parameters (Transact-SQL)"
description: "The sp_change_feed_configure_parameters system stored procedure is used to reduce latency or reduce the cost by increasing the batch size with higher transactions"
author: IdrisMotiwala
ms.author: imotiwala
ms.reviewer: wiassaf, randolphwest
ms.date: 06/13/2023
ms.service: synapse-analytics
ms.topic: "reference"
f1_keywords:
  - "sp_change_feed_configure_parameters_TSQL"
  - "sp_change_feed_configure_parameters"
helpviewer_keywords:
  - "sp_change_feed_configure_parameters"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_configure_parameters (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Used to reduce latency – by decreasing change batch size with maxtrans or to reduce the cost by increasing the batch size, as the batch size increases less IO operation will be performed between Azure SQL DB and the Landing Zone (LZ). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

> [!NOTE]  
> This stored procedure is used to fine tune the operational performance for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_configure_parameters
    [ [ @maxtrans = ] max_trans ]
    [ , [ @pollinterval = ] polling_interval ]
[ ; ]
```

## Arguments

#### [ @maxtrans = ] *max_trans*

Data type is **int**. Indicates the maximum number of transactions to process in each scan cycle. Default value if not specified is `500`. If specified, the value must be a positive integer.

#### [ @pollinterval = ] *polling_interval*

Data type is **int**. Describes the frequency that the log is scanned for any new changes, in seconds. Default interval if not specified is 5 seconds. The value must be `5` or larger.

## Result set

`0` (success) or `1` (failure).

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## See also

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
