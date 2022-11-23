---
title: "Define logical backup device - disk"
description: This article shows you how to define a logical backup device for a disk file in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.custom: seo-lt-2019
ms.date: "12/17/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backup devices [SQL Server], defining"
  - "backup devices [SQL Server], disks"
  - "disk backup devices [SQL Server]"
  - "database backups [SQL Server], disks"
  - "backing up databases [SQL Server], disks"
ms.assetid: 86331d43-c738-4523-ae3d-7d6700348ed1
author: MashaMSFT
ms.author: mathoma
---
# Define a Logical Backup Device for a Disk File (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to define a logical backup device for a disk file in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. A logical device is a user-defined name that points to a specific physical backup device (a disk file or tape drive).  The initialization of the physical device occurs later, when a backup is written to the backup device.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To define a logical backup device for a disk file, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The logical device name must be unique among all the logical backup devices on the server instance. To view the existing logical device names, query the [sys.backup_devices](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md) catalog view.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   We recommend that a backup disk be a different disk than the database data and log disks. This is necessary to make sure that you can access the backups if the data or log disk fails.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **diskadmin** fixed server role.  
  
 Requires permission to write to the disk.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To define a logical backup device for a disk file  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Server Objects**, and right-click **Backup Devices**.  
  
3.  Click **New Backup Device**. The **Backup Device** dialog box opens.  
  
4.  Enter a device name.  
  
5.  For the destination, click **File** and specify the full path of the file.  
  
6.  To define the new device, click **OK**.  
  
 To back up to this new device, add it to the **Back up to:** field in the **Back up Database** (**General**) dialog box. For more information, see [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To define a logical backup for a disk file  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_addumpdevice](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md) to define a logical backup device for a disk file. The example adds the disk backup device named `mydiskdump`, with the physical name `c:\dump\dump1.bak`.  
  
```sql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_addumpdevice 'disk', 'mydiskdump', 'c:\dump\dump1.bak' ;  
GO  
```  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [sys.backup_devices &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)   
 [sp_dropdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdevice-transact-sql.md)   
 [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)   
 [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
  
