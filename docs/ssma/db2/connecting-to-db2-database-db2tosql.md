---
title: "Connecting to DB2 Database (DB2ToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 5eb5801d-f0c3-4127-97c0-0b1ef49f4844
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connecting to DB2 Database (DB2ToSQL)
To migrate DB2 databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the DB2 database that you want to migrate. When you connect, SSMA obtains metadata about all DB2 schemas, and then displays it in the DB2 Metadata Explorer pane. SSMA stores information about the database server, but does not store passwords.  
  
Your connection to the database stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database.  
  
Metadata about the DB2 database is not automatically updated. Instead, if you want to update the metadata in DB2 Metadata Explorer, you must manually update it. For more information, see the "Refreshing DB2 Metadata" section later in this topic.  
  
## Required DB2 Permissions  
User authorization defines the list of the commands and objects that are available for a user. This list thereby controls user actions. In DB2, there are predetermined groups of privileges for authorization, both at the instance level and at the level of a DB2 database. This enables SSMA to obtain metadata from schemas owned by the connecting user. To obtain metadata for objects in other schemas and then convert objects in those schemas, the account must have the following permissions:  
  
-   Schema Access for schema migration is normally granted to PUBLIC unless the RESTRICT keyword was used in CREATE  
  
-   Data access for data migration requires DATAACCESS  
  
## Establishing a Connection to DB2  
When you connect to a database, SSMA reads the database metadata, and then adds this metadata to the project file. This metadata is used by SSMA when it converts objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and when it migrates data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can browse this metadata in the DB2 Metadata Explorer pane and review properties of individual database objects.  
  
> [!IMPORTANT]  
> Before you try to connect, make sure that the database server is running and can accept connections.  
  
**To connect to DB2**  
  
1.  On the **File** menu, select **Connect to DB2**.  
  
    If you previously connected to DB2, the command name will be **Reconnect to DB2**.  
  
2.  In the **Provider** box you will see the **OLE DB Provider** which is currently the only DB2 client access provider.  
  
3.  In the **Manager** box you can select either **Db2 for zOs**, or **DB2 for LUW**  
  
4.  In the **Mode** box, select either **Standard mode**, or **Connection string mode**.  
  
    Use standard mode to specify the server name and port. Use service name mode to specify the DB2 service name manually. Use connection string mode to provide a full connection string.  
  
5.  If you select **Standard mode**, provide the following values:  
  
    -   In the **Server name** box, enter or select the name or IP address of the database server.  
  
    -   If the database server is not configured to accept connections on the default port (1521), enter the port number that is used for DB2 connections in the **Server port** box.  
  
    -   In the **Server Port** box, enter the TCP/IP Port number.  
  
    -   In the **Initial Catalog** box, enter the database name  
  
    -   In the **User name** box, enter an DB2 account that has the necessary permissions.  
  
    -   In the **Password** box, enter the password for the specified user name.  
  
6.  If you select **Connection string mode**, provide a connection string in the **Connection string** box.  
  
    The following example shows an OLE DB connection string:  
  
    `Provider=OraOLEDB.DB2;Data Source=MyDB2DB;User Id=myUsername;Password=myPassword;`  
  
    The following example shows an DB2 Client connection string that uses integrated security:  
  
    `Data Source=MyDB2DB;Integrated Security=yes;`  
  
    For more information, see [Connect To Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/connect-to-oracle-oracletosql.md).  
  
## Reconnecting to DB2  
Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database. You can work offline until you want to update metadata, load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and migrate data.  
  
## Refreshing DB2 Metadata  
Metadata about the DB2 database is not automatically refreshed. The metadata in DB2 Metadata Explorer is a snapshot of the metadata when you first connected, or the last time that you manually refreshed metadata. You can manually update metadata for all schemas, a single schema, or individual database objects.  
  
**To refresh metadata**  
  
1.  Make sure that you are connected to the database.  
  
2.  In DB2 Metadata Explorer, select the check box next to each schema or database object that you want to update.  
  
3.  Right-click **Schemas**, or the individual schema or database object, and then select **Refresh from Database**.  
  
    If you do not have an active connection, SSMA will display the **Connect to DB2** dialog box so that you can connect.  
  
4.  In the Refresh from Database dialog box, specify which objects to refresh.  
  
    -   To refresh an object, click the **Active** field adjacent to the object until an arrow appears.  
  
    -   To prevent an object from being refreshed, click the **Active** field adjacent to the object until an **X** appears.  
  
    -   To refresh or decline a category of objects, click the **Active** field adjacent to the category folder.  
  
    To view the definitions of the color coding, click the **Legend** button.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## Next Step  
  
-   The next step in the migration process is to [Connecting to SQL Server](https://msdn.microsoft.com/b59803cb-3cc6-41cc-8553-faf90851410e).  
  
## See Also  
[Migrating DB2 Databases to SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-databases-to-sql-server-db2tosql.md)  
  
