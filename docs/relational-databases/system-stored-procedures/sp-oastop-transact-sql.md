---
title: "sp_OAStop (Transact-SQL)"
description: sp_OAStop stops the server-wide OLE Automation stored procedure execution environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OAStop"
  - "sp_OAStop_TSQL"
helpviewer_keywords:
  - "sp_OAStop"
dev_langs:
  - "TSQL"
---
# sp_OAStop (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops the server-wide OLE Automation stored procedure execution environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OAStop
[ ; ]
```

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Remarks

A single execution environment is shared by all clients that use OLE Automation stored procedures. If one client calls `sp_OAStop`, the shared execution environment is stopped for all clients. After the execution environment is stopped, any call to `sp_OACreate` restarts the execution environment.

## Permissions

Requires membership in the **sysadmin** fixed server role or execute permission directly on this stored procedure. The [Ole Automation Procedures](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md) server configuration option must be enabled to use any system procedure related to OLE Automation.

## Examples

The following example stops the shared OLE Automation execution environment.

```sql
EXEC sp_OAStop;
GO
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
