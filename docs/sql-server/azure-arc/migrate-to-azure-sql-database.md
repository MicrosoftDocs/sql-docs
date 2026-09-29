---
title: Migration to Azure SQL Database (Preview)
titleSuffix: SQL Server migration in Azure Arc
description: Migrate databases from a SQL Server instance enabled by Azure Arc to Azure SQL Database in the Azure portal.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 09/15/2026
ms.topic: how-to
ms.collection: ce-skilling-ai-copilot
ai-usage: ai-assisted
---

# Migration to Azure SQL Database (Preview) - SQL Server migration in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article shows you how to migrate databases from a [SQL Server instance enabled by Azure Arc](overview.md) to [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview) in the Azure portal.

> [!NOTE]
>
> - Migration to Azure SQL Database through the Azure portal is currently in [preview](release-notes.md#preview).
> - You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).

## Overview

Azure SQL Database is a fully managed platform as a service (PaaS) database engine. SQL Server migration in Azure Arc helps you assess your source SQL Server databases, select an Azure SQL Database target, and start an offline migration from the Azure portal.

Use the migration assessment to identify compatibility issues and choose an appropriate service tier and configuration. During migration, Azure Database Migration Service copies the selected schema objects and table data to the target Azure SQL Database.

> [!IMPORTANT]
> Online migrations to Azure SQL Database aren't currently available with Azure Database Migration Service. Application downtime starts when the offline migration starts.

## Microsoft Copilot-assisted migration

Microsoft Copilot is built into the migration experience to help you make decisions and complete actions. You can ask questions about assessments, compare targets, start or monitor a migration, and troubleshoot migration issues.

Select the **Copilot** icon on the **Database migration** pane to open the Copilot chat window.

## Prerequisites

Before you start the migration:

- Prepare your source environment and target database. For permissions, SHIR requirements, and limitations, see [Prepare for migration to Azure SQL Database](migration-sql-database-prepare.md).
- Confirm that the source SQL Server instance has the [latest version](release-notes.md) of the Azure extension for SQL Server.
- Resolve blocking issues identified by the migration assessment.

## Update the extension

The Azure extension for SQL Server is updated independently of SQL Server. Install the current extension version to get the latest migration features and fixes. For more information, see [Upgrade the extension](connect.md#upgrade-the-extension).

## Migrate to Azure SQL Database

The **Database migration** pane guides you through four stages:

1. [Assess the source instance](#assess-the-source-instance).
1. [Select a target](#select-a-target).
1. [Migrate data](#migrate-data).
1. [Monitor the migration](#monitor-the-migration).

### Assess the source instance

1. Go to your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) in the Azure portal.
1. Under **Migration**, select **Database migration**.
1. Under **Assess source instance**, select **View report**.
1. If the assessment isn't current, select **Run assessment**.
1. In the **Azure SQL Database** tile, select **View assessment details**. Review database readiness, blocking issues, warnings, and the recommended target configuration.

Resolve blocking issues before you start the migration. Review warnings to determine whether they affect your workload.

### Select a target

1. On the **Assessments** pane, select **Create or select target**. You can also select **Select target** on the **Database migration** pane.
1. Select an existing Azure SQL Database target or create a target.
1. Provide the requested subscription, resource group, logical server, database, and authentication information.
1. Confirm the target selection and return to the **Database migration** pane.

## Migrate data

Migration to Azure SQL Database uses *logical migration* through Azure Database Migration Service (DMS) and a self-hosted integration runtime. Unlike migration to SQL Server on Azure VMs, this method doesn't stage backups in Azure Blob Storage. Instead, DMS copies the selected schema and data from the source to the target through the integration runtime.

This migration is offline. Changes made to the source after migration starts aren't captured. Plan for application downtime from the start of migration until cutover is complete.

### Choose the migration method

On the **Database migration** pane, choose the migration method for your data transfer:

1. On the **Database migration** pane, select **Migrate data**.
1. On the **New data migration** pane, under **Migration method**, select **Migration using DMS (preview)**.
1. Choose **Select** to open the Migration wizard.

### Migrate with the Migration wizard

The wizard guides you through the following steps:

1. [Set up the integration runtime](#set-up-the-integration-runtime).
1. [Connect to source and target](#connect-to-source-and-target).
1. [Select source and target databases](#select-source-and-target-databases).
1. [Select database tables to migrate](#select-database-tables-to-migrate).

:::image type="content" source="media/migrate-to-azure-sql-database/migration-steps.png" alt-text="Screenshot of the migration wizard steps, starting with integration runtime setup." lightbox="media/migrate-to-azure-sql-database/migration-steps.png":::

#### Set up the integration runtime

The self-hosted integration runtime lets DMS connect to your source SQL Server instance.

1. Under **Select existing DMS**, choose an existing DMS instance from the **Select a DMS** list. If that instance already has a registered integration runtime, its details are shown and you can continue to the next step.
1. If you don't have a DMS instance, select **Auto create new DMS**, then follow the guided steps to download, install, and register the integration runtime. Use the authentication key to register the runtime node.
1. Confirm the runtime reports as online, and then select **Next**.

For full installation, networking, and troubleshooting guidance, see [Create and configure a self-hosted integration runtime](/azure/data-factory/create-self-hosted-integration-runtime).

#### Connect to source and target

1. Under **Connect to source SQL Server**, provide the details relevant to your source environment.

1. Under **Connect to target Azure SQL Database**, provide details for your target environment.
1. Select **Next**. Both connections must validate successfully before you can continue.

#### Select source and target databases

Map each source database to its target database.

> [!NOTE]
> You can only migrate databases that are online. Selecting databases in other states is unavailable.

#### Select database tables to migrate

You can expand each database to view and select its individual tables for migration: 


1. Under **Select what you'd like to migrate**, choose:
   - **Migrate data** when the target schema already exists.
   - **Migrate schema** to create objects without copying data.
   - Both options to migrate both schema and data.

   The portal detects what's already on the target. When it finds a matching schema, it displays *Schema was found on target. Schema migration is not required* and disables **Migrate schema**. If the target table isn't empty, selecting that table replaces the existing target data.

1. Repeat for each database you intend to migrate, then select **Next**.

When you're ready, select **Review + create** and then **Create** to start the migration.

## Monitor the migration

1. On the **Database migration** pane, select **Monitor migrations**.
1. Select the migration to view its status and the status of each selected table.
1. Select **Refresh** to retrieve the latest status.

The migration completes when its status shows **Succeeded**. Validate the migrated schema and, if selected, data. Verify application connectivity before you direct production workloads to Azure SQL Database.

## Limitations

Review the [Azure SQL Database migration limitations](migration-sql-database-prepare.md#limitations) before you start a migration.

## Related content

- [Prepare for migration to Azure SQL Database](migration-sql-database-prepare.md)
- [SQL Server migration in Azure Arc overview](migration-overview.md)
- [What is Azure SQL Database?](/azure/azure-sql/database/sql-database-paas-overview)
- [Migration experience feedback](https://aka.ms/arc-migrations-feedback)
