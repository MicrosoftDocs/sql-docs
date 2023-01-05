---
title: Back up and restore a database
description: Follow this tutorial to learn how to back up and restore databases by using Azure Data Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 11/04/2019
ms.service: azure-data-studio
ms.topic: tutorial
ms.custom: seodec18
---

# Tutorial: Back up and restore databases using Azure Data Studio

In this tutorial, you learn how to use Azure Data Studio to:
> [!div class="checklist"]
> * Back up a database.
> * View the backup status.
> * Generate the script used to perform the backup.
> * Restore a database.
> * View the status of the restore task.

## Prerequisites

This tutorial requires SQL Server *TutorialDB*. To create the TutorialDB database, complete the following quickstart:

* [Use Azure Data Studio to connect and query SQL Server](quickstart-sql-server.md)

This tutorial requires a connection to a SQL Server database. Azure SQL Database has automated backups, so Azure Data Studio doesn't perform Azure SQL Database backup and restore. For more information, see [Learn about automatic SQL Database backups](/azure/sql-database/sql-database-automated-backups).

## Back up a database

1. Open the TutorialDB database dashboard by opening the **SERVERS** sidebar. Then select **Ctrl+G**, expand **Databases**, right-click **TutorialDB**, and select **Manage**.

1. Open the **Backup database** dialog box by selecting **Backup** on the **Tasks** widget.

   ![Screenshot that shows the Tasks widget.](./media/tutorial-backup-restore-sql-server/tasks.png)

1. This tutorial uses the default backup options, so select **Backup**.

   ![Screenshot that shows the Backup dialog box.](./media/tutorial-backup-restore-sql-server/backup-dialog.png)

After you select **Backup**, the **Backup database** dialog box disappears and the backup process begins.

## View the backup status and the backup script

1. The **Task History** pane appears, or select **Ctrl+T** to open it.

   ![Screenshot that shows the Task History pane.](./media/tutorial-backup-restore-sql-server/task-history.png)

1. To view the backup script in the editor, right-click **Backup Database succeeded** and select **Script**.

   ![Screenshot that shows backup script.](./media/tutorial-backup-restore-sql-server/task-script.png)

## Restore a database from a backup file

1. Open the **SERVERS** sidebar by selecting **Ctrl+G**. Then right-click your server, and select **Manage**.

1. Open the **Restore database** dialog box by selecting **Restore** on the **Tasks** widget.

   ![Screenshot that shows Task restore.](media/tutorial-backup-restore-sql-server/tasks-restore.png)

1. Select **Backup file** in the **Restore from** box.

1. Select the ellipses (...) in the **Backup file path** box, and select the latest backup file for *TutorialDB*.

1. Enter **TutorialDB_Restored** in the **Target database** box in the **Destination** section to restore the backup file to a new database. Then select **Restore**.

   ![Screenshot that shows Restore backup.](./media/tutorial-backup-restore-sql-server/restore.png)

1. To view the status of the restore operation, select **Ctrl+T** to open the **Task History**.

   ![Screenshot that shows History task restore.](./media/tutorial-backup-restore-sql-server/task-history-restore.png)