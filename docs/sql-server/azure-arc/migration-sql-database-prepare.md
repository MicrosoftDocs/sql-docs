---
title: Prepare for migration to Azure SQL Database
titleSuffix: SQL Server migration in Azure Arc
description: Prepare your SQL Server instance enabled by Azure Arc for migration to Azure SQL Database.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 09/15/2026
ms.topic: how-to
ai-usage: ai-assisted
---

# Prepare for migration to Azure SQL Database - SQL Server migration in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article helps you prepare your SQL Server instance enabled by Azure Arc for [migration to Azure SQL Database](migrate-to-azure-sql-database.md) in the Azure portal.

> [!NOTE]
>
> - Migration to Azure SQL Database through the Azure portal is currently in [preview](release-notes.md#preview).
> - You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).

## Prerequisites

To migrate your SQL Server databases to Azure SQL Database through the Azure portal, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A SQL Server instance [enabled by Azure Arc](overview.md) with the [latest version](release-notes.md) of the Azure extension for SQL Server. To upgrade the extension, see [Upgrade the extension](connect.md#upgrade-the-extension).
- A target [Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).
- The **Microsoft.DataMigration** resource provider [registered in your subscription](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).
- Network connectivity from the self-hosted integration runtime to the source SQL Server instance and target Azure SQL Database.

> [!IMPORTANT]
> Online migrations to Azure SQL Database aren't currently available with Azure Database Migration Service. Application downtime starts when the offline migration starts. Test the migration to determine whether the downtime is acceptable.

## Configure permissions

The user who performs the migration needs the following Azure permissions:

- The **Contributor** role for the target Azure SQL Database.
- The **Reader** role for the resource group that contains the target Azure SQL Database.
- The **Owner** or **Contributor** role for the subscription if you need to create an instance of Azure Database Migration Service.

The source SQL Server login must be a member of the **db_datareader** role on the source database and have the **VIEW ANY DEFINITION** server permission. The target login must be a member of the **db_owner** role on the target database.

If you migrate the database schema, the source login needs the **db_owner** role. The target login also needs the Azure SQL Database server-level roles listed in [Permissions required to migrate to Azure SQL Database](/data-migration/sql-server/database/custom-roles#permissions-required-to-migrate-to-azure-sql-database).

## Configure a self-hosted integration runtime

Azure Database Migration Service uses a self-hosted integration runtime (SHIR) to access the source SQL Server instance and move data to Azure SQL Database. For schema migration, use SHIR version 5.37 or later.

When the migration experience prompts you to configure the integration runtime:

1. Download and install the [latest self-hosted integration runtime](https://www.microsoft.com/download/details.aspx?id=39717) on a computer that can connect to the source SQL Server instance and target Azure SQL Database.
1. Use the authentication key provided by the migration experience to register the runtime.
1. Confirm that the runtime status is **Running** before you start the migration.

For network requirements, scaling guidance, and restrictions, see [Self-hosted integration runtime for database migrations](/data-migration/sql-server/self-hosted-integration-runtime).

> [!NOTE]
> You can't use an existing self-hosted integration runtime created in Azure Data Factory for database migrations with Azure Database Migration Service.

## Prepare the target database

Create the target Azure SQL Database before you start the migration. Ensure its service tier has enough compute and storage capacity for the source workload. Migration speed depends on both the target service tier and the resources available to the SHIR host.

If the target database doesn't contain the required tables, select the schema migration option during migration. If you migrate the schema separately, create the required schema objects before you start the data migration.

## Limitations

Migration to Azure SQL Database is an offline migration. Azure Database Migration Service uses Azure Data Factory pipelines for data movement, so Azure Data Factory limits apply. Review the current [Azure SQL Database migration limitations](/azure/dms/tutorial-sql-server-to-azure-sql#limitations) before you begin.

## Next step

> [!div class="nextstepaction"]
> [Migrate to Azure SQL Database](migrate-to-azure-sql-database.md)

## Related content

- [SQL Server migration in Azure Arc overview](migration-overview.md)
- [What is Azure SQL Database?](/azure/azure-sql/database/sql-database-paas-overview)
- [Migration experience feedback](https://aka.ms/arc-migrations-feedback)
