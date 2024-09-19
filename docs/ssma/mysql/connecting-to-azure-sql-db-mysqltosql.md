---
title: "Connecting to Azure SQL Database (MySQLToSQL)"
description: "Connecting to Azure SQL Database (MySQLToSQL)"
author: cpichuka
ms.author: cpichuka
ms.date: "11/16/2020"
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Connecting to Azure SQL Database, SQL Azure permissions"
  - "Connecting to Azure SQL Database, synchronization"
---

# Connecting to Azure SQL Database (MySQLToSQL)

To migrate MySQL databases to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you must connect to the target instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. When you connect, SSMA obtains metadata about all the databases in the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and displays database metadata in the **Azure SQL Database Metadata Explorer**. SSMA stores information of the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] you are connected to, but does not store passwords.

Your connection to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and migrate data.

Metadata about the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is not automatically synchronized. Instead, to update the metadata in **Azure SQL Database Metadata Explorer**, you must manually update the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] metadata. For more information, see the "Synchronizing Azure SQL Database Metadata" section later in this topic.

## Required Azure SQL Database Permissions

The account that is used to connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] requires different permissions depending on the actions that the account performs:

- To convert MySQL objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or to save converted syntax to scripts, the account must have permission to log on to the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

- To load database objects into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the account must be a member of the **db_ddladmin** database role.

- To migrate data to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the account must be a member of the **db_owner** database role.

## Establishing an Azure SQL Database Connection

Before you convert MySQL database objects to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] syntax, you must establish a connection to the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you want to migrate the MySQL database or databases.

When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the MySQL schema level after you connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. For more information, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md).

> [!IMPORTANT]
> Before you try to connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], make sure that your IP address is allowed through the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] firewall.

To connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]:

1. On the **File** menu, select **Connect to Azure SQL Database** (this option is enabled after the creation of a project).
   If you have previously connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the command name will be **Reconnect to Azure SQL Database**.

2. In the connection dialog box, enter or select the server name of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

3. Enter, select or **Browse** the Database name.

4. Enter or select **Username**.

5. Enter the **Password**.

6. SSMA recommends encrypted connection to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

7. Click **Connect**.
  
## Synchronizing Azure SQL Database Metadata

Metadata about databases in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is not automatically updated. The metadata in **Azure SQL Database Metadata Explorer** is a snapshot of the metadata when you first connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object. To synchronize metadata:

1. Make sure that you are connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

2. In **Azure SQL Database Metadata Explorer**, select the check box next to the database or database schema that you want to update.
   For example, to update the metadata for all databases, select the box next to **Databases**.

3. Right-click **Databases**, or the individual database or database schema, and then select **Synchronize with Database**.

## Next Step

The next step in the migration depends on your project needs:

- To customize the mapping between MySQL schemas and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md).
- To customize configuration options for the projects, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md).
- To customize the mapping of source and target data types, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md).
- If you do not have to perform any of these tasks, you can convert the MySQL database object definitions into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] object definitions. For more information, see [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md).

## See Also

[Migrating MySQL Databases to SQL Server - Azure SQL Database &#40;MySQLToSQL&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
