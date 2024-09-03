---
title: "DBCC dllname (FREE) (Transact-SQL)"
description: DBCC dllname (FREE) unloads the specified extended stored procedure DLL from memory.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "dbcc_dllname_(FREE)_TSQL"
  - "dllname"
  - "dbcc dllname (FREE)"
  - "FREE"
  - "dbcc_dllname(FREE)_TSQL"
  - "FREE_TSQL"
  - "dllname_TSQL"
  - "dbcc dllname(FREE)"
helpviewer_keywords:
  - "DLL unloading [SQL Server]"
  - "DBCC dllname (FREE)"
  - "freeing DLLs"
  - "unloading DLLs"
dev_langs:
  - "TSQL"
---
# DBCC dllname (FREE) (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Unloads the specified extended stored procedure DLL from memory.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC <dllname> ( FREE ) [ WITH NO_INFOMSGS ]
```

## Arguments

#### `<dllname>`

The name of the DLL to release from memory.

#### WITH NO_INFOMSGS

Suppresses all informational messages.

## Remarks

When an extended stored procedure is executed, the DLL remains loaded by the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] until the server is shut down. This statement allows for a DLL to be unloaded from memory without shutting down [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To display the DLL files currently loaded by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], execute `sp_helpextendedproc`.

## Result sets

When a valid DLL is specified, `DBCC <dllname> (FREE)` returns:

```output
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

The following example assumes that `xp_sample` is implemented as `xp_sample.dll` and has been executed. `DBCC <dllname> (FREE)` unloads the `xp_sample.dll` file associated with the `xp_sample` extended procedure.

```sql
DBCC xp_sample (FREE);
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [Execution Characteristics of Extended Stored Procedures](../../relational-databases/extended-stored-procedures-programming/execution-characteristics-of-extended-stored-procedures.md)
- [sp_addextendedproc (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addextendedproc-transact-sql.md)
- [sp_dropextendedproc (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-dropextendedproc-transact-sql.md)
- [sp_helpextendedproc (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helpextendedproc-transact-sql.md)
- [Unloading an Extended Stored Procedure DLL](../../relational-databases/extended-stored-procedures-programming/unloading-an-extended-stored-procedure-dll.md)
