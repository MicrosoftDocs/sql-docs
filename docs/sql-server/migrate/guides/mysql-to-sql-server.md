---
title: "MySQL to SQL Server: Migration guide"
description: 'This guide teaches you how to migrate your MySQL databases to Microsoft SQL Server by using SQL Server Migration Assistant for MySQL (SSMA for MySQL).'
ms.service: sql
ms.reviewer: ""
ms.subservice: migration-guide
ms.topic: how-to
author: MashaMSFT
ms.author: mathoma
ms.date: 10/05/2021
ms.custom:
  - intro-migration
---

# Migration guide: MySQL to SQL Server

[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]

In this guide, you learn how to migrate your MySQL databases to SQL Server.

For other migration guides, see [Azure Database Migration Guides](/data-migration). 

## Prerequisites

Before you begin migrating your MySQL database to SQL Server:

- Verify that your source environment is supported. Currently, MySQL 5.6 and 5.7 are supported.
- Get [SQL Server Migration Assistant for MySQL (SSMA for MySQL)](https://www.microsoft.com/download/details.aspx?id=54257).
- Get connectivity and sufficient permissions to access both the source and target.

## Pre-migration

After you've met the prerequisites, you're ready to discover your source MySQL environment and assess the feasibility of your migration.

### Assess

By using SSMA for MySQL, you can review database objects and data and assess databases for migration.

To create an assessment:

1. Open SSMA for MySQL.
1. On the **File** menu, select **New Project**.
1. Enter the project name and a location to save your project and the migration target. Then select **SQL Server** in the **Migrate To** option.

   ![Screenshot that shows New Project.](./media/mysql-to-sql-server/new-project.png)

1. In the **Connect to MySQL** dialog box, enter connection details, and then connect to your MySQL server.

   ![Screenshot that shows Connect to MySQL.](./media/mysql-to-sql-server/connect-to-mysql.png)

1. Select the MySQL databases you want to migrate.

   ![Screenshot that shows selecting the MySQL database you want to migrate.](./media/mysql-to-sql-server/select-database.png)

1. Right-click the MySQL database in **MySQL Metadata Explorer**, and select **Create Report**. Alternatively, you can select the **Create Report** tab in the upper-right corner.

   ![Screenshot that shows Create Report.](./media/mysql-to-sql-server/create-report.png)

1. Review the HTML report to understand conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of MySQL objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects, as shown here:

    `drive:\Users\<username>\Documents\SSMAProjects\MySQLMigration\report\report_2016_11_12T02_47_55\`.
   
   ![Screenshot that shows a conversion report.](./media/mysql-to-sql-server/conversion-report.png)

### Validate the type mappings

Validate the default data type mappings and change them based on requirements, if necessary. To do so:

1. On the **Tools** menu, select **Project Settings**.
1. Select the **Type Mapping** tab.

   ![Screenshot that shows Type Mapping.](./media/mysql-to-sql-server/type-mappings.png)

1. You can change the type mapping for each table by selecting the table in **MySQL Metadata Explorer**.

To learn more about conversion settings in SSMA for MySQL, see [Project Settings](../../../ssma/mysql/project-settings-conversion-mysqltosql.md).

### Convert the schema

Converting database objects takes the object definitions from MySQL, converts them to similar SQL Server objects, and then loads this information into the SSMA for MySQL metadata. It doesn't load the information into the instance of SQL Server. You can then view the objects and their properties by using SQL Server Metadata Explorer.

During the conversion, SSMA for MySQL prints output messages to the output pane and error messages to the **Error List** pane. Use the output and error information to determine whether you have to modify your MySQL databases or your conversion process to obtain the desired conversion results.

To convert the schema:

1. (Optional) To convert dynamic or ad-hoc queries, right-click the node and select **Add Statement**.
1. Select the **Connect to SQL Server** tab.
     1. Enter connection details for your SQL Server instance.
     1. Select your target database from the drop-down list, or enter a new name, in which case a database will be created on the target server.
     1. Enter authentication details, and then select **Connect**.

   ![Screenshot that shows Connect to SQL Server.](./media/mysql-to-sql-server/connect-to-sql-server.png)

1. Right-click the MySQL database in **MySQL Metadata Explorer**, and then select **Convert Schema**. Alternatively, you can select the **Convert Schema** tab in upper-right corner.

   ![Screenshot that shows Convert Schema.](./media/mysql-to-sql-server/convert-schema.png)

1. After the conversion finishes, compare and review the converted objects to the original objects to identify potential problems and address them based on the recommendations.

   ![Screenshot that shows comparing and reviewing objects.](./media/mysql-to-sql-server/table-comparison.png)

1. Compare the converted Transact-SQL text to the original code, and review the recommendations.

   ![Screenshot that shows comparing and reviewing converted code.](./media/mysql-to-sql-server/procedure-comparison.png)
   
1. In the output pane, select **Review results** and review the errors in the **Error List** pane.
1. Save the project locally for an offline schema remediation exercise. On the **File** menu, select **Save Project**. This step gives you an opportunity to evaluate the source and target schemas offline and perform remediation before you publish the schema to SQL Server.

To learn more, see [Screenshot that shows converting MySQL databases.](../../../ssma/mysql/converting-mysql-databases-mysqltosql.md).

## Migration

After you have the necessary prerequisites in place and have completed the tasks associated with the *pre-migration* stage, you're ready to perform the schema and data migration.

You have two options for migrating data:

- **Client-side data migration**
	 -   To perform client-side data migration, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.

    > [!NOTE]
    > When SQL Express edition is used as the target database, only client-side data migration is allowed and server-side data migration isn't supported.

- **Server-side data migration**
	-   Before you perform data migration on the server side, ensure that:
	    - The SSMA for MySQL Extension Pack is installed on the instance of SQL Server.
	    - The SQL Server Agent service is running on the instance of SQL Server.
	-   To perform server-side data migration, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.

> [!IMPORTANT]  
> If you plan to use the Server Side Data Migration Engine, before you migrate data, you must install the SSMA for MySQL Extension Pack and the MySQL providers on the computer that's running SSMA for MySQL. The SQL Server Agent service must also be running. For more information about how to install the extension pack, see [Installing SQL Server Migration Assistant Components on SQL Server (MySQL to SQL)](../../../ssma/mysql/installing-ssma-components-on-sql-server-mysqltosql.md).

To publish your schema and migrate the data:

1. Publish the schema by right-clicking the database in **SQL Server Metadata Explorer** and selecting **Synchronize with Database**. This action publishes the MySQL database to the SQL Server instance.

   ![Screenshot that shows Synchronize with Database.](./media/mysql-to-sql-server/synchronize-database.png)

1. Review the mapping between your source project and your target.

   ![Screenshot that shows reviewing the synchronization with the database.](./media/mysql-to-sql-server/synchronize-database-review.png)

1. Migrate the data by right-clicking the database or object you want to migrate in **MySQL Metadata Explorer** and selecting **Migrate Data**. Alternatively, you can select the **Migrate Data** tab. To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand **Tables**, and then select the check boxes next to the tables. To omit data from individual tables, clear the check boxes.

   ![Screenshot that shows Migrate Data.](./media/mysql-to-sql-server/migrate-data.png)

1. After migration is completed, view the **Data Migration Report**.

   ![Screenshot that shows the Data Migration Report.](./media/mysql-to-sql-server/migration-report.png)

1. Connect to your SQL Server instance by using [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md), and validate the migration by reviewing the data and schema.

   ![Screenshot that shows validation in SQL Server Management Studio.](./media/mysql-to-sql-server/validate-in-ssms.png)

## Post-migration

After you've successfully completed the *migration* stage, you need to complete a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications

After you've migrated the data to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this task will require changes to the applications in some cases.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you've defined.
1. **Set up a test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance tests against the source and the target, and then analyze and compare the results.

### Optimize

The post-migration phase is crucial for reconciling any data accuracy issues, verifying completeness, and addressing performance issues with the workload.

> [!Note]
> For more information about these issues and the steps to mitigate them, see the [Post-migration validation and optimization guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md).

## Migration assets

For more assistance with completing this migration scenario, see the following resource. It was developed in support of a real-world migration project engagement.

| Title                    | Description            |
| ----------------------------- | ---------------------- |
| [Data Workload Assessment Model and Tool](https://www.microsoft.com/download/details.aspx?id=103130) | This tool provides suggested "best fit" target platforms, cloud readiness, and application or database remediation level for a given workload. It offers simple, one-click calculation and report generation that helps to accelerate large estate assessments by providing an automated and uniform target platform decision process.                |
|[MySQL to SQL Server - Database Compare utility](https://www.microsoft.com/download/details.aspx?id=103016)|The Database Compare utility is a Windows console application that you can use to verify that the data is identical both on source and target platforms. You can use the tool to efficiently compare data down to the row or column level in all or selected tables, rows, and columns.|

The Data SQL Engineering team developed these resources. This team's core charter is to unblock and accelerate complex modernization for data platform migration projects to Microsoft's Azure data platform.


## Next steps

- To learn more about migrating MySQL databases to SQL Server, see [SQL Server Migration Assistant documentation for MySQL](../../../ssma/mysql/sql-server-migration-assistant-for-mysql-mysqltosql.md).
- For a matrix of Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios and specialty tasks, see [Services and tools for data migration](/azure/dms/dms-tools-matrix).
- For other migration guides, see [Azure Database Migration Guides](https://datamigration.microsoft.com/).
