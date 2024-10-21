---
title: "Connect to Db2 database (Db2ToSQL)"
description: Learn how to connect to a target instance of Db2 database to migrate Db2 databases. SSMA obtains metadata about all Db2 schemas.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Connect to Db2 database (Db2ToSQL)

To migrate Db2 databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the Db2 database that you want to migrate. When you connect, SQL Server Migration Assistant (SSMA) obtains metadata about all Db2 schemas, and then displays it in the Db2 Metadata Explorer pane. SSMA stores information about the database server, but doesn't store passwords.

Your connection to the database stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database.

Metadata about the Db2 database isn't automatically updated. Instead, if you want to update the metadata in Db2 Metadata Explorer, you must manually update it. For more information, see the [Refresh Db2 metadata](#refresh-db2-metadata) section in this article.

## Required Db2 permissions

User authorization defines the list of the commands and objects that are available for a user. This list is what controls user actions. In Db2, there are predetermined groups of privileges for authorization, both at the instance level and at the level of a Db2 database. This enables SSMA to obtain metadata from schemas owned by the connecting user. To obtain metadata for objects in other schemas and then convert objects in those schemas, the account must have the following permissions:

- Schema Access for schema migration is normally granted to `PUBLIC` unless the `RESTRICT` keyword was used in `CREATE`
- Data access for data migration requires `DATAACCESS`

## Establish a connection to Db2

When you connect to a database, SSMA reads the database metadata, and then adds this metadata to the project file. This metadata is used by SSMA when it converts objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and when it migrates data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can browse this metadata in the Db2 Metadata Explorer pane and review properties of individual database objects.

> [!IMPORTANT]  
> Before you try to connect, make sure that the database server is running and can accept connections.

#### Connect to Db2

1. Navigate to **File** > **Connect to Db2**.

   If you previously connected to Db2, the command name is **Reconnect to Db2**.

1. In the **Provider** box, you see the **OLE DB Provider** which is currently the only Db2 client access provider.

1. In the **Manager** box you can select either **Db2 for zOS**, **Db2 for LUW** or **Db2 for i**

1. In the **Mode** box, select either **Standard mode**, or **Connection string mode**.

   Use standard mode to specify the server name and port. Use service name mode to specify the Db2 service name manually. Use connection string mode to provide a full connection string.

1. If you select **Standard mode**, provide the following values:

   - In the **Server name** box, enter or select the name or IP address of the database server.
   - If the database server isn't configured to accept connections on the default port (`1521`), enter the port number that is used for Db2 connections in the **Server port** box.
   - In the **Server Port** box, enter the TCP/IP port number.
   - In the **Initial Catalog** box, enter the database name.
   - In the **User name** box, enter a Db2 account that has the necessary permissions.
   - In the **Password** box, enter the password for the specified user name.

1. If you select **Connection string mode**, provide a connection string in the **Connection string** box.

   The following example shows an OLE DB connection string:

   `Provider=DB2OLEDB;Data Source=MyDb2DB;User Id=myUsername;Password=myPassword;`

   The following example shows a Db2 Client connection string that uses integrated security:

   `Data Source=MyDb2DB;Integrated Security=yes;`

   For more information, see [Connect to Db2](connect-to-db2-db2tosql.md).

## Reconnect to Db2

Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database. You can work offline until you want to update metadata, load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and migrate data.

## Refresh Db2 metadata

Metadata about the Db2 database isn't automatically refreshed. The metadata in Db2 Metadata Explorer is a snapshot of the metadata when you first connected, or the last time that you manually refreshed metadata. You can manually update metadata for all schemas, a single schema, or individual database objects.

#### Refresh metadata

1. Make sure that you're connected to the database.
1. In Db2 Metadata Explorer, select the check box next to each schema or database object that you want to update.
1. Right-click **Schemas**, or the individual schema or database object, and then select **Refresh from Database**.

   If you don't have an active connection, SSMA displays the **Connect to Db2** dialog box so that you can connect.

1. In the **Refresh from Database** dialog box, specify which objects to refresh.

   - To refresh an object, select the **Active** field next to the object until an arrow appears.
   - To prevent an object from being refreshed, select the **Active** field next to the object until an **X** appears.
   - To refresh or decline a category of objects, select the **Active** field next to the category folder.

     To view the definitions of the color coding, select the **Legend** button.

1. Select **OK**.

## Related content

- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
- [Connect to SQL Server (Db2ToSQL)](connecting-to-sql-server-db2tosql.md)
