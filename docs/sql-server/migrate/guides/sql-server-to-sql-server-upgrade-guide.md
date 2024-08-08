---
title: Upgrade to the latest version of SQL Server
description: Step-by-step guidance for modernizing your data assets
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 07/25/2024
ms.service: sql
ms.subservice: migration-guide
ms.topic: how-to
---

# Upgrade SQL Server to the latest version

In this guide, you learn how to upgrade your user databases from previous versions of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] by using the Data Migration Assistant (DMA).

For other migration guides, see [Azure Database Migration](/data-migration/).

## Prerequisites

Before beginning your migration project, it's important to address the associated prerequisites.
Learn about the supported versions and considerations for [upgrading SQL Server](../../../database-engine/install-windows/upgrade-sql-server.md).

To prepare for the migration, download and install the following items:

- [Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) v5.3 or later.
- [Database Experimentation Assistant](https://www.microsoft.com/download/details.aspx?id=54090).

## Premigration

After you confirm the source environment is supported and any prerequisites are addressed, you can start the Premigration stage. The process involves conducting an inventory of the databases that you need to migrate. Next, assess the databases for potential migration issues or blockers, and then resolving any items you might have uncovered. The following two sections cover the premigration steps of discover, assess.

### Discover

The [Azure Migrate: Discovery and assessment tool](/azure/migrate/migrate-services-overview#azure-migrate-discovery-and-assessment-tool) discovers and assesses on-premises VMware VMs, Hyper-V VMs, and physical servers for migration to Azure.

You can use this tool for the following steps:

- **Azure readiness**: Assesses whether on-premises servers, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances, and web apps are ready for migration to Azure.
- **Azure sizing**: Estimates the size of Azure VMs/Azure SQL configuration/number of Azure VMware Solution nodes after migration.
- **Azure cost estimation**: Estimates costs for running on-premises servers in Azure.
- **Dependency analysis**: Identifies cross-server dependencies and optimization strategies for moving interdependent servers to Azure. Learn more about Discovery and assessment with [dependency analysis](/azure/migrate/concepts-dependency-visualization).

The Discovery and assessment tool uses a lightweight [Azure Migrate appliance](/azure/migrate/migrate-appliance) that you deploy on-premises.

- The appliance runs on a VM or physical server. You can install it easily using a downloaded template.
- The appliance discovers on-premises servers. It also continually sends server metadata and performance data to Azure Migrate.
- Appliance discovery is agentless. Nothing is installed on discovered servers.
- After appliance discovery, you can gather discovered servers into groups and run assessments for each group.

## Assess and convert

After you identify the data sources, the next step is to assess the on-premises [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances. Use the Data Migration Assistant (DMA) to assess your source database before upgrading your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance.

To use DMA to create an assessment, complete the following steps.

1. Download the [DMA tool](https://www.microsoft.com/download/details.aspx?id=53595), and then install it.

1. Create a **New Assessment** project.

   1. Select the New (+) icon, select the **Assessment** project type, specify a project name, select **SQL Server** as the source and target, and then select **Create**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-new-project.png" alt-text="Screenshot of New Assessment." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-new-project.png":::

   1. Select the target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] version that you plan to migrate to and against which you need to run an assessment, select one or both of the assessment report types (**Compatibility Issues** and **New features' recommendation**), and then select **Next**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-assessment.png" alt-text="Screenshot of Report Types." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-assessment.png":::

   1. In **Connect to a server**, specify the name of the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance to connect to, specify the Authentication type and Connection properties, and then select **Connect**.

   1. In the **Add Sources** panel, select the databases you that want to assess, and then select **Add**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-add-db.png" alt-text="Screenshot of Add databases." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-add-db.png":::

   1. Select **Start Assessment**.

      Now wait for the assessment results; the duration of the assessment depends on the number of databases added and the schema size of each database. Results are displayed per database as soon as they're available.

   1. Select the database that has completed assessment, and then switch between **Compatibility issues** and **Feature recommendations** by using the switcher.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-assessment-results.png" alt-text="Screenshot of Assessment results." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-assessment-results.png":::

   1. Review the compatibility issues by analyzing the affected object and its details for every issue identified under **Breaking changes**, **Behavior changes**, and **Deprecated features**.

   1. Review feature recommendations across the **Performance**, **Storage**, and **Security** areas.

      Feature recommendations cover various features such as In-Memory OLTP and Columnstore, Always Encrypted (AE), Dynamic Data Masking (DDM), and Transparent Data Encryption (TDE).

1. Review assessment results.

   1. After all database assessments are complete, select **Export report** to export the results to either a JSON or CSV file for analyzing the data at your own convenience.

### Optional A/B testing

This step is considered optional and not necessary to complete migration. To use DEA for database migration testing, complete the following steps.

1. **Download the [DEA tool](https://www.microsoft.com/download/details.aspx?id=54090)**, and then install it.

1. **Run a trace capture**

   1. On the left navigation tree, select the camera icon the go to **All Captures**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-new-capture.png" alt-text="Screenshot of New trace capture.":::

   1. To start a new capture, select **New Capture**.

   1. To configure the capture, specify the trace name, duration, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance name, database name, and the share location for storing the trace file on the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-capture-inputs.png" alt-text="Screenshot of Provide trace capture inputs." lightbox="media/sql-server-to-sql-server-upgrade-guide/dea-capture-inputs.png":::

   1. Select **Start** to begin trace capture.

1. **Run a trace replay**

   1. On the left navigation tree, select the play icon the go to **All Replays**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-new-replay.png" alt-text="Screenshot of New trace replay.":::

   1. To start a new replay, select **New Replay**.

   1. To configure the replay, specify the replay name, controller machine name, path to source trace file on controller, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance name, and the path for storing the target trace file on the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

   1. Select **Start** to begin replay of your capture.

1. **Create a new Analysis Report**

   1. On the left navigation tree, select the checklist icon to go to **Analysis Reports**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-new-analysis.png" alt-text="Screenshot of New Analysis Report.":::

   1. Connect to the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on which you'll store your report databases.

      You see the list of all reports in the server.

   1. Select **New Report**.

   1. To configure the report, specify the report name, and specify paths to the traces for the source and target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-analysis-input.png" alt-text="Screenshot of Provide report analysis inputs." lightbox="media/sql-server-to-sql-server-upgrade-guide/dea-analysis-input.png":::

1. **Review an analysis report**

   1. On the first page of the report, the version and build information for the target servers on which the experiment was run displays.

      Threshold allows you to adjust the sensitivity or tolerance of your A/B Test analysis.

      > [!NOTE]  
      > By default, threshold is set to 5%; any performance improvement that is greater than or equal to 5% is categorized as 'Improved'. The dropdown list selector allows you to evaluate the report using different performance thresholds.

   1. Select the individual slices of the pie chart to view detailed metrics on performance.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-chart.png" alt-text="Screenshot of Drill-down report." lightbox="media/sql-server-to-sql-server-upgrade-guide/dea-chart.png":::

      On the detail page for a performance change category, you see a list of queries in that category.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-error-queries.png" alt-text="Screenshot of Drill-down report queries." lightbox="media/sql-server-to-sql-server-upgrade-guide/dea-error-queries.png":::

   1. Select an individual query to get performance summary statistics, error information, and query plan information.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dea-summary-stats.png" alt-text="Screenshot of Summary Statistics." lightbox="media/sql-server-to-sql-server-upgrade-guide/dea-summary-stats.png":::

### Convert

After assessing one or more source database instances you're migrating, for heterogenous migrations, you need to convert the schema to work in the target environment. Since upgrading to a newer version of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] would be considered a homogeneous migration, the Convert step is unnecessary.

## Migration overview

After you have the necessary prerequisites in place, and complete the tasks associated with the **Pre-migration** stage, you're ready to complete the schema and data migration. A successful migration and upgrade means you addressed all the issues discovered from the premigration stage.

Review the compatibility issues discovered with DMA tool.

Preserve backup logs, maintenance plans, and other automated tasks, including jobs by creating a backup of the system [database msdb](../../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).

View [linked servers](../../../relational-databases/linked-servers/linked-servers-database-engine.md) by using SQL Server Management Studio. In the Object Explorer, right-click server objects to expand list.

Additional considerations might be needed based on the complexity of your data and environment.

- [Troubleshoot orphaned users (SQL Server)](../../failover-clusters/troubleshoot-orphaned-users-sql-server.md)
- [Migrating Triggers](../../../relational-databases/in-memory-oltp/migrating-triggers.md)
- [Generate and Publish Scripts Wizard](../../../ssms/scripting/generate-and-publish-scripts-wizard.md)
- [Mirrored Backup Media Sets (SQL Server)](../../../relational-databases/backup-restore/mirrored-backup-media-sets-sql-server.md)
- [Backup overview (SQL Server)](../../../relational-databases/backup-restore/backup-overview-sql-server.md)
- [Editions and supported features of SQL Server 2022](../../editions-and-components-of-sql-server-2022.md)

### Migrate schema and data

After you assess your databases, the next step is to begin the process of migrating the schema and database by using DMA.

### Migrate schema and data sync

To use DMA to create a migration project, complete the following steps.

1. Create a **New Migration** project

   1. Select the New icon, select the **Migration** project type, select **SQL Server** as source and target types, and then select **Create**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-migrate.png" alt-text="Screenshot of New Migration." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-migrate.png":::

   1. Provide source and target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] connection details, and then select **Next**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-source-target.png" alt-text="Screenshot of Source & Target details." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-source-target.png":::

   1. Select databases from the source to migrate, and then specify the **Shared location accessible by source and target SQL servers for backup operation**.

      > [!NOTE]  
      > Be sure that the service account running the source [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance has write privileges on the shared location and that the target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] service account has read privileges on the shared location.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-migrate-add-db.png" alt-text="Screenshot of Select databases to migrate." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-migrate-add-db.png":::

   1. Select **Next**, select the logins that you want to migrate, and then select **Start Migration**.

      :::image type="content" source="media/sql-server-to-sql-server-upgrade-guide/dma-select-logins.png" alt-text="Screenshot of Migration Logins." lightbox="media/sql-server-to-sql-server-upgrade-guide/dma-select-logins.png":::

   1. Now, monitor the progress of migration in the **View Results** screen.

1. **Review Migration Results**

   1. Select **Export report** to save the migration results to a .csv or .json file.

   1. Review the saved file for details about data and logins migration and verify successful completion of the process.

### Data sync and cutover

For minimal-downtime migrations, the source you're migrating continues to change after the one-time migration occurs, data and schema might be different from the target.
During this process, you need to ensure every change in the source are captured and applied to the target in near real time. After you verify changes in source are applied to the target, cutover from the source to the target environment.

Support for minimal-downtime migrations isn't yet available for this scenario, so the Data sync and Cutover plans aren't currently applicable.

## Post migration

After you successfully complete the **Migration** stage, you need to go through a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible. The post-migration is crucial for reconciling any data accuracy issues and verifying completeness, and addressing performance issues with the workload.

For more information about these issues, specific steps to mitigate them, and after the migration
see the [Post-migration Validation and Optimization Guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md).

#### Verify applications

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this requires changes to the applications in some cases. Test against the databases to verify that the applications work as expected after the migration.

## Related content

- [Services and tools for data migration](/azure/dms/dms-tools-matrix)
- [Azure Database Migration Guide](/data-migration/)
- [Overview of the migration journey](https://azure.microsoft.com/resources/videos/overview-of-migration-and-recommended-tools-services/)
