---
title: Self-Hosted Integration Runtime for Database Migrations
description: Learn about the self-hosted integration runtime, used to migrate databases with Azure Database Migration Service.
author: abhims14
ms.author: abhishekum
ms.reviewer: abhishekum
ms.date: 02/19/2026
ms.service: azure-database-migration-service
ms.topic: upgrade-and-migration-article
ms.collection:
  - sql-migration-content
ms.custom:
  - references_regions
  - sfi-image-nochange
---

# Self-hosted integration runtime for database migrations

You can configure a self-hosted integration runtime (SHIR) to use your own compute resources to access the source SQL Server instance backup files in your on-premises environment.

You need a self-hosted integration runtime to access database backups from your on-premises network share.

The SHIR runtime can be downloaded from the Azure portal. The key is provided by Azure Database Migration Service (Azure DMS).

For information about specific migration scenarios and Azure SQL targets, see the list of tutorials in the following table:

| Migration scenario | Migration mode |
| --- | --- |
| SQL Server to Azure SQL Managed Instance | [SQL Server migration experience in Azure Arc](/sql/sql-server/azure-arc/migration-overview) |
| SQL Server to SQL Server on an Azure virtual machine | [Online](virtual-machines/database-migration-service-online.md) / [Offline](virtual-machines/database-migration-service-offline.md) |
| SQL Server to Azure SQL Database | [Offline](database/database-migration-service.md) |

> [!IMPORTANT]  
> If your target is Azure SQL Database, you can migrate database schema and data both using Azure DMS via Azure portal. You can also use the [SQL Database Projects extension](/sql/tools/visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension) for Visual Studio Code to deploy the database schema before you begin a data migration.

## Recommendations

Use a single self-hosted integration runtime for multiple source SQL Server databases.

Install only one instance of a self-hosted integration runtime on any single computer.

Associate only one self-hosted integration runtime with one instance of Azure DMS.

The self-hosted integration runtime uses resources (memory and CPU) on the computer it's installed on. Install the self-hosted integration runtime on a computer that's separate from your source SQL Server instance. But the two computers should be in close proximity. Having the self-hosted integration runtime close to the data source reduces the time it takes for the self-hosted integration runtime to connect to the data source.

Use the self-hosted integration runtime only when you have your database backups in an on-premises SMB network share. A self-hosted integration runtime isn't required for database migrations if your source database backups are already in the storage blob container.

Use up to 10 concurrent database migrations per self-hosted integration runtime on a single computer. To increase the number of concurrent database migrations, scale out the self-hosted runtime to up to four nodes or create separate instances of the self-hosted integration runtime on different computers.

Configure the self-hosted integration runtime to auto-update and automatically apply any new features, bug fixes, and enhancements that are released. For more information, see [Self-hosted integration runtime auto-update](/azure/data-factory/self-hosted-integration-runtime-auto-update).

## Limitations

You can't use an existing self-hosted integration runtime that you created in Azure Data Factory for database migrations with Azure DMS.

## Related content

- [What is Azure Database Migration Service?](/azure/dms/dms-overview)
- [Azure Data Migration Guides](../index.yml)
