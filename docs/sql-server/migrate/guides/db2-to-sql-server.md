---
title: "Migration guide: DB2 to SQL Server"
description: Follow this guide to migrate your DB2 server to SQL Server. 
ms.custom: ""
ms.date: "08/17/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "processors [SQL Server], supported"
  - "number of processors supported"
  - "maximum number of processors supported"
author: MashaMSFT
ms.author: mathoma
---
# Migration guide: DB2 to SQL Server
[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]

This migration guide teaches you to migrate your user databases from DB2 to SQL Server using the SQL Server Migration Assistant for DB2. 

For other migration guides, see [Database Migration](https://datamigration.microsoft.com/). 


## Prerequisites

To migrate your DB2 database to SQL Server, you need:

- [SQL Server Migration Assistant (SSMA) for DB2](https://www.microsoft.com/download/details.aspx?id=54254)


## Pre-migration

Before you begin your migration, discover the topology of your environment and assess the feasibility of your intended migration.

### Discover

The goal of the Discover phase is to identify existing data sources and details about the features that are being used to get a better understanding of and plan for the migration. This process involves scanning your network to identify all your organization’s DB2 instances together with the version and features in use.

### Assess and convert

Create an assessment using SQL Server Migration Assistant (SSMA). 

To create an assessment, follow these steps:

1. Open SQL Server Migration Assistant (SSMA) for DB2. 
1. Select **File** and then choose **New Project**. 
1. Provide a project name, a location to save your project, and then select a SQL Server migration target from the drop-down. 
1. Select **OK**. 

   :::image type="content" source="media/db2-to-sql-server/ssma-db2-new-project.png" alt-text="Select new SSMA project":::

1. Enter in values for the DB2 connection details on the **Connect to DB2** dialog box. 
1. Select the DB2 schema you want to migrate, and then select **Create report**. You can do so by either selecting the schema and then choosing **Create Report** from the navigation bar, or right-clicking your schema and then choosing **Create Report**. This will generate an HTML report . 
1. Review the HTML report to identify conversion statistics and any errors or warnings. You can also open the report in Excel to get an inventory of DB2 objects and the effort required to perform schema conversions. The default location for the report is in the report folder within SSMAProjects. 
   For example: `drive:\<username>\Documents\SSMAProjects\MyDB2Migration\report\report_<date>`. 


### Validate data types

Validate the default data type mappings and change them based on requirements if necessary. To do so, follow these steps: 

1. Select **Tools** from the menu. 
1. Select **Project Settings**. 
1. Select the **Type mappings** tab. 
1. You can change the type mapping for each table by selecting the table in the **DB2 Metadata explorer**. 

### Schema conversion 

To convert the schema, follow these steps:

1. Add dynamic or ad-hoc queries to statements. Right-click the node, and then choose **Add statements**. 
1. Select **Connect to SQL Server**. 
    1. Enter connection details to connect to your SQL Server instance. 
    1. Choose an existing database on the target server, or provide a new name to create a new database. 
    1. Select **Connect**. 
1. Right-click the schema and then choose **Convert Schema**. 
1. After the conversion completes, compare and review the structure of the schema to identify potential problems and address them based on the recommendations. 
1. Save the project locally for an offline schema remediation exercise. Select **Save Project** from the **File** menu. 


## Migrate

After you have completed assessing your databases and addressing any discrepancies, the next step is to execute the migration process.

To migrate your data, follow these steps:

1. Publish the schema: Right-click the database from the **Databases** node in the **SQL Server Metadata Explorer** and choose **Synchronize with Database**. 
1. Migrate the data: Right-click the schema from the **DB2 Metadata Explorer** and choose **Migrate Data**. 
1. Provide connection details for both the DB2 and SQL Server instances. 
1. View the **Data Migration report**. 
1. Connect to your SQL Server instance by using SQL Server Management Studio and validate the migration by reviewing the data and schema. 

## Post-migration 

After you have successfully completed the Migration stage, you need to go through a series of post-migration tasks to ensure that everything is functioning as smoothly and efficiently as possible.

### Remediate applications 

After the data is migrated to the target environment, all the applications that formerly consumed the source need to start consuming the target. Accomplishing this will in some cases require changes to the applications.

### Perform tests

The test approach for database migration consists of the following activities:

1. **Develop validation tests**: To test database migration, you need to use SQL queries. You must create the validation queries to run against both the source and the target databases. Your validation queries should cover the scope you have defined.
1. **Set up test environment**: The test environment should contain a copy of the source database and the target database. Be sure to isolate the test environment.
1. **Run validation tests**: Run the validation tests against the source and the target, and then analyze the results.
1. **Run performance tests**: Run performance test against the source and the target, and then analyze and compare the results.

   > [!NOTE]
   > For assistance developing and running post-migration validation tests, consider the Data Quality Solution available from the partner QuerySurge. 

## Migration assets 

The following resources were developed in support of a real-world migration project engagement and are intended to be used as examples for this migration scenario. 


|Asset  |Description  |
|---------|---------|
|[Data workload assessment model and tool](https://github.com/Microsoft/DataMigrationTeam/tree/master/Data%20Workload%20Assessment%20Model%20and%20Tool)| This tool provides suggested “best fit” target platforms, cloud readiness, and application/database remediation level for a given workload. It offers simple, one-click calculation and report generation that greatly helps to accelerate large estate assessments by providing and automated and uniform target platform decision process.|
|[DB2 zOS data assets discovery and assessment package](https://github.com/Microsoft/DataMigrationTeam/tree/master/DB2%20zOS%20Data%20Assets%20Discovery%20and%20Assessment%20Package)|After running the SQL script on a database, you can export the results to a file on the file system. Several file formats are supported, including *.csv, so that you can capture the results in external tools such as spreadsheets. This method can be useful if you want to more easily share results information with team who do not have the workbench installed.|
|[IBM DB2 LUW inventory scripts and artifacts](https://github.com/Microsoft/DataMigrationTeam/tree/master/IBM%20DB2%20LUW%20Inventory%20Scripts%20and%20Artifacts)|This asset includes a SQL query that hits IBM DB2 LUW version 11.1 system tables and provides a count of objects by schema and object type, a rough estimate of 'Raw Data' in each schema, and the sizing of tables in each schema, with results stored in a CSV format.|
|[DB2 LUW pure scale on Azure - setup guide](https://github.com/Microsoft/DataMigrationTeam/blob/master/Whitepapers/DB2%20PureScale%20on%20Azure.pdf)|This guide serves as a starting point for a DB2 implementation plan. While business requirements will differ, the same basic pattern applies. This architectural pattern may also be used for OLAP applications on Azure.|

These resources were developed as part of the Data Migration Jumpstart Program (DM Jumpstart), which is sponsored by the Azure Data Group engineering team. The core charter DM Jumpstart is to unblock and accelerate complex modernization and compete data platform migration opportunities to Microsoft’s Azure Data platform. If you think your organization would be interested in participating in the DM Jumpstart program, please contact your account team and ask that they submit a nomination.

## Partners

|  | |  |
|---------|---------|---------|
|**[:::image type="content" source="media/db2-to-sql-server/Blitzz_logo_84.png" alt-text="Blitzz":::](https://www.blitzz.io/product)**|[:::image type="content" source="media/db2-to-sql-server/blueprint_logo.png" alt-text="Blueprint":::](https://bpcs.com/what-we-do)|[:::image type="content" source="media/db2-to-sql-server/Cognizant-220.1.png" alt-text="Cognizant":::](https://www.cognizant.com/partners/microsoft)| 
|[:::image type="content" source="media/db2-to-sql-server/DXC_logo_cropped.png" alt-text="DXC":::](https://www.dxc.technology/application_services/offerings/139843/142343-application_services_for_microsoft_azure)|**[:::image type="content" source="media/db2-to-sql-server/HVR-logo.png" alt-text="HVR":::](https://www.hvr-software.com/solutions/azure-data-integration/)**|[:::image type="content" source="media/db2-to-sql-server/InfosysLogo.png" alt-text="Infosys":::](https://www.infosys.com/services/)|
|**[:::image type="content" source="media/db2-to-sql-server/ispirer_logo.png" alt-text="Ispirer":::](https://www.ispirer.com/blog/migration-to-the-microsoft-technology-stack)**|[:::image type="content" source="media/db2-to-sql-server/querysurge_logo-84.png" alt-text="Querysurge":::](https://www.querysurge.com/company/partners/microsoft)|[:::image type="content" source="media/db2-to-sql-server/scalability-experts-logo3.png" alt-text="Scalability Experts":::](http://www.scalabilityexperts.com/products/index.html)|
|**[:::image type="content" source="media/db2-to-sql-server/wipro-220.png" alt-text="Wipro":::](https://www.wipro.com/analytics/)**|||



## Next step

After migration, review the [Post-migration validation and optimization guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md). 

For a matrix of the Microsoft and third-party services and tools that are available to assist you with various database and data migration scenarios, as well as specialty tasks, see [Data migration services and tools](/azure/dms/dms-tools-matrix).

For other migration guides, see [Database Migration](https://datamigration.microsoft.com/). 

For video content, see:
- [How to use the Database Migration Guide](https://azure.microsoft.com/resources/videos/how-to-use-the-azure-database-migration-guide/)
- [Overview of the migration journey](https://azure.microsoft.com/resources/videos/overview-of-migration-and-recommended-tools-services/)