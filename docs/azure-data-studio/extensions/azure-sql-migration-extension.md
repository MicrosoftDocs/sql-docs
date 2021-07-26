---
title: Azure SQL Migration extension for Azure Data Studio
description: This article describes how you can migrate your data with Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: mokabir, chadam
ms.custom: 
ms.date: 07/30/2021
---

# Azure SQL Migration extension for Azure Data Studio (Preview)

The Azure SQL Migration extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to use the new migration capability in Azure Data Studio.

The Azure SQL Migration extension guides you to migrate your on-premises SQL Server or your SQL virtual machines (IaaS) running in any cloud platform to Azure SQL Managed Instance (SQL MI) or SQL Server on Azure Virtual Machines (SQL VM).

This article describes how to install the Azure SQL migration extension through Azure Data Studio, powered by the Azure Database Migration service.

This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

- [Azure Data Studio installed](../download-azure-data-studio.md).

### Environment requirements

- Source SQL Server version 2008 or later.
- SQL Server editions: Enterprise, Standard, Express, or Developer.
- Full backup is taken as one file or striped into multiple files.
- Log back up taken as one file or stripped into multiple files
- The **CHECKSUM option needs to be enabled for the backups provided for migration**. This option is mandatory for migrating to SQL MI and optional for SQL VM.
- Each backup should have its own backup set and can't be appended to any existing backup set. DMS always uses the first backup file in the set and ignores the rest.

## Install the Azure SQL Migration extension

To install the Azure SQL Migration extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

    :::image type="content" source="media/azure-sql-migration-extension/extension-icon.jpg" alt-text="Extension icon":::

2. Type in *Azure SQL Migration* in the search bar.

3. Select the **sql migration** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-market-place.jpg" alt-text="Azure SQl Migration extension from market place":::

5. Once installed you can see the extension in the extension list once installed.

    :::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-icon.png" alt-text="Azure SQL migration extension":::

6. You can connect to the SQL Server instance in Azure Data Studio and either double-click the instance name or right-click the instance name and select **Manage** to see the instance dashboard and the **Azure SQL Migration extension landing page**.

:::image type="content" source="media/azure-sql-migration-extension/azure-sql-migration-extension-landing-page.jpg" alt-text="Landing page":::

When a newer version of the Azure SQL Migration extension is published in the Azure Data Studio marketplace, the extension gets updated automatically.

## Azure SQL targets and migration modes

Select one of the Azure SQL targets and migration modes to follow that environment's migration steps.

### Azure SQL Managed Instance (SQL MI)

| Migration mode | Description |
|----------------|-------------|
| [Online]() | The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to cutover at the end of migration. |
| [Offline]() | The source database can't be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime starts when the migration begins. |

### SQL Server on Azure Virtual Machines (SQL VM)

SQL VM runs on Windows and is registered with the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-server-iaas-agent-extension-automate-management) in FULL management mode.

Supported versions: SQL Server 2012 and later.

| Migration mode | Description |
|----------------|-------------|
| [Online]() | The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to cutover at the end of migration. |
| [Offline]() | The source database can't be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime starts when the migration begins. |

## Self-Hosted Integration Runtime (SHIR)

Install the [Self-Hosted Integration Runtime (SHIR)](/azure/data-factory/create-self-hosted-integration-runtime) on a Windows machine that can connect to the source SQL Server instance.

## Environments and regions

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

### Azure regions

- Canada Central
- East US
- East US 2

## System requirements

### Operating System

- Windows Server 2012 - 2019, Windows 8.1 - 10.
- Microsoft Integration Runtime (Self-hosted) requires a 64-bit Operating System with .NET Framework 4.7.2 or above.
- The minimum configuration for the Integration Runtime (Self-hosted) machine is 2 GHz, four Core CPUs, 8 GB Memory, and 80-GB disk.

## Extension settings

To change the settings for the Kusto extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Find the **Azure SQL Migration** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

## Known issues

You can file a [feature request](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=feature_request.md&title=) to provide feedback to the product team.  
You can file a [bug](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=bug_report.md&title=) to provide feedback to the product team.

### Implicit Assessment – Unsupported features

- Ability to assess SQL Server extended event files.
- Ability to consume Data Access Migration Toolkit (DAMT) input file for assessment.

### Unsupported source SQL Server environments

