---
title: "Perform a SQL Server migration assessment"
titleSuffix: Data Migration Assistant
description: Learn how to use Data Migration Assistant to assess an on-premises SQL Server before migrating to another SQL Server or to Azure SQL Database
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.custom:
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Perform a SQL Server migration assessment with Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

The following step-by-step instructions help you perform your first assessment for migrating to on-premises SQL Server, SQL Server running on an Azure VM, or Azure SQL Database by using Data Migration Assistant.

Data Migration Assistant v5.0 introduces support for analyzing database connectivity and embedded SQL queries in the application code. For more information, see the blog post [Using Data Migration Assistant to assess an application's data access layer](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/using-data-migration-assistant-to-assess-an-application-s-data/ba-p/990430).

[!INCLUDE [online-offline](../includes/azure-migrate-to-assess-sql-data-estate.md)]

## Create an assessment

1. Select the **New** (+) icon, and then select the **Assessment** project type.

1. Set the source and target server type.

    If you're upgrading your on-premises SQL Server instance to a modern on-premises SQL Server instance or to SQL Server hosted on an Azure VM, set the source and target server type to **SQL Server**. If you're migrating to Azure SQL Database, instead set the target server type to **Azure SQL Database**.

1. Select **Create**.

   :::image type="content" source="media/dma-assesssqlonprem/new-assessment.png" alt-text="Screenshot of Create an assessment." lightbox="media/dma-assesssqlonprem/new-assessment.png":::

## Choose assessment options

1. Select the target SQL Server version to which you plan to migrate.

1. Select the report type.

   When you're assessing your source SQL Server instance for migrating to on-premises SQL Server or to SQL Server hosted on Azure VM targets, you can choose one or both of the following assessment report types:

    - **Compatibility Issues**
    - **New features' recommendation**

   :::image type="content" source="media/dma-assesssqlonprem/assessment-types.png" alt-text="Screenshot of Select an assessment report type for SQL Server target." lightbox="media/dma-assesssqlonprem/assessment-types.png":::

   When assessing your source SQL Server instance for migrating to Azure SQL Database, you can choose one or both of the following assessment report types:

    - **Check database compatibility**
    - **Check feature parity**

    :::image type="content" source="media/dma-assesssqlonprem/assessment-types-azure.png" alt-text="Screenshot of Select assessment report type for SQL Database target." lightbox="media/dma-assesssqlonprem/assessment-types-azure.png":::

## Add databases and extended events trace to assess

1. Select **Add Sources** to open the connection flyout menu.

1. Enter the SQL Server instance name, choose the Authentication type, set the correct connection properties, and then select **Connect**.

1. Select the databases to assess, and then select **Add**.

    > [!NOTE]  
    > You can remove multiple databases by selecting them while holding the Shift or Ctrl key, and then selecting **Remove Sources**. You can also add databases from multiple SQL Server instances by selecting **Add Sources**.

1. If you have any ad hoc or dynamic SQL queries or any DML statements initiated through the application data layer, then enter the path to the folder in which you placed all the extended events session files that you collected to capture the workload on the source SQL Server.

     The following example shows how to create an extended event session on your source SQL Server to capture the application data layer workload. Capture the workload for the duration that represents your peak workload.

    ```sql
    DROP EVENT SESSION [DatalayerSession] ON SERVER
    go
    CREATE EVENT SESSION [DatalayerSession] ON SERVER
    ADD EVENT sqlserver.sql_batch_completed(
        ACTION (sqlserver.sql_text,sqlserver.client_app_name,sqlserver.client_hostname,sqlserver.database_id))
    ADD TARGET package0.asynchronous_file_target(SET filename=N'C:\temp\Demos\DataLayerAppassess\DatalayerSession.xel')
    WITH (MAX_MEMORY=2048 KB,EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,MAX_DISPATCH_LATENCY=3 SECONDS,MAX_EVENT_SIZE=0 KB,MEMORY_PARTITION_MODE=NONE,TRACK_CAUSALITY=OFF,STARTUP_STATE=OFF)
    go
    ---Start the session
    ALTER EVENT SESSION [DatalayerSession]
          ON SERVER
        STATE = START;
    ---Wait for few minutes

    ---Query events

        SELECT
        object_name,
        CAST(event_data as xml) as event_data,
        file_name,
        file_offset
    FROM sys.fn_xe_file_target_read_file('C:\temp\Demos\DataLayerAppassess\DatalayerSession*xel',
                'C:\\temp\\Demos\\DataLayerAppassess\\DatalayerSession*xem',
                null,
                null)
    ---Stop the session after capturing the peak load.
    ALTER EVENT SESSION [DatalayerSession]
          ON SERVER
        STATE = STOP;

        go
    ```

1. Select **Next** to start the assessment.

    :::image type="content" source="media/dma-assesssqlonprem/select-database1.png" alt-text="Screenshot of Add sources and start assessment." lightbox="media/dma-assesssqlonprem/select-database1.png":::

> [!NOTE]  
> You can run multiple assessments concurrently and view the state of the assessments by opening the **All Assessments** page.

## View results

The duration of the assessment depends on the number of databases added and the schema size of each database. Results are displayed for each database as soon as they're available.

1. Select the database that has completed the assessment, and then switch between **Compatibility issues** and **Feature recommendations** by using the switcher.

1. Review the compatibility issues across all compatibility levels supported by the target SQL Server version that you selected on the **Options** page.

You can review compatibility issues by analyzing the affected object, its details, and potentially a fix for every issue identified under **Breaking changes**, **Behavior changes**, and **Deprecated features**.

:::image type="content" source="media/dma-assesssqlonprem/review-results.png" alt-text="Screenshot of View assessment results." lightbox="media/dma-assesssqlonprem/review-results.png":::

Similarly, you can review feature recommendation across **Performance**, **Storage**, and **Security** areas.

Feature recommendations cover different kinds of features such as In-Memory OLTP, columnstore, Always Encrypted, Dynamic Data Masking, and Transparent Data Encryption.

:::image type="content" source="media/dma-assesssqlonprem/feature-recommendations.png" alt-text="Screenshot of View feature recommendations." lightbox="media/dma-assesssqlonprem/feature-recommendations.png":::

For Azure SQL Database, the assessments provide migration blocking issues and feature parity issues. Review the results for both categories by selecting the specific options.

- The **SQL Server feature parity** category provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps. It helps you plan this effort in your migration projects.

  :::image type="content" source="media/dma-assesssqlonprem/sql-feature-parity.png" alt-text="Screenshot of View information for SQL Server feature parity." lightbox="media/dma-assesssqlonprem/sql-feature-parity.png":::

- The **Compatibility issues** category provides partially supported or unsupported features that block migrating on-premises SQL Server databases to Azure SQL databases. It then provides recommendations to help you address those issues.

  :::image type="content" source="media/dma-assesssqlonprem/compatibility-issues.png" alt-text="Screenshot of View compatibility issues." lightbox="media/dma-assesssqlonprem/compatibility-issues.png":::

## Assess a data estate for target readiness

If you want further extend these assessments to the entire data estate and find the relative readiness of SQL Server instances and databases for migration to Azure SQL Database, upload the results to the Azure Migrate hub by selecting **Upload to Azure Migrate**.

Doing so allows you to view the consolidated results on the Azure Migrate hub project.

Detailed, step-by-step guidance for target readiness assessments is available [here](dma-assess-sql-data-estate-to-sqldb.md).

   :::image type="content" source="media/dma-assesssqlonprem/upload-to-azure-migrate.png" alt-text="Screenshot of Upload results to Azure Migrate." lightbox="media/dma-assesssqlonprem/upload-to-azure-migrate.png":::

## Export results

After all databases finish the assessment, select **Export report** to export the results to either a JSON file or a CSV file. You can then analyze the data at your own convenience.

## Save and load assessments

In addition to exporting the results of an assessment, you can save assessment detail to a file and load an assessment file for later review. For more information, see the article [Save and load assessments with Data Migration Assistant](dma-save-load-assessments.md).
