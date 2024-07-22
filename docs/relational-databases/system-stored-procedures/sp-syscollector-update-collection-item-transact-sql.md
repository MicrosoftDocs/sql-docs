---
title: "sp_syscollector_update_collection_item (Transact-SQL)"
description: Used to modify the properties of a user-defined collection item or to rename a user-defined collection item.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_update_collection_item"
  - "sp_syscollector_update_collection_item_TSQL"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_update_collection_item"
dev_langs:
  - "TSQL"
---
# sp_syscollector_update_collection_item (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used to modify the properties of a user-defined collection item or to rename a user-defined collection item.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_update_collection_item
    [ [ @collection_item_id = ] collection_item_id ]
    [ , [ @name = ] N'name' ]
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @frequency = ] frequency ]
    [ , [ @parameters = ] N'parameters' ]
[ ; ]
```

## Arguments

#### [ @collection_item_id = ] *collection_item_id*

The unique identifier that identifies the collection item. *@collection_item_id* is **int**, with a default of `NULL`. *@collection_item_id* must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection item. *@name* is **sysname**, with a default of `NULL`. *@name* must have a value if *@collection_item_id* is `NULL`.

#### [ @new_name = ] N'*new_name*'

The new name for the collection item. *@new_name* is **sysname**, with a default of `NULL`, and if used, can't be an empty string.

*@new_name* must be unique. For a list of current collection item names, query the `syscollector_collection_items` system view.

#### [ @frequency = ] *frequency*

The frequency (in seconds) that data is collected by this collection item. *@frequency* is **int**, with a default of `5`, the minimum value that can be specified.

#### [ @parameters = ] N'*parameters*'

The input parameters for the collection item. *@parameters* is **xml**, with a default of an empty string. The *@parameters* schema must match the parameters schema of the collector type.

## Return code values

`0` (success) or `1` (failure).

## Remarks

If the collection set is set to non-cached mode, changing the frequency is ignored because this mode causes both data collection and upload to occur at the schedule specified for the collection set. To view the status of the collection set, run the following query. Replace `<collection_item_id>` with the ID of the collection item to be updated.

```sql
USE msdb;
GO

SELECT cs.collection_set_id,
    collection_set_uid,
    cs.name,
    'is running' = CASE
        WHEN is_running = 0
            THEN 'No'
        ELSE 'Yes'
        END,
    'cache mode' = CASE
        WHEN collection_mode = 0
            THEN 'Cached mode'
        ELSE 'Non-cached mode'
        END
FROM syscollector_collection_sets AS cs
INNER JOIN syscollector_collection_items AS ci
    ON ci.collection_set_id = cs.collection_set_id
WHERE collection_item_id = < collection_item_id >;
GO
```

## Permissions

Requires membership in the **dc_admin** or the **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure. Although **dc_operator** can run this stored procedure, members of this role are limited in the properties that they can change. The following properties can only be changed by **dc_admin**:

- *@new_name*
- *@parameters*

## Examples

The following examples are based on the collection item created in the example defined in [sp_syscollector_create_collection_item (Transact-SQL)](sp-syscollector-create-collection-item-transact-sql.md).

### A. Change the collection frequency

The following example changes the collection frequency for the specified collection item.

```sql
USE msdb;
GO

EXEC sp_syscollector_update_collection_item
    @name = N'My custom T-SQL query collector item',
    @frequency = 3000;
GO
```

### B. Rename a collection item

The following example renames a collection item.

```sql
USE msdb;
GO

EXEC sp_syscollector_update_collection_item
    @name = N'My custom T-SQL query collector item',
    @new_name = N'My modified T-SQL item';
GO
```

### C. Change the parameters of a collection item

The following example changes the parameters associated with the collection item. The statement defined within the `<Value>` attribute is changed and the `UseSystemDatabases` attribute is set to false. To view the current parameters for this item, query the parameters column in the `syscollector_collection_items` system view. You may need to modify the value for *@collection_item_id*.

```sql
USE msdb;
GO
EXEC sp_syscollector_update_collection_item
@collection_item_id = 9,
@parameters = '
    <ns:TSQLQueryCollector xmlns:ns="DataCollectorType">
        <Query>
            <Value>SELECT * FROM sys.dm_db_index_usage_stats</Value>
            <OutputTable>MyOutputTable</OutputTable>
        </Query>
        <Databases>
            <Database> UseSystemDatabases = "false"
                       UseUserDatabases = "true"</Database>
        </Databases>
    </ns:TSQLQueryCollector>';
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
- [sp_syscollector_create_collection_item (Transact-SQL)](sp-syscollector-create-collection-item-transact-sql.md)
- [syscollector_collection_items (Transact-SQL)](../system-catalog-views/syscollector-collection-items-transact-sql.md)
