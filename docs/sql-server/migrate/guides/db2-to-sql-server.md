---
title: "IBM Db2 to SQL Server: Migration guide"
description: 'This guide teaches you how to migrate your Db2 databases to Microsoft SQL Server by using SQL Server Migration Assistant (SSMA) for Db2.'
ms.custom:
  - intro-migration
ms.date: 10/05/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: migration-guide
ms.topic: how-to
author: MashaMSFT
ms.author: mathoma
---
# Migration guide: IBM Db2 to SQL Server
[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]

In this guide, you learn how to migrate your user databases from IBM Db2 to SQL Server by using SQL Server Migration Assistant (SSMA) for Db2. 

For other migration guides, see [Azure Database Migration Guides](/data-migration). 


## Prerequisites

Before you begin migrating your Db2 database to SQL Server, do the following:

- Verify that your [source environment is supported](../../../ssma/db2/installing-ssma-for-db2-client-db2tosql.md#prerequisites).
- Download and install [SSMA for Db2](https://www.microsoft.com/download/details.aspx?id=54254).
- Get connectivity and sufficient permissions to access both source and target. 

## Pre-migration

After you've met the prerequisites, you're ready to discover the topology of your environment and assess the feasibility of your migration. 

### Assess and convert 

Use SSMA for Db2 to review database objects and data, and assess your databases for migration. 

To create an assessment, do the following:

1. Open SSMA for Db2. 
1. Select **File**, and then select **New Project**. 
1. Provide a project name and location and then, in the drop-down list, select a SQL Server migration target. Select **OK**.

   :::image type="content" source="media/db2-to-sql-server/new-project.png" alt-text="Screenshot of the New Project pane in SSMA for Db2.":::


1. Select **Connect to Db2**, and then enter the Db2 connection details.

   :::image type="content" source="media/db2-to-sql-server/connect-to-db2.png" alt-text="Screenshot of the Connect to Db2 pane.":::


1. Right-click the Db2 schema you want to migrate, and then select **Create report** to generate an HTML report. Alternatively, you can select **Create report** at the upper right.

   :::image type="content" source="media/db2-to-sql-server/create-report.png" alt-text="Screenshot of the 'Create Report' links in Db2 Metadata Explorer.":::

1. Review the HTML report to understand the conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of Db2 objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects, as shown here:  

   `drive:\<username>\Documents\SSMAProjects\MyDb2Migration\report\report_<date>` 

   :::image type="content" source="media/db2-to-sql-server/report.png" alt-text="Screenshot of a conversion report in SSMA.":::


### Validate data types

Validate the default data-type mappings, and change them based on requirements, if necessary. To do so: 

1. Select **Tools**, and then select **Project Settings**.  
1. Select the **Type mapping** tab.

   :::image type="content" source="media/db2-to-sql-server/type-mapping.png" alt-text="Screenshot of the 'Type Mapping' pane in SSMA for Db2.":::

1. You can change the type mapping for each table by selecting the table name on the **Db2 Metadata explorer** pane. 

### Convert schema 

To convert the schema, do the following:

1. (Optional) To convert dynamic or specialized queries, right-click the node, and then select **Add statement**. 

1. Select the **Connect to SQL Server** tab, and then enter the connection details for your SQL Server instance. 

    a. In the **Database** drop-down list, select your target database, or provide a new name to create a database on the target server.   
    b. Provide authentication details.   
    c. Select **Connect**.

   :::image type="content" source="media/db2-to-sql-server/connect-to-sql-server.png" alt-text="Screenshot of the Connect to SQL Server pane in SSMA for Db2.":::

1. Right-click the schema you're working with, and then select **Convert Schema**. Alternatively, you can select the **Convert Schema** tab at the upper right.

   :::image type="content" source="media/db2-to-sql-server/convert-schema.png" alt-text="Screenshot of the 'Convert Schema' command on the 'Db2 Metadata Explorer' pane.":::

1. After the conversion is completed, compare the converted structure to the original structure to identify potential problems, and address them based on the recommendations.

   :::image type="content" source="media/db2-to-sql-server/compare-review-schema-structure.png" alt-text="Screenshot showing a comparison of the converted objects to the original objects.":::

1. On the output pane, select the **Review results** icon, and then review any errors on the **Error list** pane. 
1. For an offline schema remediation exercise, save the project locally by selecting **File** > **Save Project**. Doing so gives you an opportunity to evaluate the source and target schemas offline and remediate them before you publish the schema to your SQL Server instance.



## Migrate

After you've completed assessing your databases and addressing any discrepancies, the next step is to run the migration process.

To publish your schema and migrate your data, do the following:

1. Publish the schema. On the **SQL Server Metadata Explorer** pane, right-click the database, and then select **Synchronize with Database**.

   :::image type="content" source="media/db2-to-sql-server/synchronize-with-database.png" alt-text="Screenshot of the 'Synchronize with Database' command on the SQL Server Metadata Explorer pane.":::

1. Migrate the data. On the **Db2 Metadata Explorer** pane, right-click the schema or object you want to migrate, and then select **Migrate Data**. Alternatively, you can select the **Migrate Data** tab at the upper right.
 
   To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand **Tables**, and then select the check box next to the table. To omit data from individual tables, clear the check box.

   :::image type="content" source="media/db2-to-sql-server/migrate-data.png" alt-text="Screenshot of the Migrate Data links.":::

1. Provide connection details for both the Db2 and SQL Server instances. 
1. After the migration is completed, view the **Data Migration Report**.  

   :::image type="content" source="media/db2-to-sql-server/data-migration-report.png" alt-text="Screenshot of the Data Migration Report.":::

1. Connect to your SQL Server instance by using [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md), and then validate the migration by reviewing the data and schema. 

   :::image type="content" source="media/db2-to-sql-server/compare-schema-in-ssms.png" alt-text="Screenshot of SQL Server Management Server.":::

## Post-migration 

After you've successfully completed the *migration* stage, you need to complete a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications 

After you've migrated the data to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this will in some cases require changes to the applications.

### Perform tests

The test approach to database migration consists of the following activities:

1. **Develop validation tests**: To test the database migration, you need to use SQL queries. You must create the validation queries to run against both the source and target databases. Your validation queries should cover the scope you've defined.

1. **Set up a test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.

1. **Run validation tests**: Run validation tests against the source and the target, and then analyze the results.

1. **Run performance tests**: Run performance tests against the source and the target, and then analyze and compare the results.


## Migration assets 

For more assistance with completing this migration scenario, see the following resources. They were developed in support of a real-world migration project engagement.

| Title | Description |
| --- | --- |
| [Data workload assessment model and tool](https://www.microsoft.com/download/details.aspx?id=103130) | Provides suggested "best fit" target platforms, cloud readiness, and application/database remediation levels for specified workloads. It offers simple, one-click calculation and report generation that helps to accelerate large estate assessments by providing an automated, uniform target-platform decision process. |
| [IBM Db2 zOS data assets discovery and assessment package](https://www.microsoft.com/download/details.aspx?id=103108) | After running the SQL script on a database, you can export the results to a file on the file system. Several file formats are supported, including CSV, so that you can capture the results in external tools, such as spreadsheets. This method can be useful if you want to easily share results with teams that do not have the workbench installed.|
| [IBM Db2 LUW inventory scripts and artifacts](https://www.microsoft.com/download/details.aspx?id=103109) | Includes a SQL query that hits IBM Db2 LUW version 11.1 system tables and provides a count of objects by schema and object type, a rough estimate of "Raw Data" in each schema, and the sizing of tables in each schema, with results stored in CSV format. |
|[IBM Db2 to SQL Server - Database Compare utility](https://www.microsoft.com/download/details.aspx?id=103016)|The Database Compare utility is a Windows console application that you can use to verify that the data is identical both on source and target platforms. You can use the tool to efficiently compare data down to the row or column level in all or selected tables, rows, and columns.|

The Data SQL Engineering team developed these resources. This team's core charter is to unblock and accelerate complex modernization for data platform migration projects to Microsoft's Azure data platform.

## Next steps

After the migration, review the [Post-migration validation and optimization guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md). 

For a matrix of Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios and specialty tasks, see [Services and tools for data migration](/azure/dms/dms-tools-matrix).

For other migration guides, see [Azure Database Migration Guide](https://datamigration.microsoft.com/). 

For migration videos, see [Overview of the migration journey](https://azure.microsoft.com/resources/videos/overview-of-migration-and-recommended-tools-services/).
