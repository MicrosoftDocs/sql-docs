---
title: "sp_delete_database_firewall_rule (Azure SQL Database)"
description: sp_delete_database_firewall_rule removes database-level firewall setting from your Azure SQL Database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: azure-sql-database
ms.topic: "reference"
f1_keywords:
  - "sp_delete_database_firewall_rule"
  - "sp_delete_database_firewall_rule_TSQL"
  - "sys.sp_delete_database_firewall_rule"
  - "sys.sp_delete_database_firewall_rule_TSQL"
helpviewer_keywords:
  - "sp_delete_database_firewall_rule procedure"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---
# sp_delete_database_firewall_rule (Azure SQL Database)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Removes database-level firewall setting from your [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. Database firewall rules can be configured and deleted for the `master` database, and for user databases on [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].

## Syntax

```syntaxsql
sp_delete_database_firewall_rule [ @name = ] [ N ] 'name'
[ ; ]
```

## Arguments

#### [ @name = ] [ N ] '*name*'

The name of the database-level firewall setting to be removed. *@name* is **nvarchar(128)** with no default value. The Unicode prefix `N` is optional for [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].

## Permissions

Only the server-level principal login created by the provisioning process, or a Microsoft Entra admin assigned as admin, can delete database-level firewall rules.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Examples

The following example removes the database-level firewall setting named `Example DB Setting 1`.

```sql
EXECUTE sp_delete_database_firewall_rule N'Example DB Setting 1';
```

## Related content

- [Azure SQL Database and Azure Synapse IP firewall rules](/azure/azure-sql/database/firewall-configure)
- [sp_set_firewall_rule (Azure SQL Database)](sp-set-firewall-rule-azure-sql-database.md)
- [sp_set_database_firewall_rule (Azure SQL Database)](sp-set-database-firewall-rule-azure-sql-database.md)
- [sys.database_firewall_rules (Azure SQL Database)](../system-catalog-views/sys-database-firewall-rules-azure-sql-database.md)
