---
title: "SQL Server Backup and Restore with Windows Azure Blob Storage Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 6a0c9b6a-cf71-4311-82f2-12c445f63935
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Backup and Restore with Windows Azure Blob Storage Service
  This topic introduces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups to and restoring from the [Windows Azure Blob storage service](http://www.windowsazure.com/develop/net/how-to-guides/blob-storage/). It also provides a summary of the benefits of using the Windows Azure Blob service to store [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups.  
  
 SQL Server supports storing backups to the Windows Azure Blob storage service in the following ways:  
  
-   **Manage your backups  to Windows Azure:** Using the same methods used to backup to DISK and TAPE, you can now back up to Windows Azure storage by Specifying URL as the backup destination.  You can use this feature to manually backup or configure your own backup strategy like you would for a local storage or other off-site options. This feature is also referred to as **SQL Server Backup to URL**. For more information, see [SQL Server Backup to URL](sql-server-backup-to-url.md). This feature is available in SQL Server 2012 SP1 CU2 or later.  
  
    > [!NOTE]  
    >  For SQL Server versions previous to SQL Server 2014, you can use the add-in SQL Server Backup to Windows Azure Tool to quickly and easily create backups to Windows Azure storage. For more information, see [download center](https://go.microsoft.com/fwlink/?LinkID=324399).  
  
-   **Let SQL Server Manage backups to Windows Azure:** Configure SQL Server to manage the backup strategy and schedule backups for a single database, or several databases, or set defaults at the instance level. This feature is referred to as  **[!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]**. For more information see [SQL Server Managed  Backup to Windows Azure](sql-server-managed-backup-to-microsoft-azure.md). This feature is available in SQL Server 2014 or later.  
  
## Benefits of Using the Windows Azure Blob Service for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Backups  
  
-   Flexible, reliable, and limitless off-site storage: Storing your backups on Windows Azure Blob service can be a convenient, flexible, and easy to access off-site option. Creating off-site storage for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups can be as easy as modifying your existing scripts/jobs. Off-site storage should typically be far enough from the production database location to prevent a single disaster that might impact both the off-site and production database locations. By choosing to geo replicate the Blob storage you have an extra layer of protection in the event of a disaster that could affect the whole region. In addition, backups are available from anywhere and at any time and can easily be accessed for restores.  
  
-   Backup Archive: The Windows Azure Blob Storage service offers a better alternative to the often used tape option to archive backups. Tape storage might require physical transportation to an off-site facility and measures to protect the media. Storing your backups in Windows Azure Blob Storage provides an instant, highly available, and a durable archiving option.  
  
-   No overhead of hardware management:There is no overhead of hardware management with Windows Azure services. Windows Azure services manage the hardware and provide geo-replication for redundancy and protection against hardware failures.  
  
-   Currently for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in a Windows Azure Virtual Machine, backing up to Windows Azure Blob storage services can be done by creating attached disks. However, there is a limit to the number of disks you can attach to a Windows Azure Virtual Machine. This limit is 16 disks for an extra large instance and fewer for smaller instances. By enabling a direct backup to Windows Azure Blob Storage, you can bypass the 16 disk limit.  
  
     In addition, the backup file which now is stored in the Windows Azure Blob storage service is directly available to either an on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in a Windows Azure Virtual Machine, without the need for database attach/detach or downloading and attaching the VHD.  
  
-   Cost Benefits: Pay only for the service that is used. Can be cost-effective as an off-site and backup archive option. See the [Windows Azure Billing Considerations](#Billing) section for more information and links.  
  
##  <a name="Billing"></a> Windows Azure Billing Considerations:  
 Understanding Windows Azure storage costs enables you to forecast the cost of creating and storing backups in Windows Azure.  
  
 The [Windows Azure pricing calculator](https://go.microsoft.com/fwlink/?LinkId=277060) can help estimate your costs.  
  
 **Storage:** Charges are based on the space used and are calculated on a graduated scale and the level of redundancy. For more details, and up-to-date information, see the **Data Management** section of the [Pricing Details](https://go.microsoft.com/fwlink/?LinkId=277059) article.  
  
 **Data Transfers:** Inbound data transfers to Windows Azure are free. Outbound transfers are charged for the bandwidth use and calculated based on a graduated region-specific scale. For more details, see the [Data Transfers](https://go.microsoft.com/fwlink/?LinkId=277061) section of the Pricing Details article.  
  
## See Also  
 [SQL Server Backup to URL Best Practices and Troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)   
 [Back Up and Restore of System Databases &#40;SQL Server&#41;](back-up-and-restore-of-system-databases-sql-server.md)   
 [Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service](../tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)   
 [SQL Server Backup to URL](sql-server-backup-to-url.md)  
  
  
