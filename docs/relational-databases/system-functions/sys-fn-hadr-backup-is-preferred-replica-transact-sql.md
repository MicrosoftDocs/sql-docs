---
title: "sys.fn_hadr_backup_is_preferred_replica  (Transact-SQL)"
description: sys.fn_hadr_backup_is_preferred_replica is used to determine if the current replica is the preferred backup replica.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 11/01/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_hadr_backup_is_preferred_replica_TSQL"
  - "sys.fn_hadr_backup_is_preferred_replica"
  - "fn_hadr_backup_is_preferred_replica_TSQL"
  - "fn_hadr_backup_is_preferred_replica"
helpviewer_keywords:
  - "backup on secondary replicas"
  - "active secondary replicas [SQL Server], backup on secondary replicas"
  - "sys.fn_hadr_backup_is_preferred_replica function"
dev_langs:
  - "TSQL"
---
# sys.fn_hadr_backup_is_preferred_replica (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used to determine if the current replica is the preferred backup replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_hadr_backup_is_preferred_replica ( 'dbname' )
```

## Arguments

#### '*dbname*'

The name of the database to be backed up. *dbname* is type sysname.

## Returns

Returns data type **bit**: `1` if the database on the current instance is on the preferred replica, otherwise `0`.

For databases that aren't part of an availability group, this function always returns `1`.

If the database specified does not exist, the function would return 1 before SQL 2019 CU20 and SQL 2022 CU2. Starting from these 2 versions, the function would return 0 in this scenario. 

## Remarks

Use this function in a backup script to determine if the current database is on the replica that is preferred for backups. You can run a script on every availability replica. Each of these jobs looks at the same data to determine which job should run, so only one of the scheduled jobs actually proceeds to the backup stage. Sample code could be similar to the following.

```sql
IF sys.fn_hadr_backup_is_preferred_replica(@dbname) <> 1
    BEGIN
-- If this is not the preferred replica, exit (probably without error).
        SELECT 'This is not the preferred replica, exiting with success';
    END
-- If this is the preferred replica, continue to do the backup.
/* actual backup command goes here */
```

## Examples

### A. Use sys.fn_hadr_backup_is_preferred_replica

The following example returns 1 if the current database is the preferred backup replica.

```sql
SELECT sys.fn_hadr_backup_is_preferred_replica('TestDB');
GO
```

## Related tasks

- [Configure backups on secondary replicas of an Always On availability group](../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)

## Related content

- [Always On Availability Groups Functions (Transact-SQL)](always-on-availability-groups-functions-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP (Transact-SQL)](../../t-sql/statements/alter-availability-group-transact-sql.md)
- [Offload supported backups to secondary replicas of an availability group](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
