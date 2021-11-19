---
description: "Firewall Rules Stored Procedures (Azure SQL Database)"
title: "Firewall Rules Stored Procedures"
titleSuffix: Azure SQL Database
ms.date: "07/28/2016"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "firewall rules stored procedures"
  - "firewall_rules, setting"
  - "firewall_rules, Azure SQL Database"
  - "firewall systems, Azure SQL Database"
ms.assetid: 3d4c2585-00de-46b5-8eee-0efb71cb3aea
author: VanMSFT
ms.author: vanto
ms.custom: seo-dt-2019
monikerRange: "= azuresqldb-current || = azure-sqldw-latest"
---
# Firewall Rules Stored Procedures (Azure SQL Database)
[!INCLUDE [asdb-asa](../../includes/applies-to-version/asdb-asa.md)]

  This section contains the following stored procedures that set or delete firewall rules. [!INCLUDE[tsql_md](../../includes/tsql-md.md)] firewall rules can be used with [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] and [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)]. For more information, see [Configure Azure SQL Database firewall rules - overview](/azure/azure-sql/database/firewall-configure).

:::row:::
    :::column:::
        [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)
    :::column-end:::
    :::column:::
        [sp_delete_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-delete-firewall-rule-azure-sql-database.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_set_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md)
    :::column-end:::
    :::column:::
        [sp_delete_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-delete-database-firewall-rule-azure-sql-database.md)
    :::column-end:::
:::row-end:::

&nbsp;
  
For [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], use Windows firewall rules. For more information, see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).   
