---
title: "View or Change the Recovery Model of a Database (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "database backups [SQL Server], recovery models"
  - "recovery [SQL Server], recovery model"
  - "backing up databases [SQL Server], recovery models"
  - "recovery models [SQL Server], switching"
  - "recovery models [SQL Server], viewing"
  - "database restores [SQL Server], recovery models"
  - "modifying database recovery models"
ms.assetid: 94918d1d-7c10-4be7-bf9f-27e00b003a0f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View or Change the Recovery Model of a Database (SQL Server)
  This topic describes how to view or change the recovery model of a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. A *recovery model* is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available. Three recovery models exist: simple, full, and bulk-logged. Typically, a database uses the full recovery model or simple recovery model. A database can be switched to another recovery model at any time. The **model** database sets the default recovery model of new databases.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To view or change the recovery model of a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up Recommendations:**  [After You Change the Recovery Model](#FollowUp)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Before switching from the full recovery or bulk-logged recovery model, back up the transaction log.  
  
-   Point-in-time recovery is not possible with bulk-logged model. Therefore, if you run transactions under the bulk-logged recovery model that might require a transaction log restore, these transactions could be exposed to data loss. To maximize data recoverability in a disaster-recovery scenario, we recommend that you switch to the bulk-logged recovery model only under the following conditions:  
  
    -   Users are currently not allowed in the database.  
  
    -   All modifications made during bulk processing are recoverable without depending on taking a log backup; for example, by re-running the bulk processes.  
  
     If you satisfy these two conditions, you will not be exposed to any data loss while restoring a transaction log that was backed up under the bulk-logged recovery model..  
  
> [!NOTE]  
>  If you switch to the full recovery model during a bulk operation, the logging of the bulk operation changes from minimal logging to full logging, and vice versa.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or change the recovery model  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, and then click **Properties**, which opens the **Database Properties** dialog box.  
  
4.  In the **Select a page** pane, click **Options**.  
  
5.  The current recovery model is displayed in the **Recovery model** list box.  
  
6.  Optionally, to change the recovery model select a different model list. The choices are **Full**, **Bulk-logged**, or **Simple**.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the recovery model  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to query the [sys.databases](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql) catalog view to learn the recovery model of the **model** database.  
  
```tsql  
SELECT name, recovery_model_desc  
   FROM sys.databases  
      WHERE name = 'model' ;  
GO  
  
```  
  
#### To change the recovery model  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to change the recovery model in the `model` database to `FULL` by using the `SET RECOVERY` option of the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql-set-options) statement.  
  
```tsql  
USE master ;  
ALTER DATABASE model SET RECOVERY FULL ;  
```  
  
##  <a name="FollowUp"></a> Follow Up Recommendations: After You Change the Recovery Model  
  
-   **After switching between the full and bulk-logged recovery models**  
  
    -   After completing the bulk operations, immediately switch back to full recovery mode.  
  
    -   After switching from the bulk-logged recovery model back to the full recovery model, back up the log.  
  
        > [!NOTE]  
        >  Your backup strategy remains the same: continue performing periodic database, log, and differential backups.  
  
-   **After switching from the simple recovery model**  
  
    -   Immediately after switching to the full recovery model or bulk-logged recovery model, take a full or differential database backup to start the log chain.  
  
        > [!NOTE]  
        >  The switch to the full or bulk-logged recovery model takes effect only after the first data backup.  
  
    -   Schedule regular log backups, and update your restore plan accordingly.  
  
        > [!IMPORTANT]  
        >  If you do not back up the log frequently enough, the transaction log can expand until it runs out of disk space.  
  
-   **After switching to the simple recovery model**  
  
    -   Discontinue any scheduled jobs for backing up the transaction log.  
  
    -   Ensure periodic database backups are scheduled. Backing up your database is essential both to protect your data and to truncate the inactive portion of the transaction log.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   [Create a Job](../../ssms/agent/create-a-job.md)  
  
-   [Disable or Enable a Job](../../ssms/agent/disable-or-enable-a-job.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Database Maintenance Plans](../maintenance-plans/maintenance-plans.md) (in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Books Online)  
  
## See Also  
 [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md)   
 [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)   
 [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md)  
  
  
