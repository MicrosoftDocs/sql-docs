---
title: "Set database recovery model"
description: Learn how to switch a SQL Server database from one recovery model to another by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/17/2022
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "database backups [SQL Server], recovery models"
  - "recovery [SQL Server], recovery model"
  - "backing up databases [SQL Server], recovery models"
  - "recovery models [SQL Server], switching"
  - "recovery models [SQL Server], viewing"
  - "database restores [SQL Server], recovery models"
  - "modifying database recovery models"
---
# View or Change the Recovery Model of a Database (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article describes how to view or change the database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. 
  
  A *recovery model* is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available. Three recovery models exist: simple, full, and bulk-logged. Typically, a database uses the full recovery model or simple recovery model. A database can be switched to another recovery model at any time. The **model** database sets the default recovery model of new databases.  
  
  For a more in depth explanation, see [recovery models](recovery-models-sql-server.md).
  
  
##  <a name="BeforeYouBegin"></a> Before you begin  
  

-   [Back up the transaction log](back-up-a-transaction-log-sql-server.md) **before** switching from the [full recovery or bulk-logged recovery model](recovery-models-sql-server.md).  
  
-   Point-in-time recovery isn't possible with bulk-logged model. Running transactions under the bulk-logged recovery model that require a transaction log restore, can expose them to data loss. To maximize data recoverability in a disaster-recovery scenario, switch to the bulk-logged recovery model only under the following conditions:  
  
    -   Users are currently not allowed in the database.  
  
    -   All modifications made during bulk processing are recoverable without depending on taking a log backup; for example, by rerunning the bulk processes.  
  
     If you satisfy these two conditions, you won't be exposed to any data loss while restoring a transaction log that was backed up under the bulk-logged recovery model.  
  
    > [!NOTE]  
    > If you switch to the full recovery model during a bulk operation, bulk operations logging changes from minimal logging to full logging, and vice versa.  
  
###  <a name="Security"></a> Required permissions  
   Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or change the recovery model  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, select the server name to expand the server tree.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, and then select **Properties**, which opens the **Database Properties** dialog box.  
  
4.  In the **Select a page** pane, select **Options**.  
  
5.  The current recovery model is displayed in the **Recovery model** list box.  
  
6.  Optionally, to change the recovery model select a different model list. The choices are **Full**, **Bulk-logged**, or **Simple**.  
  
7.  Select **OK**.

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the recovery model  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example shows how to query the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view to learn the recovery model of the **model** database.  
  
```sql  
SELECT name, recovery_model_desc  
   FROM sys.databases  
      WHERE name = 'model' ;  
GO  
  
```  
  
#### To change the recovery model  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example shows how to change the recovery model in the `model` database to `FULL` by using the `SET RECOVERY` option of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement.  
  
```sql  
USE [master] ;  
ALTER DATABASE [model] SET RECOVERY FULL ;  
```  
  
##  <a name="FollowUp"></a> Recommendations: After you change the recovery model  
  
-   **After switching between the full and bulk-logged recovery models**  
  
    -   After completing the bulk operations, immediately switch back to full recovery mode.  
  
    -   After switching from the bulk-logged recovery model back to the full recovery model, back up the log.  
  
        > [!NOTE]  
        > Your backup strategy remains the same: continue performing periodic database, log, and differential backups.  
  
-   **After switching from the simple recovery model**  
  
    -   Immediately after switching to the full recovery model or bulk-logged recovery model, take a full or differential database backup to start the log chain.  
  
        > [!NOTE]  
        > The switch to the full or bulk-logged recovery model takes effect only after the first data backup.  
  
    -   Schedule regular log backups, and update your restore plan accordingly.  
  
        > [!IMPORTANT]  
        > **Back up your logs**. If you do not back up the log frequently enough, the transaction log can expand until it runs out of disk space!  
  
-   **After switching to the simple recovery model**  
  
    -   Discontinue any scheduled jobs for backing up the transaction log.  
  
    -   Ensure periodic database backups are scheduled. Backing up your database is essential both to protect your data and to truncate the inactive portion of the transaction log.  
  
##  <a name="RelatedTasks"></a> Related tasks  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   [Create a Job](../../ssms/agent/create-a-job.md)  
  
-   [Disable or Enable a Job](../../ssms/agent/disable-or-enable-a-job.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Database Maintenance Plans](../maintenance-plans/maintenance-plans.md) (in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Books Online)  
  
## See Also  
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)  
  
  
