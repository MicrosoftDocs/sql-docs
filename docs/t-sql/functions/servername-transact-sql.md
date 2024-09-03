---
title: "@@SERVERNAME (Transact-SQL)"
description: "@@SERVERNAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@SERVERNAME"
  - "@@SERVERNAME_TSQL"
helpviewer_keywords:
  - "@@SERVERNAME function"
  - "local servers [SQL Server]"
dev_langs:
  - "TSQL"
---
# @@SERVERNAME (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the name of the local server that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
@@SERVERNAME
```

## Return types

**nvarchar**

## Remarks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the server name to the computer name during installation. To change the name of the server, use [sp_addserver](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), and then restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

With multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installed, `@@SERVERNAME` returns the following local server name information if the local server name didn't change since it was set up.

| Instance | Server information |
| --- | --- |
| **Default instance** | `<servername>` |
| **Named instance** | `<servername>\<instancename>` |
| **Failover cluster instance - default instance** | `<network_name_for_fci_in_wsfc>` |
| **Failover cluster instance - named instance** | `<network_name_for_fci_in_wsfc>\<instancename>` |

Although the `@@SERVERNAME` function and the `SERVERNAME` property of [SERVERPROPERTY](serverproperty-transact-sql.md) function might return strings with similar formats, the information can be different. The `SERVERNAME` property automatically reports changes in the network name of the computer.

In contrast, `@@SERVERNAME` doesn't report such changes. `@@SERVERNAME` reports changes made to the local server name using the [sp_addserver](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md) or [sp_dropserver](../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md) stored procedure.

## Examples

The following example shows using `@@SERVERNAME`.

```sql
SELECT @@SERVERNAME AS 'Server Name';
```

Here's a sample result set.

```output
Server Name
---------------------------------
ACCTG
```

## Related content

- [Configuration Functions (Transact-SQL)](configuration-functions-transact-sql.md)
- [SERVERPROPERTY (Transact-SQL)](serverproperty-transact-sql.md)
- [sp_addserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)
