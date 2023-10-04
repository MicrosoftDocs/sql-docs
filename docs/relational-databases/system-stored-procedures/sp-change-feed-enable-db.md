---
title: "sp_change_feed_enable_db (Transact-SQL)"
description: "The sp_change_feed_enable_db system stored procedure enables the current database for Azure Synapse Link publishing."
author: IdrisMotiwala
ms.author: imotiwala
ms.reviewer: wiassaf, randolphwest
ms.date: 06/13/2023
ms.service: synapse-analytics
ms.topic: "reference"
f1_keywords:
  - "sp_change_feed_enable_db_TSQL"
  - "sp_change_feed_enable_db"
helpviewer_keywords:
  - "sp_change_feed_enable_db"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_enable_db (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Enables current database for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

> [!NOTE]  
> This stored procedure is used internally and is not recommended for direct administrative use. Use Synapse Studio instead. Using this procedure will introduce inconsistency with Synapse Workspace configuration.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_change_feed_enable_db
    [ [ @maxtrans = ] max_trans ]
    [ , [ @pollinterval = ] polling_interval ]
GO
```

## Arguments

#### [ [ @maxtrans = ] *max_trans* ]

Data type is **int**. Indicates the maximum number of transactions to process in each scan cycle. Default value if not specified is `500`. If specified, the value must be a positive integer.

#### [ @pollinterval = ] *polling_interval*

Data type is **int**. Describes the frequency that the log is scanned for any new changes in seconds. Default interval if not specified is 5 seconds. The value must be `5` or larger.

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## See also

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
