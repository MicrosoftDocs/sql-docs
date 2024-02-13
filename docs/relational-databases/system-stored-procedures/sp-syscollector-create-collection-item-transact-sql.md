---
title: "sp_syscollector_create_collection_item (Transact-SQL)"
description: Creates a collection item in a user-defined collection set.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_create_collection_item"
  - "sp_syscollector_create_collection_item_TSQL"
helpviewer_keywords:
  - "sp_syscollector_create_collection_item"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_create_collection_item (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a collection item in a user-defined collection set. A collection item defines the data to be collected and the frequency with which the data is collected.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_create_collection_item
    [ @collection_set_id = ] collection_set_id
    , [ @collector_type_uid = ] 'collector_type_uid'
    , [ @name = ] N'name'
    [ , [ @frequency = ] frequency ]
    [ , [ @parameters = ] N'parameters' ]
    , [ @collection_item_id = ] collection_item_id OUTPUT
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, with no default.

#### [ @collector_type_uid = ] '*collector_type_uid*'

The GUID that identifies the collector type to use for this item. *@collector_type_uid* is **uniqueidentifier**, with no default. For a list of collector types, query the `syscollector_collector_types` system view.

#### [ @name = ] N'*name*'

The name of the collection item. *@name* is **sysname** and can't be an empty string or `NULL`.

*@name* must be unique. For a list of current collection item names, query the `syscollector_collection_items` system view.

#### [ @frequency = ] *frequency*

Used to specify (in seconds) how frequently this collection item collects data. *@frequency* is **int**, with a default of `5`. The minimum value that can be specified is 5 seconds.

If the collection set is set to non-cached mode, the frequency is ignored, because this mode causes both data collection and upload to occur at the schedule specified for the collection set. To view the collection mode of the collection set, query the [syscollector_collection_sets (Transact-SQL)](../system-catalog-views/syscollector-collection-sets-transact-sql.md) system view.

#### [ @parameters = ] N'*parameters*'

The input parameters for the collector type. *@parameters* is **xml**, with a default of `NULL`. The *@parameters* schema must match the parameters schema of the collector type.

#### [ @collection_item_id = ] *collection_item_id* OUTPUT

The unique identifier that identifies the collection set item. *@collection_item_id* is an OUTPUT parameter of type **int**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_create_collection_item` must be run in the context of the `msdb` system database.

The collection set to which the collection item is being added must be stopped before creating the collection item. Collection items can't be added to system collection sets.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example creates a collection item based on the collection type `Generic T-SQL Query Collector Type` and adds it to the collection set named `Simple collection set test 2`. To create the specified collection set, run example B in [sp_syscollector_create_collection_set (Transact-SQL)](sp-syscollector-create-collection-set-transact-sql.md).

```sql
USE msdb;
GO

DECLARE @collection_item_id INT;
DECLARE @collection_set_id INT = (
        SELECT collection_set_id
        FROM syscollector_collection_sets
        WHERE name = N'Simple collection set test 2');
DECLARE @collector_type_uid UNIQUEIDENTIFIER = (
        SELECT collector_type_uid
        FROM syscollector_collector_types
        WHERE name = N'Generic T-SQL Query Collector Type');
DECLARE @params XML = CONVERT(XML, N'\<ns:TSQLQueryCollector xmlns:ns="DataCollectorType">
            <Query>
                <Value>SELECT * FROM sys.objects</Value>
                <OutputTable>MyOutputTable</OutputTable>
            </Query>
            <Databases>
                <Database> UseSystemDatabases = "true"
                           UseUserDatabases = "true"
                </Database>
            </Databases>
         \</ns:TSQLQueryCollector>');

EXEC sp_syscollector_create_collection_item @collection_set_id = @collection_set_id,
    @collector_type_uid = @collector_type_uid,
    @name = 'My custom T-SQL query collector item',
    @frequency = 6000,
    @parameters = @params,
    @collection_item_id = @collection_item_id OUTPUT;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
- [sp_syscollector_update_collection_item (Transact-SQL)](sp-syscollector-update-collection-item-transact-sql.md)
- [sp_syscollector_delete_collection_item (Transact-SQL)](sp-syscollector-delete-collection-item-transact-sql.md)
- [syscollector_collector_types (Transact-SQL)](../system-catalog-views/syscollector-collector-types-transact-sql.md)
- [sp_syscollector_create_collection_set (Transact-SQL)](sp-syscollector-create-collection-set-transact-sql.md)
- [syscollector_collection_items (Transact-SQL)](../system-catalog-views/syscollector-collection-items-transact-sql.md)
