---
title: "Connecting to Azure SQL DB (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Connecting to SQL Azure, SQL Azure permissions"
  - "Connecting to SQL Azure, synchronization"
ms.assetid: d0b6f16a-1880-459d-a0c7-28b7ef15c56a
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connecting to Azure SQL DB (MySQLToSQL)
To migrate MySQL databases to SQL Azure, you must connect to the target instance of SQL Azure. When you connect, SSMA obtains metadata about all the databases in the instance of SQL Azure and displays database metadata in the SQL Azure Metadata Explorer. SSMA stores information of the instance of SQL Azure you are connected to, but does not store passwords.  
  
Your connection to SQL Azure stays active until you close the project. When you reopen the project, you must reconnect to SQL Azure if you want an active connection to the server. You can work offline until you load database objects into SQL Azure and migrate data.  
  
Metadata about the instance of SQL Azure is not automatically synchronized. Instead, to update the metadata in SQL Azure Metadata Explorer, you must manually update the SQL Azure metadata. For more information, see the "Synchronizing SQL Azure Metadata" section later in this topic.  
  
## Required SQL Azure Permissions  
The account that is used to connect to SQL Azure requires different permissions depending on the actions that the account performs:  
  
-   To convert MySQL objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from SQL Azure, or to save converted syntax to scripts, the account must have permission to log on to the instance of SQL Azure.  
  
-   To load database objects into SQL Azure, the minimum permission requirement is membership in the **db_owner** database role in the target database.  
  
## Establishing a SQL Azure Connection  
Before you convert MySQL database objects to SQL Azure syntax, you must establish a connection to the instance of SQL Azure where you want to migrate the MySQL database or databases.  
  
When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the MySQL schema level after you connect to SQL Azure. For more information, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md)  
  
> [!IMPORTANT]  
> Before you try to connect to SQL Azure, make sure that the instance of SQL Azure is running and can accept connections.  
  
**To connect to SQL Azure**  
  
1.  On the **File** menu, select **Connect to SQL Azure** (this option is enabled after the creation of a project).  
  
    If you have previously connected to SQL Azure, the command name will be **Reconnect to SQL Azure**.  
  
2.  In the connection dialog box, enter or select the server name of SQL Azure.  
  
3.  Enter, select or **Browse** the Database name.  
  
4.  Enter or select **UserName**.  
  
5.  Enter the **Password**.  
  
6.  SSMA recommends encrypted connection to SQL Azure.  
  
7.  Click **Connect**.  
  
> [!IMPORTANT]  
> SSMA for MySQL does not support connection to **master** database in SQL Azure.  
  
## Synchronizing SQL Azure Metadata  
Metadata about SQL Azure databases is not automatically updated. The metadata in SQL Azure Metadata Explorer is a snapshot of the metadata when you first connected to SQL Azure, or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object.  
  
**To synchronize metadata**  
  
1.  Make sure that you are connected to SQL Azure.  
  
2.  In SQL Azure Metadata Explorer, select the check box next to the database or database schema that you want to update.  
  
    For example, to update the metadata for all databases, select the box next to Databases.  
  
3.  Right-click Databases, or the individual database or database schema, and then select **Synchronize with Database**.  
  
## Next Step  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping between MySQL schemas and SQL Azure databases and schemas, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md)  
  
-   To customize configuration options for the projects, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md)  
  
-   To customize the mapping of source and target data types, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md)  
  
-   If you do not have to perform any of these tasks, you can convert the MySQL database object definitions into SQL Azure object definitions. For more information, see [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md)  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
