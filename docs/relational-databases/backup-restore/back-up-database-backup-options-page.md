---
title: "Back Up Database (Backup Options Page) | Microsoft Docs"
description: In SQL Server, use the Backup Options page of the Back Up Database dialog box to view or modify options for backup set, compression, and encryption.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.backupdatabase.options.f1"
  - "swb.backupdatabase.options.f1"
ms.assetid: df0ddcdb-c94e-472b-b786-469ae8117b93
author: MashaMSFT
ms.author: mathoma
---
# Back Up Database (Backup Options Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the  **Backup Options** page of the **Back Up Database** dialog box to view or modify database backup options.  
  
 **To create a backup by using SQL Server Management Studio**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
> [!IMPORTANT]  
>  You can define a database maintenance plan to create database backups. For more information, see [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md) and [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md).  
  
> [!NOTE]  
>  When you specify a backup task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)][BACKUP](../../t-sql/statements/backup-transact-sql.md) script by clicking the **Script** button and then selecting a destination for the script.  
  
## Options  
  
### Backup set  
 The options of the **Backup set** panel allow for you to specify optional information about the backup set created by the back up operation.  
  
 **Name**  
 Specify the backup set name. The system automatically suggests a default name based on the database name and the backup type.  
  
 For information about backup sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
 **Description**  
 Enter a description of the backup set.  
  
 **Backup set will expire**  
 Choose one of the following expiration options. This option is disabled if **URL** is chosen as the backup destination.  
  
|Option|Description|  
|-|-|  
|**After**|Specify the number of days that must elapse before this backup set expires and can be overwritten. This value can be from 0 to 99999 days; a value of 0 days means that the backup set will never expire.<br /><br /> The default value for backup expiration is the value set in the **Default backup media retention (in days)** option. To access this, right-click the server name in Object Explorer and select **Properties**; then click the **Database Settings** page of the **Server Properties** dialog box.|  
|**On**|Specify a specific date when the backup set expires and can be overwritten.|  
  
### Compression  
 [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or a later version) supports [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md).  
  
 **Set backup compression**  
 In [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or a later version), select one of the following backup compression values:  
  
|Value|Description|  
|-|-|  
|**Use the default server setting**|Click to use the server-level default.<br /><br /> This default is set by the **backup compression default** server-configuration option. For information about how to view the current setting of this option, see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).|  
|**Compress backup**|Click to compress the backup, regardless of the server-level default.<br /><br /> **\*\* Important \*\*** By default, compression significantly increases CPU usage, and the additional CPU consumed by the compression process might adversely impact concurrent operations. Therefore, you might want to create low-priority compressed backups in a session whose CPU usage is limited by [Resource Governor](../../relational-databases/resource-governor/resource-governor.md). For more information, see [Use Resource Governor to Limit CPU Usage by Backup Compression &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md).|  
|**Do not compress backup**|Click to create an uncompressed backup, regardless of the server-level default.|  
  
### Encryption  
 To create an encrypted backup, check the **Encrypt backup** check box. Select an encryption algorithm to use for the encryption step, and provide a Certificate or Asymmetric key from a list of existing certificates or asymmetric keys. The available algorithms for encryption are:  
  
-   AES 128  
  
-   AES 192  
  
-   AES 256  
  
-   Triple DES  
  
> [!TIP]  
>  The encryption option is disabled if you selected to append to existing backup set.  
>   
>  It is recommended practice to back up your certificate or keys and store them in a different location than the backup you encrypted.  
>   
>  Only keys residing in the Extensible Key Management (EKM) are supported.  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)   
 [Back Up Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-files-and-filegroups-sql-server.md)   
 [Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)  
  
  
