---
title: "Connecting to Azure SQL Database (AccessToSQL) | Microsoft Docs"
description: Learn how to connect to a target instance of Azure SQL Database to migrate Access databases. SSMA obtains metadata about databases in Azure SQL Database.
ms.service: sql
ms.custom: ""
ms.date: "11/16/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "instance of SQL Azure"
  - "metadata, refreshing"
  - "refreshing metadata"
  - "SQL Azure"
  - "SQL Azure, connecting"
  - "SQL Azure, connecting to"
  - "SQL Azure, reconnecting"
  - "SQL Azure, synchronizing metadata"
ms.assetid: 1ba0d113-dc05-4431-8689-e14a8821bafd
author: cpichuka 
ms.author: cpichuka 
---

# Connecting to Azure SQL Database (AccessToSQL)

To migrate Access databases to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you must connect to the target instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)]. When you connect, SSMA obtains metadata about all the databases in the instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] and displays database metadata in the **Azure SQL Database Metadata Explorer**. SSMA stores information about which instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] you are connected to, but does not store passwords.

Your connection to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] and migrate data.

Metadata about the instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] is not automatically synchronized. Instead, to update the metadata in **Azure SQL Database Metadata Explorer**, you must manually update the [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] metadata. For more information, see the "Synchronizing Azure SQL Database Metadata" section later in this topic.

## Required Azure SQL Database Permissions

The account that is used to connect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] requires different permissions depending on the actions that the account performs:

- To convert Access objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], or to save converted syntax to scripts, the account must have permission to log on to the instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

- To load database objects into [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], the account must be a member of the **db_ddladmin** database role.

- To migrate data to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], the account must be a member of the **db_owner** database role.

## Establishing an Azure SQL Database Connection

Before you convert Access database objects to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] syntax, you must establish a connection to the instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] where you want to migrate the Access database or databases.

When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the Access schema level after you connect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)]. For more information, see [Mapping Access Databases to SQL Server Schemas](mapping-source-and-target-databases-accesstosql.md).
  
> [!IMPORTANT]
> Before you try to connect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], make sure that your IP address is allowed through the [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] firewall.
  
To connect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)]:

1. On the **File** menu, select **Connect to SQL Azure** (this option is enabled after the creation of a project).
   If you previously connected to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], the command name will be **Reconnect to SQL Azure**.

2. In the connection dialog box, enter or select the server name of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

3. Enter, select, or **Browse** the Database name.

4. Enter or select **Username**.

5. Enter the **Password**.

6. SSMA recommends encrypted connection to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

7. Click **Connect**.
  
If there are no databases in the [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you can create the first database using **Create Azure Database** option that appears on the click of **Browse** button.

## Synchronizing Azure SQL Database Metadata

Metadata about databases in [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] is not automatically updated. The metadata in **Azure SQL Database Metadata Explorer** is a snapshot of the metadata when you first connected to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object. To synchronize metadata:

1. Make sure that you are connected to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

2. In **Azure SQL Database Metadata Explorer**, select the check box next to the database or database schema that you want to update.
   For example, to update the metadata for all databases, select the box next to **Databases**.

3. Right-click **Databases**, or the individual database or database schema, and then select **Synchronize with Database**.

## Refreshing Azure SQL Database Metadata

If [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] schemas change after you connect, you can refresh metadata from the server.

To refresh [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] metadata:

- In **Azure SQL Database Metadata Explorer**, right-click **Databases**, and then select **Refresh from Database**.

## Reconnecting to Azure SQL Database

Your connection to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] and migrate data.

The procedure for reconnecting to [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] is the same as the procedure for establishing a connection.

## Next steps

The next step in the migration depends on your project needs:

- To customize the mapping between Access schemas and Azure SQL Database, see [Mapping Access Databases to SQL Server Schemas](mapping-source-and-target-databases-accesstosql.md).
- To customize configuration options for the projects, see [Setting Project Options](setting-conversion-and-migration-options-accesstosql.md).
- To customize the mapping of source and target data types, see [Mapping Source and Target Data Types](mapping-source-and-target-data-types-accesstosql.md).
- If you do not have to perform any of these tasks, you can convert the Access database object definitions into SQL Azure object definitions. For more information, see [Converting Access Databases](converting-access-database-objects-accesstosql.md).

## See Also

[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
