---
title: "Manage DQS Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 655a67aa-d662-42f2-b982-c6217125ada8
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Manage DQS Databases

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This section provides information about database management activities that can be performed on the DQS databases such as backup/restore or detach/attach.  
  
##  <a name="BackupRestore"></a> Backup and Restore the DQS Databases  
 Backup and restore of SQL Server databases are common operations that database administrators perform for preventing loss of data in a case of disaster by recovering data from the backup databases. [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] is primarily implemented by two SQL Server databases: DQS_MAIN and DQS_PROJECTS. The backup and restore procedures of the [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) databases are similar to any other SQL Server databases.There are three challenges that are associated with backup and restore of the DQS databases:  
  
-   The backup and restore operations of the DQS databases must be synchronized. Otherwise the restored [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] will not be functional.  
  
-   The two DQS databases, DQS_MAIN and DQS_PROJECTS, contain assemblies and other complex objects, apart from just simple database objects (such as tables and stored procedures).  
  
-   There are some entities outside of the DQS databases that must exist for the DQS databases to be functional as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)], specifically the two SQL Server logins (##MS_dqs_db_owner_login## and ##MS_dqs_service_login##), and an initialization stored procedure (DQInitDQS_MAIN) in the master database.  
  
 For detailed information about backup and restore in SQL Server, see [Back Up and Restore of SQL Server Databases](../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
### Default Autogrowth Size and Recovery Model for the DQS Databases  
 To prevent DQS databases and transaction logs to grow infinitely and potentially fill the hard disk:  
  
-   The default **Autogrowth** size of the DQS databases is set to 10%.  
  
-   The default recovery model of the DQS databases is set to **Simple**. In the Simple recovery model, transactions are minimally logged, and the log truncation happens automatically after the transaction is complete to free up space in the transaction log (.ldf file). For detailed information about the simple recovery model, see [Full Database Backups &#40;SQL Server&#41;](../relational-databases/backup-restore/full-database-backups-sql-server.md).  
  
> [!IMPORTANT]
>  -   In the Simple recovery model, when log records remain active for a long time (for example, a long and time-consuming transaction), log truncation can be delayed, and therefore can result in the filling up of transaction log. Also, log truncation does not reduce the size of the physical log file (.ldf file). To reduce the size of a physical log file, you need to shrink the log file. For information about troubleshooting issues around transaction log, see [The Transaction Log &#40;SQL Server&#41;](../relational-databases/logs/the-transaction-log-sql-server.md) or the Microsoft Support article at [https://go.microsoft.com/fwlink/?LinkId=237446](https://go.microsoft.com/fwlink/?LinkId=237446).  
> -   You must regularly perform a Full or Differential backup of the DQS databases and back up the transaction log as well to perform point-in-time recovery of data. For more information, see [Full Database Backups &#40;SQL Server&#41;](../relational-databases/backup-restore/full-database-backups-sql-server.md) and [Back Up a Transaction Log &#40;SQL Server&#41;](../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md).  
  
##  <a name="DetachAttach"></a> Detach/Attach the DQS Databases  
 You can detach the data and transaction log files of the DQS databases, and then reattach the databases to the same or another instance of SQL Server if you want to change the DQS databases to a different instance of SQL Server on the same computer or to move the database.  
  
 For detailed information about things to consider before and during detaching and attaching databases in SQL Server, see [Database Detach and Attach &#40;SQL Server&#41;](../relational-databases/databases/database-detach-and-attach-sql-server.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to back up and restore the DQS databases.|[Backing Up and Restoring DQS Databases](../data-quality-services/backing-up-and-restoring-dqs-databases.md)|  
|Describes how to detach and attach the DQS databases.|[Detaching and Attaching DQS Databases](../data-quality-services/detaching-and-attaching-dqs-databases.md)|  
  
## See Also  
 [DQS Administration](../data-quality-services/dqs-administration.md)  
  
  
