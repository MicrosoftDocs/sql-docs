---
title: "@@SERVICENAME (Transact-SQL)"
description: "@@SERVICENAME returns the name of the registry key under which the SQL Server Database Engine is running."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@SERVICENAME_TSQL"
  - "@@SERVICENAME"
helpviewer_keywords:
  - "@@SERVICENAME function"
  - "names [SQL Server], registry keys"
  - "registry keys [SQL Server]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017"
---
# @@SERVICENAME (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the name of the registry key under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

If the current instance is the default instance, `@@SERVICENAME` returns `MSSQLSERVER`. If the current instance is a named instance, `@@SERVICENAME` returns the instance name.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
@@SERVICENAME
```

## Return types

**nvarchar**

## Remarks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs as a service named `MSSQLServer`.

## Examples

The following example shows using `@@SERVICENAME` for a default instance.

```sql
SELECT @@SERVICENAME AS 'Service Name';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Service Name
------------------------------
MSSQLSERVER
```

The following example shows using `@@SERVICENAME` for the named instance `localhost\SQL2025`.

```sql
SELECT @@SERVICENAME AS 'Service Name';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Service Name
------------------------------
SQL2025
```

## Related content

- [Manage the Database Engine services](../../database-engine/configure-windows/manage-the-database-engine-services.md)
