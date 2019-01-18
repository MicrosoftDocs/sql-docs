---
title: "Connecting to Azure SQL DB (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 9e77e4b0-40c0-455c-8431-ca5d43849aa7
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connecting to Azure SQL DB (SybaseToSQL)
To migrate Sybase databases to Azure SQL DB, you must connect to the target instance of Azure SQL DB. When you connect, SSMA obtains metadata about all the databases in the instance of Azure SQL DB and displays database metadata in the Azure SQL DB Metadata Explorer. SSMA stores information of the instance of Azure SQL DB you are connected to, but does not store passwords.  
  
Your connection to Azure SQL DB stays active until you close the project. When you reopen the project, you must reconnect to Azure SQL DB if you want an active connection to the server. You can work offline until you load database objects into Azure SQL DB and migrate data.  
  
Metadata about the instance of Azure SQL DB is not automatically synchronized. Instead, to update the metadata in Azure SQL DB Metadata Explorer, you must manually update the Azure SQL DB metadata. For more information, see the "Synchronizing Azure SQL DB Metadata" section later in this topic.  
  
## Required Azure SQL DB Permissions  
The account that is used to connect to Azure SQL DB requires different permissions depending on the actions that the account performs:  
  
1.  To convert Sybase objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from Azure SQL DB, or to save converted syntax to scripts, the account must have permission to log on to the instance of Azure SQL DB.  
  
2.  To load database objects into Azure SQL DB, the minimum permission requirement is membership in the  **db_owner** database role in the target database.  
  
## Establishing a Azure SQL DB Connection  
Before you convert Sybase database objects to Azure SQL DB syntax, you must establish a connection to the instance of Azure SQL DB where you want to migrate the Sybase database or databases.  
  
When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the Sybase schema level after you connect to Azure SQL DB. For more information, see [Mapping Sybase ASE Schemas to SQL Server Schemas &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-schemas-to-sql-server-schemas-sybasetosql.md)  
  
> [!WARNING]  
> Before you try to connect to Azure SQL DB, make sure that the instance of Azure SQL DB is running and can accept connections.  
  
**To connect to Azure SQL DB**  
  
1.  On the **File** menu, select **Connect to Azure SQL DB**(this option is enabled after the creation of a project).  
  
    If you have previously connected to Azure SQL DB, the command name will be **Reconnect to Azure SQL DB**  
  
2.  In the connection dialog box, enter or select the server name of Azure SQL DB.  
  
3.  Enter, select or **Browse** the Database name.  
  
4.  Enter or select **UserName**.  
  
5.  Enter the **Password**.  
  
6.  SSMA recommends encrypted connection to Azure SQL DB.  
  
7.  Click **Connect**.  
  
> [!IMPORTANT]  
> SSMA for Sybase does not support connection to **master** database in Azure SQL DB.  
  
## Synchronizing Azure SQL DB Metadata  
Metadata about Azure SQL DB databases is not automatically updated. The metadata in Azure SQL DB Metadata Explorer is a snapshot of the metadata when you first connected to Azure SQL DB, or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object.  
  
**To synchronize metadata**  
  
1.  Make sure that you are connected to Azure SQL DB.  
  
2.  In Azure SQL DB Metadata Explorer, select the check box next to the database or database schema that you want to update.  
  
    For example, to update the metadata for all databases, select the box next to Databases.  
  
3.  Right-click Databases, or the individual database or database schema, and then select **Synchronize with Database**.  
  
## Next Step  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping between Sybase schemas and Azure SQL DB databases and schemas, see [Mapping Sybase ASE Schemas to SQL Server Schemas &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-schemas-to-sql-server-schemas-sybasetosql.md)  
  
-   To customize configuration options for the projects, see [Setting Project Options &#40;SybaseToSQL&#41;](../../ssma/sybase/setting-project-options-sybasetosql.md)  
  
-   To customize the mapping of source and target data types, see [Mapping Sybase ASE and SQL Server Data Types &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md)  
  
-   If you do not have to perform any of these tasks, you can convert the Sybase database object definitions into Azure SQL DB object definitions. For more information, see [Converting Sybase ASE Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md)  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
