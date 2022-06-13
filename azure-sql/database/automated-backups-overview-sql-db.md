---
title: Automatic, geo-redundant backups - Option 1
titleSuffix: Azure SQL Database
description: Azure SQL Database automatically create a local database backup every few minutes and use Azure read-access geo-redundant storage for geo-redundancy.
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
# Automated backups - Azure SQL Database 
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

**** **OPTION 1** ****
this article uses an include file, and then has a "differences" section that highlights the differences. 
**** **OPTION 1** ****

> [!div class="op_single_selector"]
> * [Azure SQL Database](automated-backups-overview-sql-db.md)
> * [Azure SQL Managed Instance](../managed-instance/automated-backups-overview-sql-mi.md)



[!INCLUDE[automated-backup-overview](../includes/automated-backups-overview-include.md)]


## Backups and SQL Database 

While the rest of the article applies to automated backups in both Azure SQL Database and Azure SQL Managed Instance, there are a few implementation differences between the two products that are highlighted in this section. 

**Backup storage redundancy**
Azure SQL Database supports **locally-redundant (LRS)**, **zone-redundant (ZRS)**, and **geo-redundant (GRS)** storage blobs for automated backups.  To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy). 

> [!IMPORTANT]
> Backup storage redundancy for Hyperscale can only be set during database creation. This setting cannot be modified once the resource is provisioned. Use the [Database copy](database-copy.md) process to update the backup storage redundancy settings for an existing Hyperscale database.  Learn more in [Hyperscale backups and storage redundancy](#hyperscale-backups-and-storage-redundancy).

**Short-term retention**

Differential backups for short-term retention can be configured to either a 12-hour or 24-hour frequency. A 24-hour differential backup frequency may increase the time required to restore the database. 

If you delete a server, all databases on that server are also deleted and cannot be recovered. You cannot restore a deleted server. But if you had configured long-term retention (LTR) for a database, long-term retention backups are not deleted, and can be used to restore databases to a different server in the same subscription, to a point in time when a long-term retention backup was taken.

**Long-term retention**

It's possible to change the backup storage redundancy option for long-term retention (LTR) with Azure SQL Database after your database is already created by changing the storage redundancy option of your short-term backups. However, the new storage redundancy option only applies to future backups taken after the change is made. All existing LTR backups for the database will continue to reside in the existing storage blob and only new backups will be stored on the newly requested storage blob type. 

> [!IMPORTANT]
> - Databases in the Hyperscale service tier for Azure SQL Database do not currently support long-term retention. 

**Backup storage costs**

In the DTU model, there's no additional charge for backup storage for databases and elastic pools. The price of backup storage is a part of database or pool price.

For single databases in the vCore model, a backup storage amount equal to 100 percent of the maximum data storage size for the database is provided at no extra charge. For elastic pools in vCore model, a backup storage amount equal to 100 percent of the maximum data storage for the pool is provided at no extra charge. 

For single databases, this equation is used to calculate the total billable backup storage usage:

`Total billable backup storage size = (size of full backups + size of differential backups + size of log backups) – maximum data storage`

For pooled databases, the total billable backup storage size is aggregated at the pool level and is calculated as follows:

`Total billable backup storage size = (total size of all full backups + total size of all differential backups + total size of all log backups) - maximum pool data storage`

Formulae used to calculate backup storage costs for Hyperscale databases can be found in [Hyperscale backup storage costs](#hyperscale-backup-storage-costs). 

For pricing see the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page. 

## Monitor storage consumption

For vCore databases in Azure SQL Database, the storage consumed by each type of backup (full, differential, and log) is reported on the database monitoring pane as a separate metric. The following diagram shows how to monitor the backup storage consumption for a single database. This feature is not available for databases using the DTU-purchasing model, or for Azure SQL Managed Instance. 

![Monitor database backup consumption in the Azure portal](./media/automated-backups-overview/backup-metrics.png)

Instructions on how to monitor consumption in Hyperscale can be found in [Hyperscale monitor backup consumption](#hyperscale-monitor-backup-consumption)

## Backup integrity

On an ongoing basis, the Azure SQL engineering team automatically tests the restore of automated database backups. 

Upon point-in-time restore, databases also receive DBCC CHECKDB integrity checks.

Any issues found during the integrity check will result in an alert to the engineering team. For more information, see [Data Integrity in SQL Database](https://azure.microsoft.com/blog/data-integrity-in-azure-sql-database/).

All database backups are taken with the CHECKSUM option to provide additional backup integrity.

## Compliance

When you migrate your database from a DTU-based service tier to a vCore-based service tier, the PITR retention is preserved to ensure that your application's data recovery policy isn't compromised. If the default retention doesn't meet your compliance requirements, you can change the PITR retention period. For more information, see [Change the PITR backup retention period](#change-the-short-term-retention-policy).

[!INCLUDE [GDPR-related guidance](~/../azure/includes/gdpr-intro-sentence.md)]


## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they protect your data from accidental corruption or deletion. To learn about the other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- To learn all about backup storage consumption on Azure SQL Managed Instance, see [Backup storage consumption on Managed Instance explained](https://aka.ms/mi-backup-explained).
- To learn how to fine-tune backup storage retention and costs for Azure SQL Managed Instance, see [Fine tuning backup storage costs on Managed Instance](https://aka.ms/mi-backup-tuning).
