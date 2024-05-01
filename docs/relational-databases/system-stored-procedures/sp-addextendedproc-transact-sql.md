---
title: "sp_addextendedproc (Transact-SQL)"
description: Registers the name of a new extended stored procedure to SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addextendedproc_TSQL"
  - "sp_addextendedproc"
helpviewer_keywords:
  - "sp_addextendedproc"
dev_langs:
  - "TSQL"
---
# sp_addextendedproc (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Registers the name of a new extended stored procedure to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR Integration](../clr-integration/common-language-runtime-integration-overview.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addextendedproc
    [ @functname = ] N'functname'
    , [ @dllname = ] 'dllname'
[ ; ]
```

## Arguments

#### [ @functname = ] N'*functname*'

The name of the function to call within the dynamic-link library (DLL). *@functname* is **nvarchar(517)**, with no default. *@functname* optionally can include the owner name in the form `<owner.function>`.

#### [ @dllname = ] '*dllname*'

The name of the DLL that contains the function. *@dllname* is **varchar(255)**, with no default. You should specify the complete path of the DLL.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

After an extended stored procedure is created, it must be added to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using `sp_addextendedproc`. For more information, see [Adding an Extended Stored Procedure to SQL Server](../extended-stored-procedures-programming/adding-an-extended-stored-procedure-to-sql-server.md).

This procedure can be run only in the `master` database. To execute an extended stored procedure from a database other than `master`, qualify the name of the extended stored procedure with `master`.

`sp_addextendedproc` adds entries to the [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) catalog view, registering the name of the new extended stored procedure with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. It also adds an entry in the [sys.extended_procedures](../system-catalog-views/sys-extended-procedures-transact-sql.md) catalog view.

> [!IMPORTANT]  
> Existing DLLs that aren't registered with a complete path don't work after upgrading to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. To correct the problem, use `sp_dropextendedproc` to unregister the DLL, and then reregister it with `sp_addextendedproc`, specifying the complete path.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_addextendedproc`.

## Examples

The following example adds the `xp_hello` extended stored procedure.

```sql
USE master;
GO
EXEC sp_addextendedproc xp_hello, 'c:\xp_hello.dll';
```

## Related content

- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md)
- [REVOKE (Transact-SQL)](../../t-sql/statements/revoke-transact-sql.md)
- [sp_dropextendedproc (Transact-SQL)](sp-dropextendedproc-transact-sql.md)
- [sp_helpextendedproc (Transact-SQL)](sp-helpextendedproc-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
