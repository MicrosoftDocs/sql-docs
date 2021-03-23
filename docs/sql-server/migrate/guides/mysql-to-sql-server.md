---
title: "MySQL to SQL Server: Migration guide"
description: 'This guide teaches you to migrate your MySQL databases to Microsoft SQL Server by using SQL Server Migration for MySQL (SSMA for MySQL). '
ms.prod: sql
ms.reviewer: ""
ms.technology: migration-guide
ms.custom:
ms.devlang:
ms.topic: how-to
author: MashaMSFT
ms.author: mathoma
ms.date: 03/19/2021
---

# Migration guide: MySQL to SQL Server
[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]

This guide helps you migrate your MySQL databases to SQL Server. 

For other migration guides, see [Database Migration](https://docs.microsoft.com/data-migration). 

## Prerequisites 

To migrate your MySQL database to SQL Server, you need:

- To verify your source environment is supported. Currently, MySQL 5.6 and 5.7 is supported. 
- [SQL Server Migration Assistant for MySQL](https://www.microsoft.com/download/details.aspx?id=54257).
- Connectivity and proper permissions to access both source and target. 

## Pre-migration 

After you have met the prerequisites, you are ready to discover your source MySQL environment and assess the feasibility of your migration.

### Assess 

To use [SSMA for MySQL](https://www.microsoft.com/download/details.aspx?id=54257) to create an assessment, follow these steps: 

1. Open SQL Server Migration Assistant for MySQL. 
1. Select **File** and choose **New Project**. 
1. Provide the project name, a location to save your project and the migration target. Select **SQL Server** in the **Migrate to** option:

   ![New Project](./media/mysql-to-sql-server/new-project.png)

1. Provide connect details in the **Connect to MySQL** dialog box and connect to  your MySQL server:

   ![Connect to mysql](./media/mysql-to-sql-server/connect-to-mysql.png)

1. Right-click the MySQL schema in **MySQL Metadata Explorer** and choose **Create Report**. Alternatively, you can select **Create Report** from the top-line navigation bar:

   ![Create Report](./media/mysql-to-sql-server/create-report.png)

1. Review the HTML report to understand conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of MySQL objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects. 

   For example: `drive:\Users\<username>\Documents\SSMAProjects\MySQLMigration\report\report_2016_11_12T02_47_55\` 
   
   ![Conversion Report](./media/mysql-to-sql-server/conversion-report.png)

### Validate type mappings

Validate the default data type mappings and change them based on requirements if necessary. To do so, follow these steps: 

1. Select **Tools** from the menu. 
1. Select **Project Settings**. 
1. Select the **Type mappings** tab:

   ![Type Mappings](./media/mysql-to-sql-server/type-mappings.png)

1. You can change the type mapping for each table by selecting the table in the **MySQL Metadata explorer**. 


To learn more about conversion settings in SSMA, see [Project Settings](../../../ssma/mysql/project-settings-conversion-mysqltosql.md).

### Convert schema

Converting database objects takes the object definitions from MySQL, converts them to similar SQL Server objects, and then loads this information into the SSMA metadata. It does not load the information into the instance of SQL Server. You can then view the objects and their properties by using the SQL Server Metadata Explorer.

During the conversion, SSMA prints output messages to the Output pane and error messages to the Error List pane. Use the output and error information to determine whether you have to modify your MySQL databases or your conversion process to obtain the desired conversion results.

To convert the schema, follow these steps:

1. (Optional) To convert dynamic or ad-hoc queries, right-click the node, and choose **Add Statement**. 
1. Select **Connect to SQL Server** from the top-line navigation bar. 
     1. Enter connection details for your SQL Server instance. 
     1. Choose your target database from the drop-down, or provide a new name, in which case a database will be created on the target server. 
     1. Provide authentication details. 
     1. Select **Connect**. 

   ![Connect to SQL](./media/mysql-to-sql-server/connect-to-sql-server.png)

1. Right-click the MySQL schema in **MySQL Metadata Explorer** and choose **Convert schema**. Alternatively, you can select **Convert schema** from the top-line navigation bar:

   ![Convert Schema](./media/mysql-to-sql-server/convert-schema.png)

1. After the conversion completes, compare and review the converted objects to the original objects to identify potential problems and address them based on the recommendations:

   ![Compare and review object](./media/mysql-to-sql-server/table-comparison.png)

   Compare the converted Transact-SQL text to the original code and review the recommendations:

   ![Compare and review converted code](./media/mysql-to-sql-server/procedure-comparison.png)
   
1. Select **Review results** in the Output pane, and review errors in the **Error list** pane. 
1. Save the project locally for an offline schema remediation exercise. Select **Save Project** from the **File** menu. This gives you an opportunity to evaluate the source and target schemas offline and perform remediation before you can publish the schema to SQL Server.

To learn more, see [Converting MySQL databases](../../../ssma/mysql/converting-mysql-databases-mysqltosql.md).

## Migration 

After you have the necessary prerequisites in place and have completed the tasks associated with the **Pre-migration** stage, you are ready to perform the schema and data migration.

To migrate data, two cases arise:

- **Client Side Data Migration:**
	 -   For performing  **Client Side Data Migration**, select the  **Client Side Data Migration Engine**  option in the  **Project Settings**  dialog box.

    > [!NOTE]
    > When SQL Express edition is used as the target database, only client side data migration is allowed and server side data migration is not supported.

- **Server Side Data Migration:**
	-   Before performing data migration on the server side, ensure:
	    - The SSMA for MySQL Extension Pack is installed on the instance of SQL Server.
	    - The SQL Server Agent service is running on the instance of SQL Server. 
	-   For performing  **Server Side Data Migration**, select the  **Server Side Data Migration Engine**  option in the  **Project Settings**  dialog box.

> [!IMPORTANT]  
> If the engine being used is Server Side Data Migration Engine, then, before migrating data, you must install the SSMA for MySQL Extension Pack and the MySQL providers on the computer that is running SSMA. The SQL Server Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA Components on SQL Server (MySQL to SQL)](../../../ssma/mysql/installing-ssma-components-on-sql-server-mysqltosql.md)  

To publish your schema and migrate the data, follow these steps: 

1. Publish the schema: Right-click the database in **SQL Server Metadata Explorer** and choose **Synchronize with Database**.  This action publishes the MySQL schema to the SQL Server instance:

   ![Synchronize with Database](./media/mysql-to-sql-server/synchronize-database.png)

   Review the mapping between your source project and your target:

   ![Synchronize with Database - Review](./media/mysql-to-sql-server/synchronize-database-review.png)

1. Migrate the data: Right-click the database or object you want to migrate in **MySQL Metadata Explorer**, and choose **Migrate data**. Alternatively, you can select **Migrate Data** from the top-line navigation bar. To migrate data for an entire database, select the check box next to the database name. To migrate data from individual tables, expand the database, expand Tables, and then select the check box next to the table. To omit data from individual tables, clear the check box:

   ![Migrate Data](./media/mysql-to-sql-server/migrate-data.png)

1. After migration completes, view the **Data Migration Report**: 

   ![Data Migration Report](./media/mysql-to-sql-server/migration-report.png)

1. Connect to your SQL Server instance by using [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) and validate the migration by reviewing the data and schema: 

   ![Validate in SSMA](./media/mysql-to-sql-server/validate-in-ssms.png)



## Post-migration 

After you have successfully completed the **Migration** stage, you need to go through a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this will in some cases require changes to the applications.

### Perform tests

The test approach for database migration consists of performing the following activities:

1. **Develop validation tests**. To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you have defined.

2. **Set up test environment**. The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.

3. **Run validation tests**. Run the validation tests against the source and the target, and then analyze the results.

4. **Run performance tests**. Run performance test against the source and the target, and then analyze and compare the results.


### Optimize

The post-migration phase is crucial for reconciling any data accuracy issues and verifying completeness, as well as addressing performance issues with the workload.

> [!Note]
> For additional detail about these issues and specific steps to mitigate them, see the [Post-migration Validation and Optimization Guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md).

## Migration assets 

For additional assistance with completing this migration scenario, please see the following resources, which were developed in support of a real-world migration project engagement.

| Title/link                    | Description            |
| ----------------------------- | ---------------------- |
| [Data Workload Assessment Model and Tool](https://github.com/Microsoft/DataMigrationTeam/tree/master/Data%20Workload%20Assessment%20Model%20and%20Tool) | This tool provides suggested “best fit” target platforms, cloud readiness, and application/database remediation level for a given workload. It offers simple, one-click calculation and report generation that greatly helps to accelerate large estate assessments by providing and automated and uniform target platform decision process.                |

These resources were developed as part of the Data SQL Ninja Program, which is sponsored by the Azure Data Group engineering team. The core charter of the Data SQL Ninja program is to unblock and accelerate complex modernization and compete data platform migration opportunities to Microsoft's Azure Data platform. If you think your organization would be interested in participating in the Data SQL Ninja program, please contact your account team and ask them to submit a nomination.

## Next steps

- To learn more about migrating MySQL databases to SQL Server, see [SSMA documentation for MySQL](../../../ssma/mysql/sql-server-migration-assistant-for-mysql-mysqltosql.md).

- For a matrix of the Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios as well as specialty tasks, see the article [Service and tools for data migration](https://docs.microsoft.com/azure/dms/dms-tools-matrix).

- For other migration guides, see [Database Migration](https://datamigration.microsoft.com/). 

