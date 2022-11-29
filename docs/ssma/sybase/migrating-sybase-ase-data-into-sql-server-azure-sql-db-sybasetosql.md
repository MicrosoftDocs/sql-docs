---
description: "Migrating Sybase ASE Data into SQL Server - Azure SQL Database  (SybaseToSQL)"
title: "Migrate Sybase ASE Data into SQL Server - Azure SQL Database | Microsoft Docs"
ms.custom:
  - intro-migration
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords:
  - "Migrating data,Client Side Data Migration"
  - "Migrating data,Server Side Data Migration"
ms.assetid: 54a39f5e-9250-4387-a3ae-eae47c799811
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.sybase.migratedata.f1"

---
# Migrating Sybase ASE Data into SQL Server - Azure SQL Database  (SybaseToSQL)
After you have successfully loaded the Sybase Adaptive Server Enterprise (ASE) database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, you can migrate data from ASE to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.  
  
> [!IMPORTANT]  
> If the engine being used is Server Side Data Migration Engine, then before you migrate data, you must install the SSMA for Sybase ASE Extension Pack and the Sybase ASE providers on the computer that is running SSMA. The SQL Server Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA Components on SQL Server (SybaseToSQL)](./installing-ssma-components-on-sql-server-sybasetosql.md)  
  
## Setting Migration Options  
Before migrating data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, review the project migration options in the **Project Settings** dialog box.  
  
-   By using this dialog box you can set options such as migration batch size, table locking, constraint checking, null value handling and identity value handling. For more information about the Project Migration Settings, see [Project Settings (Migration) (Sybase)](./project-settings-migration-sybasetosql.md).  
  
    For more information on **Extended Data Migration Settings**, see [Data Migration Settings](data-migration-settings-sybasetosql.md)  
  
-   The **Migration Engine** in the **Project Settings** dialog box, allows the user to perform the migration process using two types of data migration engines, viz.:  
  
    1.  Client Side Data Migration Engine  
  
    2.  Server Side Data Migration Engine  
  
**Client Side Data Migration:**  
  
-   To initiate data migration on the client side, select the option **Client Side Data Migration Engine** in the **Project Settings** dialog box.  
  
-   In **Project Settings**, the **Client Side Data Migration Engine** option is set by default.  
  
    > [!NOTE]  
    > The Client-Side Data Migration Engine resides inside the SSMA application and, therefore, is not dependent on the availability of the extension pack.  
  
**Server Side Data Migration:**  
  
-   During Server side data migration, the engine resides on the target database. It is installed through the extension pack. For more information on how to install the extension pack, see [Installing SSMA Components on SQL Server (SybaseToSQL)](./installing-ssma-components-on-sql-server-sybasetosql.md)  
  
-   To initiate migration on the server side, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog.  
  
> [!NOTE]  
> When Azure SQL Database is used as the target database, only **Client side data migration** is allowed and server side data migration is not supported.  
  
## Migrating Data to SQL Server or Azure SQL Database  
Migrating data is a bulk-load operation that moves rows of data from the ASE tables into SQL Server tables in transactions. The number of rows loaded into SQL Server or Azure SQL Database in each transaction is configured in the project settings.  
  
To view the migration messages, make sure that the Output pane is visible. Otherwise, select **Output** from the **View** menu.  
  
**To migrate data**  
  
1.  Verify the following:  
  
    -   The ASE providers are installed on the computer that is running SSMA.  
  
    -   You have synchronized the converted objects with the target database (SQL Server or Azure SQL Database).  
  
2.  In Sybase Metadata Explorer, select the objects that contain the data that you want to migrate:  
  
    -   To migrate data for all schemas, select the check box next to **Schemas**.  
  
    -   To migrate data or omit individual tables, first expand the schema, expand **Tables**, and then select or clear the check box next to the table.  
  
3.  To migrate data, two cases arise:  
  
    **Client Side Data Migration:**  
  
    For performing **Client Side Data Migration**, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
    **Server Side Data Migration:**  
  
    -   Before performing Server side data migration, ensure:  
  
        1.  The SSMA for Sybase Extension Pack is installed on the instance of SQL Server.  
  
        2.  The SQL Server Agent service is running on the instance of SQL Server  
  
    -   For performing **Server Side Data Migration**, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
4.  Right-click **Schemas** in Sybase Metadata Explorer, and then click **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder, and select the **Migrate Data** option.  
  
    > [!NOTE]  
    > If the SSMA for Sybase Extension Pack is not installed on the instance of SQL Server, and if **Server Side Data Migration Engine** is selected, then while migrating the data to the target database, the following error is encountered: 'SSMA Data Migration components were not found on SQL Server, server-side data migration will not be possible. Please check if Extension Pack is installed correctly'. Click **Cancel** to terminate the data migration.  
  
5.  In the **Connect to Sybase ASE** dialog box, enter the connection credentials, and then click **Connect**. For more information on connecting to Sybase ASE, see [Connect to Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/connect-to-sybase-sybasetosql.md)  
  
    If the target database is SQL Server, then, enter the connection credentials in the **Connect to SQL Server** dialog box, and click **Connect**. For more information on connecting to SQL Server, see [Connecting to SQL Server(SybaseToSQL)](./connecting-to-sql-server-sybasetosql.md)  
  
    If the target database is Azure SQL Database, then enter the connection credentials in the **Connect to Azure SQL Database** dialog box, and click **Connect**. For more information on connecting to Azure SQL Database, see [Connecting to Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-azure-sql-db-sybasetosql.md)  
  
    Messages will appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data did not migrate, click the row that contains the errors, and then click **Details**. When you are finished with the report, click **Close**. For more information on Data Migration Report, see [Data Migration Report (SSMA Common)](./data-migration-report-sybasetosql.md)  
  
> [!NOTE]  
> When SQL Express edition is used as the target database, only client side data migration is allowed and server side data migration is not supported.  

## Migrating data at scale (Preview)

When large data size 100GB and above needs to be migrated within a short duration, at scale offline migration is a suitable option. This feature uses the azure data factory pipeline and the scalability of cloud to migrate on-premises or external Sybase data source to Azure SQL platform whether IaaS VM or SQL PaaS. This capability will create the data factory or use an existing data factory to migrate data from Sybase.

1. For large offline migration, click on Migrate data at scale. The offline data migration supports database level migration.

1. Provide Azure active directory account for authentication. Once you entered the user name, click on connect.

1. Select the tenant and the subscription that already has the Azure SQL VM or Azure SQL database or managed instance configured and click next.

1. If  there is an existing data factory that will is preferred to be used, you can select the resource group and the data factory name that would be used for this migration. Otherwise, click new to create a resource group and azure data factory.

1. In order to migrate an external data source ,  an integration runtime is required. Either click new and follow the steps to create the integration runtime or use an existing integration service. To create an integration service, a gateway installation needs to be configured and a key needs to be supplied to configure integration runtime.

1. After the integration runtime configuration, the migrate workflow will create the linked service in the data factory.  Provide the Sybase credentials followed by SQL credentials.

1. Finally, provide an unique pipeline name. Pipeline name supports characters and numeric values. Avoid any special characters.

1. If the target tables contains data, it will be truncated and reloaded. A warning dialog box will appear.  Select OK to proceed or cancel to avoid truncate and load activity.

1. It takes few minutes to create a pipeline. The status bar will provide the progress of the pipeline creation. If the pipeline is created successfully, a message wil be written in the output log "A data factory pipeline [PipelineName] is created for the data migration activity."

1. For monitoring the data migration, click on the monitoring URL or go to the data factory monitoring page in Azure portal.
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
