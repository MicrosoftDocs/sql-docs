---
title: "Migrate managed backup settings"
description: This topic covers migration considerations for SQL Server Managed Backup to Microsoft Azure when upgrading from SQL Server 2014 to SQL Server 2016.
author: MashaMSFT
ms.author: mathoma
ms.date: "12/17/2019"
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom: intro-migration
---
# Migrate managed backup settings
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic covers migration considerations for [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] when upgrading from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
 The procedures and underlying behavior of [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] has changed in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. The following sections describe the functional changes and their implications.  
  
## Overview  
 The following table describes some of the key functional differences for [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] between [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
|Area|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]|  
|----------|---------------------------|---------------------------|  
|**Namespace:**|smart_admin|managed_backup|  
|**System Stored Procedures:**|sp_set_db_backup<br /><br /> sp_set_instance_backup|[managed_backup.sp_backup_config_basic (Transact-SQL)](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md)<br /><br /> [sp_backup_config_advanced](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md)<br /><br /> [sp_backup_config_schedule](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md)|  
|**Security:**|SQL credential using a Microsoft Azure storage account and access key.|SQL credential using a Microsoft Azure Shared Access Signature (SAS) token.|  
|**Underlying Storage:**|Microsoft Azure Storage using page blobs.|Microsoft Azure Storage using block blobs.|  
  
## Benefits  
 There are several benefits to using the new functionality in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
-   Block blobs cost less to store.  
  
-   With striping, you can store much larger backups (12 TB versus 1 TB for page blobs).  
  
-   Striping also improves the restore time for large databases  
  
-   For other improvements to [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], see [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
## Considerations  
 After you upgrade from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], note the following [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] considerations:  
  
-   Any databases previously configured for [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] on [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] will continue to use the **smart_admin** system procedures and underlying behavior on [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].  
  
-   The **smart_admin** procedures are not supported for any new configurations of [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] on [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. You must use the new **managed_backup** procedures and functionality.  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
