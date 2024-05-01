---
title: "sp_delete_firewall_rule"
titleSuffix: Azure SQL Database
description: Removes server-level firewall settings from your Azure SQL Database logical server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "sp_delete_firewall_rule_TSQL"
  - "sp_delete_firewall_rule"
  - "sys.sp_delete_firewall_rule"
  - "sys.sp_delete_firewall_rule_TSQL"
helpviewer_keywords:
  - "sp_delete_firewall_rule procedure"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest"
---
# sp_delete_firewall_rule (Azure SQL Database)

[!INCLUDE [asdb-asa](../../includes/applies-to-version/asdb-asa.md)]

Removes server-level firewall settings from your [!INCLUDE [ssSDS](../../includes/sssds-md.md)] server. This stored procedure is only available in the `master` database to the server-level principal login.

## Syntax

```syntaxsql
sp_delete_firewall_rule [ @name = ] N'name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the server-level firewall setting that will be removed. *@name* is **nvarchar(128)** with no default.

## Remarks

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], the credential data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

Because `sp_delete_firewall_rule` is an extended stored procedure, the data type of the value passed in for the parameter much match exactly. Implicit conversions from other types don't take place.

## Permissions

Only the server-level principal login created by the provisioning process can delete server level firewall rules. The user must be connected to the `master` database to execute `sp_delete_firewall_rule`.

## Examples

The following example removes the server-level firewall setting named `Example setting 1`. Execute the statement in the virtual `master` database.

```sql
EXEC sp_delete_firewall_rule N'Example setting 1';
```

## Related content

- [Azure SQL Database Firewall](/azure/azure-sql/database/firewall-configure)
- [How to: Configure Firewall Settings (Azure SQL Database)](/azure/azure-sql/database/firewall-configure)
- [sp_set_firewall_rule (Azure SQL Database)](sp-set-firewall-rule-azure-sql-database.md)
- [sys.firewall_rules (Azure SQL Database)](../system-catalog-views/sys-firewall-rules-azure-sql-database.md)
