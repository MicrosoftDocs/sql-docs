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
ms.date: 07/30/2021
---

# Azure SQL Migration extension for Azure Data Studio (Preview)

The Azure SQL Migration extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to use the new SQL Server assessment and migration capability in Azure Data Studio.

[!INCLUDE [database-migration-services-sql-mi-sql-vm](../../includes/database-migration-services-sql-mi-sql-vm.md)]

This article describes how to install the Azure SQL migration extension through Azure Data Studio, powered by the [Azure Database Migration service (DMS)](/azure/dms/dms-overview.md).

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required to install this extension:

- [Azure Data Studio installed](../download-azure-data-studio.md).

## Install the Azure SQL Migration extension

To install the Azure SQL Migration extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

    :::image type="content" source="media/azure-sql-migration-extension/extension-icon.jpg" alt-text="Extension icon":::

2. Type in *Azure SQL Migration* in the search bar.

3. Select the **Azure SQL Migration** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-market-place.jpg" alt-text="Azure SQl Migration extension from market place":::

5. You can see the Azure SQL Migration extension in the extension list once installed.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-icon.png" alt-text="Azure SQL migration extension":::

6. You can connect to the SQL Server instance in Azure Data Studio and either double-Select the instance name or right-Select the instance name and select **Manage** to see the instance dashboard and the **Azure SQL Migration** extension landing page.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-landing-page.jpg" alt-text="Landing page":::

### Set up auto update for the extension

You can check for updates to the extension and have them automatically updated by configuring **Auto Update** in Azure Data Studio settings.

To enable auto updates:
1. Select the **Settings** icon in Azure Data Studio.
2. Select the **checkbox** under **User > Features > Extensions > Auto Check Updates**.
3. Select on the **dropdown** under **User > Features > Extensions > Auto Update** and select either **All Extensions** or **Only Enabled Extensions**.

:::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-auto-update.jpg" alt-text="auto update the extension":::

> [!NOTE]
> If you want to update the extension manually, you can disable **Auto Update** and install the updates from the extension in the Marketplace.

## Features

### Azure SQL targets

The Azure SQL Migration extension supports database migrations to the following Azure SQL targets.

- SQL Server on Azure Virtual Machines (SQL VM)
- Azure SQL Managed Instance (SQL MI)

### Azure SQL target readiness assessment

The Azure SQL Migration extension supports target readiness for the following Azure SQL targets.

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

> [!NOTE]
> The Azure SQL Migration extension is supported in Azure Data Studio that is installed on a Windows Operating System.

## Next steps

- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)
