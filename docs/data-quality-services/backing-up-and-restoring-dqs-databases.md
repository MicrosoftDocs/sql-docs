---
title: "Backing Up and Restoring DQS Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: f3091f62-2234-4a80-a615-cf14c2a1da85
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Backing Up and Restoring DQS Databases

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to back up and restore the DQS databases.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must know or remember the password for the database master key that you provided during the DQS server installation.  
  
-   Ensure that there are no running activities or processes in DQS. This can be verified using the **Activity Monitoring** screen. For detailed information about working in this screen, see [Monitor DQS Activities](../data-quality-services/monitor-dqs-activities.md).  
  
-   Ensure that there are no users logged on the DQS server.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Your Windows user account must be a member of the sysadmin fixed server role in the SQL Server instance to perform the backup and restore operations.  
  
-   You must have the dqs_administrator role on the DQS_MAIN database to terminate any running activities or stop any running processes in DQS.  
  
##  <a name="BackupRestore"></a> Backup and Restore DQS Databases  
  
1.  Start Microsoft SQL Server Management Studio, and connect to the appropriate SQL Server instance.  
  
2.  In Object Explorer, expand the **Databases** node.  
  
3.  Back up the DQS_STAGING_DATA database. For step-by-step instructions for backing a SQL Server database, see [Create a Full Database Backup &#40;SQL Server&#41;](../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
4.  Back up the DQS_PROJECTS database.  
  
5.  Back up the DQS_MAIN database.  
  
6.  Disconnect from the current instance of SQL Server, and connect to the SQL Server instance where you want to restore these databases.  
  
7.  Restore DQS_MAIN database. For step-by-step instructions to restore a SQL Server database, see [Restore a Database Backup Using SSMS](../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).  
  
8.  Restore the DQS_PROJECTS database.  
  
9. Restore the DQS_STAGING_DATA database.  
  
10. In Object Explorer, right-click the server, and then click **New Query**.  
  
11. In the Query Editor window, copy the following SQL statements, and replace *\<PASSWORD>* with the password that you provided during the DQS installation for the database master key:  
  
    ```  
    USE [DQS_MAIN]  
    GO  
    EXECUTE [internal_core].[RestoreDQDatabases] '<PASSWORD>'  
    GO  
  
    ```  
  
12. Press F5 to execute the statements. Check the **Results** pane to verify that the statements have executed successfully.  
  
## See Also  
 [Manage DQS Databases](../data-quality-services/manage-dqs-databases.md)  
  
  