- Migrating from AWS RDS (PaaS offering) for SQL Server to Azure SQL PaaS offering.
- Migration from GCP Cloud SQL (PaaS offering) for SQL Server to Azure SQL PaaS offering.
- Azure Arc enabled SQL Managed Instance (preview).

### Unsupported SQL Server services

- SQL Server reporting services
- SQL Server analysis services
- SQL Server integration services
- SQL Server Master Data Services (MDS)
- SQL Server Data Quality Services (DQS)

### Unsupported Azure SQL target

- Azure SQL Database
- SQL VM running on Linux

### Unsupported backup file locations

- Azure recovery services vault
- AWS S3
- GCP Cloud Storage
- Third-party backup solutions like Tivoli
- Volume Shadow Copy Service (VSS) backups

### Database backups

Migration workflow doesn't generate the latest backups to use for migration and instead uses existing backup files.

#### Unsupported migration mode, back up file location, and backup type

| Migration Mode | Backup file location | DMS associated with Self-Hosted Integration Runtime (SHIR) | Backup types allowed |
|----------------|----------------------|--------------------------|----------------------|
| Online | Azure Storage Blob Container | No | Full backup, Differential backup, and Transaction log backups. |
| Online | Azure Storage File Share | Yes | Full backup and Transaction log backups. (See below note for challenges related to differential backups.) |
| Offline | On-premise network file share | Yes | Differential Backup (Latest), one or more Transaction log backups. |
| Offline | Azure Storage Blob Container | No | Full backup (Latest), Differential Backup (Latest), one or more Transaction log backups. |
| Offline | Azure Storage File Share | Yes | Full backup (Latest), Differential Backup (Latest), one or more Transaction log backups. |

> [!Note]
> Supporting differential backups for DMS is complex due to upload or copy step involved from on-premise network file share or Azure Storage File Share to Azure Storage account, which is the size of data operation and may cause potential wastage of network bandwidth if multiple differential backups are to be uploaded or copied as database restore plan can change dynamically.

### Unsupported target SQL MI scenarios

- Reusing target database name in a particular SQL MI while repeating migration even after deleting the migrated database.
- Provisioning a new SQL MI during migration
- Recommendation for SQL MI SKU size.
- If the database is encrypted with Transparent Data Encryption (TDE), ensure the corresponding certificate from the source SQL Server instance is already migrated to target SQL MI before starting database migration.
- If database backup is encrypted with a certificate or symmetric key, ensure the corresponding certificate from the source SQL Server instance is already migrated to target SQL MI before starting database migration.
- Overwriting an existing database in target SQL MI.
- Customization of blocksize, buffer count, maxtransfersize parameters in RESTORE operation.

### Unsupported target SQL VM scenarios

- Reusing target database name in a particular SQL VM while repeating migration even after deleting the migrated database.
- SQL VM with SQL Server 2008 R2 and below versions.
- Provisioning a new SQL VM during migration.
- Recommendation on SQL VM SKU size and disk layout.
- If the database is encrypted with Transparent Data Encryption (TDE), ensure the corresponding certificate from the source SQL Server instance is already migrated to the target SQL VM before starting database migration.
- If database backup is encrypted with a certificate or symmetric key, then ensure corresponding certificate or symmetric key from the source SQL Server instance is backed up and created on target SQL VM before starting database migration.
- Setting up high availability and disaster recovery (Availability Groups, Failover Clustering, Log Shipping, Replication) in SQL VM to match the source topology.
- Overwriting an existing database in target SQL VM.
- Customization of blocksize, buffer count, maxtransfersize parameters in RESTORE operation.
- Renaming of target physical data file names for the restored database.
- There's an issue were concurrent database migration actions like start, cancel & cut over against a particular SQL VM may hit a race condition resulting in errors.

### Unsupported server objects

- Logins
- SQL Server Agent jobs
- Linked Servers
- Credentials
- SQL Server Integration Services (SSIS) packages
- Cryptographic providers
- Server roles
- Server audit
- Database audit
- Extended event sessions
- Maintenance plans
- Database mail
- DTC
- Policy-Based Management (PBM)
- Instance Trace Flags
- Service Broker
- Event notifications
- SQLCLR
- Server level triggers

### Command-line Interface (CLI)

There's no CLI support.

## Next steps

- [SQL on Azure VM Online migration]()
- [SQL on Azure VM Offline migration]()
- [SQL MI Online migration]()
- [SQL MI Offline migration]()
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)
