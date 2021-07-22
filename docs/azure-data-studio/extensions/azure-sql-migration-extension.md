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

This article captures various aspects of Azure SQL migration through Azure Data Studio, powered by the Azure Database Migration service.

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

## Azure Database Migration service

If you don’t have an existing Azure Database Migration service, then [register the resource provider](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).

## Supported Azure SQL targets and migration modes

To follow the migration steps, select one of the Azure SQL targets and migration modes.

### Azure SQL Managed Instance (SQL MI)

| Migration mode | Description |
|----------------|-------------|
| Online | The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to cutover at the end of migration. |
| Offline | The source database can't be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime starts when the migration begins. |

### SQL Server on Azure Virtual Machines (SQL VM)

SQL VM runs on Windows and is registered with the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-server-iaas-agent-extension-automate-management) in FULL management mode.

Supported versions: SQL Server 2012 and later.

| Migration mode | Description |
|----------------|-------------|
| Online | The source SQL Server database is available for read and write activity while database backups are continuously restored on target Azure SQL. Application downtime is limited to cutover at the end of migration. |
| Offline | The source database can't be used for write activity while database backup files are restored on the target Azure SQL database. Application downtime starts when the migration begins. |

## Self-Hosted Integration Runtime (SHIR)

Install the [Self-Hosted Integration Runtime (SHIR)](/azure/data-factory/create-self-hosted-integration-runtime) on a Windows machine that can connect to the source SQL Server instance.

## Supported environments and regions

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

### Backup file locations

- On-premise network file share

Supported migration mode, back up file location, and backup type.

| Migration Mode | Backup file location | DMS associated with Self-Hosted Integration Runtime (SHIR) | Backup types allowed |
|----------------|----------------------|--------------------------|----------------------|
| Online | On-premise network file share | Yes | Full backup and Transaction log backups. |
| Offline | On-premise network file share | Yes | Only Full backup. |

> [!Note]
> Supporting differential backups for DMS is complex due to the upload or copy step involved from on-premise network file share or Azure Storage File Share to Azure Storage account. The size of the data operation may cause a potential waste of network bandwidth if multiple differential backups are to be uploaded or copied as the database restore plan can change dynamically.

## System requirements

### Operating System

- Windows Server 2012 - 2019, Windows 8.1 - 10.
- Microsoft Integration Runtime (Self-hosted) requires a 64-bit Operating System with .NET Framework 4.7.2 or above.
- The minimum configuration for the Integration Runtime (Self-hosted) machine is 2 GHz, four Core CPUs, 8 GB Memory, and 80-GB disk.

### Azure Storage account

For migrating to Azure SQL, DMS needs a storage account to upload backup files to and then restore the files from that storage account. Backup files are uploaded as block blobs in your Azure Storage account.

