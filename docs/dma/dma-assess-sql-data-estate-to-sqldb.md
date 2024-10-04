---
title: "Assess SQL Server readiness to migrate to Azure SQL Database"
titleSuffix: Data Migration Assistant
description: "Learn how to use Data Migration Assistant to migrate a SQL Server data estate for migration to Azure SQL Database"
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, on-premises SQL Server"
---
# Assess the readiness of a SQL Server data estate migrating to Azure SQL Database using the Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

Migrating hundreds of SQL Server instances and thousands of databases to Azure SQL Database or Azure SQL Managed Instance, our Platform as a Service (PaaS) offerings, is a considerable task. To streamline the process as much as possible, you need to feel confident about your relative readiness for migration. Identifying low-hanging fruit, including the servers and databases that are fully ready or that require minimal effort to prepare for migration, eases and accelerates your efforts.

This article provides step-by-step instructions for leveraging the [Data Migration Assistant](dma-overview.md) to summarize readiness results and surface them on the [Azure Migrate](https://portal.azure.com/?feature.customPortal=false#blade/Microsoft_Azure_Migrate/AmhResourceMenuBlade/overview) hub.

[!INCLUDE [online-offline](../includes/azure-migrate-to-assess-sql-data-estate.md)]

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Data-Migration-Assistant/player?WT.mc_id=dataexposed-c9-niner]

## Create a project and add a tool

Set up a new Azure Migrate project in an Azure subscription, and then add a tool.

An Azure Migrate project is used to store discovery, assessment, and migration metadata collected from the environment you're assessing or migrating. You also use a project to track discovered assets and to orchestrate assessment and migration.

1. Sign in to the Azure portal, select **All services**, and then search for Azure Migrate.
1. Under **Services**, select **Azure Migrate**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-services.png" alt-text="Screenshot of Azure Migrate - select service.":::

1. On the **Overview** page, select **Assess and migrate databases**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-hub-assess.png" alt-text="Screenshot of Azure Migrate - initiate assessment." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-hub-assess.png":::

1. In **Databases**, under **Getting started**, select **Add tool(s)**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-add-tools.png" alt-text="Screenshot of Azure Migrate - add tools." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-add-tools.png":::

