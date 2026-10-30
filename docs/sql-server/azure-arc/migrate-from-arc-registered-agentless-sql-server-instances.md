---
title: Migrate from Arc-registered (agentless) SQL Server instances
description: SQL Server migration in Azure Arc now supports registered agentless instances. Learn how to migrate databases without deploying the Azure Connected Machine agent.
ms.date: 10/23/2026
ms.topic: concept-article
author: subhojit-msft
ms.author: subasak
---

# Migrate from Arc-registered (agentless) SQL Server instances

**Applies to: SQL Server**

This article shows how to use SQL Server migration in Azure Arc to migrate databases from SQL Server instances that are registered with Azure Arc without the Azure Connected Machine agent (agentless, registered instances) to Azure SQL Managed Instance, SQL Server on Azure VMs, or Azure SQL Database, directly from the Azure portal.

## Overview

Previously, the migration experience in Azure Arc was available only for SQL Server instances connected to Azure Arc through the Arc agent and the Azure extension for SQL Server. Many organizations register SQL Server instances with Azure Arc but can't or won't deploy an agent because of security, compliance, or operational constraints.

The migration experience is now available for Arc-registered, agentless instances. You can start and complete a database migration without first deploying the Arc agent.

Because no agent continuously synchronizes inventory, the database list for an agentless instance might be stale, incomplete, or unavailable. The experience tells you when this condition exists and lets you add databases manually so that you can still proceed.

:::image type="content" source="media/migrate-from-arc-registered-agentless-sql-server-instances/sql-server-registered-mode-overview.png" alt-text="Screenshot of Azure portal showing SQL Server instance Overview page with Mode set to Registered." lightbox= "media/migrate-from-arc-registered-agentless-sql-server-instances/sql-server-registered-mode-overview.png":::

### Agent-connected vs. agentless instances

The following table compares the migration capabilities that are available for each type of instance.

| Capability                                                                                                                                 | Arc agent-connected instance            | Arc-registered (agentless) instance                          |
|--------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------|--------------------------------------------------------------|
| Database migration pane                                                                                                                    | Available                               | Available                                                    |
| Migration readiness assessment in the Azure portal                                                                                         | Available (automatic, weekly)           | Not available – requires the Arc agent                       |
| Database inventory                                                                                                                         | Synchronized automatically by the agent | Might be stale or incomplete; you can add databases manually |
| Azure SQL Managed Instance – Log Replay Service (LRS)                                                                                      | Available                               | Available                                                    |
| Azure SQL Managed Instance – Managed Instance link                                                                                         | Available                               | Not available                                                |
| SQL Server on Azure VMs                                                                                                                    | Available                               | Available                                                    |
| Azure SQL Database (preview) – offline migration using Azure Database Migration Service (DMS) and a self-hosted integration runtime (SHIR) | Available                               | Available                                                    |
| Monitor migrations in the portal                                                                                                           | Available                               | Available                                                    |

## Prerequisites

Before you begin, ensure that you have the following items:

- An active Azure subscription.

- A SQL Server instance registered with Azure Arc without the agent.

- Permissions on the target, as described in the target-specific preparation articles.

- Preparation for your chosen target:

- Azure SQL Managed Instance (LRS): An Azure Blob Storage account in the same subscription as the target managed instance, with backups uploaded and access permissions granted. For more information, see [Prepare environment for LRS migration](migration-sql-mi-prepare-log-replay-service.md).

- SQL Server on Azure VMs: A Blob Storage account with backups uploaded and permissions granted. See [Prepare environment for a SQL VM migration](migration-sql-vm-prepare.md).

- Azure SQL Database: A target Azure SQL Database that you create beforehand, the `Microsoft.DataMigration` resource provider registered in your subscription, and a self-hosted integration runtime registered with your Azure Database Migration Service instance, with network connectivity to both the source SQL Server instance and the target. You can't reuse a self-hosted integration runtime created in Azure Data Factory. See [Prepare for migration to Azure SQL Database](migration-sql-database-prepare.md).

> [!IMPORTANT]
>  - Migration to Azure SQL Database is offline. Changes made on the source after migration starts aren't captured.
>  - Application downtime starts when the migration starts.

## Limitations

The following limitations apply when you migrate from an agentless instance:

- Migration readiness assessment, target sizing, and target recommendations in the Azure portal require the Arc agent and aren't available for agentless instances.

- Managed Instance link (real-time replication) isn't available for agentless instances. Use LRS to migrate to Azure SQL Managed Instance.

- The database list might not reflect the current state of the source instance. Verify databases before you migrate.

- Migration to Azure SQL Database is in preview and offline only.

- The limitations of LRS, SQL Server on Azure VM migration, and Azure Database Migration Service also apply.

## Open the Database migration pane

1. In the Azure portal, go to your Arc-registered SQL Server instance.

1. Under **Migration**, select **Database migration**.

The pane guides you through four steps: **Assess source instance**, **Select target**, **Migrate data**, and **Monitor and cutover**.