Use [General Purpose V2 Azure Storage account](/azure/storage/common/storage-account-overview#types-of-storage-accounts) in standard performance tier or [Block Blob Storage account](/azure/storage/common/storage-account-overview#types-of-storage-accounts) in premium performance tier with any redundancy option.

Azure Storage account is needed if backup files are located on an on-premises network file share or an Azure file share.

Azure Storage account isn't required if the files already exist on a supported type of storage account.

### Networking

1. From the machine(s) where Self-Hosted Integration Runtime (SHIR) is installed, outbound port 443 (HTTPS) to the following domain names should be enabled.

    | Domain Name | Outbound port | Description |
    |-------------|---------------|-------------|
    | `{datafactory}.{region}.datafactory.azure.net` </br> or </br> `*.frontend.clouddatahub.net` | 443 | Required by the self-hosted integration runtime to connect to the Data Factory service. </br> For new created Data Factory, find the FQDN from your Self-hosted Integration Runtime key,  which is in format `{datafactory}.{region}.datafactory.azure .net`. If you don't see the FQDN in your Self-hosted Integration key for the old Data Factory, use `*.frontend.clouddatahub.net` instead. |
    | `download.microsoft.com` | 443 | Required by the self-hosted integration runtime for downloading the updates. If you have disabled auto-update, you can skip configuring this domain. |
    | `*.core.windows.net` | 443 | Used by the self-hosted integration runtime to connect to the Azure storage account for uploading database backups. |

2. The machine(s) where Self-Hosted Integration Runtime (SHIR) is installed outbound SMB port 445 and other dependent ports should allow access to the on-premise network file share.

3. Connecting to the source SQL Server instance should succeed from the machine(s) where Self-Hosted Integration Runtime (SHIR) is installed.

4. From target SQL VM, outbound port 443 should be enabled to Azure storage account where backups are uploaded.

### Permissions

| User Credentials | Required | Permission needed |
|------------------|----------|-------------------|
| Source SQL Server connection in Azure Data Studio | Invoking implicit assessment | SYSADMIN fixed server role or CONTROL SERVER permission |
| Source SQL Server credentials in Azure Data Studio | ADF pipeline to connect to source SQL Server and RESTORE HEADER ONLY. | CONNECT SQL |
| Azure Account | Listing Resource Groups | Need one of the following built-in roles: </br> Owner / Contributor role at subscription level </br> Owner / Contributor role for each resource group. </br> SQL Managed Instance Contributor, Virtual Machine Contributor, Storage Account Contributor |
| Azure Account | Listing Azure SQL targets | Need one of the following built-in roles: </br> Owner / Contributor role at subscription level </br> Owner / Contributor role for each resource group. </br> SQL Managed Instance Contributor, Virtual Machine Contributor, Storage Account Contributor |
| Azure Account | Listing Azure Storage Accounts | Need one of the following built-in roles: </br> Owner / Contributor role at subscription level </br> Owner / Contributor role for each resource group. </br> SQL Managed Instance Contributor, Virtual Machine Contributor, Storage Account Contributor |
| On-premise network file share credentials | ADF pipeline to access network share | On-premise network file share credentials should have read permissions on the network file share. |
| Source SQL Server service account | Running RESTORE HEADER ONLY statement | SQL Server service account should have read permissions on the network file share. </br> If backups are taken directly to the same network file share, the SQL Server service account should also have written permissions on the network file share. Status Possible status for various stages of migration workflow. |

## DMS migration Status

1. Succeeded
2. Failed
3. Canceled
4. InProgress
5. Creating
6. Completing

## Self-Hosted Integration Runtime (SHIR) status

1. Need Registration
2. Online
3. Limited
4. Offline
5. Upgrading
6. Initializing
7. InitializeFailed

## Backup file status

1. Arrived
2. Uploading
3. Uploaded
4. Restoring
5. Restored
6. Canceled
7. Ignored

## Migration time factors

Key factors that affect the time taken for migrating database to Azure SQL MI or SQL VM using SQL Server native backup files are:

1. Availability of latest database backups as DMS doesn't generate backups and instead uses existing backups.
2. Database backup size based on whether the backups are compressed or not.
3. Number of stripes in the backup.
4. Location of backup files (on-premise or Azure)
5. Physical distance between backup files and target Azure SQL region.
6. Network bandwidth between backup file's location and target Azure SQL region.
7. On-premise storage bottleneck if backup files must be read from on-premise.
8. Azure SQL MI or SQL VM SKU size determines the resource governance and throttling while restoring database backups.
9. Azure storage account throttling, as backup files must be copied from on-premise to Azure storage account before restoring them on target Azure SQL.

## Migration checklist

### SQL Managed Instance (SQL MI)

1. Ensure the Managed Instance selected meets the following criteria.
    a. The total size of databases selected for migration + the full Size of existing databases on the target is less than:

    | Business Type | Size |
    |---------|------|
    | General Purpose | 8 TB |
    | Business Critical with 4,8,16 vCores | 1 TB |
    | Business Critical with 24 vCores | 2 TB |
    | Business Critical with 32,40,64,80 vCores | 4 TB |

    b. The number of databases selected for migration + the number of databases on target is less than 100.
    c. The number of database files in the databases selected for migration + the number of existing database files in the user's target databases is less than:

    | Business type | Size |
    |---------|------|
    | General Purpose | 280 |
    | Business Critical | 32,767 |

    d. For the target, General Purpose SQL MI, databases can't include any in-memory OLTP objects. Examine if you have any in-memory OLTP objects in your database before migration by using the T-SQL script for [In-Memory OLTP (memory-optimized tables)](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview#in-memory-oltp-memory-optimized-tables).
    e. If the target is a business critical SQL MI environment, The total in-memory space required + any existing in-memory space on the target should be less than the [limits](/azure/azure-sql/managed-instance/resource-limits#in-memory-oltp-available-space).

2. Ensure that server collation of target Azure SQL matches with the source SQL Server.

3. Ensure Azure DMS and target Azure SQL region are the same.

4. Ensure that the on-premises network file share or Azure Storage File Share or Azure Storage Blob Container contains only backup files belonging to the migrated databases.

5. For more extensive databases, stripe full database backup into multiple files.

6. Enable compression for database backups.

7. When specifying Azure Storage account to upload/copy backup files from on-premises network file share or Azure File share, use a Block Blob Storage account in the premium performance tier to achieve higher migration speed. Azure Premium Storage provides consistent low latency and better throughput performance. Backup files are uploaded as block blobs.

8. Set up a [maintenance window](/azure/azure-sql/database/maintenance-window) for SQL MI to ensure routine service maintenance events don't impact migrations.

9. For a [high availability of Self-Hosted Integration Runtime (SHIR)](/azure/data-factory/create-self-hosted-integration-runtime#high-availability-and-scalability), install it on multiple nodes. Currently, four nodes are supported, and we recommend installing them on at least two nodes. High availability for Self-Hosted Integration Runtime (SHIR) avoids a single point of failure and ensures that scanning network share and backups uploading is resilient.

## SQL Virtual Machines (SQL VM)

1. Ensure the SQL VM selected meets the following criteria:
    a. The default location of data and log file locations set for SQL Server instance meets the requirements as it's used by DMS for restoring databases and cannot be configured.
    b. The target SQL Server version should be equal to or higher than the source SQL Server version.
    c. If the source SQL Server instance/database has file stream enabled, make sure it's enabled even in the target SQL VM.

2. Ensure that server collation of target Azure SQL matches with the source SQL Server.

3. Ensure Azure DMS and target Azure SQL region are the same.

4. Ensure the on-premises network file share or Azure Storage File Share or Azure Storage Blob Container contains only backup files belonging to the databases being migrated.

5. For larger databases, stripe full database backup into multiple files.

6. Enable compression for database backups.

7. When specifying Azure Storage account to upload/copy backup files from on-premises network file share or Azure File share, use [Block Blob Storage account](/azure/storage/common/storage-account-overview#types-of-storage-accounts) in premium performance tier if you want to achieve higher migration speed. Azure Premium Storage provides consistent low latency and better throughput performance. Backup files are uploaded as block blobs.

8. For [high availability of Self-Hosted Integration Runtime (SHIR)](/azure/data-factory/create-self-hosted-integration-runtime#high-availability-and-scalability), install it on multiple nodes. Currently, four nodes are supported, and we recommend installing them on at least two nodes. High availability for Self-Hosted Integration Runtime (SHIR) avoids a single point of failure and ensures that scanning network share and backups uploading is resilient.

## Billing

DMS for Azure SQL migration doesn't incur any billing on your subscriptions.

- Billing for orchestrating activities through the ADF Self-Hosted Integration Runtime (SHIR) component is accrued in Microsoft-owned subscription.
- Azure Storage Account provided to DMS for uploading database backups is subject to usage charges based on the size of backup files and retention. DMS creates a new container for uploading backups, and it doesn't delete the container once the migration is completed.

## Extension settings

To change the settings for the Kusto extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Find the **Azure SQL Migration** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

## Known issues and considerations

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

Currently, there's no CLI support.

## Next steps

- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Azure Data Studio extensions](add-extensions.md)
