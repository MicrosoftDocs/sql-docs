---
title: "sp_procoption (Transact-SQL)"
description: "sp_procoption sets or clears a stored procedure for automatic execution."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_procoption"
  - "sp_procoption_TSQL"
helpviewer_keywords:
  - "sp_procoption"
dev_langs:
  - "TSQL"
---
# sp_procoption (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets or clears a stored procedure for automatic execution. A stored procedure that is set to automatic execution runs every time an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is started.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_procoption
    [ @ProcName = ] N'ProcName'
    , [ @OptionName = ] 'OptionName'
    , [ @OptionValue = ] 'OptionValue'
[ ; ]
```

## Arguments

#### [ @ProcName = ] N'*ProcName*'

The name of the procedure for which to set an option. *@ProcName* is **nvarchar(776)**, with no default.

#### [ @OptionName = ] '*OptionName*'

The name of the option to set. *@OptionName* is **varchar(35)**, and the only value possible is `startup`.

#### [ @OptionValue = ] '*OptionValue*'

Whether to set the option on (`true` or `on`) or off (`false` or `off`). *@OptionValue* is **varchar(12)**, with no default.

## Return code values

`0` (success) or error number (failure).

## Remarks

Startup procedures must be in the `dbo` schema of the `master` database, and can't contain `INPUT` or `OUTPUT` parameters. Execution of the stored procedures starts when all databases are recovered and the "Recovery is completed" message is logged at startup.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example sets a procedure for automatic execution.

```sql
EXEC sp_procoption @ProcName = N'<procedure name>',
    @OptionName = 'startup',
    @OptionValue = 'on';
```

The following example stops a procedure from executing automatically.

```sql
EXEC sp_procoption @ProcName = N'<procedure name>',
    @OptionName = 'startup',
    @OptionValue = 'off';
```

## Related content

- [Execute a stored procedure](../stored-procedures/execute-a-stored-procedure.md)
