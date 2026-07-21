---
title: "ALTER PARTITION SCHEME (Transact-SQL)"
description: "ALTER PARTITION SCHEME adds a filegroup to a partition scheme or alters the designation of the NEXT USED filegroup for the partition scheme."
author: markingmyname
ms.author: maghan
ms.date: 09/26/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ALTER PARTITION SCHEME"
  - "ALTER_PARTITION_SCHEME_TSQL"
helpviewer_keywords:
  - "ALTER PARTITION SCHEME statement"
  - "partition schemes [SQL Server], modifying"
  - "modifying partition schemes"
  - "adding filegroups"
  - "NEXT USED filegroups"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# ALTER PARTITION SCHEME (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Adds a filegroup to a partition scheme or alters the designation of the `NEXT USED` filegroup for the partition scheme.

Learn more about filegroups and partitioning strategies in [Filegroups](../../relational-databases/partitions/partitioned-tables-and-indexes.md#filegroups).

> [!NOTE]
> In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], only primary filegroups are supported.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER PARTITION SCHEME partition_scheme_name
NEXT USED [ filegroup_name ] [ ; ]
```

## Arguments

#### *partition_scheme_name*

The name of the partition scheme to be altered.

#### *filegroup_name*

Specifies the filegroup to be marked by the partition scheme as `NEXT USED`. This means the filegroup accepts a new partition that is created by using an [ALTER PARTITION FUNCTION](alter-partition-function-transact-sql.md) statement.

In a partition scheme, only one filegroup can be designated `NEXT USED`. A filegroup that is not empty can be specified. If *filegroup_name* is specified and there currently is no filegroup marked `NEXT USED`, *filegroup_name* is marked `NEXT USED`. If *filegroup_name* is specified, and a filegroup with the `NEXT USED` property already exists, the `NEXT USED` property transfers from the existing filegroup to *filegroup_name*.

If *filegroup_name* is not specified and a filegroup with the `NEXT USED` property already exists, that filegroup loses its `NEXT USED` state so that there are no `NEXT USED` filegroups in *partition_scheme_name*.

If *filegroup_name* is not specified, and there are no filegroups marked `NEXT USED`, `ALTER PARTITION SCHEME` returns a warning.

## Remarks

Any filegroup affected by `ALTER PARTITION SCHEME` must be online.

## Permissions

The following permissions can be used to execute `ALTER PARTITION SCHEME`:

- `ALTER ANY DATASPACE` permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.

- `CONTROL` or `ALTER` permission on the database in which the partition scheme was created.

- `CONTROL SERVER` or `ALTER ANY DATABASE` permission on the server of the database in which the partition scheme was created.

## Examples

The following example assumes the partition scheme `MyRangePS1` and the filegroup `test5fg` exist in the current database.

```sql
ALTER PARTITION SCHEME MyRangePS1
NEXT USED test5fg;
```

Filegroup `test5fg` receives any additional partition of a partitioned table or index as a result of an `ALTER PARTITION FUNCTION` statement.

## Related content

- [CREATE PARTITION SCHEME (Transact-SQL)](create-partition-scheme-transact-sql.md)
- [Modify a partition function](../../relational-databases/partitions/modify-a-partition-function.md)
- [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)
- [Create partitioned tables and indexes](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md)
- [Modify a partition scheme](../../relational-databases/partitions/modify-a-partition-scheme.md)
