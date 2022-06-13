---
title: Automatic, geo-redundant backups
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Azure SQL Database and Azure SQL Managed Instance automatically create a local database backup every few minutes and use Azure read-access geo-redundant storage for geo-redundancy.
services:
  - "sql-database"
ms.service: sql-db-mi
ms.subservice: backup-restore
ms.custom:
  - "references_regions"
  - "devx-track-azurepowershell"
  - "devx-track-azurecli"
ms.topic: conceptual
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: wiassaf, mathoma, danil
ms.date: 04/26/2022
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Automated backups - Azure SQL Database & Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

**** **OPTION 3** ****
this article uses an include file that has tabs to highlight the differences between the two products
**** **OPTION 3** ****

> [!div class="op_single_selector"]
> * [Azure SQL Database](automated-backups-overview-sql-db-include-tabs.md)
> * [Azure SQL Managed Instance](../managed-instance/automated-backups-overview-sql-mi-include-tabs.md)



[!INCLUDE[automated-backup-overview-tabs](../includes/automated-backups-overview-include-tabs.md)]


## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they protect your data from accidental corruption or deletion. To learn about the other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- To learn all about backup storage consumption on Azure SQL Managed Instance, see [Backup storage consumption on Managed Instance explained](https://aka.ms/mi-backup-explained).
- To learn how to fine-tune backup storage retention and costs for Azure SQL Managed Instance, see [Fine tuning backup storage costs on Managed Instance](https://aka.ms/mi-backup-tuning).
