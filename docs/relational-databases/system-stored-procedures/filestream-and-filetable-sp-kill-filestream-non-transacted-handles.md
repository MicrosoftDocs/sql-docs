---
title: "sp_kill_filestream_non_transacted_handles (Transact-SQL)"
description: "Closes nontransactional file handles to FileTable data."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_kill_filestream_non_transacted_handles_TSQL"
  - "sp_kill_filestream_non_transacted_handles"
helpviewer_keywords:
  - "sp_kill_filestream_non_transacted_handles"
dev_langs:
  - "TSQL"
---
# sp_kill_filestream_non_transacted_handles (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Closes nontransactional file handles to FileTable data.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_kill_filestream_non_transacted_handles [
    [ @table_name = ] 'table_name'
    , [ [ @handle_id = ] handle_id ]
    ]
```

## Arguments

#### [ @table_name = ] '*table_name*'

The name of the table in which to close nontransactional handles.

You can pass *table_name* without *handle_id* to close all open nontransactional handles for the FileTable.

You can pass NULL for the value of *table_name* to close all open nontransactional handles for all FileTables in the current database. NULL is the default value.

#### [ @handle_id = ] *handle_id*

The optional ID of the individual handle to be closed. You can get the *handle_id* from the [sys.dm_filestream_non_transacted_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md) dynamic management view. Each ID is unique in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. If you specify *handle_id*, then you also have to provide a value for *table_name*.

You can pass NULL for the value of *handle_id* to close all open nontransactional handles for the FileTable specified by *table_name*. NULL is the default value.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The *handle_id* required by `sp_kill_filestream_non_transacted_handles` isn't related to the `session_id` or unit of work that is used in other `kill` commands.

For more information, see [Manage FileTables](../blob/manage-filetables.md).

## Metadata

For information about open nontransactional file handles, query the dynamic management view [sys.dm_filestream_non_transacted_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md).

## Permissions

You must have VIEW DATABASE STATE permission to get file handles from the `sys.dm_filestream_non_transacted_handles` dynamic management view and to run `sp_kill_filestream_non_transacted_handles`.

## Examples

The following examples show how to call `sp_kill_filestream_non_transacted_handles` to close nontransactional file handles for FileTable data.

```sql
-- Close all open handles in the current database.
sp_kill_filestream_non_transacted_handles;

-- Close all open handles in myFileTable.
sp_kill_filestream_non_transacted_handles @table_name = 'myFileTable';

-- Close a specific handle in myFileTable.
sp_kill_filestream_non_transacted_handles @table_name = 'myFileTable', @handle_id = 0xFFFAAADD;
```

The following example shows how to use a script to get a *handle_id* and close it.

```sql
DECLARE @handle_id VARBINARY(16);
DECLARE @table_name SYSNAME;

SELECT TOP 1 @handle_id = handle_id,
    @table_name = Object_name(table_id)
FROM sys.dm_FILESTREAM_non_transacted_handles;

EXEC sp_kill_filestream_non_transacted_handles @dbname,
    @table_name,
    @handle_id;
GO
```

## Related content

- [Manage FileTables](../blob/manage-filetables.md)
- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
- [FILESTREAM and FileTable catalog views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
- [sp_filestream_force_garbage_collection (Transact-SQL)](filestream-and-filetable-sp-filestream-force-garbage-collection.md)
