---
title: "Server Configuration: Ole Automation Procedures"
description: "Learn about the Ole Automation Procedures option. See how it specifies whether SQL Server can instantiate OLE Automation objects within Transact-SQL batches."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Ole Automation Procedures option"
---
# Server configuration: Ole Automation Procedures

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `Ole Automation Procedures` option to specify whether OLE Automation objects can be instantiated within [!INCLUDE [tsql](../../includes/tsql-md.md)] batches. This option can also be configured using the Policy-Based Management or the `sp_configure` stored procedure. For more information, see [Surface area configuration](../../relational-databases/security/surface-area-configuration.md).

The `Ole Automation Procedures` option can be set to the following values.

- `0`: OLE Automation Procedures are disabled. Default for new instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- `1`: OLE Automation Procedures are enabled.

When OLE Automation Procedures are enabled, a call to `sp_OACreate` starts the OLE shared execution environment.

The current value of the `Ole Automation Procedures` option can be viewed and changed by using the `sp_configure` system stored procedure.

## Examples

The following example shows how to view the current setting of OLE Automation procedures.

```sql
EXECUTE sp_configure 'Ole Automation Procedures';
GO
```

The following example shows how to enable OLE Automation procedures.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'Ole Automation Procedures', 1;
GO

RECONFIGURE;
GO
```

## Related content

- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Surface area configuration](../../relational-databases/security/surface-area-configuration.md)
- [Server configuration options](server-configuration-options-sql-server.md)
