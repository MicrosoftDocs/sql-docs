---
title: Migration to SQL Server on Azure VMs
titleSuffix: SQL Server migration in Azure Arc
description: SQL Server migration in Azure Arc to SQL Server on Azure VMs in the Azure portal. Learn how to migrate your instance to Azure.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 07/10/2026
ms.topic: how-to
ms.collection: ce-skilling-ai-copilot

---

# Migration to SQL Server on Azure VMs - SQL Server migration in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article shows you how to perform a SQL Server migration in Azure Arc to [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) in the Azure portal for your [SQL Server instance enabled by Azure Arc](overview.md).

> [!NOTE]  
> You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).

## Overview

SQL Server on Azure VMs is an infrastructure as a service (IaaS) target to run your SQL Server workloads on the Azure cloud platform. After your SQL Server instance is enabled by Azure Arc, you can assess your SQL Server data estate to identify an optimal SQL Server VM configuration. Then you can migrate your SQL Server databases to a SQL Server VM directly from the Azure portal.

When your SQL Server instance is enabled by Azure Arc, you can:

- Evaluate and assess whether your SQL Server instance is ready to migrate to a SQL Server VM.
- Identify potential migration issues, and learn how to mitigate them.
- Optimize for performance and cost with guidance around configuration and sizing.

Discovery of SQL Server instances and generation of readiness reports happen automatically every weekend, but you can start them manually at any time. The process takes only a few minutes to complete. No extra configuration or setup is required.

First, you choose an appropriate SQL Server VM target and prepare your environment. Then, you can migrate your SQL Server databases to a SQL Server VM directly from the Azure portal through a fully managed and automated process.

Database migration is available by default for all SQL Server instances enabled by Azure Arc, starting with [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)].

The **Database Migration** pane also has a useful summary of the migration status for your instance, such as the number of total databases, the recommended target, the number of completed migrations, and the number of ongoing migrations: 

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/database-migration-summary.png" alt-text="Screenshot of the summary on the Database Migration pane in the Azure portal." lightbox="media/migrate-to-azure-sql-managed-instance/database-migration-summary.png":::


## Microsoft Copilot assisted migration

Microsoft Copilot is built into the experience to assist you throughout the migration process. Interactively chatting with Microsoft Copilot searches through the Microsoft knowledgebase to help you along the way as you migrate to Azure.

Microsoft Copilot provides AI-powered assistance to help you make decisions or take actions at certain points with prompts such as:
- How are assessments made?
- Help me compare.
- Start the migration.
- Help me choose the right migration method.
- Monitor the migration.
- Complete the migration.

Select the **Copilot** icon on the **Database migration** pane to open the Copilot chat window:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/copilot-integration.png" alt-text="Screenshot that shows the Copilot icon on the Database migration pane in the Azure portal.":::

## Prerequisites

To use SQL Server migration in Azure Arc, you need the following prerequisites:

- An active Azure subscription. If you don't have one, you can [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- Your SQL Server instance must be [enabled by Azure Arc](overview.md) with the [latest version](release-notes.md) of the Azure extension for SQL Server. 
- Your source environment has been [prepared](migration-sql-vm-prepare.md) for migration.

## Update the extension

Before starting the migration, make sure you have the current version of the Azure extension for SQL Server installed on your SQL Server instance. The extension is updated independently of SQL Server, so having the latest version ensures you have all the latest features and fixes for migration.

