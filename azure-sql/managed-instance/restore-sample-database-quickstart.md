---
title: "Quickstart: Restore a backup (SSMS)"
titleSuffix: Azure SQL Managed Instance
description: In this quickstart, learn how to restore a database backup to Azure SQL Managed Instance by using SQL Server Management Studio (SSMS).
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, nvraparl
ms.date: 04/04/2024
ms.service: azure-sql-managed-instance
ms.subservice: backup-restore
ms.topic: quickstart
ms.custom:
  - mode-other
  - kr2b-contr-experiment
---
# Quickstart: Restore a database to Azure SQL Managed Instance with SSMS

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this quickstart, you'll use SQL Server Management Studio (SSMS) to restore a database from Azure Blob Storage to [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). The quickstart restores the Wide World Importers sample database from a publicly available backup file using a SAS.

> [!NOTE]
> - For a video tutorial, see [Azure SQL Managed Instance- restore database backup](https://www.youtube.com/embed/RxWYojo_Y3Q).
> - For more information on migration using Azure Database Migration Service, see [Tutorial: Migrate SQL Server to an Azure SQL Managed Instance using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
> - For more information on various migration methods, see [SQL Server to Azure SQL Managed Instance Guide](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).

## Prerequisites

This quickstart:

- Uses resources from the [Create a SQL managed instance](instance-create-quickstart.md) quickstart.
- Requires the latest version of [SSMS](/sql/ssms/sql-server-management-studio-ssms) installed.
- Requires SSMS to connect to SQL Managed Instance. See these quickstarts on how to connect:
  - [Enable a public endpoint](public-endpoint-configure.md) on SQL Managed Instance. This approach is recommended for this quickstart.
  - [Connect to SQL Managed Instance from an Azure VM](connect-vm-instance-configure.md).
  - [Configure a point-to-site connection to SQL Managed Instance from on-premises](point-to-site-p2s-configure.md).

> [!NOTE]
> For more information on backing up and restoring a SQL Server database by using Blob Storage and a [shared access signature key](/azure/storage/common/storage-sas-overview), see [SQL Server Backup to URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url).

## Use the restore wizard to restore from a backup file

In SSMS, take the steps in the following sections to restore the Wide World Importers database to SQL Managed Instance by using the restore wizard. The database backup file is stored in a preconfigured Blob Storage account.

### Open the restore wizard

1. Open SSMS and connect to your SQL managed instance.
1. In **Object Explorer**, right-click the **Databases** folder of your SQL managed instance, and then select **Restore Database** to open the restore wizard.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-start.png" alt-text="Screenshot of Object Explorer in SSMS. The Databases folder is selected. In its shortcut menu, Restore Database is selected.":::

### Select the backup source

1. In the restore wizard, select the ellipsis (**...**) to select the source of the backup set to restore.

   :::image type="content" source="media/restore-sample-database-quickstart/new-restore-wizard.png" alt-text="Screenshot of a page in the restore wizard. In the Source section, Device is selected, and the ellipsis is called out.":::

1. In **Select backup devices**, select **Add**. In **Backup media type**, **URL** is the only option that's available because it's the only source type supported. Select **OK**.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-select-device.png" alt-text="Screenshot of the Select backup devices dialog. The Add and OK buttons are called out.":::

1. In **Select a Backup File Location**, choose from one of three options to provide information about the location of your backup files:

   - Select a preregistered storage container from the **Azure storage container** list.
   - Enter a new storage container and a shared access signature. A new SQL credential will be registered for you.
   - Select **Add** to browse more storage containers from your Azure subscription.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-backup-file-location.png" alt-text="Screenshot of the Select a Backup File Location dialog. In the Azure storage container section, Add is selected.":::

   If you select **Add**, proceed to the next section, [Browse Azure subscription storage containers](#browse-azure-subscription-storage-containers). If you use a different method to provide the location of the backup files, skip to [Restore the database](#restore-the-database).

   To restore the Wide World Importers sample database from publicly accessible read-only storage, provide the value `https://mitutorials.blob.core.windows.net/examples/` and for the Shared Access Signature field, provide any value, such as `SAS`.

    :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-backup-file-location-sample.png" alt-text="Screenshot of the Select a Backup File Location dialog. The sample WideWorldImporters folder location is specified. Select is boxed in red.":::

#### Browse Azure subscription storage containers

> [!NOTE]
> These steps aren't necessary to restore the `WideWorldImporters` sample database from the publicly-accessible read-only Azure blob storage, but are necessary to restore databases from your own Azure blob storage.

1. In **Connect to a Microsoft Subscription**, select **Sign in** to sign in to your Azure subscription.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-connect-subscription-sign-in.png" alt-text="Screenshot of the Connect to a Microsoft Subscription dialog. The Sign In button is called out.":::

1. Sign in to your Microsoft Account to initiate the session in Azure.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-sign-in-session.png" alt-text="Screenshot of the Sign in to your account dialog. The Microsoft logo, a sign-in box, and other UI elements are visible.":::

1. Select the subscription of the storage account that contains the backup files.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-select-subscription.png" alt-text="Screenshot of the Connect to a Microsoft Subscription dialog. Under Select a subscription to use, the down arrow on the list box is called out.":::

1. Select the storage account that contains the backup files.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-select-storage-account.png" alt-text="Screenshot of the Connect to a Microsoft Subscription dialog. The down arrow on the Select Storage Account list box is called out.":::

1. Select the blob container that contains the backup files.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-select-container.png" alt-text="Screenshot of the Connect to a Microsoft Subscription dialog. The down arrow on the Select Blob Container list box is called out.":::

1. Enter the expiration date of the shared access policy and select **Create Credential**. A shared access signature with the correct permissions is created. Select **OK**.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-generate-shared-access-signature.png" alt-text="Screenshot of the Connect to a Microsoft Subscription dialog. Create Credential, OK, and the Shared Access Policy Expiration box are called out.":::

### Restore the database

Now that you've selected a storage container, you should see the **Locate Backup File in Microsoft Azure** dialog.

1. In the left pane, expand the folder structure to show the folder that contains the backup files. In the right pane, select all the backup files that are related to the backup set that you're restoring, and then select **OK**. For example, the following screenshot shows the publicly accessible read-only blob storage containing the `WideWorldImporters` sample database.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-backup-file-selection.png" alt-text="Screenshot of the Locate Backup File in Microsoft Azure dialog. The WideWorldImporters-Standard.bak backup file is selected, and the OK button is boxed in red." lightbox="media/restore-sample-database-quickstart/restore-wizard-backup-file-selection.png":::

   SSMS validates the backup set. This process takes at most a few seconds. The duration depends on the size of the backup set.

1. If the backup is validated, you need to specify a name for the database that's being restored. By default, under **Destination**, the **Database** box contains the name of the backup set database. To change the name, enter a new name for **Database**. Select **OK**.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-start-restore.png" alt-text="Screenshot of a page in the restore wizard. In the Destination section, the Database box is called out. The OK button is also called out." lightbox="media/restore-sample-database-quickstart/restore-wizard-start-restore.png":::

   The restore process starts. The duration depends on the size of the backup set.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-running-restore.png" alt-text="Screenshot of a page in the restore wizard. A progress indicator is called out.":::

1. When the restore process finishes, a dialog shows that it was successful. Select **OK**.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-finish-restore.png" alt-text="Screenshot of a dialog over a page in the restore wizard. A message in the dialog indicates that the database was successfully restored.":::

1. In **Object Explorer**, check for the restored database by selecting **Refresh**.

   :::image type="content" source="media/restore-sample-database-quickstart/restore-wizard-restored-database.png" alt-text="Screenshot of Object Explorer. The restored database is called out.":::

## Use T-SQL to restore from a backup file

As an alternative to the restore wizard, you can use T-SQL statements to restore a database. In SSMS, follow these steps to restore the Wide World Importers database to SQL Managed Instance by using T-SQL. The database backup file is stored in a preconfigured Blob Storage account.

1. Open SSMS and connect to your SQL managed instance.

1. In **Object Explorer**, right-click your SQL managed instance and select **New Query** to open a new query window.

1. Run the following T-SQL statement, which uses publicly available preconfigured storage container and a shared access signature key to [create a credential](/sql/t-sql/statements/create-credential-transact-sql) in your SQL managed instance.

   > [!IMPORTANT]
   >
   > - `CREDENTIAL` must match the container path, begin with `https`, and can't contain a trailing forward slash.
   > - `IDENTITY` must be `SHARED ACCESS SIGNATURE`.
   > - `SECRET` must be the shared access signature token and can't contain a leading `?`.
   > - In this example, `SECRET` is omitted because the storage account is publicly available. If you use a storage account that isn't publicly available, you must provide a shared access signature token.

   ```sql
   CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE';
   ```

   :::image type="content" source="media/restore-sample-database-quickstart/credential.png" alt-text="Screenshot that shows the SSMS Query Editor. The CREATE CREDENTIAL statement is visible, and a message indicates that the query ran successfully." lightbox="media/restore-sample-database-quickstart/credential.png":::

   The credential path in the previous example provides access to a single file. You can also create a shared access signature token to a folder path, for example:

   ```sql
   CREATE CREDENTIAL [https://<your storage>.blob.core.windows.net/databases/backups/]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE'
   , SECRET = '<your shared access signature>'; -- omit the leading ?
   ```

1. To check your credential is working properly, run the following statement, which uses a URL to get a backup file list.

   ```sql
   RESTORE FILELISTONLY FROM URL = 'https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak';
   ```

   :::image type="content" source="media/restore-sample-database-quickstart/file-list.png" alt-text="Screenshot that shows the SSMS Query Editor. The RESTORE FILELISTONLY statement is visible, and the Results tab lists three files." lightbox="media/restore-sample-database-quickstart/file-list.png":::

1. Run the following statement to restore the example Wide World Importers database.

   ```sql
   RESTORE DATABASE [WideWorldImportersExample] FROM URL =
     'https://mitutorials.blob.core.windows.net/examples/WideWorldImporters-Standard.bak';
   ```

   :::image type="content" source="media/restore-sample-database-quickstart/restore-database.png" alt-text="Screenshot that shows the SSMS Query Editor. The RESTORE DATABASE statement is visible, and a message indicates that the query ran successfully." lightbox="media/restore-sample-database-quickstart/restore-database.png":::

   > [!TIP]
   > If you receive `Error: 3201` or `Operating system error 86(The specified network password is not correct.)`, it is likely because your SAS credential (created in previous steps) is incorrectly created. `DROP CREDENTIAL` and recreate, review the credential name, identity, and secret.

1. Run the following statement to track the status of your restore process.

   ```sql
   SELECT session_id as SPID, command, a.text AS Query, start_time, percent_complete
      , dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
   FROM sys.dm_exec_requests r
   CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a
   WHERE r.command in ('BACKUP DATABASE','RESTORE DATABASE');
   ```

1. When the restore process finishes, view the database in **Object Explorer**. You can verify that the database is restored by using the [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) view.

> [!NOTE]
> A database restore operation is asynchronous and retryable. You might get an error in SSMS if the connection fails or a time-out expires. SQL Managed Instance keeps trying to restore the database in the background, and you can track the progress of the restore process by using the [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql) and [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) views.
>
> In some phases of the restore process, you see a unique identifier instead of the actual database name in the system views. To learn about `RESTORE` statement behavior differences, see [T-SQL differences between SQL Server & Azure SQL Managed Instance](./transact-sql-tsql-differences-sql-server.md#restore-statement).

## Related content

- For information about troubleshooting a backup to a URL, see [SQL Server Backup to URL best practices and troubleshooting](/sql/relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting).
- For an overview of app connection options, see [Connect your applications to SQL Managed Instance](connect-application-instance.md).
- To query by using your favorite tools or languages, see [Quickstarts: Azure SQL Database connect and query](../database/connect-query-content-reference-guide.md).
