---
title: Azure SQL migration extension for Azure Data Studio
description: This article describes how you can migrate your data using the Azure SQL migration extension with Azure Data Studio.
author: croblesm
ms.author: roblescarlos
ms.reviewer:
ms.date: 09/01/2021
ms.service: azure-data-studio
ms.workload: data-services
ms.topic: conceptual
---

# Azure SQL migration extension for Azure Data Studio

[!INCLUDE [database-migration-services-ads](../../includes/database-migration-services-ads.md)]

This article describes installing the Azure SQL migration extension through Azure Data Studio.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required to install this extension:

- [Azure Data Studio installed](../download-azure-data-studio.md).

## Install the Azure SQL migration extension

Follow the steps below to install the Azure SQL migration extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

    :::image type="content" source="media/azure-sql-migration-extension/extension-icon.png" alt-text="Extension icon":::

2. Type in *Azure SQL Migration* in the search bar.

3. Select the **Azure SQL Migration** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-market-place.png" alt-text="Azure SQl Migration extension from market place":::

5. You can see the Azure SQL migration extension in the extension list once installed.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-icon.png" alt-text="Azure SQL migration extension":::

6. You can connect to the SQL Server instance in Azure Data Studio. Right-click the instance name and select **Manage** to see the dashboard and the **Azure SQL Migration** extension landing page.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-landing-page.png" alt-text="Landing page":::

### Set up auto update for the extension

[!INCLUDE [auto-update-extension](../../includes/auto-update-extension.md)]

## Features

### Azure SQL target readiness assessment and database migrations

The Azure SQL migration extension supports assessment and generates Azure recommendations (Preview) and database migrations for the following Azure SQL targets.

- SQL Server on Azure Virtual Machines
- Azure SQL Managed Instance
- Azure SQL Database (Preview)

### Migration modes

The following migration modes are supported for the corresponding Azure SQL targets.

- **Online** - The source SQL Server database is available for reading and writing activity, while database backups are continuously restored on target Azure SQL. Application downtime is limited to the duration of the cutover at the end of migration. 
- **Offline** - The source database can't be used for writing activity while backup files are restored on the target Azure SQL database (Preview). Application downtime persists from the start until the completion of the migration process.

### Support matrix

| Azure SQL target | Migration mode |
|-----------------|----------------|
Azure SQL Managed Instance| [Online](/azure/dms/tutorial-sql-server-managed-instance-online-ads) / [Offline](/azure/dms/tutorial-sql-server-managed-instance-offline-ads)
SQL Server on Azure Virtual Machine|[Online](/azure/dms/tutorial-sql-server-to-virtual-machine-online-ads) / [Offline](/azure/dms/tutorial-sql-server-to-virtual-machine-offline-ads)
Azure SQL Database (Preview)| [Offline](/azure/dms/tutorial-sql-server-azure-sql-database-offline-ads)

> [!TIP]
> For information on pre-requisites, features and migration workflow, see [Migration using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)

### Get help from Microsoft support

You can raise a support request to get Microsoft support assistance if you encounter issues or errors with your database migrations using the Azure SQL Migration extension.

Click on the **New support request** button in the upper section of the extension. It will automatically take you to the Azure portal, where you can fill in the details and then submit a support request.
:::image type="content" source="media/azure-sql-migration-extension/extension-support.png" alt-text="Get help from Microsoft support for the extension":::

You can submit ideas/suggestions for improvement, and other feedback, including bugs, in the [Azure Community forum — Azure Database Migration Service](https://feedback.azure.com/d365community/forum/2dd7eb75-ef24-ec11-b6e6-000d3a4f0da0).

> [!NOTE]
> You can also use the **Feedback** button if you have any suggestions or feedback to improve the extension.

## Next steps

- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)