---
title: "sp_fulltext_table (Transact-SQL)"
description: Marks or unmarks a table for full-text indexing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_table_TSQL"
  - "sp_fulltext_table"
helpviewer_keywords:
  - "sp_fulltext_table"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_fulltext_table (Transact-SQL)

[!INCLUDE [sql-asa](../../includes/applies-to-version/sql-asa.md)]

Marks or unmarks a table for full-text indexing.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md), [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md), and [DROP FULLTEXT INDEX](../../t-sql/statements/drop-fulltext-index-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_table
    [ @tabname = ] N'tabname'
    , [ @action = ] 'action'
    [ , [ @ftcat = ] N'ftcat' ]
    [ , [ @keyname = ] N'keyname' ]
[ ; ]
```

## Arguments

#### [ @tabname = ] N'*tabname*'

A one-part or two-part table name. The table must exist in the current database. *@tabname* is **nvarchar(517)**, with no default.

#### [ @action = ] '*action*'

The action to be performed. *@action* is **nvarchar(50)**, with no default, and can be one of these values.

| Value | Description |
| --- | --- |
| **Create** | Creates the metadata for a full-text index for the table referenced by *@tabname* and specifies that the full-text index data for this table should reside in *@ftcat*. This action also designates the use of *@keyname* as the full-text key column. This unique index must already be present and must be defined on one column of the table.<br /><br />A full-text search can't be performed against this table until the full-text catalog is populated. |
| **Drop** | Drops the metadata on the full-text index for *@tabname*. If the full-text index is active, it's automatically deactivated before being dropped. It isn't necessary to remove columns before dropping the full-text index. |
| **Activate** | Activates the ability for full-text index data to be gathered for *@tabname*, after it's been deactivated. There must be at least one column participating in the full-text index before it can be activated.<br /><br />A full-text index is automatically made active (for population) as soon as the first column is added for indexing. If the last column is dropped from the index, the index becomes inactive. If change tracking is on, activating an inactive index starts a new population.<br /><br />This doesn't actually populate the full-text index, but simply registers the table in the full-text catalog in the file system so that rows from *@tabname* can be retrieved during the next full-text index population. |
| **Deactivate** | Deactivates the full-text index for *@tabname* so that full-text index data can no longer be gathered for the *@tabname*. The full-text index metadata remains and the table can be reactivated.<br /><br />If change tracking is on, deactivating an active index freezes the state of the index: any ongoing population is stopped, and no more changes are propagated to the index. |
| **start_change_tracking** | Start an incremental population of the full-text index. If the table doesn't have a timestamp, start a full population of the full-text index. Start tracking changes to the table.<br /><br />Full-text change tracking doesn't track any WRITETEXT or UPDATETEXT operations performed on full-text indexed columns that are of type **image**, **text**, or **ntext**. |
| **stop_change_tracking** | Stop tracking changes to the table. |
| **update_index** | Propagate the current set of tracked changes to the full-text index. |
| **start_background_updateindex** | Start propagating tracked changes to the full-text index as they occur. |
| **stop_background_updateindex** | Stop propagating tracked changes to the full-text index as they occur. |
| **start_full** | Start a full population of the full-text index for the table. |
| **start_incremental** | Start an incremental population of the full-text index for the table. |
| **Stop** | Stop a full or incremental population. |

#### [ @ftcat = ] N'*ftcat*'

A valid, existing full-text catalog name for a **create** action. For all other actions, this parameter must be NULL. *@ftcat* is **sysname**, with a default of `NULL`.

#### [ @keyname = ] N'*keyname*'

A valid single-key-column, unique non-nullable index on *@tabname* for a **create** action. For all other actions, this parameter must be NULL. *@keyname* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

After a full-text index is deactivated for a particular table, the existing full-text index remains in place until the next full population; however, this index isn't used because [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] blocks queries on deactivated tables.

If the table is reactivated and the index isn't repopulated, the old index is still available for queries against any remaining, but not new, full-text enabled columns. Data from deleted columns are matched in queries that specify an all-full-text column search.

After a table has been defined for full-text indexing, switching the full-text unique key column from one data type to another, either by changing the data type of that column or changing the full-text unique key from one column to another, without a full repopulation might cause a failure to occur during a subsequent query and returning the error message:

> Conversion to type *data_type* failed for full-text search key value *key_value*.

To prevent this error, drop the full-text definition for this table using the **drop** action of `sp_fulltext_table` and redefine it using `sp_fulltext_table` and `sp_fulltext_column`.

The full-text key column must be defined to be 900 bytes or less. It's recommended that the size of the key column is as small as possible for performance reasons.

## Permissions

Only members of the **sysadmin** fixed server role, **db_owner** and **db_ddladmin** fixed database roles, or a user with reference permissions on the full-text catalog can execute `sp_fulltext_table`.

## Examples

### A. Enable a table for full-text indexing

The following example creates full-text index metadata for the `Document` table of the `AdventureWorks` database. `Cat_Desc` is a full-text catalog. `PK_Document_DocumentID` is a unique, single-column index on `Document`.

```sql
USE AdventureWorks2022;
GO

EXEC sp_fulltext_table 'Production.Document',
    'create',
    'Cat_Desc',
    'PK_Document_DocumentID';

--Add some columns
EXEC sp_fulltext_column 'Production.Document',
    'DocumentSummary',
    'add';

-- Activate the full-text index
EXEC sp_fulltext_table 'Production.Document',
    'activate';
GO
```

### B. Activate and propagating track changes

The following example activates and starts propagating tracked changes to the full-text index as they occur.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_table 'Production.Document', 'Start_change_tracking';
EXEC sp_fulltext_table 'Production.Document', 'Start_background_updateindex';
GO
```

### C. Remove a full-text index

This example removes the full-text index metadata for the `Document` table of the `AdventureWorks` database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_table 'Production.Document', 'drop';
GO
```

## Related content

- [INDEXPROPERTY (Transact-SQL)](../../t-sql/functions/indexproperty-transact-sql.md)
- [OBJECTPROPERTY (Transact-SQL)](../../t-sql/functions/objectproperty-transact-sql.md)
- [sp_help_fulltext_tables (Transact-SQL)](sp-help-fulltext-tables-transact-sql.md)
- [sp_help_fulltext_tables_cursor (Transact-SQL)](sp-help-fulltext-tables-cursor-transact-sql.md)
- [sp_helpindex (Transact-SQL)](sp-helpindex-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Full-Text Search and Semantic Search stored procedures (Transact-SQL)](full-text-search-and-semantic-search-stored-procedures-transact-sql.md)