:::image type="content" source="media/migrate-from-arc-registered-agentless-sql-server-instances/database-migration-pane-overview.png" alt-text="Screenshot of Database migration pane showing four steps: assess, select target, migrate data, and monitor." lightbox="media/migrate-from-arc-registered-agentless-sql-server-instances/database-migration-pane-overview.png":::

## Assess source instance

For an agentless instance, a banner explains that the migration readiness assessment requires the Arc agent. You have two options:

- Deploy the Arc agent to get the automatic assessment. See [Connect your SQL Server to Azure Arc](connect.md).

- Skip the assessment and continue to Select target, if you already know your configuration and target.

>[!Tip]
> To assess without the Arc agent, run an assessment from the migration component in SQL Server Management Studio (SSMS 22 or later). For instances that aren't Arc-enabled, SSMS runs an assessment and compares supported Azure SQL targets. For more information, see Migrate SQL Server to Azure SQL using the [migration component in SSMS](/ssms/migrate/migrate-sql-server-azure-sql).

:::image type="content" source="media/migrate-from-arc-registered-agentless-sql-server-instances/azure-assessments-arc-notice.png" alt-text="Screenshot of Azure Assessments page noting Arc agent requirement for automated SQL Server assessment.":::

## Review and add databases

For agentless instances, the database list doesn't continuously synchronize, so it might be out of date. A banner indicates when the database list might be stale.
To add a database manually:

1. On the Database migration pane, select option to **Add a database**.
1. Enter the database name exactly as it appears on the source SQL Server instance.
1. Select the **Add** button.

The database is added as a database resource for the instance and becomes available to select for migration.

:::image type="content" source="media/migrate-from-arc-registered-agentless-sql-server-instances/new-data-migration-database-list.png" alt-text="Screenshot of New Data Migration pane listing databases with migration readiness status and target names, showing option for adding new database.":::

> [!NOTE]  
> Verify database names against the source instance before you start the migration.

## Select target

On the Database migration pane, select **Select target**. Choose an existing target or create a new one: Azure SQL Managed Instance, SQL Server on Azure VM, or Azure SQL Database (preview). Provide the subscription, resource group, and target details.

Because the portal assessment isn't available for agentless instances, you choose and size the target yourself. For example, you can use the assessment in SSMS.

## Migrate data

Follow the procedure for your target.
 - **[Migrate to Azure SQL Database by using DMS (offline)](migrate-to-azure-sql-database.md)**

 - **[Migrate to Azure SQL Managed Instance by using LRS](migrate-to-azure-sql-managed-instance.md)**

 - **[Migrate to SQL Server on Azure VMs](migrate-to-sql-server-on-azure-vms.md)**

## Monitor and cut over

On the Database migration pane, select **Monitor migrations** to see each database's status, method, target, duration, and start time. Select **Refresh** for the latest status.

- **Azure SQL Managed Instance and SQL Server on Azure VMs:** When the status shows **Ready for cutover**, stop application traffic and other writes to the source. Take and upload the final transaction log backup for either target, and ensure all backups through that final backup are applied before completing **Cutover**.
- **Azure SQL Database**: The migration is complete when the status shows **Succeeded**. Validate the schema, data, and application connectivity before you move production workloads.

## Troubleshoot

Use the following table to resolve common issues with agentless instances.

| Issue                                                         | Cause                                                             | Resolution                                                                       |
|---------------------------------------------------------------|-------------------------------------------------------------------|----------------------------------------------------------------------------------|
| Assessment shows an agent-required banner                     | Portal assessment requires the Arc agent                          | Deploy the agent, assess with SSMS, or continue to migration without assessment. |
| A database isn't listed                                       | Inventory for agentless instances isn't continuously synchronized | Add the database manually.                                                       |
| Managed Instance link option is unavailable                   | Not supported for agentless instances                             | Use LRS.                                                                         |
| Integration runtime is offline or connection validation fails | Runtime not registered, or no network path to source or target    | Confirm the runtime is online and check network connectivity.                    |

## Related content

- [SQL Server migration in Azure Arc Overview](migration-overview.md)
- [Migration to Azure SQL Managed Instance](migrate-to-azure-sql-managed-instance.md)
- [Prepare environment for LRS migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-log-replay-service.md)
- [Migration to SQL Server on Azure VMs](migrate-to-sql-server-on-azure-vms.md)
- [Migration to Azure SQL Database (preview)](migrate-to-azure-sql-database.md)
- [Prepare for migration to Azure SQL Database](migration-sql-database-prepare.md)
- [Migration experience feedback directly to the product group](https://aka.ms/arc-migrations-feedback)
- [Connect your SQL Server to Azure Arc](overview.md)
- [Migrate SQL Server to Azure SQL using the migration component in SSMS](https://learn.microsoft.com/ssms/migrate/migrate-sql-server-azure-sql)
- [Troubleshoot issues when migrating to Azure SQL Managed Instance](migrate-to-azure-sql-managed-instance-troubleshoot.md)
