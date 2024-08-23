---
title: "Firewall rules stored procedures"
titleSuffix: Azure SQL Database
description: "Firewall rules stored procedures (Azure SQL Database)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: azure-sql-database
ms.topic: "reference"
helpviewer_keywords:
  - "firewall rules stored procedures"
  - "firewall_rules, setting"
  - "firewall_rules, Azure SQL Database"
  - "firewall systems, Azure SQL Database"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest"
---
# Firewall rules stored procedures (Azure SQL Database)

[!INCLUDE [asdb-asa](../../includes/applies-to-version/asdb-asa.md)]

This section contains the following stored procedures that set or delete firewall rules. [!INCLUDE [tsql_md](../../includes/tsql-md.md)] firewall rules can be used with [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] and [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. For more information, see [Azure SQL Database and Azure Synapse IP firewall rules](/azure/azure-sql/database/firewall-configure).

- [sp_set_firewall_rule (Azure SQL Database)](sp-set-firewall-rule-azure-sql-database.md)
- [sp_delete_firewall_rule (Azure SQL Database)](sp-delete-firewall-rule-azure-sql-database.md)
- [sp_set_database_firewall_rule (Azure SQL Database)](sp-set-database-firewall-rule-azure-sql-database.md)
- [sp_delete_database_firewall_rule (Azure SQL Database)](sp-delete-database-firewall-rule-azure-sql-database.md)

For [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)], use Windows firewall rules. For more information, see [Configure Windows Firewall for Database Engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).
