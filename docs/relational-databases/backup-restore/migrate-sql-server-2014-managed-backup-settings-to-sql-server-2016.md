---
title: "Migrate SQL Server 2014 Managed Backup Settings to SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: ae937ebb-24ff-4a33-be3c-8f85328dfc75
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Migrate SQL Server 2014 Managed Backup Settings to SQL Server 2016
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic covers migration considerations for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] when upgrading from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
 The procedures and underlying behavior of [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] has changed in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. The following sections describe the functional changes and their implications.  
  
## Overview  
 The following table describes some of the key functional differences for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
|Area|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|  
|----------|---------------------------|---------------------------|  
|**Namespace:**|smart_admin|managed_backup|  
|**System Stored Procedures:**|sp_set_db_backup<br /><br /> sp_set_instance_backup|[managed_backup.sp_backup_config_basic (Transact-SQL)](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md)<br /><br /> [sp_backup_config_advanced](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md)<br /><br /> [sp_backup_config_schedule](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md)|  
|**Security:**|SQL credential using a Microsoft Azure storage account and access key.|SQL credential using a Microsoft Azure Shared Access Signature (SAS) token.|  
|**Underlying Storage:**|Microsoft Azure Storage using page blobs.|Microsoft Azure Storage using block blobs.|  
  
## Benefits  
 There are several benefits to using the new functionality in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
-   Block blobs cost less to store.  
  
-   With striping, you can store much larger backups (12 TB versus 1 TB for page blobs).  
  
-   Striping also improves the restore time for large databases  
  
-   For other improvements to [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], see [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
## Considerations  
 After you upgrade from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], note the following [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] considerations:  
  
-   Any databases previously configured for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] on [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] will continue to use the **smart_admin** system procedures and underlying behavior on [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
-   The **smart_admin** procedures are not supported for any new configurations of [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] on [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. You must use the new **managed_backup** procedures and functionality.  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
