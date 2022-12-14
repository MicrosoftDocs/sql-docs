---
description: "Migrating MySQL Data into SQL Server - Azure SQL Database (MySQLToSQL)"
title: "Migrating MySQL Data into SQL Server - Azure SQL Database (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom:
  - intro-migration
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords:
  - "Data Migration, server side data migration"
  - "Data Migration,client side data migration"
ms.assetid: a6a7f4d6-68aa-4a38-93bf-53eba0d7dc82
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.migratedata.f1"
---
# Migrating MySQL Data into SQL Server - Azure SQL Database (MySQLToSQL)
After you have successfully synchronized the converted objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you can migrate data from MySQL to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.  
  
> [!IMPORTANT]  
> If the engine being used is Server Side Data Migration Engine, then, before migrating data, you must install the SSMA for MySQL Extension Pack and the MySQL providers on the computer that is running SSMA. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA Components on SQL Server (MySQL to SQL)](./installing-ssma-components-on-sql-server-mysqltosql.md)  
  
## Setting Migration Options  
Before migrating data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, review the project migration options in the **Project Settings** dialog box.  
  
-   By using this dialog box you can set options such as migration batch size, table locking, constraint checking, null value handling and identity value-handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](./project-settings-migration-mysqltosql.md).  
  
    For more information on **Extended Data Migration Settings**, see [Data Migration Settings](data-migration-settings-mysqltosql.md)  
  
-   The **Migration Engine** in the **Project Settings** dialog box, allows the user to perform the migration process using two types of data migration engines:  
  
    1.  Client Side Data Migration Engine  
  
    2.  Server Side Data Migration Engine  
  
**Client Side Data Migration:**  
  
-   To initiate data-migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
-   In **Project Settings**, the **Client Side Data Migration Engine** option is set.  
  
    > [!NOTE]  
    > The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.  
  
**Server Side Data Migration:**  
  
-   During the Server side data migration, the engine resides on the target database. It is installed through the extension pack. For more information on how to install the extension pack, see [Installing SSMA Components on SQL Server (MySQL to SQL)](./installing-ssma-components-on-sql-server-mysqltosql.md)  
  
-   To initiate migration on the server side, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
> [!IMPORTANT]  
> **Client Side Data Migration** option is available for SQL Azure only.  
  
## Migrating Data to SQL Server or SQL Azure  
Migrating data is a bulk-load operation that moves rows of data from MySQL tables into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables in transactions. The number of rows loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in each transaction is configured in the project settings.  
  
To view migration messages, make sure that the Output pane is visible. Otherwise, from the **View** menu, select **Output**.  
  
**To migrate data**  
  
1.  Verify the following:  
  
    -   The MySQL providers are installed on the computer that is running SSMA.  
  
    -   You have synchronized the converted objects with the target database (SQL Server / SQL Azure).  
  
2.  In MySQL Metadata Explorer, select the objects that contain the data that you want to migrate:  
  
    -   To migrate data for all schemas, select the check box next to **Schemas**.  
  
    -   To migrate data or omit individual tables, first expand the schema, expand **Tables**, and then select or clear the check box next to the table.  
  
3.  To migrate data, two cases arise:  
  
    **Client Side Data Migration:**  
  
    -   For performing **Client Side Data Migration**, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
    **Server Side Data Migration:**  
  
    -   Before performing data migration on the server side, ensure:  
  
        1.  The SSMA for MySQL Extension Pack is installed on the instance of SQL Server.  
  
        2.  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is running on the instance of SQL Server  
  
    -   For performing **Server Side Data Migration**, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
4.  Right-click **Schemas** in MySQL Metadata Explorer, and then click **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder; select the **Migrate Data** option.  
  
    > [!NOTE]  
    > If the SSMA for MySQL Extension Pack is not installed on the instance of SQL Server, and if **Server Side Data Migration Engine** is selected, then while migrating the data to the target database, the following error is encountered: 'SSMA Data Migration components were not found on SQL Server, server-side data migration will not be possible. Please check if Extension Pack is installed correctly'. Click **Cancel** to terminate the data migration.  
  
5.  In the **Connect to MySQL** dialog box, enter the connection credentials, and then click **Connect**. For more information on connecting to MySQL, see [Connect to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connect-to-mysql-mysqltosql.md)  
  
    If the target database is SQL Server then enter the connection credentials in the **Connect to SQL Server** dialog box, and click **Connect**. For more information on connecting to SQL Server, see [Connect to SQL Server](../sybase/connecting-to-sql-server-sybasetosql.md)  
  
    If the target database is SQL Azure then enter the connection credentials in the **Connect to SQL Azure** dialog box, and click **Connect**. For more information on connecting to SQL Azure, see [Connect to Azure SQL Database &#40;MySQLToSQL&#41;](../../ssma/mysql/connect-to-azure-sql-db-mysqltosql.md)  
  
    Messages will appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data did not migrate, click the row that contains the errors, and then click **Details**. When you are finished with the report, click **Close**. For more information on Data Migration Report, see [Data Migration Report (SSMA Common)](../sybase/data-migration-report-sybasetosql.md)  
  
> [!NOTE]  
> When SQL Express edition is used as the target database, only client side data migration is allowed and server side data migration is not supported.  

## Migrating data at scale (Preview)

When large data size needs to be migrated within a short duration, at scale offline migration is a suitable option. This feature uses the Azure data factory pipeline and the scalability of cloud to migrate on-premises or external Oracle data source to Azure SQL platform whether IaaS VM or SQL PaaS. This capability will create the data factory or use an existing data factory to migrate data to Azure SQL PaaS or SQL VM.

1. For large offline migration, click on Migrate data at scale. 

1. Provide Azure active directory account for authentication. Once you entered the user name, click on connect.

1. Select the tenant and the subscription that already has the Azure SQL VM or Azure SQL database or managed instance configured and click next.

1. The next step in the migrate workflow is to provide the data source credentials followed by  the target SQL credentials.

1. For this migration, either you can use existing Azure resources -  resource group and the data factory name  or click new to create a resource group and Azure data factory for migration.

1. In order to migrate an external data source,  an integration runtime is required. Either click new and follow the steps to create the integration runtime or use an existing integration service. To create an integration service, a gateway installation needs to be configured and a key needs to be supplied to configure integration runtime.

1. Finally, provide a unique data  migration name. Migration name supports characters and numeric values. Avoid any special characters.

1. If the target tables contain data, it will be truncated and reloaded. A warning dialog box will appear.  Select OK to proceed or cancel to avoid truncate and load activity.

1. It takes few minutes to create Azure data factory components. The status bar will provide the progress of the pipeline creation. If the pipeline is created successfully, a message will be written in the output log "A data factory pipeline [PipelineName] is created for the data migration activity."

1. For monitoring the data migration, click on the monitoring URL or go to the data factory monitoring page in Azure portal.
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL Database &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
