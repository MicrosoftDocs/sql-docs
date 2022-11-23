---
title: "Connecting to Oracle Database (OracleToSQL) | Microsoft Docs"
description: Learn how to connect to the Oracle database to migrate that Oracle database to SQL Server. SSMA obtains and displays metadata about all Oracle schemas.
author: cpichuka

ms.service: sql
ms.custom: ""
ms.date: "06/04/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Refreshing Oracle Metadata"
ms.assetid: e276cdbf-3ebc-4ba8-b40d-a7a42befa2b6
ms.author: cpichuka
f1_keywords: 
  - "ssma.oracle.connectoracleform.f1"
---

# Connecting to Oracle Database (OracleToSQL)

To migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the Oracle database that you want to migrate. When you connect, SSMA obtains metadata about all Oracle schemas, and then displays it in the Oracle Metadata Explorer pane. SSMA stores information about the database server, but does not store passwords.

Your connection to the database stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database.

Metadata about the Oracle database is not automatically updated. Instead, if you want to update the metadata in Oracle Metadata Explorer, you must manually update it. For more information, see the "Refreshing Oracle Metadata" section later in this topic.

## Required Oracle permissions

At minimum, the account that is used to connect to the Oracle database must have the following permissions:

- `CONNECT`  
  Required to connect (create a session) to the database.

- `SELECT ANY DICTIONARY`  
  Required to query system dictionary tables (for example, `SYS.MLOG$`) in order to discover all objects.

This will allow SSMA to load all objects in the schema owned by the connecting user. In most real-world scenarios there are cross-schema references between stored procedures and SSMA will need to be able to discover all referenced objects for a successful conversion. To obtain metadata for objects defined in other schemas, the account must have the following additional permissions:

- `SELECT ANY TABLE`  
  Required to discover tables, views, materialized views and synonyms in other schemas.

- `SELECT ANY SEQUENCE`  
  Required to discover sequences in other schemas.

- `CREATE ANY PROCEDURE`  
  Required to discover PL/SQL for procedures, functions and packages in other schemas.

- `CREATE ANY TRIGGER`  
  Required to discover trigger definitions in other schemas.

- `CREATE ANY TYPE`  
  Required to discover types defined in other schemas.

Some of the SSMA features require additional permissions. For instance, if you want to use [Tester](testing-migrated-database-objects-oracletosql.md) and [Backup Management](managing-backups-oracletosql.md) functionality, you will need to grant your connecting user the following:

- `EXECUTE ANY PROCEDURE`  
  Required to run procedures and functions you would like to test in all schemas.

- `CREATE ANY TABLE` and `ALTER ANY TABLE`  
  Required to create and modify temporary tables for change tracking and backups.

- `INSERT ANY TABLE` and `UPDATE ANY TABLE`  
  Required to insert change tracking and backup data into temporary tables.

- `DROP ANY TABLE`  
  Required to drop temporary tables used for change tracking and backups.

- `CREATE ANY INDEX` and `ALTER ANY INDEX`  
  Required to create and modify indexes on temporary tables used for change tracking and backups.

- `DROP ANY INDEX`  
  Required to drop indexes on temporary tables used for change tracking and backups.

- `CREATE ANY TRIGGER` and `ALTER ANY TRIGGER`  
  Required to create and modify temporary triggers used for change tracking.

- `DROP ANY TRIGGER`  
  Required to drop temporary triggers used for change tracking.

> [!NOTE]
> This is a generic set of permissions required for SSMA to operate properly. If you want to narrow down the scope of your migration to a subset of schemas you can do so by granting above permissions to the limited set of objects, instead of `ALL`. While possible, it might be very hard to correctly identify all dependencies, thus preventing SSMA from functioning properly. It is highly recommended to stick to the generic set as defined above to eliminate any potential permission issues during migration process.

## Establishing a connection to Oracle

When you connect to a database, SSMA reads the database metadata, and then adds this metadata to the project file. This metadata is used by SSMA when it converts objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and when it migrates data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can browse this metadata in the Oracle Metadata Explorer pane and review properties of individual database objects.

> [!IMPORTANT]
> Before you try to connect, make sure that the database server is running and can accept connections.

**To connect to Oracle**

1. On the **File** menu, select **Connect to Oracle**.  
   If you previously connected to Oracle, the command name will be **Reconnect to Oracle**.
  
2. In the **Provider** box, select **Oracle Client Provider** or **OLE DB Provider**, depending on which provider is installed. The default is Oracle client.

3. In the **Mode** box, select either **Standard mode**, **TNSNAME mode**, or **Connection string mode**.  
   Use standard mode to specify the server name and port. Use service name mode to specify the Oracle service name manually. Use connection string mode to provide a full connection string.

4. If you select **Standard mode**, provide the following values:
   1. In the **Server name** box, enter or select the name or IP address of the database server.
   2. If the database server is not configured to accept connections on the default port (1521), enter the port number that is used for Oracle connections in the **Server port** box.
   3. In the **Oracle SID** box, enter the system identifier.
   4. In the **User name** box, enter an Oracle account that has the necessary permissions.
   5. In the **Password** box, enter the password for the specified user name.

5. If you select **TNSNAME mode**, provide the following values:
   1. In the **Connect identifier** box, enter connect identifier (TNS alias) of the database.
   2. In the **User name** box, enter an Oracle account that has the necessary permissions.
   3. In the **Password** box, enter the password for the specified user name.
  
6. If you select **Connection string mode**, provide a connection string in the **Connection string** box.  
   The following example shows an OLE DB connection string:

   `Provider=OraOLEDB.Oracle;Data Source=MyOracleDB;User Id=myUsername;Password=myPassword;`

   The following example shows an Oracle Client connection string that uses integrated security:

   `Data Source=MyOracleDB;Integrated Security=yes;`

   For more information, see [Connect To Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/connect-to-oracle-oracletosql.md).

## Reconnecting to Oracle

Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database. You can work offline until you want to update metadata, load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and migrate data.

## Refreshing Oracle metadata

Metadata about the Oracle database is not automatically refreshed. The metadata in Oracle Metadata Explorer is a snapshot of the metadata when you first connected, or the last time that you manually refreshed metadata. You can manually update metadata for all schemas, a single schema, or individual database objects.

**To refresh metadata**

1. Make sure that you are connected to the database.

2. In Oracle Metadata Explorer, select the check box next to each schema or database object that you want to update.

3. Right-click **Schemas**, or the individual schema or database object, and then select **Refresh from Database**.  
   If you do not have an active connection, SSMA will display the **Connect to Oracle** dialog box so that you can connect.

4. In the Refresh from Database dialog box, specify which objects to refresh.

   - To refresh an object, click the **Active** field adjacent to the object until an arrow appears.
   - To prevent an object from being refreshed, click the **Active** field adjacent to the object until an **X** appears.
   - To refresh or decline a category of objects, click the **Active** field adjacent to the category folder.

   To view the definitions of the color coding, click the **Legend** button.

5. Select **OK**.

## Next steps

The next step in the migration process is to [Connect to an instance of SQL Server](connecting-to-sql-server-oracletosql.md).

## See also

[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)
