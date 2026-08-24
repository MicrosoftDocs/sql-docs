---
title: "sys.sp_change_feed_enable_db (Transact-SQL)"
description: "The sys.sp_change_feed_enable_db system stored procedure enables the current database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, randolphwest, ajayj
ms.date: 06/19/2026
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_change_feed_enable_db_TSQL"
  - "sys.sp_change_feed_enable_db"
  - "sp_change_feed_enable_db_TSQL"
  - "sp_change_feed_enable_db"
helpviewer_keywords:
  - "sp_change_feed_enable_db"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_change_feed_enable_db (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Enables current database for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_enable_db
    [ [ @maxtrans = ] maxtrans ]
    [ , [ @pollinterval = ] pollinterval ]
    [ , [ @destination_type = ] destination_type ]
[ ; ]
```

## Arguments

#### [ @maxtrans = ] *maxtrans*

Data type is **int**. Indicates the maximum number of transactions to process in each scan cycle.

- For Azure Synapse Link, the default value if not specified is `10000`. If specified, the value must be a positive integer.
- For Fabric mirroring, this value is dynamically determined and automatically set.

#### [ @pollinterval = ] *pollinterval*

Data type is **int**. Describes the frequency, or polling interval, that the log is scanned for any new changes in seconds.

- For Azure Synapse Link, the default interval if not specified is 5 seconds. The value must be `5` or larger.
- For Fabric mirroring, this value is dynamically determined and automatically set.

#### [ @destination_type = ] *destination_type*

**Applies to**: Fabric database mirroring only. For Synapse Link, don't specify.

Data type is **int**. Default is `0`, for Azure Synapse Link. `2` = Fabric database mirroring.

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Examples

The following sample enables the change feed.

```sql
EXECUTE sys.sp_change_feed_enable_db ;
```

Verify the database is enabled.

```sql
SELECT [name],
       is_data_lake_replication_enabled
FROM sys.databases;
```

## Related content

- [sys.sp_change_feed_enable_table (Transact-SQL)](sp-change-feed-enable-table.md)
- [sys.sp_change_feed_create_table_group (Transact-SQL)](sp-change-feed-create-table-group.md)
- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
