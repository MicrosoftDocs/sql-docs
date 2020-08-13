---
title: "Connecting to Azure SQL Database (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 9e77e4b0-40c0-455c-8431-ca5d43849aa7
author: "nahk-ivanov"
ms.author: "alexiva"
---
# Connecting to Azure SQL Database (SybaseToSQL)
To migrate Sybase databases to Azure SQL Database, you must connect to the target instance of Azure SQL Database. When you connect, SSMA obtains metadata about all the databases in the instance of Azure SQL Database and displays database metadata in the Azure SQL Database Metadata Explorer. SSMA stores information of the instance of Azure SQL Database you are connected to, but does not store passwords.  
  
Your connection to Azure SQL Database stays active until you close the project. When you reopen the project, you must reconnect to Azure SQL Database if you want an active connection to the server. You can work offline until you load database objects into Azure SQL Database and migrate data.  
  
Metadata about the instance of Azure SQL Database is not automatically synchronized. Instead, to update the metadata in Azure SQL Database Metadata Explorer, you must manually update the Azure SQL Database metadata. For more information, see the "Synchronizing Azure SQL Database Metadata" section later in this topic.  
  
## Required Azure SQL Database Permissions  
The account that is used to connect to Azure SQL Database requires different permissions depending on the actions that the account performs:  
  
1.  To convert Sybase objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from Azure SQL Database, or to save converted syntax to scripts, the account must have permission to log on to the instance of Azure SQL Database.  
  
2.  To load database objects into Azure SQL Database, the minimum permission requirement is membership in the  **db_owner** database role in the target database.  
  
## Establishing an Azure SQL Database Connection  
Before you convert Sybase database objects to Azure SQL Database syntax, you must establish a connection to the instance of Azure SQL Database where you want to migrate the Sybase database or databases.  
  
When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the Sybase schema level after you connect to Azure SQL Database. For more information, see [Mapping Sybase ASE Schemas to SQL Server Schemas &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-schemas-to-sql-server-schemas-sybasetosql.md)  
  
> [!WARNING]  
> Before you try to connect to Azure SQL Database, make sure that the instance of Azure SQL Database is running and can accept connections.  
  
**To connect to Azure SQL Database**  
  
1.  On the **File** menu, select **Connect to Azure SQL Database**(this option is enabled after the creation of a project).  
  
    If you have previously connected to Azure SQL Database, the command name will be **Reconnect to Azure SQL Database**  
  
2.  In the connection dialog box, enter or select the server name of Azure SQL Database.  
  
3.  Enter, select or **Browse** the Database name.  
  
4.  Enter or select **UserName**.  
  
5.  Enter the **Password**.  
  
6.  SSMA recommends encrypted connection to Azure SQL Database.  
  
7.  Click **Connect**.  
  
> [!IMPORTANT]  
> SSMA for Sybase does not support connection to **master** database in Azure SQL Database.  
  
## Synchronizing Azure SQL Database Metadata  
Metadata about Azure SQL Database databases is not automatically updated. The metadata in Azure SQL Database Metadata Explorer is a snapshot of the metadata when you first connected to Azure SQL Database, or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object.  
  
**To synchronize metadata**  
  
1.  Make sure that you are connected to Azure SQL Database.  
  
2.  In Azure SQL Database Metadata Explorer, select the check box next to the database or database schema that you want to update.  
  
    For example, to update the metadata for all databases, select the box next to Databases.  
  
3.  Right-click Databases, or the individual database or database schema, and then select **Synchronize with Database**.  
  
## Next Step  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping between Sybase schemas and Azure SQL Database databases and schemas, see [Mapping Sybase ASE Schemas to SQL Server Schemas &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-schemas-to-sql-server-schemas-sybasetosql.md)  
  
-   To customize configuration options for the projects, see [Setting Project Options &#40;SybaseToSQL&#41;](../../ssma/sybase/setting-project-options-sybasetosql.md)  
  
-   To customize the mapping of source and target data types, see [Mapping Sybase ASE and SQL Server Data Types &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md)  
  
-   If you do not have to perform any of these tasks, you can convert the Sybase database object definitions into Azure SQL Database object definitions. For more information, see [Converting Sybase ASE Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md)  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
