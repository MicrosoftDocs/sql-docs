---
title: "xp_sqlmaint (Transact-SQL)"
description: "Calls the sqlmaint utility with a string that contains sqlmaint options."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_sqlmaint"
  - "xp_sqlmaint_TSQL"
helpviewer_keywords:
  - "xp_sqlmaint"
dev_langs:
  - "TSQL"
---
# xp_sqlmaint (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Calls the **sqlmaint** utility with a string that contains **sqlmaint** options (also known as *switches*). The **sqlmaint** utility performs a set of maintenance operations on one or more databases.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_sqlmaint 'switch_string'
```

## Arguments

#### '*switch_string*'

A string containing the **sqlmaint** utility switches. The switches and their values must be separated by a space.

The `-?` switch is not valid for `xp_sqlmaint`.

## Return code values

None. Returns an error if the **sqlmaint** utility fails.

## Remarks

If this procedure is called by a user logged on with SQL Server Authentication, the `-U "<login_id>"` and `-P "<password>"` switches are prepended to *switch_string* before execution. If the user is logged on with Windows Authentication, *switch_string* is passed without change to **sqlmaint**.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

In the following example, `xp_sqlmaint` calls **sqlmaint** to perform integrity checks, create a report file, and update `msdb.dbo.sysdbmaintplan_history`.

```sql
EXEC xp_sqlmaint '-D AdventureWorks2022 -PlanID 02A52657-D546-11D1-9D8A-00A0C9054212
   -Rpt "C:\Program Files\Microsoft SQL Server\MSSQL\LOG\DBMaintPlan2.txt" -WriteHistory -CkDB -CkAl';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
The command(s) executed successfully.
```

## Related content

- [sqlmaint Utility](../../tools/sqlmaint-utility.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