1. On the **Migrate project** tab, select your Azure subscription and resource group (if you don't already have a resource group, create one).
1. Under **Project Details**, specify the project name and the geography in which you want to create the project.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-add-tool-dialog.png" alt-text="Screenshot of Azure Migrate - add a tool dialog box." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-add-tool-dialog.png":::

   You can create an Azure Migrate project in any of these geographies.

   | Geography | Storage location region |
   | --- | --- |
   | **Asia** | Southeast Asia or East Asia |
   | **Europe** | South Europe or West Europe |
   | **United Kingdom** | UK South or UK West |
   | **United States** | Central US or West US 2 |

   The geography specified for the project is only used to store the metadata gathered from on-premises VMs. You can select any target region for the actual migration.

1. Select **Next**, and then add an assessment tool.

   > [!NOTE]  
   > When you create a project, you must add at least one assessment or migration tool.

1. On the **Select assessment tool** tab, **Azure Migrate: Database Assessment** appears as the assessment tool to add. If you don't currently need an assessment tool, select the **Skip adding an assessment tool for now** check box. Select **Next**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-select-assessment-tool.png" alt-text="Screenshot of Azure Migrate - Select assessment tool tab." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-select-assessment-tool.png":::

1. On the **Select migration tool** tab, **Azure Migrate: Database Migration** appears as the migration tool to add. If you don't currently need a migration tool, select the **Skip adding a migration tool for now**. Select **Next**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-select-migration-tool.png" alt-text="Screenshot of Azure Migrate - Select migration tool tab." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-select-migration-tool.png":::

1. In **Review + add tools**, review the settings, and the select **Add tools**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-review-tools.png" alt-text="Screenshot of Azure Migrate - Review + add tool(s) tab.":::

   After creating the project, you can select additional tools for assessment and migration of servers and workloads, databases, and web apps.

## Assess and upload assessment results

After you successfully create a migration project, under **Assessment tools**, in the **Azure Migrate: Database Assessment** box, instructions for downloading and using the Data Migration Assistant tool display.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessment-tool-added.png" alt-text="Screenshot of Azure Migrate - Assessment tool added." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessment-tool-added.png":::

1. Download Data Migration Assistant using the link provided, and then install it on a computer with access to your source SQL Server instances.
1. Start Data Migration Assistant.

### Create an assessment

1. On the left, select the **+** icon, and then select the assessment **Project type**
1. Specify the project name, and then select the source server and target server types.

   If you're upgrading your on-premises SQL Server instance to a later version of SQL Server or to SQL Server hosted on an Azure VM, set the source and target server type to **SQL Server**. Set the target server type to **Azure SQL Managed Instance** for an Azure SQL Database (PaaS) target readiness assessment.

1. Select **Create**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-interface.png" alt-text="Screenshot of Azure Migrate - Data Migration Assistant interface." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-interface.png":::

### Choose assessment options

1. Select the report type.

   You can choose one or both of the following report types:

   - Check database compatibility
   - Check feature parity

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-options-screen.png" alt-text="Screenshot of Azure Migrate - Data Migration Assistant - assessment options screen." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-options-screen.png":::

1. Select **Next**.

### Add databases to assess

1. Select **Add Sources** to open the connection fly out menu.
1. Enter the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance name, choose the authentication type, set the correct connection properties, and then select **Connect**.
1. Select the databases to assess, and then select **Add**.

   > [!NOTE]  
   > You can remove multiple databases by selecting them while holding the Shift or Ctrl key, and then selecting Remove Sources. You can also add databases from multiple SQL Server instances by using the Add Sources button.

1. Select **Next** to start the assessment.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-select-sources-screen.png" alt-text="Screenshot of Azure Migrate - Data Migration Assistant - select sources screen." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-select-sources-screen.png":::

1. After the assessment completes, select **Upload to Azure Migrate**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-review-results-screen.png" alt-text="Screenshot showing the Data Migration Assistant with the Upload to Azure Migrate option called out." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-dma-review-results-screen.png":::

1. Sign in to the Azure portal.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-portal-signin.png" alt-text="Screenshot of the Data Migration Assistant showing the Azure portal sign in window." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-portal-signin.png":::

1. Select the subscription and Azure Migrate project into which you want to upload the assessment results, and then select **Upload**.

   Wait for the Assessment upload confirmation.

## View target readiness assessment results

1. Sign in the Azure portal, search for Azure migrate, and the select **Azure Migrate**.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-portal-search.png" alt-text="Screenshot of Azure Migrate - Azure portal - service search." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-portal-search.png":::

1. Select **Assess and migrate databases** to get to the assessment results.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-hub-assess.png" alt-text="Screenshot of Azure Migrate - review assessment results." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-hub-assess.png":::

   You can view the SQL Server readiness summary, make sure that you're on the right migration project, otherwise, use change option to select a different migration project.

   Each time you update the assessment results to Azure migrate project, Azure migrate hub consolidate all the results and provide the summary report. You can execute several Data Migration Assistant assessments in parallel and upload the results to the single migration project to get the consolidated readiness report.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-review-readiness.png" alt-text="Screenshot of Azure Migrate - review readiness results." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-review-readiness.png":::

   | Result | Description |
   | --- | --- |
   | **Assessed database instances** | The number of SQL Server instances assessed so far |
   | **Assessed databases** | Total number of databases assessed across one or more SQL Server instances assessed |
   | **Databases ready for SQL Database** | Number of databases ready to migrate to Azure SQL Database (PaaS) |
   | **Databases ready for Azure SQL VM** | Number of databases consist one or more migration blockers to Azure SQL Database (PaaS), but ready to migrate to Azure SQL Server VMs |

1. Select **Assessed database instances** to get to SQL Server instance level view.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessed-instances.png" alt-text="Screenshot of Azure Migrate - review instance readiness." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessed-instances.png":::

   You can find the percentage readiness status of each SQL Server instance migrating to Azure SQL Database (PaaS).

1. Select a specific instance to get to the database readiness view.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessed-databases.png" alt-text="Screenshot of Azure Migrate - review database readiness." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-assessed-databases.png":::

   You can see the number of migration blockers per each database, the recommended target per each database in the above view.

1. Review the DMA assessment results to get more details around the migration blockers.

   :::image type="content" source="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-migration-blockers.png" alt-text="Screenshot of Azure Migrate - review migration blockers." lightbox="media/dma-assess-sql-data-estate-to-sqldb/dms-azure-migrate-migration-blockers.png":::

## Related content

- [Data Migration Assistant (DMA)](dma-overview.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
- [Data Migration Assistant: Best Practices](dma-bestpractices.md)
