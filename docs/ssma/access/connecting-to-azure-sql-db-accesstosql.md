---
title: "Connecting to Azure SQL Database (AccessToSQL)"
description: Learn how to connect to a target instance of Azure SQL Database to migrate Access databases. SSMA obtains metadata about databases in Azure SQL Database.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "instance of Azure SQL"
  - "metadata, refreshing"
  - "refreshing metadata"
  - "Azure SQL"
  - "Azure SQL, connecting"
  - "Azure SQL, connecting to"
  - "Azure SQL, reconnecting"
  - "Azure SQL, synchronizing metadata"
---
# Connecting to Azure SQL Database (AccessToSQL)

To migrate Access databases to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you must connect to the target instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. When you connect, SQL Server Migration Assistant (SSMA) obtains metadata about all the databases in the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and displays database metadata in the **Azure SQL Database Metadata Explorer**. SSMA stores information about which instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] you are connected to, but doesn't store passwords.

Your connection to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and migrate data.

Metadata about the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] isn't automatically synchronized. Instead, to update the metadata in **Azure SQL Database Metadata Explorer**, you must manually update the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] metadata. For more information, see the [Synchronize Azure SQL Database metadata](#synchronize-azure-sql-database-metadata) section in this article.

## Required Azure SQL Database permissions

The account that is used to connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] requires different permissions depending on the actions that the account performs:

- To convert Access objects to [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax, to update metadata from [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or to save converted syntax to scripts, the account must have permission to sign in to the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

- To load database objects into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the account must be a member of the **db_ddladmin** database role.

- To migrate data to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the account must be a member of the **db_owner** database role.

## Establish an Azure SQL Database connection

Before you convert Access database objects to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] syntax, you must establish a connection to the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you want to migrate the Access database or databases.

When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the Access schema level after you connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. For more information, see [Mapping Access databases to SQL Server schemas](mapping-source-and-target-databases-accesstosql.md).

> [!IMPORTANT]  
> Before you try to connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], make sure that your IP address is allowed through the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] firewall.

To connect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]:

1. On the **File** menu, select **Connect to SQL Azure** (this option is enabled after the creation of a project).

   If you previously connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the command name is **Reconnect to SQL Azure**.

1. In the connection dialog box, enter or select the server name of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

1. Enter, select, or **Browse** the Database name.

1. Enter or select **Username**.

1. Enter the **Password**.

1. SSMA recommends encrypted connection to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

1. Select **Connect**.

If there are no databases in the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you can create the first database using **Create Azure Database** option that appears on the select of **Browse** button.

## Synchronize Azure SQL Database metadata

Metadata about databases in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] isn't automatically updated. The metadata in **Azure SQL Database Metadata Explorer** is a snapshot of the metadata when you first connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object. To synchronize metadata:

1. Make sure that you are connected to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

1. In **Azure SQL Database Metadata Explorer**, select the check box next to the database or database schema that you want to update.
   For example, to update the metadata for all databases, select the box next to **Databases**.

1. Right-click **Databases**, or the individual database or database schema, and then select **Synchronize with Database**.

## Refresh Azure SQL Database metadata

If [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] schemas change after you connect, you can refresh metadata from the server.

To refresh [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] metadata:

- In **Azure SQL Database Metadata Explorer**, right-click **Databases**, and then select **Refresh from Database**.

## Reconnect to Azure SQL Database

Your connection to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and migrate data.

The procedure for reconnecting to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is the same as the procedure for establishing a connection.

## See also

- [Migrating Access databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)

## Next steps

The next step in the migration depends on your project needs:

- [Mapping Access databases to SQL Server schemas](mapping-source-and-target-databases-accesstosql.md)
- [Setting project options](setting-conversion-and-migration-options-accesstosql.md)
- [Mapping source and target data types](mapping-source-and-target-data-types-accesstosql.md)
- [Converting Access databases](converting-access-database-objects-accesstosql.md)
