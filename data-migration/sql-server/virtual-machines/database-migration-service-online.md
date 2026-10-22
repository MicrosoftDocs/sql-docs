---
title: "Tutorial: Migrate SQL Server to SQL Server on an Azure Virtual Machine with Azure DMS (Online)"
titleSuffix: Azure Database Migration Service
description: Learn how to migrate on-premises SQL Server to SQL Server on Azure Virtual Machines online, using Azure Database Migration Service.
author: abhims14
ms.author: abhishekum
ms.reviewer: abhishekum
ms.date: 02/19/2026
ms.service: azure-database-migration-service
ms.topic: tutorial
ms.collection:
  - sql-migration-content
ms.custom:
  - sfi-image-nochange
---

# Tutorial: Migrate SQL Server to SQL Server on an Azure Virtual Machine with Azure DMS (online)

You can use Azure Database Migration Service (Azure DMS) through the Azure portal to perform an online database migration from an on-premises instance of SQL Server to [What is SQL Server on Azure Windows Virtual Machines?](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) (Azure VM).

> [!NOTE]  
> This tutorial uses online migration mode. For offline migration options, see [Tutorial: Migrate SQL Server to SQL Server on an Azure Virtual Machine with Azure DMS (offline)](database-migration-service-offline.md).

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Launch the Migrate to Azure SQL wizard in the Azure portal.
> - Specify details of your source SQL Server, backup location, and your target SQL Server on Azure VM.
> - Configure the wizard to access source server and backups.
> - Start and monitor the progress for your migration.
> - Perform the migration cutover when you're ready.

## Migration options

The following section describes how to use Azure Database Migration Service with the Azure SQL migration extension, or in the Azure portal.

## Prerequisites

Before you begin the tutorial:

