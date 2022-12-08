---
title: "Connecting to SQL Server (AccessToSQL) | Microsoft Docs"
description: Learn how to connect to a target instance of SQL Database to migrate Access databases. SSMA obtains metadata about databases in SQL Database.
ms.service: sql
ms.custom: ""
ms.date: "11/16/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "authentication"
  - "instance of SQL Server"
  - "metadata, refreshing"
  - "ports"
  - "refreshing metadata"
  - "spaces in database names"
  - "special characters"
  - "SQL Server"
  - "SQL Server, connecting"
  - "SQL Server, connecting to"
  - "SQL Server, reconnecting"
ms.assetid: f84cf007-ddf1-4396-a07c-3e0729abc769
author: cpichuka 
ms.author: cpichuka 
---

# Connecting to SQL Server (AccessToSQL)

To migrate Access databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the target instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When you connect, SSMA obtains metadata about the databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and displays database metadata in **SQL Server Metadata Explorer**. SSMA stores information about which instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you are connected to, but does not store passwords.

Your connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data.

Metadata about the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not automatically synchronized. Instead, to update the metadata in SQL Server Metadata Explorer, you must manually update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] metadata. For more information, see the "Synchronizing SQL Server Metadata" section later in this topic.

## Required SQL Server Permissions

The account that is used to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires different permissions depending on the actions that the account performs:

- To convert Access objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to save converted syntax to scripts, the account must have permission to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- To load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the account must be a member of the **db_ddladmin** database role.

- To migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the account must be a member of the **db_owner** database role.

## Establishing a SQL Server Connection

Before you convert Access database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, you must establish a connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to migrate the Access databases.

When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the Access database level after you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Mapping Source and Target Databases](mapping-source-and-target-databases-accesstosql.md).

> [!IMPORTANT]
> Before you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running and can accept connections.

To connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:

1. On the **File** menu, select **Connect to SQL Server**.
   If you previously connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the command name will be **Reconnect to SQL Server**.

2. In the **Server name** box, enter or select the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
   - If you are connecting to the default instance on the local computer, you can enter `localhost` or a dot (`.`).
   - If you are connecting to the default instance on another computer, enter the name of the computer.
   - If you are connecting to a named instance, enter the computer name, a backslash, and the instance name. For example: `MyServer\MyInstance`.
   - To connect to an active user instance of [!INCLUDE[ssExpress](../../includes/ssexpress_md.md)], connect by using named pipes protocol and specifying the pipe name, such as `\\.\pipe\sql\query`. For more information, see the [!INCLUDE[ssExpress](../../includes/ssexpress_md.md)] documentation.

3. If your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept connections on a non-default port, enter the port number that is used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections in the **Server port** box. For the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default port number is 1433. For named instances, SSMA will try to obtain the port number from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service.

4. In the **Database** box, enter the name of the target database for object and data migration.
   This option is not available when reconnecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
   The target database name cannot contain spaces or special characters. For example, you can migrate Access databases to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database named `abc`. But you cannot migrate Access databases to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database named `a b-c`.
   You can customize this mapping per database after you connect. For more information, see [Mapping Source and Target Databases](mapping-source-and-target-databases-accesstosql.md)

5. In the **Authentication** drop-down menu, select the authentication type to use for the connection. To use the current Windows account, select **Windows Authentication**. To use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, select **SQL Server Authentication**, and then provide a user name and password.

6. For Secure connection, two controls are added, **Encrypt Connection** Checkbox and **TrustServerCertificate** Checkbox. Only when **Encrypt Connection** Checkbox is checked **TrustServerCertificate** Checkbox is visible. When **Encrypt Connection** is checked(true) and **TrustServerCertificate** is unchecked(false), will validate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. Validating the server certificate is a part of the SSL handshake and ensures that the server is the correct server to connect to. To ensure this, a certificate must be installed on the client side as well as on the server side.

7. Click **Connect**.

> [!IMPORTANT]
> While you may connect to a higher version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], compared to the version chosen when the migration project was created, conversion of the database objects is determined by the target version of the project and not the version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connected to.

## Synchronizing SQL Server Metadata

If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schemas change after you connect, you can synchronize the metadata with the server.

To synchronize SQL Server metadata, **SQL Server Metadata Explorer**, right click **Databases**, and then select **Synchronize with Database**.

## Reconnecting to SQL Server

Your connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stays active until you close the project. When you reopen the project, you must reconnect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if you want an active connection to the server. You can work offline until you load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data.

The procedure for reconnecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the same as the procedure for establishing a connection.

## Next Steps

If you want to customize the mapping between source and target databases, see [Mapping Source and Target Databases](mapping-source-and-target-databases-accesstosql.md) Otherwise, the next step is to convert database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax using [convert database objects](converting-access-database-objects-accesstosql.md).

## See Also

[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
