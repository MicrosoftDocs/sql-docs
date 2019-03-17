---
title: "Secondary Database Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.databaseproperties.logshipping.settings.dest.f1"
ms.assetid: f992ffc9-ee42-43fe-acec-512032f0ded1
author: stevestein
ms.author: sstein
manager: craigg
---
# Secondary Database Settings
  Use this dialog box to configure and to modify the properties of a secondary database in the log shipping configuration.  
  
 For an explanation of log shipping concepts, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
## Options  
 **Secondary server instance**  
 Displays the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently configured to be a secondary server in the log shipping configuration.  
  
 **Secondary database**  
 Displays the name of the secondary database for the log shipping configuration. When adding a new secondary database to a log shipping configuration, you can choose a database from the list or type the name of a new of the database into the box. If you enter the name of a new database, you must select an option on the **Initialization** tab that restores a full database backup of the primary database into the secondary database. The new database is created as part of the restore operation.  
  
 **Connect**  
 Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for use as a secondary server in the log shipping configuration. The account used to connect must be a member of the sysadmin fixed server role on the secondary server instance.  
  
 **Initialize tab**  
 The options are as follows:  
  
 **Yes, generate a full backup of the primary database and restore it to the secondary database**  
 Have [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] configure your secondary database by backing up the primary database and restoring it on the secondary server. If you entered a new database name in the **Secondary database** box, the database will be created as part of the restore operation.  
  
 **Restore Options**  
 Click if you want to restore the data and log files for the secondary database into nondefault locations on the secondary server.  
  
 This button opens the **Restore Options** dialog box. There, you can specify paths to nondefault folders where you want the secondary database and its log to reside. If you specify either folder, you must specify both.  
  
 The paths must refer to drives that are local to the secondary server. Also, the paths must begin with a local drive letter and colon (for example, `C:`). Mapped drive letters or network paths are invalid.  
  
 If you click the **Restore Options** button and then decide that you want to use the default folders, we recommend that you cancel the **Restore Options** dialog box. If you have already specified nondefault locations and now want to use the default locations instead, click **Restore Options** again, clear the text boxes, and click OK.  
  
 **Yes, restore an existing backup of the primary database to the secondary database**  
 Have [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] use an existing backup of your primary database to initialize the secondary database. Type the location of that backup in the **Backup file** box. If you entered a new database name in the Secondary database box, the database will be created as part of the restore operation.  
  
 **Backup file**  
 Type the path and filename of the full database backup you want to use to initialize the secondary database if you chose the **Yes, restore and existing backup of the primary database to the secondary database option**.  
  
 **Restore Options**  
 See the description of this button earlier in this topic.  
  
 **No, the secondary database is initialized**  
 Specify that the secondary database is already initialized and ready to accept transaction log backups from the primary database. This option is not available if you typed a new database name in the **Secondary database** box.  
  
 **Copy Files tab**  
 The options are as follows:  
  
 **Destination folder for copied files**  
 Type the path to which transaction log backups should be copied for restoration to the secondary database. This is usually a local path to a folder located on the secondary server. If the folder is located on another server, however, you must specify a UNC path to the folder. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account of the secondary server instance must have Read permissions on this folder. You must also grant read and write permissions on this network share to the proxy accounts under which the copy and restore jobs will run at the secondary server instances. By default, this is the SQL Server Agent service account of the secondary server instance, but a sysadmin can choose other proxy accounts for the jobs.  
  
 **Delete copied files after**  
 Choose the length of time you want the copied transaction log backup files to remain in the destination folder before being deleted.  
  
 **Job name**  
 Displays the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job used to copy transaction log backup files from the primary server to the secondary server. When first creating this job, you can change the name by typing in the box.  
  
 **Schedule**  
 Displays the current schedule for the SQL Server Agent copy job to copy transaction log backups from the primary server to the secondary server. You can modify this schedule by clicking **Schedule...**.  
  
 **Schedule...**  
 Modify the parameters of the SQL Server Agent job that copies transaction log backups from the primary server to the secondary server.  
  
 **Disable this job**  
 Suspend the SQL Server Agent copy job.  
  
 **Restore Transaction Log tab**  
 The options are as follows:  
  
 **Disconnect users in the database when restoring backups**  
 Automatically disconnect users from the secondary database while transaction log backups are being restored.  
  
 **No recovery mode**  
 Leave the secondary database in NORECOVERY mode.  
  
 **Standby mode**  
 Leave the secondary database in STANDBY mode. This mode will allow read-only operations to be performed on the database.  
  
> [!IMPORTANT]  
>  If you change the recovery mode of an existing secondary database, for example, from **No recovery mode** to **Standby mode**, the change takes effect only after the next log backup is restored to the database.  
  
 **Delay restoring backups at least**  
 Choose the delay before transaction log backups are restored to the secondary database, if any.  
  
 **Alert if no restore occurs within**  
 Choose the amount of time you want log shipping to wait before raising an alert that no transaction log restores have occurred.  
  
 **Job name**  
 Displays the name of the SQL Server Agent job used to restore transaction log backups to the secondary database. When first creating this job, you can change the name by typing in the box.  
  
 **Schedule**  
 Displays the current schedule for the SQL Server Agent job used for restoring transaction log backups to the secondary database. You can modify this option by clicking **Schedule...**.  
  
 **Schedule...**  
 Modify the parameters associate with the SQL Server Agent restore job.  
  
 **Disable this job**  
 Suspend restore operations to the secondary database.  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)  
  
  
