---
title: Schedule a database backup operation using SSMS
description: This article describes how to schedule a database backup operation by using SQL Server Management Studio.
author: sevend2
ms.author: v-sidong
ms.reviewer: sureshka, mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
---

# How to schedule a database backup operation by using SQL Server Management Studio

This article describes how to schedule a database backup operation by using SQL Server Management Studio (SSMS) for [SQL Editions that support SQL Server Agent](../../sql-server/editions-and-components-of-sql-server-2022.md#SSMS). To schedule backups for SQL Express editions, see [Schedule and automate backups of SQL Server databases in SQL Server Express](/troubleshoot/sql/database-engine/backup-restore/schedule-automate-backup-database).

## Use maintenance plans to back up databases

You can use [Maintenance Plans](../maintenance-plans/maintenance-plans.md) to back up databases and transaction log files, perform differential backups, and define retention periods for your backups. For more information, see:

- [Create a Maintenance Plan](../maintenance-plans/create-a-maintenance-plan.md)
- [Create a Maintenance Plan (Maintenance Plan Design Surface)](../maintenance-plans/create-a-maintenance-plan-maintenance-plan-design-surface.md)
- [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md)

## Use the Script Action to Job option to back up databases

To schedule a database backup using the **Script Action to Job** option in SSMS, follow these steps:

1. Start SQL Server Management Studio and select **Connect** > **Database Engine**.
1. In the **Connect to Server** dialog box, select the appropriate values in the **Server type** list, in the **Server name** list, and in the **Authentication** list.
1. Select **Connect**.
1. In **Object Explorer**, expand **Databases**.
1. Right-click the database you want to back up, select **Tasks**, and then select **Back Up**.
1. In the **Back Up Database - \<DatabaseName>** dialog box, make sure that **Backup type** is set to **Full** and **Backup component** is set to **Database**.
1. Under **Destination**, select **Disk** for the **Back up to** option and then select **Add**. For other options on this page, see [Backup Database](back-up-database-general-page.md).
1. In the **Select Backup Destination** dialog box, enter a path and a file name in the **Destinations on disk** box, and then select **OK**.

1. In the **Script** list, select **Script Action to Job**.

    > [!NOTE]  
    > If you don't see the **Script Action to Job** option, you might be running an express edition of SQL Server. You can check your edition by running the query `select @@version`. For more information, see [Determine which version and edition of SQL Server Database Engine is running](/troubleshoot/sql/releases/find-my-sql-version).

1. In the **New Job** dialog box, select **Steps** under **Select a page**, and then select **Edit** if you want to change the job parameters.

    > [!NOTE]  
    > - In the **Job Step Properties - 1** dialog box, you can see the backup command.
    >  
    > - Microsoft is currently investigating an error message that can occur in SSMS at this step. If you get an exception at this step, repeat the procedure without this step and make any changes by editing the corresponding job under the **Jobs** menu under **SQL Server Agent** in **Object Explorer**.

1. Under **Select a page**, select **Schedules**, and then select **New**.

1. In the **New Job Schedule** dialog box, enter the job name in the **Name** box, specify the job schedule, and then select **OK**.

    > [!NOTE]  
    > If you want to configure alerts or notifications, you can select **Alerts** or **Notifications** under **Select a page**.

1. Select **OK** two times.

   You receive the following message:

   > The backup of database '\<DatabaseName>' completed successfully.

> [!NOTE]  
> - To verify the backup job, expand **SQL Server Agent**, and then expand **Jobs**. When you do this step, the SQL Server Agent service must be running.
>  
> - You can use a similar procedure to schedule transaction log and differential backups if you make the appropriate selection in Step 6 for **Backup type**. If you don't see the **Transaction Log** option in **Backup type**, check the recovery model of the database. You can't take transaction log backups if you are using the [Simple Recovery model](recovery-models-sql-server.md) for your database.

## Manually create SQL Server Agent jobs

Alternatively, you can create and schedule your own backup jobs using SQL Server Agent. For more information, see [Create Jobs](../../ssms/agent/create-jobs.md) and [Schedule a Job](../../ssms/agent/schedule-a-job.md).

## Related content

- [Script queries from the GUI](../../ssms/tutorials/scripting-ssms.md#script-queries-from-the-gui)
- [Troubleshoot SQL Server backup and restore operations](/troubleshoot/sql/database-engine/backup-restore/backup-restore-operations)
