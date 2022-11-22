---
title: "SAP ASE to SQL Server: Migration guide"
description: 'This guide teaches you how to migrate your SAP ASE databases to Microsoft SQL Server by using SQL Server Migration Assistant for SAP ASE (SSMA for SAP ASE).'
ms.service: sql
ms.reviewer: ""
ms.subservice: migration-guide
ms.custom:
  - intro-migration
ms.devlang: 
ms.topic: how-to
author: MashaMSFT
ms.author: mathoma
ms.date: 03/19/2021
---

# Migration guide: SAP ASE to SQL Server

[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]

In this guide, you learn how to migrate your SAP ASE databases to SQL Server by using SQL Server Migration Assistant for SAP ASE (SSMA for SAP ASE).

For other migration guides, see [Azure Database Migration Guides](/data-migration).

## Prerequisites

Before you begin migrating your SAP ASE database to SQL Server:

- Verify that your source environment is supported.
- Get [SQL Server Migration Assistant for SAP Adaptive Server Enterprise (formerly SAP Sybase ASE)](https://www.microsoft.com/download/details.aspx?id=54256).
- Get connectivity and sufficient permissions to access both the source and target.

## Pre-migration

After you've met the prerequisites, you're ready to discover the topology of your environment and assess the feasibility of your migration.

### Assess

By using SSMA for SAP ASE, you can review database objects and data, assess databases for migration, migrate Sybase database objects to SQL Server, and then migrate data to SQL Server. To learn more, see [SQL Server Migration Assistant for Sybase (SybaseToSQL)](../../../ssma/sybase/sql-server-migration-assistant-for-sybase-sybasetosql.md).

To create an assessment:

1. Open [SSMA for SAP ASE](https://www.microsoft.com/download/details.aspx?id=54256).
1. On the **File** menu, select **New Project**.
1. Enter a project name and a location to save your project. Then select **SQL Server** as the migration target from the drop-down list, and select **OK**.
1. In the **Connect to Sybase** dialog box, enter values for SAP connection details.
1. Right-click the SAP database you want to migrate, and then select **Create Report** to generate an HTML report.
1. Review the HTML report to understand conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of SAP ASE objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects, as shown here:

    `drive:\<username>\Documents\SSMAProjects\MySAPMigration\report\report_<date>`.

### Validate the type mappings

Before you perform a schema conversion, validate the default datatype mappings or change them based on requirements. You can go to the **Tools** menu and select **Project Settings**, or you can change type mapping for each table by selecting the table in **SAP ASE Metadata Explorer**.

### Convert the schema

To convert the schema:

1. (Optional) To convert dynamic or ad-hoc queries, right-click the node, and select **Add Statement**.
1. Select the **Connect to SQL Server** tab, and enter SQL Server details. You can choose to connect to an existing database or enter a new name, in which case a database will be created on the target server.
1. Right-click the database or object you want to migrate in **SAP ASE Metadata Explorer**, and select **Migrate Data**. Alternatively, you can select the **Migrate Data** tab. To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand **Tables**, and then select the check boxes next to the tables. To omit data from individual tables, clear the check boxes.
1. Compare and review the structure of the schema to identify potential problems.

   After the schema conversion finishes, you can save this project locally for an offline schema remediation exercise. On the **File** menu, select **Save Project**. This step gives you an opportunity to evaluate the source and target schemas offline and perform remediation before you publish the schema to SQL Server.

To learn more, see [Convert schema](../../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md).

## Migrate

After you have the necessary prerequisites in place and have completed the tasks associated with the *pre-migration* stage, you're ready to perform the schema and data migration.

To publish your schema and migrate the data:

1. Publish the schema by right-clicking the database in **SQL Server Metadata Explorer** and selecting **Synchronize with Database**. This action publishes the SAP ASE schema to the SQL Server instance.
1. Migrate the data by right-clicking the database or object you want to migrate in **SAP ASE Metadata Explorer** and selecting **Migrate Data**. Alternatively, you can select the **Migrate Data** tab. To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand **Tables**, and then select the check boxes next to the tables. To omit data from individual tables, clear the check boxes.
1. After the migration finishes, view the **Data Migration Report**.
1. Connect to your SQL Server instance by using [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md), and validate the migration by reviewing the data and schema.

## Post-migration

After you've successfully completed the *migration* stage, you need to complete a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications

After you migrate the data to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this task will require changes to the applications in some cases.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you've defined.
1. **Set up a test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance tests against the source and the target, and then analyze and compare the results.

### Optimize

The post-migration phase is crucial for reconciling any data accuracy issues, verifying completeness, and addressing performance issues with the workload.

> [!Note]
> For more information about these issues and the steps to mitigate them, see the [Post-migration validation and optimization guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md).

## Migration assets

For more assistance with completing this migration scenario, see the following resource. It was developed in support of a real-world migration project engagement.

| Title                                                                                                       | Description                                                                                                                                                                                                                                                                                                                                                                                                     |
| --------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Optimization Guide for Mainframe App/Data recompiled to .NET & SQL Server](https://aka.ms/dmj-wp-mainframe-optimize) | This guide offers optimization advice for executing point-lookups against SQL Server from .NET as efficiently as possible. Customers who want to migrate from mainframe databases to SQL Server might want to migrate existing mainframe-optimized design patterns, especially when they use third-party tools (such as Raincode Compiler) to automatically migrate mainframe code (such as COBOL/JCL) to T-SQL and C# .NET. |

> [!NOTE]
> The Data SQL Engineering team developed these resources. This team's core charter is to unblock and accelerate complex modernization for data platform migration projects to Microsoft's Azure data platform.

## Next steps

- For an overview of the Azure Database Migration Guide and the information it contains, see the video [How to Use the Database Migration Guide](https://azure.microsoft.com/resources/videos/how-to-use-the-azure-database-migration-guide/).
- For a matrix of Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios and specialty tasks, see [Services and tools for data migration](/azure/dms/dms-tools-matrix).
- For other migration guides, see [Azure Database Migration Guides](https://datamigration.microsoft.com/).
- For a migration video, see [Overview of the migration journey and the tools/services recommended for performing assessment and migration](https://azure.microsoft.com/resources/videos/overview-of-migration-and-recommended-tools-services/).
