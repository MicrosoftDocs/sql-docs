---
title: "sp_syscollector_update_collection_set (Transact-SQL)"
description: Used to modify the properties of a user-defined collection set or to rename a user-defined collection set.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_update_collection_set_TSQL"
  - "sp_syscollector_update_collection_set"
helpviewer_keywords:
  - "sp_syscollector_update_collection_set"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_update_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used to modify the properties of a user-defined collection set or to rename a user-defined collection set.

> [!WARNING]  
> In cases where the Windows account configured as a proxy is a non-interactive or interactive user that has not yet logged in, the profile directory will not exist, and the creation of the staging directory will fail. Therefore, if you are using a proxy account on a domain controller, you must specify an interactive account that has been used at least once in order to assure that the profile directory has been created.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_update_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @target = ] N'target' ]
    [ , [ @collection_mode = ] collection_mode ]
    [ , [ @days_until_expiration = ] days_until_expiration ]
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @schedule_uid = ] 'schedule_uid' ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @logging_level = ] logging_level ]
    [ , [ @description = ] N'description' ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, and must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, and must have a value if *@collection_set_id* is `NULL`.

#### [ @new_name = ] N'*new_name*'

The new name for the collection set. *@new_name* is **sysname**, with a default of `NULL`, and if used, can't be an empty string. *@new_name* must be unique. For a list of current collection set names, query the `syscollector_collection_sets` system view.

#### [ @target = ] N'*target*'

Reserved for future use. *@target* is **nvarchar(128)**, with a default of `NULL`.

#### [ @collection_mode = ] *collection_mode*

The type of data collection to use. *@collection_mode* is **smallint**, and can have one of the following values:

- `0`: Cached mode. Data collection and upload are on separate schedules. Specify cached mode for continuous collection.

- `1`: Non-cached mode. Data collection and upload is on the same schedule. Specify non-cached mode for ad hoc collection or snapshot collection.

If changing from non-cached mode to cached mode (`0`), you must also specify either *@schedule_uid* or *@schedule_name*.

#### [ @days_until_expiration = ] *days_until_expiration*

The number of days that the collected data is saved in the management data warehouse. *@days_until_expiration* is **smallint**, and must be `0` or a positive integer.

#### [ @proxy_id = ] *proxy_id*

The unique identifier for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. *@proxy_id* is **int**.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy. *@proxy_name* is **sysname**, with a default of `NULL`.

#### [ @schedule_uid = ] '*schedule_uid*'

The GUID that points to a schedule. *@schedule_uid* is **uniqueidentifier**, with a default of `NULL`.

To obtain *@schedule_uid*, query the `sysschedules` system table.

When *@collection_mode* is set to `0`, *@schedule_uid* or *@schedule_name* must be specified. When *@collection_mode* is set to `1`, *@schedule_uid* or *@schedule_name* is ignored if specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule. *@schedule_name* is **sysname**, with a default of `NULL`. If specified, *@schedule_uid* must be `NULL`. To obtain *@schedule_name*, query the `sysschedules` system table.

#### [ @logging_level = ] *logging_level*

The logging level. *@logging_level* is **smallint**, with a default of `1`, with one of the following values:

- `0`: Log execution information and [!INCLUDE [ssIS](../../includes/ssis-md.md)] events that track:

  - Starting/stopping collection sets
  - Starting/stopping packages
  - Error information

- `1`: Level-0 logging and:

  - Execution statistics
  - Continuously running collection progress
  - Warning events from [!INCLUDE [ssIS](../../includes/ssis-md.md)]

- `2`: Level-1 logging and detailed event information from [!INCLUDE [ssIS](../../includes/ssis-md.md)].

#### [ @description = ] N'*description*'

The description of the collection set. *@description* is **nvarchar(4000)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_update_collection_set` must be run in the context of the `msdb` system database.

Either *collection_set_id* or *name* must have a value, both can't be `NULL`. To obtain these values, query the `syscollector_collection_sets` system view.

If the collection set is running, you can only update *schedule_uid* and *description*. To stop the collection set, use [sp_syscollector_stop_collection_set](sp-syscollector-stop-collection-set-transact-sql.md).

## Permissions

Requires membership in the **dc_admin** or the **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure. Although **dc_operator** can run this stored procedure, members of this role are limited in the properties that they can change. The following properties can only be changed by **dc_admin**:

- *@new_name*
- *@target*
- *@proxy_id*
- *@description*
- *@collection_mode*
- *@days_until_expiration*

## Examples

### A. Rename a collection set

The following example renames a user-defined collection set.

```sql
USE msdb;
GO
EXECUTE dbo.sp_syscollector_update_collection_set
@name = N'Simple collection set test 1',
@new_name = N'Collection set test 1 in cached mode';
GO
```

### B. Change the collection mode from non-cached to cached

The following example changes the collection mode from non-cached mode to cached mode. This change requires that you specify a schedule ID or schedule name.

```sql
USE msdb;
GO
EXECUTE dbo.sp_syscollector_update_collection_set
@name = N'Collection set test 1 in cached mode',
@collection_mode = 0,
@schedule_uid = 'C7022AF3-51B8-4011-B159-64C47C88FF70';
-- alternatively, use @schedule_name.
-- @schedule_name = N'CollectorSchedule_Every_15min;
GO
```

### C. Change other collection set parameters

The following example updates various properties of the collection set named `Simple collection set test 2`.

```sql
USE msdb;
GO

EXEC dbo.sp_syscollector_update_collection_set
    @name = N'Simple collection set test 2',
    @collection_mode = 1,
    @days_until_expiration = 5,
    @description = N'This is a test collection set that runs in noncached mode.',
    @logging_level = 0;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
- [syscollector_collection_sets (Transact-SQL)](../system-catalog-views/syscollector-collection-sets-transact-sql.md)
- [dbo.sysschedules (Transact-SQL)](../system-tables/dbo-sysschedules-transact-sql.md)
