---
title: "sp_syscollector_create_collection_set (Transact-SQL)"
description: Creates a new collection set.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_create_collection_set_TSQL"
  - "sp_syscollector_create_collection_set"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_create_collection_set"
dev_langs:
  - "TSQL"
---
# sp_syscollector_create_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a new collection set. You can use this stored procedure to create a custom collection set for data collection.

> [!WARNING]  
> In cases where the Windows account configured as a proxy is a non-interactive or interactive user that has not yet logged in, the profile directory will not exist, and the creation of the staging directory will fail. Therefore, if you are using a proxy account on a domain controller, you must specify an interactive account that has been used at least once in order to assure that the profile directory has been created.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_create_collection_set
    [ @name = ] N'name'
    [ , [ @target = ] N'target' ]
    [ , [ @collection_mode = ] collection_mode ]
    [ , [ @days_until_expiration = ] days_until_expiration ]
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @schedule_uid = ] 'schedule_uid' ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @logging_level = ] logging_level ]
    [ , [ @description = ] N'description' ]
    , [ @collection_set_id = ] collection_set_id OUTPUT
    [ , [ @collection_set_uid = ] 'collection_set_uid' OUTPUT ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname** and can't be an empty string or `NULL`.

*@name* must be unique. For a list of current collection set names, query the `syscollector_collection_sets` system view.

#### [ @target = ] N'*target*'

Reserved for future use. *@target* is **nvarchar(128)**, with a default of `NULL`.

#### [ @collection_mode = ] *collection_mode*

*@collection_mode* is **smallint**, with a default of `0`.

Specifies the manner in which the data is collected and stored. *@collection_mode* is **smallint**, with a default of `0`, and can have one of the following values:

- `0`: Cached mode. Data collection and upload are on separate schedules. Specify cached mode for continuous collection.

- `1`: Non-cached mode. Data collection and upload is on the same schedule. Specify non-cached mode for ad hoc collection or snapshot collection.

When *@collection_mode* is `0`, *@schedule_uid* or *@schedule_name* must be specified.

#### [ @days_until_expiration = ] *days_until_expiration*

The number of days that the collected data is saved in the management data warehouse. *@days_until_expiration* is **smallint**, with a default of `730` (two years). *@days_until_expiration* must be `0` or a positive integer.

#### [ @proxy_id = ] *proxy_id*

The unique identifier for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. *@proxy_id* is **int**, with a default of `NULL`. If specified, *@proxy_name* must be `NULL`. To obtain *@proxy_id*, query the `sysproxies` system table. The **dc_admin** fixed database role must have permission to access the proxy. For more information, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy account. *@proxy_name* is **sysname**, with a default of `NULL`. If specified, *@proxy_id* must be `NULL`. To obtain *@proxy_name*, query the `sysproxies` system table.

#### [ @schedule_uid = ] '*schedule_uid*'

The GUID that points to a schedule. *@schedule_uid* is **uniqueidentifier**, with a default of `NULL`. If specified, *@schedule_name* must be `NULL`. To obtain *@schedule_uid*, query the `sysschedules` system table.

When *@collection_mode* is set to `0`, *@schedule_uid* or *@schedule_name* must be specified. When *@collection_mode* is set to `1`, *@schedule_uid* or *@schedule_name* is ignored if specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule. *@schedule_name* is **sysname**, with a default of `NULL`. If specified, *@schedule_uid* must be `NULL`. To obtain *@schedule_name*, query the `sysschedules` system table.

#### [ @logging_level = ] *logging_level*

The logging level. *@logging_level* is **smallint**, with a default of `1`, with one of the following values:

- `0`: log execution information and [!INCLUDE [ssIS](../../includes/ssis-md.md)] events that track:

  - Starting/stopping collection sets
  - Starting/stopping packages
  - Error information

- `1`: level `0` logging and:

  - Execution statistics
  - Continuously running collection progress
  - Warning events from [!INCLUDE [ssIS](../../includes/ssis-md.md)]

- `2`: level `1` logging and detailed event information from [!INCLUDE [ssIS](../../includes/ssis-md.md)].

#### [ @description = ] N'*description*'

The description of the collection set. *@description* is **nvarchar(4000)**, with a default of `NULL`.

#### [ @collection_set_id = ] *collection_set_id* OUTPUT

The unique local identifier for the collection set. *@collection_set_id* is an OUTPUT parameter of type **int**.

#### [ @collection_set_uid = ] '*collection_set_uid*' OUTPUT

The GUID for the collection set. *@collection_set_uid* is an OUTPUT parameter of type **uniqueidentifier**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_create_collection_set` must be run in the context of the `msdb` system database.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

### A. Create a collection set by using default values

The following example creates a collection set by specifying only the required parameters. *@collection_mode* isn't required, but the default collection mode (cached) requires specifying either a schedule ID or schedule name.

```sql
USE msdb;
GO

DECLARE @collection_set_id INT;

EXECUTE dbo.sp_syscollector_create_collection_set
    @name = N'Simple collection set test 1',
    @description = N'This is a test collection set that runs in non-cached mode.',
    @collection_mode = 1,
    @collection_set_id = @collection_set_id OUTPUT;
GO
```

### B. Create a collection set by using specified values

The following example creates a collection set by specifying values for many of the parameters.

```sql
USE msdb;
GO

DECLARE @collection_set_id INT;
DECLARE @collection_set_uid UNIQUEIDENTIFIER;

SET @collection_set_uid = NEWID();

EXEC dbo.sp_syscollector_create_collection_set
    @name = N'Simple collection set test 2',
    @collection_mode = 0,
    @days_until_expiration = 365,
    @description = N'This is a test collection set that runs in cached mode.',
    @logging_level = 2,
    @schedule_name = N'CollectorSchedule_Every_30min',
    @collection_set_id = @collection_set_id OUTPUT,
    @collection_set_uid = @collection_set_uid OUTPUT;
GO
```

## Related content

- [Data collection](../data-collection/data-collection.md)
- [Create a Custom Collection Set That Uses the Generic T-SQL Query Collector Type (Transact-SQL)](../data-collection/create-custom-collection-set-generic-t-sql-query-collector-type.md)
- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [syscollector_collection_sets (Transact-SQL)](../system-catalog-views/syscollector-collection-sets-transact-sql.md)
