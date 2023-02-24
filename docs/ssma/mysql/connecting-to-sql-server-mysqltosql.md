---
title: "Connecting to SQL Server (MySQLToSQL) | Microsoft Docs"
description: Learn how to connect to a target instance of SQL Server to migrate MySQL databases. SSMA obtains metadata about databases in SQL Server.
ms.service: sql
ms.custom: ""
ms.date: "11/16/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to SQL Server 2008, SQL Server permission"
  - "connecting to SQL Server 2008, synchronization"
ms.assetid: 08233267-693e-46e6-9ca3-3a3dfd3d2be7
author: cpichuka 
ms.author: cpichuka
f1_keywords: 
    - "ssma.mysql.connecttotarget.f1" 
    - "ssma.mysql.connectmssqlform.f1"
---

# Connecting to SQL Server (MySQLToSQL)

To migrate MySQL databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the target instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When you connect, SSMA obtains metadata about all the databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and displays database metadata in the **SQL Server Metadata Explorer**. SSMA stores information of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connected to, but does not store passwords.

Your connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data.

Metadata about the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not automatically synchronized. Instead, to update the metadata in **SQL Server Metadata Explorer**, you must manually update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] metadata. For more information, see the "Synchronizing SQL Server Metadata" section later in this topic.

## Required SQL Server Permissions

The account that is used to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires different permissions depending on the actions that the account performs:

- To convert MySQL objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to save converted syntax to scripts, the account must have permission to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- To load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the account must be a member of the **db_ddladmin** database role.

- To migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the account must be:
  - A member of the **db_owner** database role, if using client-side data migration engine.
  - A member of the **sysadmin** server role, if using server-side data migration engine. This is required to create the `CmdExec` [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step during data migration to run SSMA bulk copy tool.
    > [!NOTE]
    > [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy accounts are not supported by the server-side data migration.

## Establishing a SQL Server Connection

Before you convert MySQL database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, you must establish a connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to migrate the MySQL database or databases.

When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the MySQL schema level after you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md).

> [!IMPORTANT]
> Before you try to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running and can accept connections.

To connect to SQL Server:

1. On the **File** menu, select **Connect to SQL Server** (this option is enabled after the creation of a project).
   If you have previously connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the command name will be **Reconnect to SQL Server**.

2. In the connection dialog box, enter or select the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
   - If you are connecting to the default instance on the local computer, you can enter `localhost` or a dot (`.`).
   - If you are connecting to the default instance on another computer, enter the name of the computer.
   - If you are connecting to a named instance on another computer, enter the computer name followed by a backslash and then the instance name, such as `MyServer\MyInstance`.

3. If your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept connections on a non-default port, enter the port number that is used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections in the **Server port** box. For the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default port number is 1433. For named instances, SSMA will try to obtain the port number from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service.

4. In the **Authentication** box, select the authentication type to use for the connection. To use the current Windows account, select **Windows Authentication**. To use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, select **SQL Server Authentication**, and then provide the login name and password.

5. For Secure connection, two controls are added, the **Encrypt Connection** and **TrustServerCertificate** check boxes. Only when **Encrypt Connection** is checked, the **TrustServerCertificate** check box is visible. When **Encrypt Connection** is checked (true) and **TrustServerCertificate** is unchecked (false), it will validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. Validating the server certificate is a part of the SSL handshake and ensures that the server is the correct server to connect to. To ensure this, a certificate must be installed on the client side as well as on the server side.

6. Click **Connect**.

> [!IMPORTANT]
> While you may connect to a higher version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], compared to the version chosen when the migration project was created, conversion of the database objects is determined by the target version of the project and not the version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connected to.

## Synchronizing SQL Server Metadata

Metadata about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases is not automatically updated. The metadata in **SQL Server Metadata Explorer** is a snapshot of the metadata when you first connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object. To synchronize metadata:

1. Make sure that you are connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

2. In **SQL Server Metadata Explorer**, select the check box next to the database or database schema that you want to update.
   For example, to update the metadata for all databases, select the box next to **Databases**.

3. Right-click **Databases**, or the individual database or database schema, and then select **Synchronize with Database**.

## Next Step

The next step in the migration depends on your project needs:

- To customize the mapping between MySQL schemas and SQL Server databases and schemas, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md).
- To customize configuration options for the projects, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md).
- To customize the mapping of source and target data types, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md).
- If you do not have to perform any of these tasks, you can convert the MySQL database object definitions into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object definitions. For more information, see [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md).

## See Also

[Migrating MySQL Databases to SQL Server - Azure SQL Database &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
