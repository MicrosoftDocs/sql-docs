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

The Azure SQL Migration extension guides you to migrate your on-premises SQL Server or your SQL virtual machines (IaaS) running in any cloud platform to:

**Azure SQL Managed Instance (SQL MI) or SQL Server on Azure Virtual Machines (SQL VM)**.

This article describes how to install the Azure SQL migration extension through Azure Data Studio, powered by the [Azure Database Migration service (DMS)]().

This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

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
1. Select the **checkbox** under **User > Features > Extensions > Auto Check Updates**.
1. Select on the **dropdown** under **User > Features > Extensions > Auto Update** and select either **All Extensions** or **Only Enabled Extensions**.

> [!NOTE]
> If you want to update the extension manually, you can disable **Auto Update** and install the updates from the extension in the Marketplace.

## Features

### Azure SQL targets

The Azure SQL Migration extension supports database migrations to the following Azure SQL targets.

- SQL on Azure Virtual Machines (SQL VM)
- Azure SQL Managed Instance (SQL MI)

### Azure SQL target readiness assessment

The Azure SQL Migration extension supports target readiness for the following Azure SQL targets.

- Azure SQL Managed Instance (SQL MI)
- SQL on Azure Virtual Machines (SQL VM)

### Migration modes

The following migration modes are supported for the corresponding Azure SQL targets.

- **Online** - The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to duration for the cutover at the end of migration. 
- **Offline** - The source database cannot be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime persists through the start until the completion of the migration process.

### Support matrix

| Azure SQL target | Migration mode |
|-----------------|----------------|
| Azure SQL Managed Instance | [Online](/azure/dms/tutorial-sql-server-managed-instance-online) |
| Azure SQL Managed Instance | [Offline](/azure/dms/tutorial-sql-server-to-managed-instance) |
| SQL Server on Azure VM | [Online]() |
| SQL Server on Azure VM | [Offline]() |

### Environment requirements

- Source SQL Server version 2008 or later.
- SQL Server editions: Enterprise, Standard, Express, or Developer.
- Full backup is taken as one file or striped into multiple files.
- Log back up taken as one file or stripped into multiple files
- The **CHECKSUM option needs to be enabled for the backups provided for migration**.
- Each backup should have its own backup set and can't be appended to any existing backup set. Azure DMS always uses the first backup file in the set and ignores the rest.

### Source SQL Server environments

- SQL Server running on on-premises Windows physical server/VM
- SQL Server running on on-premises Linux physical server/VM
- SQL Server on Azure IaaS Windows VM
- SQL Server on Azure IaaS Linux VM
- SQL Server running on AWS EC2 (IaaS) Windows VM
- SQL Server running on AWS EC2 (IaaS) Linux VM
- SQL Server running on GCP Compute Engine (IaaS) Windows VM
- SQL Server running on GCP Compute Engine (IaaS) Linux VM Supported SQL Server services
- SQL Server database engine

> [!NOTE]
> The Azure SQL Migration extension is supported in Azure Data Studio that is installed on a Windows Operating System.

## Limitations and unsupported environments

You can file a [feature request](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=feature_request.md&title=) to provide feedback to the product team.  
You can file a [bug](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=bug_report.md&title=) to provide feedback to the product team.

### Unsupported Azure SQL targets

- Azure SQL Database
- SQL VM running on Linux

### Unsupported SQL Server services

- SQL Server Reporting Services
- SQL Server Analysis Services
- SQL Server Integration Services
- SQL Server Master Data Services (MDS)
- SQL Server Data Quality Services (DQS)

### Unsupported SQL MI target scenarios

For details about unsupported online SQL MI scenarios visit, [Limitations and unsupported environments for online SQL MI]().

For details about unsupported offline SQL MI scenarios visit, [Limitations and unsupported environments for offline SQL MI]().

### Unsupported SQL VM target scenarios

For details about unsupported online SQL VM scenarios visit, [Limitations and unsupported environments for online SQL VM]().

For details about unsupported offline SQL VM scenarios visit, [Limitations and unsupported environments for offline SQL VM]().

## Next steps

- [SQL MI Online migration](/azure/dms/tutorial-sql-server-managed-instance-online)
- [SQL MI Offline migration](/azure/dms/tutorial-sql-server-to-managed-instance)
- [SQL VM Online migration]()
- [SQL VM Offline migration]()
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)
