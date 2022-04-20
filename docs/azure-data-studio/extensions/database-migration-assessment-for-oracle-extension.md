---
title: Database Migration Assessment for Oracle extension 
description: Learn how to install the Azure Data Studio Database Migration Assessment for Oracle extension. This extension helps guide you choose the best Azure SQL database migration path. 
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: markingmyname
ms.author: maghan
ms.reviewer: niball
ms.topic: conceptual
ms.custom:
ms.date: 04/25/2022
---

# Database Migration Assessment for Oracle extension (Preview)

The Database Migration Assessment for Oracle extension in Azure Data Studio helps you assess an Oracle workload for migrating to SQL. The extension identifies an appropriate Azure SQL target with right-sizing recommendations, and how complex the migration can be.

## Pre-requisites

- An [Oracle database](https://www.oracle.com/database/technologies/oracle-database-software-downloads.html) running version 10 g or higher.
- [Azure Data Studio installed](../download-azure-data-studio.md).
- [Extension for Oracle installed](extension-for-oracle.md)

## Install Azure Data Studio extension

Follow the steps below to install the Database Migration Assessment for Oracle extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the View menu.

2. Type *Oracle* in the search bar.

3. Select the **Database Migration Assessment for Oracle** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/install-database-migration-assessment-for-oracle-extension.png" alt-text="install extension":::

## Run Assessment

Once the assessment extension installs, the next step is to connect to Oracle your database, collect metadata information from your Oracle instance and generate an assessment report.

1. Go to the connections icon in the menu bar.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-menu-bar.png" alt-text="connections menu bar":::

2. In the **Connection Details**, fill out the fields.
    1. In **Connection type** field, select **Oracle**.
    2. In the **Data Source** field, type in your Oracle server name and instance.
        1. You can provide the TNS name as well.
    3. In the **User Id** field, provide the database username.
    4. In the **Password** field, provide the database password.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-details.png" alt-text="connection details":::

3. Select **Connect**.

4. Now, a new connection appears in the connection details menu.

5. Right-click on the Oracle connection and select **Manage**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-manage-database-connection.png" alt-text="mangae database connection":::

6. Select **Oracle Assessment**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-select-oracle-assessment.png" alt-text="manage database connection":::

7. Select **Run new Assessment**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-run-new-oracle-assessment.png" alt-text="run new assessment":::

8. Provide your assessment details.
    1. In the **Assessment name** field, enter a title.
        1. For example, *demo1*.
    1. Enter your **Assessment setting**.
        1. In the **Target Platform** field, enter the destination migration database.
            1. For example, **SQL**.
        1. In the **Scale factor** field, enter the multiplier value.
        1. In the **Percentile utilization** field, enter the percentile value for sizing the Azure target.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-assessment-details.png" alt-text="assessment details":::

    The scale factor multiplier that is applied to the performance metrics utilization ( CPU, Memory, IOPS, Storage)  The multiple value is to factor scenario  issues such as seasonal usage, short performance history, partial workload migration etc. 
    If the recommended SKU needs to factor additional load , the multiplier should be greater than 1. Example: Burst Load, Seasonal usage , future capacity planning etc. The other scenario , when you move only partial Oracle to workload , then the multiplier should be less than 1.
 The percentile value of the performance sample set to be considered for sizing the Azure target.

9. Select **Run new Assessment**.
    1. Now, you see the new Assessment in the last five sections.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-demo.png" alt-text="name the assessment demo1":::

10. You can select the assessment link, and it takes you to the assessment details page. You can get the latest assessment status.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-latest-assessment-status.png" alt-text="latest assessment status":::

11. Select refresh until the Assessment completes. The status updates to show you 1 of 3 statuses - **success**, **failed** or **in-progress**.

## View Assessment

Once the Assessment is complete, a consolidated output is generated for each Azure SQL target. Currently, these targets include **SQL Server on Azure Virtual Machines**, **Azure SQL Database**, and **Azure SQL Managed Instance**.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-view-assessment.png" alt-text="view assessment":::

Each card has three sections. It shows the overall feasibility of the migration to the SQL target. It also drills down on various workloads under various migration complexity categories. There's a feature compatibility score that provides the feature assessment review result. SKU recommendation provides the proper sizing of the target.

When you Select on view detail report, it first shows the summary of the Assessment.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-card-sections.png" alt-text="card sections":::

The following database details tab provides the breakup per schema basis. It shows how many schemas are discovered, how complex it can be to migrate, and the estimated time to migrate your database in hours.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-per-schema-basis.png" alt-text="per schema basis":::

The SKU recommendation provides the suitable Azure SQL target and the reasoning and justification of the Azure SQL target.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-skus.png" alt-text="Sku recommendations":::

>[!Note]
> If automatic workload repository (AWR) feature is enabled, then the SKU recommender will use the DBA_HIST_ views to gather the performance metrics metadata. Otherwise, the recommender will use server configuration and other system views information for sizing the Azure SQL target.
 
The feature assessment provides the Oracle to Azure SQL mapped assessment and migration effort to identify the suitability.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-mapped-assessment.png" alt-text="Mapped assessment":::

You can also download the report for offline viewing by selecting download combined reports or download individual reports.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-downlaod-report.png" alt-text="downlaod assessment report":::

## Troubleshoot

You can troubleshoot this extension with the migration tool.

## Next steps

- [Azure SQL Migration extension](azure-sql-migration-extension.md)
- [Oracle extension](extension-for-oracle.md)
- [Add extensions](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md)
