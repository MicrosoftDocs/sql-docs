---
title: "SQL Server to SQL Server on Azure Virtual Machines: Migration guide"
titleSuffix: SQL Server on Azure VMs
description: In this guide, you learn how to migrate your individual SQL Server databases to SQL Server on Azure Virtual Machines.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mathoma
ms.date: 06/26/2024
ms.service: azure-vm-sql-server
ms.subservice: migration-guide
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Migration guide: SQL Server to SQL Server on Azure Virtual Machines

[!INCLUDE [appliesto--sqlmi](../../includes/appliesto-sqlvm.md)]

In this guide, you learn [how to migrate](https://azure.microsoft.com/migration/migration-journey) your user databases from SQL Server to an instance of SQL Server on Azure Virtual Machines by tools and techniques based on your requirements.

Complete [pre-migration](../pre-migration.md) steps before continuing.

## Migrate

After you complete the steps for the [pre-migration stage](../pre-migration.md), you're ready to migrate the user databases and components. Migrate your databases by using your preferred [migration method](overview.md#migrate).

The following sections provide options for performing a migration in order of preference:

- [migrate using the Azure SQL migration extension for Azure Data Studio with minimal downtime](#migrate-using-the-azure-sql-migration-extension-for-azure-data-studio-minimal-downtime)
- [backup and restore](#backup-and-restore)
- [detach and attach from a URL](#detach-and-attach-from-a-url)
- [convert to a VM, upload to a URL, and deploy as a new VM](#convert-vm)
- [log shipping](#log-shipping)
- [ship a hard drive](#ship-a-hard-drive)
- [migrate objects outside user databases](#migrate-objects-outside-user-databases)

### Migrate using the Azure SQL migration extension for Azure Data Studio (minimal downtime)

To perform a minimal downtime migration using Azure Data Studio, follow the high level steps below. For a detailed step-by-step tutorial, see [Tutorial: Migrate SQL Server to SQL Server on Azure Virtual Machines with DMS](database-migration-service.md):

1. Download and install [Azure Data Studio](/azure-data-studio/download-azure-data-studio) and the [Azure SQL migration extension](/azure-data-studio/extensions/azure-sql-migration-extension).
1. Launch the Migrate to Azure SQL wizard in the extension in Azure Data Studio.
1. Select databases for assessment and view migration readiness or issues (if any). Additionally, collect performance data and get right-sized Azure recommendation.
1. Select your Azure account and your target SQL Server on Azure Machine from your subscription.
1. Select the location of your database backups. Your database backups can either be located on an on-premises network share or in an Azure Blob Storage container.
1. Create a new Azure Database Migration Service using the wizard in Azure Data Studio. If you have previously created an Azure Database Migration Service using Azure Data Studio, you can reuse the same if desired.
1. *Optional*: If your backups are on an on-premises network share, download and install [self-hosted integration runtime](https://www.microsoft.com/download/details.aspx?id=39717) on a machine that can connect to source SQL Server and the location containing the backup files.
1. Start the database migration and monitor the progress in Azure Data Studio. You can also monitor the progress under the Azure Database Migration Service resource in Azure portal.
1. Complete the cutover.
   1. Stop all incoming transactions to the source database.
   1. Make application configuration changes to point to the target database in SQL Server on Azure Virtual Machine.
   1. Take any tail log backups for the source database in the backup location specified.
   1. Ensure all database backups have the status Restored in the monitoring details page.
   1. Select **Complete cutover** in the monitoring details page.

### Backup and restore

To perform a standard migration by using backup and restore:

1. Set up connectivity to SQL Server on Azure Virtual Machines based on your requirements. For more information, see [Connect to a SQL Server virtual machine on Azure](/azure/azure-sql/virtual-machines/windows/ways-to-connect-to-sql).
1. Pause or stop any applications that are using databases intended for migration.
1. Ensure user databases are inactive by using [single user mode](/sql/relational-databases/databases/set-a-database-to-single-user-mode).
1. Perform a full database backup to an on-premises location.
1. Copy your on-premises backup files to your VM by using a remote desktop, [Azure Data Explorer](/azure/data-explorer/data-explorer-overview), or the [AzCopy command-line utility](/azure/storage/common/storage-use-azcopy-v10). (Greater than 2-TB backups are recommended.)
1. Restore full database backups to the SQL Server on Azure Virtual Machines.

### Detach and attach from a URL

Detach your database and log files and transfer them to [Azure Blob storage](/sql/relational-databases/databases/sql-server-data-files-in-microsoft-azure). Then attach the database from the URL on your Azure VM. Use this method if you want the physical database files to reside in Blob storage, which might be useful for very large databases. Use the following general steps to migrate a user database using this manual method:

1. Detach the database files from the on-premises database instance.
1. Copy the detached database files into Azure Blob storage using the [AzCopy command-line utility](/azure/storage/common/storage-use-azcopy-v10).
1. Attach the database files from the Azure URL to the SQL Server instance in the Azure VM.

### <a id="convert-vm"></a> Convert to a VM, upload to a URL, and deploy as a new VM

Use this method to migrate all system and user databases in an on-premises SQL Server instance to an Azure virtual machine. Use the following general steps to migrate an entire SQL Server instance using this manual method:

1. Convert physical or virtual machines to Hyper-V VHDs.
1. Upload VHD files to Azure Storage by using the [Add-AzureVHD cmdlet](/previous-versions/azure/dn495173(v=azure.100)).
1. Deploy a new virtual machine by using the uploaded VHD.

> [!NOTE]  
> To migrate an entire application, consider using [Azure Site Recovery](/azure/site-recovery/site-recovery-overview).

### Log shipping

Log shipping replicates transactional log files from on-premises on to an instance of SQL Server on an Azure VM. This option provides minimal downtime during failover and has less configuration overhead than setting up an Always On availability group.

For more information, see [Log Shipping Tables and Stored Procedures](/sql/database-engine/log-shipping/log-shipping-tables-and-stored-procedures).

### Ship a hard drive

Use the [Windows Import/Export Service method](/azure/import-export/storage-import-export-service) to transfer large amounts of file data to Azure Blob storage in situations where uploading over the network is prohibitively expensive or not feasible. With this service, you send one or more hard drives containing that data to an Azure data center where your data will be uploaded to your storage account.

### Migrate objects outside user databases

More SQL Server objects might be required for the seamless operation of your user databases post migration.

The following table provides a list of components and recommended migration methods that can be completed before or after migration of your user databases.

| Feature | Component | Migration methods |
| --- | --- | --- |
| **Databases** | Model | Script with SQL Server Management Studio. |
| | The `tempdb` database | Plan to move `tempdb` onto [Azure VM temporary disk (SSD)](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist#storage) for best performance. Be sure to pick a VM size that has a sufficient local SSD to accommodate your `tempdb`. |
| | User databases with FileStream | Use the [Backup and restore](#backup-and-restore) methods for migration. Data Migration Assistant doesn't support databases with FileStream. |
| **Security** | SQL Server and Windows logins | Use Data Migration Assistant to [migrate user logins](/sql/dma/dma-migrateserverlogins). |
| | SQL Server roles | Script with SQL Server Management Studio. |
| | Cryptographic providers | Recommend [converting to use Azure Key Vault](/azure/azure-sql/virtual-machines/windows/azure-key-vault-integration-configure). This procedure uses the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm). |
| **Server objects** | Backup devices | Replace with database backup by using [Azure Backup](/azure/backup/backup-sql-server-database-azure-vms), or write backups to [Azure Storage](/azure/azure-sql/virtual-machines/windows/azure-storage-sql-server-backup-restore-use) (SQL Server 2012 SP1 CU2 +). This procedure uses the [SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm). |
| | Linked servers | Script with SQL Server Management Studio. |
| | Server triggers | Script with SQL Server Management Studio. |
| **Replication** | Local publications | Script with SQL Server Management Studio. |
| | Local subscribers | Script with SQL Server Management Studio. |
| **PolyBase** | PolyBase | Script with SQL Server Management Studio. |
| **Management** | Database mail | Script with SQL Server Management Studio. |
| **SQL Server Agent** | Jobs | Script with SQL Server Management Studio. |
| | Alerts | Script with SQL Server Management Studio. |
| | Operators | Script with SQL Server Management Studio. |
| | Proxies | Script with SQL Server Management Studio. |
| **Operating system** | Files, file shares | Make a note of any other files or file shares that are used by your SQL servers and replicate on the Azure Virtual Machines target. |

## Post-migration

After you successfully complete the migration stage, you need to complete a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this task might require changes to the applications in some cases.

Apply any fixes recommended by Data Migration Assistant to user databases. You need to script these fixes to ensure consistency and allow for automation.

### Perform tests

The test approach to database migration consists of the following activities:

1. **Develop validation tests**: To test the database migration, you need to use SQL queries. Create validation queries to run against both the source and target databases. Your validation queries should cover the scope you've defined.
1. **Set up a test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance tests against the source and target, and then analyze and compare the results.

> [!TIP]  
> Use the [Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview) to assist with evaluating the target SQL Server performance.

### Optimize

The post-migration phase is crucial for reconciling any data accuracy issues, verifying completeness, and addressing potential performance issues with the workload.

For more information about these issues and the steps to mitigate them, see:

- [Post-migration validation and optimization guide](/sql/relational-databases/post-migration-validation-and-optimization-guide)
- [Checklist: Best practices for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist)
- [Azure cost optimization center](https://azure.microsoft.com/overview/cost-optimization/)

## Related content

- [Azure global infrastructure center](https://azure.microsoft.com/global-infrastructure/services/?regions=all&products=synapse-analytics,virtual-machines,sql-database)
- [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix)
- [What is Azure SQL?](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview)
- [What is SQL Server on Windows Azure Virtual Machines?](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
- [Azure Total Cost of Ownership (TCO) Calculator](https://azure.microsoft.com/pricing/tco/calculator/)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads for migration to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Change the license model for a SQL virtual machine in Azure](/azure/azure-sql/virtual-machines/windows/licensing-model-azure-hybrid-benefit-ahb-change)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support)
- [Data Access Migration Toolkit (preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
- [Overview of Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview)
