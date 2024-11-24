---
title: "Oracle to Azure SQL Database: Migration Guide"
description: In this guide, you learn how to migrate your Oracle schema to Azure SQL Database by using SQL Server Migration Assistant for Oracle.
author: nilabjaball
ms.author: niball
ms.reviewer: mathoma, randolphwest
ms.date: 11/19/2024
ms.service: azure-sql-database
ms.subservice: migration-guide
ms.topic: how-to
ms.collection:
  - sql-migration-content
---

# Migration guide: Oracle to Azure SQL Database

[!INCLUDE [appliesto-sqldb-sqlmi](../../includes/appliesto-sqldb.md)]

This guide teaches you [to migrate](https://azure.microsoft.com/migration/migration-journey) your Oracle schemas to Azure SQL Database by using SQL Server Migration Assistant (SSMA) for Oracle.

For other migration guides, see [Azure Database Migration Guides](/data-migration).

> [!IMPORTANT]  
> Try the [Database Migration Assessment for Oracle extension](/azure-data-studio/extensions/database-migration-assessment-for-oracle-extension) > for Oracle to Azure SQL pre-assessment and workload categorization. This extension is useful:
>
> - if you're in the early phase of Oracle to Azure SQL migration and need to do a high level workload assessment
> - if you're interested in sizing your Azure SQL target for the Oracle workload
> - if you want to understand feature migration parity

For detailed code assessment and conversion, continue with SSMA for Oracle.

## Prerequisites

Before you begin migrating your Oracle schema to SQL Database:

- Verify that your source environment is supported.

- Download [SSMA for Oracle](https://www.microsoft.com/download/details.aspx?id=54258).

- Have a target [Quickstart: Create a single database - Azure SQL Database](../../database/single-database-create-quickstart.md) instance.

- Obtain the [necessary permissions for SSMA for Oracle](/sql/ssma/oracle/connecting-to-oracle-database-oracletosql) and [provider](/sql/ssma/oracle/connect-to-oracle-oracletosql).

## Pre-migration

After you meet the prerequisites, you're ready to discover the topology of your environment and assess the feasibility of your [Azure cloud migration](https://azure.microsoft.com/migration). This part of the process involves conducting an inventory of the databases that you need to migrate, assessing those databases for potential migration issues or blockers, and then resolving any items you might have uncovered.

### Assess

By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to SQL Database, and then finally migrate data to the database.

To create an assessment:

1. Open [SSMA for Oracle](https://www.microsoft.com/download/details.aspx?id=54258).
1. Select **File**, and then select **New Project**.
1. Enter a project name and a location to save your project. Then select **Azure SQL Database** as the migration target from the dropdown list and select **OK**.

   :::image type="content" source="media/oracle-to-sql-database-guide/connect-to-oracle.png" alt-text="Screenshot that shows Connect to Oracle.":::

1. Select **Connect to Oracle**. Enter values for Oracle connection details in the **Connect to Oracle** dialog box.

1. Select the Oracle schemas you want to migrate.

   :::image type="content" source="media/oracle-to-sql-database-guide/select-schema.png" alt-text="Screenshot that shows selecting Oracle schema.":::

1. In **Oracle Metadata Explorer**, right-click the Oracle schema you want to migrate and then select **Create Report** to generate an HTML report. Instead, you can select a database and then select the **Create Report** tab.

   :::image type="content" source="media/oracle-to-sql-database-guide/create-report.png" alt-text="Screenshot that shows Create Report.":::

1. Review the HTML report to understand conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of Oracle objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects.

   For example, see `drive:\<username>\Documents\SSMAProjects\MyOracleMigration\report\report_2020_11_12T02_47_55\`.

   :::image type="content" source="media/oracle-to-sql-database-guide/assessment-report.png" alt-text="Screenshot that shows an Assessment report." lightbox="media/oracle-to-sql-database-guide/assessment-report.png":::

### Validate the data types

Validate the default data type mappings and change them based on requirements if necessary. To do so, follow these steps:

1. In SSMA for Oracle, select **Tools**, and then select **Project Settings**.
1. Select the **Type Mapping** tab.

   :::image type="content" source="media/oracle-to-sql-database-guide/type-mappings.png" alt-text="Screenshot that shows Type Mapping." lightbox="media/oracle-to-sql-database-guide/type-mappings.png":::

1. You can change the type mapping for each table by selecting the table in **Oracle Metadata Explorer**.

### Convert the schema

To convert the schema:

1. (Optional) Add dynamic or ad hoc queries to statements. Right-click the node, and then select **Add statements**.

1. Select the **Connect to Azure SQL Database** tab.

   1. In **SQL Database**, enter connection details to connect your database.

   1. Select your target SQL Database instance from the dropdown list, or enter a new name, in which case a database will be created on the target server.

   1. Enter authentication details, and select **Connect**.

   :::image type="content" source="media/oracle-to-sql-database-guide/connect-to-sql-database.png" alt-text="Screenshot that shows Connect to Azure SQL Database.":::

1. In **Oracle Metadata Explorer**, right-click the Oracle schema and then select **Convert Schema**. Or, you can select your schema and then select the **Convert Schema** tab.

   :::image type="content" source="media/oracle-to-sql-database-guide/convert-schema.png" alt-text="Screenshot that shows Convert Schema.":::

1. After the conversion finishes, compare and review the converted objects to the original objects to identify potential problems and address them based on the recommendations.

   :::image type="content" source="media/oracle-to-sql-database-guide/table-mapping.png" alt-text="Screenshot that shows the Review recommendations schema." lightbox="media/oracle-to-sql-database-guide/table-mapping.png":::

1. Compare the converted Transact-SQL text to the original stored procedures, and review the recommendations.

   :::image type="content" source="media/oracle-to-sql-database-guide/procedure-comparison.png" alt-text="Screenshot that shows the Review recommendations." lightbox="media/oracle-to-sql-database-guide/procedure-comparison.png":::

1. In the output pane, select **Review results** and review the errors in the **Error List** pane.
1. Save the project locally for an offline schema remediation exercise. On the **File** menu, select **Save Project**. This step gives you an opportunity to evaluate the source and target schemas offline and perform remediation before you publish the schema to SQL Database.

## Migrate

After you assess your databases and address any discrepancies, the next step is to run the migration process. Migration involves two steps: publishing the schema and migrating the data.

To publish your schema and migrate your data:

1. Publish the schema by right-clicking the database from the **Databases** node in **Azure SQL Database Metadata Explorer** and selecting **Synchronize with Database**.

   :::image type="content" source="media/oracle-to-sql-database-guide/synchronize-with-database.png" alt-text="Screenshot that shows Synchronize with Database.":::

1. Review the mapping between your source project and your target.

   :::image type="content" source="media/oracle-to-sql-database-guide/synchronize-with-database-review.png" alt-text="Screenshot that shows Synchronize with the Database review.":::

1. Migrate the data by right-clicking the database or object you want to migrate in **Oracle Metadata Explorer** and selecting **Migrate Data**. Or, you can select the **Migrate Data** tab. To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand **Tables**, and then select the checkboxes next to the tables. To omit data from individual tables, clear the checkboxes.

   :::image type="content" source="media/oracle-to-sql-database-guide/migrate-data.png" alt-text="Screenshot that shows Migrate Data.":::

1. Enter connection details for both Oracle and SQL Database.
1. After the migration is completed, view the **Data Migration Report**.

   :::image type="content" source="media/oracle-to-sql-database-guide/data-migration-report.png" alt-text="Screenshot that shows the Data Migration Report." lightbox="media/oracle-to-sql-database-guide/data-migration-report.png":::

1. Connect to your SQL Database instance by using [Download SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms), and validate the migration by reviewing the data and schema.

   :::image type="content" source="media/oracle-to-sql-database-guide/validate-data.png" alt-text="Screenshot that shows validation in SQL Server Management Studio.":::

Or, you can also use SQL Server Integration Services to perform the migration. To learn more, see:

- [Getting started with SQL Server Integration Services](/sql/integration-services/sql-server-integration-services)
- [SQL Server Integration Services for Azure and Hybrid Data Movement](https://download.microsoft.com/download/D/2/0/D20E1C5F-72EA-4505-9F26-FEF9550EFD44/SSIS%20Hybrid%20and%20Azure.docx)

## Post-migration

After you successfully complete the *migration* stage, you need to complete a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this task requires changes to the applications in some cases.

The [Data Access Migration Toolkit](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit) is an extension for Visual Studio Code that allows you to analyze your Java source code and detect data access API calls and queries. The toolkit provides you with a single-pane view of what needs to be addressed to support the new database back end. To learn more, see the [Migrate your Java applications from Oracle](https://techcommunity.microsoft.com/blog/microsoftdatamigration/migrate-your-java-applications-from-oracle-to-sql-server-with-the-help-of-the-da/368727) blog post.

### Perform tests

The test approach to database migration consists of the following activities:

1. **Develop validation tests**: To test the database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you define.

1. **Set up a test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.

1. **Run validation tests**: Run validation tests against the source and the target, and then analyze the results.

1. **Run performance tests**: Run performance tests against the source and the target, and then analyze and compare the results.

### Validate migrated objects

Microsoft SQL Server Migration Assistant for Oracle Tester (SSMA Tester) allows you to test migrated database objects. The SSMA Tester is used to verify that converted objects behave in the same way.

#### Create test case

1. In SSMA for Oracle, navigate to **Tester** > **New Test Case**.

   :::image type="content" source="media/oracle-to-sql-database-guide/ssma-tester-new.png" alt-text="Screenshot that shows to create new test case.":::

1. Provide the following information for the new test case:

   **Name:** Enter the name to identify the test case.

   **Creation date:** Today's current date, defined automatically.

   **Last Modified date:** This value is filled in automatically, and shouldn't be changed.

   **Description:** Enter any additional information to identify the purpose of the test case.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-init-test-case.png" alt-text="Screenshot that shows steps to initialize a test case.":::

1. Select the objects that are part of the test case from the Oracle object tree located in the left side.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-select-configure-objects.png" alt-text="Screenshot that shows step to select and configure object.":::

   In this example, the stored procedure `ADD_REGION`, and table `REGION`, are selected.

   To learn more, see [Selecting and Configuring Objects to Test (OracleToSQL)](/sql/ssma/oracle/selecting-and-configuring-objects-to-test-oracletosql)

1. Next, select the tables, foreign keys, and other dependent objects from the Oracle object tree in the left window.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-select-configure-affected.png" alt-text="Screenshot that shows step to select and configure affected object.":::

   To learn more, see [Selecting and Configuring Affected Objects (OracleToSQL)](/sql/ssma/oracle/selecting-and-configuring-affected-objects-oracletosql)

1. Review the evaluation sequence of objects. Change the order by selecting the buttons in the grid.

   :::image type="content" source="media/oracle-to-sql-database-guide/test-call-ordering.png" alt-text="Screenshot that shows step to sequence test object execution.":::

1. Finalize the test case by reviewing the information provided in the previous steps. Configure the test execution options based on the test scenario.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-finalize-case.png" alt-text="Screenshot that shows step to finalize object.":::

   For more information on test case settings,[Finishing Test Case Preparation (OracleToSQL)](/sql/ssma/oracle/finishing-test-case-preparation-oracletosql)

1. Select finish to create the test case.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-test-repo.png" alt-text="Screenshot that shows step to test repo.":::

#### Run test case

When SSMA Tester runs a test case, the test engine executes the objects selected for testing and generates a verification report.

1. Select the test case from test repository and then select run.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-repo-run.png" alt-text="Screenshot that shows to review test repo.":::

1. Review the launch test case and select run.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-run-test-case.png" alt-text="Screenshot that shows step to run test case.":::

1. Next, provide Oracle source credentials. Select connect after entering the credentials.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-oracle-connect.png" alt-text="Screenshot that shows step to connect to oracle source.":::

1. Provide target SQL Server credentials and select connect.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-sql-connect.png" alt-text="Screenshot that shows step to connect to sql target.":::

   On success, the test case moves to initialization stage.

1. A real-time progress bar shows the execution status of the test run.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-run-status.png" alt-text="Screenshot that shows tester test progress.":::

1. Review the report after the test is completed. The report provides the statistics, any errors during the test run and a detail report.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-test-result.png" alt-text="Screenshot that shows a sample tester test report.":::

1. Select details to get more information.

   Example of positive data validation.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-test-success.png" alt-text="Screenshot that shows a sample tester success report.":::

   Example of failed data validation.

   :::image type="content" source="media/oracle-to-sql-database-guide/tester-test-failed.png" alt-text="Screenshot that shows tester failure report.":::

### Optimize

The post-migration phase is crucial for reconciling any data accuracy issues, verifying completeness, and addressing performance issues with the workload.

For more information about these issues and the steps to mitigate them, see the [Post-migration validation and optimization guide](/sql/relational-databases/post-migration-validation-and-optimization-guide).

## Migration assets

For more assistance with completing this migration scenario, see the following resources. They were developed in support of a real-world migration project engagement.

- [Data Workload Assessment Model and Tool](https://www.microsoft.com/download/details.aspx?id=103130)

  This tool provides suggested "best fit" target platforms, cloud readiness, and application or database remediation level for a given workload. It offers easy calculation and report generation, which helps to accelerate large estate assessments by providing an automated and uniform target platform decision process.

- [Oracle Inventory Script Artifacts](https://www.microsoft.com/download/details.aspx?id=103121)

  This asset includes a PL/SQL query that hits Oracle system tables and provides a count of objects by schema type, object type, and status. It also provides a rough estimate of raw data in each schema and the sizing of tables in each schema, with results stored in a CSV format.

- [Automate SSMA Oracle Assessment Collection & Consolidation](https://www.microsoft.com/download/details.aspx?id=103120)

  This set of resources uses a .csv file as entry (sources.csv in the project folders) to produce the XML files that are needed to run an SSMA assessment in console mode. The source.csv is provided by the customer based on an inventory of existing Oracle instances. The output files are `AssessmentReportGeneration_source_1.xml`, `ServersConnectionFile.xml`, and `VariableValueFile.xml`.

- [Oracle to SQL DB - Database Compare utility](https://www.microsoft.com/download/details.aspx?id=103016)

  SSMA for Oracle Tester is the recommended tool to automatically validate the database object conversion and data migration, and it's a superset of Database Compare functionality.

  If you're looking for an alternative data validation option, you can use the Database Compare utility to compare data down to the level of the row or column, in all or selected tables, rows, and columns.

The Data SQL Engineering team developed these resources. This team's core charter is to unblock and accelerate complex modernization for data platform migration projects to Microsoft's Azure data platform.

## Related content

- [Services and tools for data migration](/azure/dms/dms-tools-matrix)
- [What is Azure SQL Database?](../../database/sql-database-paas-overview.md)
- [Azure Total Cost of Ownership (TCO) Calculator](https://azure.microsoft.com/pricing/tco/calculator/)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads for migration to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Cloud Migration Resources](https://azure.microsoft.com/migration/resources)
- [Overview of the migration journey and the tools and services recommended for performing assessment and migration](https://azure.microsoft.com/resources/videos/overview-of-migration-and-recommended-tools-services/)