- Ensure that you can access the [Azure portal](https://portal.azure.com).

- Make sure that the **Microsoft.DataMigration** resource provider is [registered in your subscription](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).

- Have an Azure account that's assigned to one of the following built-in roles:

  - **Contributor** for the target instance of SQL Server on an Azure VM, and for the storage account where you upload your database backup files from a Server Message Block (SMB) network share.

  - **Reader** role for the Azure resource group that contains the target instance of SQL Server on an Azure VM or for your Azure Storage account.

  - **Owner** or **Contributor** role for the Azure subscription.

  - As an alternative to using one of these built-in roles, you can [assign custom roles](custom-roles.md).

  When you use the **Azure portal** to migrate, the signed-in user must have **Storage Blob Data Reader** access on the blob container that holds the backup files, to be able to list files and folders during migration setup.

[!INCLUDE [prerequisites-base](../../includes/prerequisites-base.md)]

## Start a new migration

This tutorial describes an online migration from SQL Server to SQL Server on an Azure Virtual Machine (Azure VM).

To start a new migration:

1. Go to [Azure Database Migration Service](https://portal.azure.com) in the Azure portal. Use **+Create** to create a new instance of Database Migration Service, or select an existing instance. Then, go to your Database Migration Service instance.

1. On the **Overview** pane of your Azure DMS instance, select **New migration**.

   :::image type="content" source="../database/media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png" alt-text="Screenshot of Azure Database Migration Dashboard." lightbox="../database/media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png":::

1. Under **Select new migration** scenario, choose your source, target server type, backup file storage location, migration mode as **Online migration**, and choose **Select**.

   Your database backups can be located either on an on-premises network share or in an Azure Storage blob container.

   :::image type="content" source="media/database-migration-service-online/online-vm-01.png" alt-text="Screenshot of new migration scenario." lightbox="media/database-migration-service-online/online-vm-01.png":::

   If you provide your database backups in an on-premises network share, set up a self-hosted integration runtime in the next step of the wizard. You need a self-hosted integration runtime to access your source database backups, check the validity of the backup set, and upload backups to Azure storage account. If your database backups are already in an Azure storage blob container, you don't need a self-hosted integration runtime.

   In the online migration mode, the source SQL Server database can be used for read and write activity while database backup files are continuously restored on the target instance of SQL Server on an Azure VM. Application downtime is limited to duration for the cutover at the end of migration.

1. On the **Azure SQL Virtual Machine Online Blob Migration Wizard**, follow these steps:

   1. On the **Source details** tab, enter details for the source SQL Server instance, then select **Next: Connect to source SQL Server**.

      :::image type="content" source="../database/media/database-migration-service/dms-portal-sql-database-source-1-new.png" alt-text="Screenshot of Source Tracking." lightbox="../database/media/database-migration-service/dms-portal-sql-database-source-1-new.png":::

   1. On the **Select migration target** tab, enter details for the subscription, resource group, and target SQL Server VM. Then select **Next: Data source configuration**.

   1. In the **Data source configuration** step, select the location of your database backups. Your database backups can be located either on an on-premises network share or in an Azure Storage blob container.

      If you provide your database backups in an on-premises network share, set up a self-hosted integration runtime in the next step of the wizard. You need a self-hosted integration runtime to access your source database backups, check the validity of the backup set, and upload backups to Azure storage account. If your database backups are already in an Azure storage blob container, you don't need a self-hosted integration runtime.

      - For backups that are stored in an Azure Storage blob container, enter or select the following information:

        | Name | Description |
        | --- | --- |
        | **Resource group** | The resource group where backup files are located. |
        | **Storage account details** | The storage account where backup files are located. |
        | **Blob container** | The blob container where backup files are located. |
        | **Folder** | The folder where backup files are located. |
        | **Target database name** | You can change the target database name during the migration process. |

        If loopback check functionality is enabled and the source SQL Server and file share are on the same computer, the source can't access the file share with the FQDN. To fix this issue, [disable loopback check functionality](/troubleshoot/windows-server/networking/accessing-server-locally-with-fqdn-cname-alias-denied).

        :::image type="content" source="media/database-migration-service-online/online-vm-03.png" alt-text="Screenshot of online blob migration wizard data source configuration." lightbox="media/database-migration-service-online/online-vm-03.png":::

      - For backups that are located on a network share, enter the following additional information on the respective pages.

        | Name | Description |
        | --- | --- |
        | **Source server name** | The FQDN or IP of the source server. Ensure that the service account running the source SQL Server instance has read privileges on the network share. |
        | **Authentication Type** | Select the authentication type: SQL or Windows |
        | **Source Credentials - Username** | The credential (Windows and SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
        | **Source Credentials - Password** | The credential (Windows and SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
        | **Network share location that contains backups** | The network share location that contains the full and transaction log backup files. The migration process automatically ignores any invalid files or backup files in the network share that don't belong to the valid backup set. |
        | **Windows user account with read access to the network share location** | The Windows credential (username) that has read access to the network share to retrieve the backup files. |
        | **Password** | The Windows credential (password) that has read access to the network share to retrieve the backup files. |
        | **Target database name** | You can change the target database name during the migration process. |

### Backup storage scenarios

[!INCLUDE [data-migration-storage-access](../../includes/data-migration-storage-access.md)]

## Create a Database Migration Service instance

[!INCLUDE [create-database-migration-service-instance](../../includes/create-database-migration-service-instance.md)]

## Start the database migration

On the **Database migration summary** tab, review the details, then select **Start migration**. The service starts the database migration and automatically takes you back to the Azure DMS dashboard.

:::image type="content" source="media/database-migration-service-online/online-vm-04.png" alt-text="Screenshot of online blob migration wizard data migration summary." lightbox="media/database-migration-service-online/online-vm-04.png":::

## Monitor the database migration

1. To monitor your database migration, on the **Overview** pane of your Database Migration Service instance, select **Monitor migrations**.

   :::image type="content" source="../database/media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png" alt-text="Screenshot of Azure Database Migration Service overview in the Azure portal." lightbox="../database/media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png":::

1. To monitor your database migration, on the **Overview** pane of your DMS instance, select **Monitor migrations**.

1. Under the **Migrations** tab, you can track migrations that are in progress, completed, and failed (if any), or you can view all database migrations. In the menu bar, select **Refresh** to update the migration status.

   :::image type="content" source="media/database-migration-service-online/online-vm-05.png" alt-text="Screenshot of monitoring the migration." lightbox="media/database-migration-service-online/online-vm-05.png":::

Azure DMS returns the latest known migration status each time migration status refreshes. The following table describes possible statuses:

| Status | Description |
| --- | --- |
| **Arrived** | The backup file arrived in the source backup location and was validated. |
| **Uploading** | The integration runtime is uploading the backup file to Azure storage. |
| **Uploaded** | The backup file was uploaded to Azure storage. |
| **Restoring** | The service is restoring the backup file to SQL Server on an Azure VM. |
| **Restored** | The backup file was successfully restored on SQL Server on an Azure VM. |
| **Canceled** | The migration process was canceled. |
| **Ignored** | The backup file was ignored because it doesn't belong to a valid database backup chain. |

## Complete migration cutover

The final step of the tutorial is to complete the migration cutover. The completion ensures the migrated database in SQL Server on Azure Virtual Machine is ready for use. Downtime is required for applications that connect to the database and the timing of the cutover needs to be carefully planned with business or application stakeholders.

To complete the cutover:

1. Stop all incoming transactions to the source database.

1. Make application configuration changes to point to the target database in SQL Server on Azure Virtual Machines.

1. Take a final log backup of the source database in the backup location specified.

1. Put the source database in read-only mode. Therefore, users can read data from the database but not modify it.

1. Ensure all database backups have the status *Restored* in the monitoring details page.

1. Select *Complete cutover* in the monitoring details page.

During the cutover process, the migration status changes from *in progress* to *completing*. The migration status changes to *succeeded* when the cutover process is completed. The database migration is successful and that the migrated database is ready for use.

## Related content

- [Migrate a SQL Server database to SQL Server on a virtual machine](/azure/azure-sql/virtual-machines/windows/migrate-to-vm-from-sql-server)
- [What is SQL Server on Azure Windows Virtual Machines?](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
- [Connect to a SQL Server virtual machine on Azure](/azure/azure-sql/virtual-machines/windows/ways-to-connect-to-sql)
- [Migrate a database to SQL Server on Azure Virtual Machines by using the T-SQL RESTORE command](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide)
