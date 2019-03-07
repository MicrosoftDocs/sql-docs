---
title: "Configure Backup Compression (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 430905eb-d218-458c-bd48-aeee6fbb7446
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure Backup Compression (SQL Server)
  At installation, backup compression is off by default. The default behavior for backup compression is defined by the **backup compression default** Option server-level configuration option. However, you can override the server-level default when creating a single backup or scheduling a series of routine backups. To change the server-level default, see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).  
  
## Override the Backup Compression Default  
 You can change the backup compression behavior for an individual backup, backup job, or log shipping configuration.  
  
-   **[!INCLUDE[tsql](../../includes/tsql-md.md)]**  
  
     To override the server backup-compression default when creating a backup, use either WITH NO_COMPRESSION or WITH COMPRESSION in you [BACKUP](/sql/t-sql/statements/backup-transact-sql) statement.  
  
     For a log shipping configuration, you can control the backup compression behavior of log backups by using [sp_add_log_shipping_primary_database](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql)[sp_change_log_shipping_primary_database &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-change-log-shipping-primary-database-transact-sql).  
  
-   **[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
     For information about how to view or configure the backup compression default option for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).  
  
     You can override the server backup-compression default when creating a backup by specifying **Compress backup** or **Do not compress backup** in any of the following dialog boxes:  
  
    -   [Back Up Database (Options Page)](back-up-database-backup-options-page.md)  
  
         When backing up a database, you can control backup compression for an individual database, file, or log backup.  
  
    -   [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md)  
  
         The Maintenance Plan Wizard enables you to control backup compression for each set full or differential database backups or log backups that you schedule.  
  
    -   Integration Services (SSIS) [Back Up Database task](../../integration-services/control-flow/back-up-database-task.md)  
  
         You can control the backup compression behavior when creating a package for backing up a single database or multiple databases.  
  
    -   [Log Shipping Transaction Log Backup Settings](../databases/log-shipping-transaction-log-backup-settings.md)  
  
         You can control the backup compression behavior of log backups.  
  
  
## See Also  
 [Backup Compression &#40;SQL Server&#41;](backup-compression-sql-server.md)  
  
  
