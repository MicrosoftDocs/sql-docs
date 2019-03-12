---
title: "Backup Stretch-enabled databases (Stretch Database) | Microsoft Docs"
ms.date: "06/14/2016"
ms.service: sql-server-stretch-database
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Stretch Database, disabling"
  - "disabling Stretch Database"
ms.assetid: 18f0dff0-d8ce-4bee-a935-76ed6dfb3208
author: rothja
ms.author: jroth
manager: craigg
---
# Backup Stretch-enabled databases (Stretch Database)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]


 Database backups help you to recover from many types of failures, errors, and disasters.  
  
 -   You have to back up your Stretch-enabled SQL Server databases.  
      
 -   Microsoft Azure automatically backs up the remote data that Stretch Database has migrated from SQL Server to Azure.  

> [!TIP]
> Backup is only one part of a complete high availability and business continuity solution. For more info about high availability, see [High Availability Solutions](../../database-engine/sql-server-business-continuity-dr.md).
   
## Back up your SQL Server data  
  
To back up your Stretch-enabled SQL Server databases, you can continue to use the SQL Server backup methods that you currently use. For more info, see [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).
  
 Backups of a Stretch-enabled SQL Server database contain only local data and data eligible for migration at the point in time when the backup runs. (Eligible data is data that has not yet been migrated, but will be migrated to Azure based on the migration settings of the tables.) This is known as a **shallow** backup and does not include the data already migrated to Azure.  
  
## Back up your remote Azure data   
  
Microsoft Azure automatically backs up the remote data that Stretch Database has migrated from SQL Server to Azure.    
### Azure reduces the risk of data loss with automatic backup  
The SQL Server Stretch Database service on Azure protects your remote databases with automatic storage snapshots at least every 8 hours. It retains each snapshot for 7 days to provide you with a range of possible restore points.  
  
### Azure reduces the risk of data loss with geo-redundancy  
Azure database backups are stored on geo-redundant Azure Storage (RA-GRS) and are therefore geo-redundant by default. Geo-redundant storage replicates your data to a secondary region that is hundreds of miles away from the primary region. In both primary and secondary regions, your data is replicated three times each, across separate fault domains and upgrade domains. This ensures that your data is durable even in the case of a complete regional outage or disaster that renders one of the Azure regions unavailable.

### <a name="stretchRPO"></a>Stretch Database reduces the risk of data loss for your Azure data by retaining migrated rows temporarily
After Stretch Database migrates eligible rows from SQL Server to Azure, it retains those rows in the staging table for a minimum of 8 hours. If you restore a backup of your Azure database, Stretch Database uses the rows saved in the staging table to reconcile the SQL Server and the Azure databases.

After you restore a backup of your Azure data, you have to run the stored procedure [sys.sp_rda_reauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect the Stretch-enabled SQL Server database to the remote Azure database. When you run **sys.sp_rda_reauthorize_db**, Stretch Database automatically reconciles the SQL Server and the Azure databases.

To increase the number of hours of migrated data that Stretch Database retains temporarily in the staging table, run the stored procedure [sys.sp_rda_set_rpo_duration](../../relational-databases/system-stored-procedures/sys-sp-rda-set-rpo-duration-transact-sql.md) and specify a number of hours greater than 8. To decide how much data to retain, consider the following factors:
-   The frequency of automatic Azure backups (at least every 8 hours).
-   The time required after a problem to recognize the problem and to decide to restore a backup.
-   The duration of the Azure restore operation.

> [!NOTE]
> Increasing the amount of data that Stretch Database retains temporarily in the staging table increases the amount of space required on the SQL Server.

To check the number of hours of data that Stretch Database currently retains temporarily in the staging table, run the stored procedure [sys.sp_rda_get_rpo_duration](../../relational-databases/system-stored-procedures/sys-sp-rda-get-rpo-duration-transact-sql.md).

## See Also  
[Restore Stretch-enabled databases](../../sql-server/stretch-database/restore-stretch-enabled-databases-stretch-database.md)  
 [Manage and troubleshoot Stretch Database](../../sql-server/stretch-database/manage-and-troubleshoot-stretch-database.md)   
   
  
  
