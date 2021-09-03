---
title: Azure SQL Migration extension for Azure Data Studio
description: This article describes how you can migrate your data using the Azure SQL migration extension with Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: mokabiru, chadam
ms.custom: 
ms.date: 09/01/2021
---

# Azure SQL Migration extension for Azure Data Studio (preview)

The Azure SQL Migration extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to use the SQL Server assessment and migration capability in Azure Data Studio.

[!INCLUDE [database-migration-services-sql-mi-sql-vm](../../includes/database-migration-services-sql-mi-sql-vm.md)]

This article describes how to install the Azure SQL migration extension through Azure Data Studio.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required to install this extension:

- [Azure Data Studio installed](../download-azure-data-studio.md).

## Install the Azure SQL Migration extension

To install the Azure SQL Migration extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

    :::image type="content" source="media/azure-sql-migration-extension/extension-icon.png" alt-text="Extension icon":::

2. Type in *Azure SQL Migration* in the search bar.

3. Select the **Azure SQL Migration** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-market-place.png" alt-text="Azure SQl Migration extension from market place":::

5. You can see the Azure SQL Migration extension in the extension list once installed.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-icon.png" alt-text="Azure SQL migration extension":::

6. You can connect to the SQL Server instance in Azure Data Studio. Right-select the instance name and select **Manage** to see the instance dashboard and the **Azure SQL Migration** extension landing page.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-landing-page.png" alt-text="Landing page":::

> [!NOTE]
> The Azure SQL Migration extension requires Azure Data Studio on a Windows operating system.

### Set up auto update for the extension

[!INCLUDE [auto-update-extension](../../includes/auto-update-extension.md)]

## Features

### Azure SQL target readiness assessment and database migrations

The Azure SQL Migration extension supports target readiness and database migrations for the following Azure SQL targets.

- SQL Server on Azure Virtual Machines (SQL VM)
- Azure SQL Managed Instance (SQL MI)

### Migration modes

The following migration modes are supported for the corresponding Azure SQL targets.

- **Online** - The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to duration for the cutover at the end of migration. 
- **Offline** - The source database cannot be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime persists through the start until the completion of the migration process.

### Support matrix

| Azure SQL target | Migration mode |
|-----------------|----------------|
| Azure SQL Managed Instance | Online |
| Azure SQL Managed Instance | Offline |
| SQL Server on Azure VM | Online |
| SQL Server on Azure VM | Offline |

> [!TIP]
> For information on pre-requisites, features and migration workflow, see [Migration using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)

### Get help from Microsoft support

You can raise a support request for a Microsoft engineer to assist if you encounter issues or errors with your database migrations using this extension.

Select **New support request** on the extension to navigate to the Azure portal where you can submit a support request.
:::image type="content" source="media/azure-sql-migration-extension/extension-support.png" alt-text="Get help from Microsoft support for the extension":::

> [!NOTE]
> Select the **Feedback** button if you have any suggestions or feedback to improve the extension.

## Next steps

- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)
