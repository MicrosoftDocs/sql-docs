---
title: "sp_can_tlog_be_applied (Transact-SQL)"
description: Verifies whether a transaction log backup can be applied to a SQL Server database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_can_tlog_be_applied_TSQL"
  - "sp_can_tlog_be_applied"
helpviewer_keywords:
  - "sp_can_tlog_be_applied"
dev_langs:
  - "TSQL"
---
# sp_can_tlog_be_applied (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Verifies whether a transaction log backup can be applied to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. `sp_can_tlog_be_applied` requires that the database is in the *restoring* state.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_can_tlog_be_applied
    [ @backup_file_name = ] N'backup_file_name'
    , [ @database_name = ] N'database_name'
    [ , [ @result = ] result OUTPUT ]
    [ , [ @verbose = ] verbose ]
[ ; ]
```

## Arguments

#### [ @backup_file_name = ] N'*backup_file_name*'

The name of a backup file. *@backup_file_name* is **nvarchar(500)**, with no default.

#### [ @database_name = ] N'*database_name*'

The name of the database. *@database_name* is **sysname**, with no default.

#### [ @result = ] *result* OUTPUT

Indicates whether the transaction log can be applied to the database. *@result* is an OUTPUT parameter of type **bit**.

- `1` = The log can be applies
- `0` = The log can't be applied.

#### [ @verbose = ] *verbose*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_can_tlog_be_applied`.

## Examples

The following example declares a local variable, `@MyBitVar`, to store the result.

```sql
USE master;
GO

DECLARE @MyBitVar BIT;

EXEC sp_can_tlog_be_applied
    @backup_file_name = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\AdventureWorks2022.bak',
    @database_name = N'AdventureWorks2022',
    @result = @MyBitVar OUTPUT;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
