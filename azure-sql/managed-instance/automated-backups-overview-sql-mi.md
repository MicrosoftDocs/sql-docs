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

**** **OPTION 1** ****
this article uses an include file, and then has a "differences" section that highlights the differences. 
**** **OPTION 1** ****

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/automated-backups-overview-sql-db.md)
> * [Azure SQL Managed Instance](automated-backups-overview-sql-mi.md)


[!INCLUDE[automated-backup-overview](../includes/automated-backups-overview-include.md)]

## Backups and SQL Managed Instance 

This section highlights specific differences when configuring backups for Azure SQL Managed Instance. 

**Backup storage redundancy**

Azure SQL Database supports **locally-redundant (LRS)**, **zone-redundant (ZRS)**, **geo-redundant (GRS)**, and **geo-zone-redundant (GZRS)** storage blobs for automated backups.  To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy). 

**Short-term retention**

If you delete a managed instance, all databases on that managed instance are also deleted and cannot be recovered. You cannot restore a deleted managed instance. But if you had configured long-term retention (LTR) for a managed instance, long-term retention backups are not deleted, and can be used to restore databases to a different managed instance in the same subscription, to a point in time when a long-term retention backup was taken.

**Long-term retention** 

It is possible to set the point-in-time restore (PITR) backup retention rate once a database has been deleted in the 0-35 day range. 

LTR backup storage redundancy in Azure SQL Managed Instance is inherited from the backup storage redundancy used by STR at the time the LTR policy is defined and cannot be changed subsequently, even if the STR backup storage redundancy is changed in the future. 

**Backup storage costs**

For managed instances, a backup storage amount equal to 100 percent of the maximum instance storage size, is provided at no extra charge. 

The total billable backup storage size is aggregated at the instance level and is calculated as follows:

`Total billable backup storage size = (total size of full backups + total size of differential backups + total size of log backups) – maximum instance data storage`

For pricing see the [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql/sql-managed-instance/single/) page. 





## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they protect your data from accidental corruption or deletion. To learn about the other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- To learn all about backup storage consumption on Azure SQL Managed Instance, see [Backup storage consumption on Managed Instance explained](https://aka.ms/mi-backup-explained).
- To learn how to fine-tune backup storage retention and costs for Azure SQL Managed Instance, see [Fine tuning backup storage costs on Managed Instance](https://aka.ms/mi-backup-tuning).
