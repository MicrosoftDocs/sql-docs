---
title: "Configure Backup Compression (SQL Server) | Microsoft Docs"
description: This article describes how to over override the server-level default when creating a single backup or scheduling a series of routine backups in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 430905eb-d218-458c-bd48-aeee6fbb7446
author: MashaMSFT
ms.author: mathoma
---
# Configure Backup Compression (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

 At installation, backup compression is off by default. The **backup compression default** server-level configuration option sets the default behavior for backup compression. However, you can override the server-level default when creating a single backup or scheduling a series of routine backups. To change the server-level default, see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).  

## Use integrated acceleration and offloading

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], use [Integrated acceleration and offloading](../integrated-acceleration/overview.md) to compress backups with Intel&reg; QuickAssist Technology.
  
## Override the Backup Compression Default  
 You can change the backup compression behavior for an individual backup, backup job, or log shipping configuration.  
  
-   **[!INCLUDE[tsql](../../includes/tsql-md.md)]**  
  
     To override the server backup-compression default when creating a backup, use either WITH NO_COMPRESSION or WITH COMPRESSION in your [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement.  
  
     For a log shipping configuration, you can control the backup compression behavior of log backups by using [sp_add_log_shipping_primary_database](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md)[sp_change_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-log-shipping-primary-database-transact-sql.md).  
  
-   **[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
     For information about how to view or configure the backup compression default option for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).  
  
     You can override the server backup-compression default when creating a backup by specifying **Compress backup** or **Do not compress backup** in any of the following dialog boxes:  
  
    -   [Back Up Database (Options Page)](../../relational-databases/backup-restore/back-up-database-backup-options-page.md)  
  
         When backing up a database, you can control backup compression for an individual database, file, or log backup.  
  
    -   [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md)  
  
         The Maintenance Plan Wizard enables you to control backup compression for each set full or differential database backups or log backups that you schedule.  
  
    -   Integration Services (SSIS) [Back Up Database task](../../integration-services/control-flow/back-up-database-task.md)  
  
         You can control the backup compression behavior when creating a package for backing up a single database or multiple databases.  
  
    -   [Log Shipping Transaction Log Backup Settings](../../relational-databases/databases/log-shipping-transaction-log-backup-settings.md)  
  
         You can control the backup compression behavior of log backups.  
  
  
## See Also  
 [Backup Compression &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-compression-sql-server.md)  
  
  
