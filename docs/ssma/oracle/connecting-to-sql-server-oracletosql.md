---
title: "Connecting to SQL Server (OracleToSQL)"
description: Learn how to connect to SQL Server to migrate an Oracle database. SSMA obtains and displays metadata for databases in SQL Server.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Connecting to SQL Server,Synchronizing SQL Server Metadata"
---

# Connecting to SQL Server (OracleToSQL)

To migrate Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must connect to the target instance of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When you connect, Microsoft SQL Server Migration Assistant (SSMA) for Oracle obtains metadata about all the databases in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and displays database metadata in SQL Server Metadata Explorer. SSMA stores information about which instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] you're connected to, but doesn't store passwords.

Your connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stays active until you close the project. When you reopen the project, if you want an active connection to the server, you must reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can work offline until you load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data.

Metadata about the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] isn't automatically synchronized. To update the metadata in SQL Server Metadata Explorer, you must manually update the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] metadata. For more information, see the [Synchronize SQL Server metadata](#synchronize-sql-server-metadata) section later in this article.

## Required SQL Server permissions

The account that is used to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] requires different permissions depending on the actions that the account performs.

In order to perform the following actions, the account must have permission to sign in to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- To convert Oracle objects to [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax
- To update metadata from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]
- To save converted syntax to scripts

To load database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the account must be a member of the **db_ddladmin** database role.

To migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the account must be:

- A member of the **db_owner** database role, if using the client-side data migration engine.
- A member of the **sysadmin** server role, if using the server-side data migration engine. This server role is required to create the `CmdExec` [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step during data migration to run the SSMA bulk copy tool.

  > [!NOTE]  
  > [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy accounts aren't supported by the server-side data migration.

- To run the code that SSMA generates, the account must have `EXECUTE` permissions for all user-defined functions in the `ssma_oracle` schema of the target database. These functions provide equivalent functionality of Oracle system functions, and are used by converted objects.

## Establish a SQL Server connection

Before you convert Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax, you must establish a connection to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where you want to migrate the Oracle database or databases.

When you define the connection properties, you also specify the database to which you want to migrate objects and data. You can customize this mapping at the Oracle schema level after you connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Map Oracle schemas to SQL Server schemas](mapping-oracle-schemas-to-sql-server-schemas-oracletosql.md).

> [!IMPORTANT]  
> Before you try to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running and can accept connections.

To connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance:

1. On the **File** menu, select **Connect to SQL Server**.
   If you previously connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the command name is **Reconnect to SQL Server**.

1. In the connection dialog, enter or select the name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
   - If you're connecting to the default instance on the local computer, you can enter `localhost` or a dot (`.`).
   - If you're connecting to the default instance on another computer, enter the name of the computer.
   - If you're connecting to a named instance on another computer, enter the computer name followed by a backslash, and then the instance name (example: `MyServer\MyInstance`).

1. If your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept connections on a non-default port, enter the port number that is used for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections in the **Server port** box. For the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the default port number is `1433`. For named instances, SSMA tries to obtain the port number from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.

1. In the **Database** box, enter the name of the target database.
   This option isn't available when you reconnect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. In the **Authentication** box, select the authentication type to use for the connection. To use the current Windows account, select **Windows Authentication**. To use a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] username, select **SQL Server Authentication**, and then provide the username and password.

1. For a secure connection, you can add two controls via the **Encrypt Connection** and **TrustServerCertificate** checkboxes. The **TrustServerCertificate** option is only visible after you select **Encrypt Connection**. When **Encrypt Connection** is checked (with a value of `true`) and **TrustServerCertificate** is unchecked (with a value of `false`), it validates the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] SSL certificate. Validating the server certificate is a part of the SSL handshake and ensures that you connect to the correct server. To ensure that this process works, a certificate must be installed on both the client side and the server side.

1. Select **Connect**.

> [!IMPORTANT]  
> You might connect to a later version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], compared to the version chosen when the migration project was created. Conversion of the database objects is determined by the target version of the project and not the version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] you're connected to.

## Synchronize SQL Server metadata

Metadata about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases doesn't automatically update. The metadata in SQL Server Metadata Explorer is either:

- A snapshot of the metadata that was present when you first connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- A snapshot of the metadata that you input the last time you manually updated metadata.

You can manually update metadata for all databases, or for any single database or database object. To synchronize the metadata:

1. Make sure that you're connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. In **SQL Server Metadata Explorer**, select the checkbox next to the database or database schema that you want to update.
   For example, to update the metadata for all databases, select the box next to **Databases**.

1. Right-click **Databases**, or the individual database or database schema, and then select **Synchronize with Database**.

## Related content

- [Map Oracle schemas to SQL Server schemas](mapping-oracle-schemas-to-sql-server-schemas-oracletosql.md)
- [Set project options](setting-project-options-oracletosql.md)
- [Map Oracle and SQL Server data types](mapping-oracle-and-sql-server-data-types-oracletosql.md)
- [Convert Oracle schemas](converting-oracle-schemas-oracletosql.md)