To update your extension, see [Update the extension](connect.md#upgrade-the-extension).

## Migrate to SQL Server on Azure VMs

The following tiles on the **Database Migration** pane guide you through the migration of your SQL Server databases to SQL Server on Azure VMs:

1. [Assess source instance](#assess-source-instance): Assess your SQL Server instance to determine its readiness to migrate to a SQL Server VM.
1. [Select target](#select-target): Select a SQL Server VM target for your migration.
1. [Migrate data](#migrate-data): Migrate your SQL Server databases to a SQL Server VM.
1. [Monitor and cutover](#monitor-and-cutover): Monitor the migration process and cut over to the SQL Server VM target.

The following screenshot shows the tiles on the **Database migration** pane for your SQL Server instance in the Azure portal:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/migration-home-page.png" alt-text="Screenshot that shows the migration home page for a SQL Server instance in the Azure portal." lightbox="media/migrate-to-azure-sql-managed-instance/migration-home-page.png":::

### Assess source instance

To assess the source instance, follow these steps:

1. Go to your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) in the Azure portal.
1. Under **Migration**, select **Database migration** to open the **Database migration** pane. Under **Assess source instance**, select **View report** to open the **Assessments** pane.

   :::image type="content" source="media/migrate-to-azure-sql-managed-instance/database-migration-pane.png" alt-text="Screenshot that shows the Database migration pane for the SQL Server instance in the Azure portal, with View report highlighted.":::

1. On the **Assessments** pane:

   - Use **Run assessment** to start a new assessment if one wasn't run recently.
   - Use **View assessment details** in the **SQL Server on Azure VM** tile to learn more about your assessment results, including the readiness of your SQL Server instance to migrate to a SQL Server VM. You also learn about the recommended configuration for your target instance.

### Select target

After you assess your SQL Server instance, select a SQL Server VM target.

1. On the **Assessments** pane, select **Create or select target**. Or, on the **Database migration** pane, select **Select target**. Both options open the **Create or select target** pane where you can identify a migration target.

   :::image type="content" source="media/migrate-to-sql-server-on-azure-vms/select-target.png" alt-text="Screenshot that shows the Assessments pane in the Azure portal, with Create target highlighted.":::

1. On the **Create or select target** pane, under **Target exists**, select one of the following options:

   - **Yes, I have already created a target**: Select an existing SQL Server VM target.
   - **No, I want to create a new target**: Create a new SQL Server VM target.

   :::image type="content" source="media/migrate-to-sql-server-on-azure-vms/select-or-create-target.png" alt-text="Screenshot that shows the Create or select target pane.":::

1. Fill in the required information for the SQL Server VM target. Then use either **Select target** or **Create target** to proceed to the next step based on your **Target exists** selection.
   - If you already have a SQL Server VM as a target, choosing **Select target** takes you to the **Database migration** pane for your SQL Server VM. The SQL Server VM name is populated as the selected target. Then you can proceed to the **Migrate data** step.
   - If you chose to create a new SQL Server VM, you're guided to the **Create SQL Server VM** pane on the **Database migration** pane to create the target SQL Server VM. After you finish, check the progress of the deployment on the **Database migration** pane for your SQL Server VM. The target name populates in the **Target** tile. Then you can proceed to the **Migrate data** step.

### Migrate data

After your target is ready, start the migration process.

Selections on the **New data migration** page aren't available, or will error out, after you choose **Migrate using backup and restore** unless the Azure Blob Storage account has at least one single full backup, and permissions are correctly set.

SQL Server VM migration relies on backups that you upload to an intermediary Azure Blob Storage account. If you have multiple backups, or want to have a continuous migration, you need to continue [uploading backups](migration-sql-vm-prepare.md#upload-backups-to-your-blob-storage-account) to the same blob storage account until you're ready to cut over.

Follow these steps to migrate your SQL Server databases to a SQL Server VM by using backup and restore:

1. On the **Database migration** pane, select **Migrate data**.
1. On the **New data migration** pane, choose **Migrate using backup and restore**, and then use **Select** to proceed to the next page:

   :::image type="content" source="media/migrate-to-sql-server-on-azure-vms/migrate-data.png" alt-text="Screenshot of the Migrate Data page in the Azure portal.":::

1. On the **Select source databases** tab, check the boxes next to the databases that you want to migrate, and then use **Next** to proceed to the next page:

   :::image type="content" source="media/migrate-to-sql-server-on-azure-vms/select-source-database.png" alt-text="Screenshot of the select source databases page when you migrate your database in the Azure portal." lightbox="media/migrate-to-sql-server-on-azure-vms/select-source-database.png":::

1. On the **Azure Blob Storage details** tab, provide the subscription, location, resource group, blob container, and directory details for where you stored your backups.

1. On the **Review + create** tab, review the settings, and check for errors and warnings. While it's possible to start the migration when there are warnings, the warnings should be addressed at some point to ensure a successful long-term migration. Select **Start data migration** to migrate your data to your SQL Server VM target.

### Monitor and cutover

After you start your migration, you can monitor the progress. On the **Database migration** pane, select **Monitor migrations**.

The **Monitor and cutover** pane shows useful information about the migration process, such as:

- The databases that successfully migrated and the databases that are still in progress.
- The chosen migration method.
- The target instance and target database.
- The duration of the current migration of each database.
- The time when the migration started.

You can complete or cancel the migration from the **Monitor and cutover** pane. You can also view logs for information about the migration. Selecting a database takes you to a pane with more details about the source and target.

After the migration finishes, the migration status shows **Ready for cutover**. To cut over to the SQL Server VM target, select **Cutover** on the **Monitor and cutover** pane. You can also use the database details pane.

Select a database and then use **Cutover** to open the **Cutover** pane and see different options based on the migration method that you selected.

## Limitations

To learn more, see [limitations](migration-sql-vm-prepare.md#limitations).

## Related content

- [Migrate SQL Server to Azure SQL](/azure/dms/dms-overview)
- [What is SQL Server on Azure Windows Virtual Machines?](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
- [SQL Server enabled by Azure Arc](overview.md)
- [Migration experience feedback directly to the product group](https://aka.ms/arc-migrations-feedback)
