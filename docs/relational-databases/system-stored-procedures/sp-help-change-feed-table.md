---
title: "sp_help_change_feed_table (Transact-SQL)"
description: "The sp_help_change_feed_table system stored procedure provides the provision or deprovision flow status of Azure Synapse Link for SQL."
author: im-microsoft
ms.author: imotiwala
ms.reviewer: wiassaf, randolphwest
ms.date: 06/13/2023
ms.service: synapse-analytics
ms.topic: "reference"
f1_keywords:
  - "sp_help_change_feed_table_TSQL"
  - "sp_help_change_feed_table"
helpviewer_keywords:
  - "sp_help_change_feed_table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_help_change_feed_table (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Provides the provision or deprovision status and information of the [Azure Synapse Link for SQL](../../sql-server/synapse-link/synapse-link-sql-server-change-feed.md) table group and table metadata. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_help_change_feed_table;
```

## Arguments

#### @table_group_id

The unique identifier of the table group

#### @table_id

The source table identifier

#### @source_schema

The source table schema name

#### @source_name

The source table name

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| **table_group_id** | **uniqueidentifier** | The unique identifier of the table group. |
| **table_group_name** | **nvarchar(140)** | The name of the table group. |
| **schema_name** | **sysname** | The schema name of the original table |
| **table_name** | **sysname** | The name of the original table. |
| **table_id** | **uniqueidentifier** | The source table identifier. |
| **destination_location** | **nvarchar(512)** | URL string of the landing zone folder. |
| **workspace_id** | **nvarchar(247)** | The related Synapse workspace Azure resource ID. |
| **state** | **nvarchar(50)** | The current state of the Azure Synapse Link table. |
| **table_object_id** | **int** | The object ID of the change feed table. |

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## See also

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
