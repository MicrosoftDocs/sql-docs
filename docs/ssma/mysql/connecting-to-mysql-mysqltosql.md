---
title: "Connecting to MySQL (MySQLToSQL)"
description: Learn how to connect to a target MySQL database to migrate a MySQL database. SSMA obtains metadata about databases in Azure SQL Database.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.mysql.connectmysqlform.f1"
  - "ssma.mysql.connecttosource.f1"
helpviewer_keywords:
  - "Connecting to MySQL, MySQL permission"
  - "Connecting to MySQL,reconnecting"
---
# Connecting to MySQL (MySQLToSQL)

To migrate MySQL databases to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Azure SQL, you must connect to the MySQL database that you want to migrate. When you connect, SQL Server Migration Assistant (SSMA) obtains metadata about all MySQL schemas, and then displays it in the MySQL Metadata Explorer pane. SSMA stores information about the database server, but doesn't store passwords.

Your connection to the database stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database.

Metadata about the MySQL database isn't automatically updated. Instead, if you want to update the metadata in MySQL Metadata Explorer, you must manually update it. For more information, see the "Refreshing MySQL Metadata" section later in this article.

## Required MySQL Permissions

The account that is used to connect to the MySQL database must have at least **CONNECT** permissions. This enables SSMA to obtain metadata from schemas owned by the connecting user. To obtain metadata for objects in other schemas and then convert objects in those schemas, the account must have the following permissions:

- 'SHOW' privileges on database objects

- 'SELECT' privilege on 'Information_schema'

- 'SELECT' privilege on mysql (for UDFs)

## Establish a Connection to MySQL

When you connect to a database, SSMA reads the database metadata, and then adds this metadata to the project file. This metadata is used by SSMA when it converts objects to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Azure SQL syntax, and when it migrates data to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Azure SQL. You can browse this metadata in the MySQL Metadata Explorer pane and review properties of individual database objects.

> [!IMPORTANT]  
> Before you try to connect, make sure that the database server is running and can accept connections.

**To connect to MySQL**

1. On the **File** menu, select **Connect to MySQL** (this option will be enabled after the creation of project).

    If you are previously connected to MySQL, the command name is **Reconnect to MySQL**.

1. In the **Provider** box, select MySQL ODBC 5.1 Driver (trusted). This driver is the default provider in the standard mode.

1. In the **Mode** box, select **Standard mode** (the default mode).

    Use standard mode to specify the server name and port.

1. In **Standard mode**, provide the following values:

    1. In the **Server name** box, enter the MySQL server name. In the **Server port** box, enter the port number to be `3306` (the default port).

    1. In the **User name** box, enter a MySQL account that has the necessary permissions.

    1. In the **Password** box, enter the password for the specified user name.

1. **SSL:** If you want to securely connect to MySQL, make use of TLS or Secure Socket Layer (SSL) by checking the **SSL** checkbox.

1. **Configure:** Use this option to configure the connection to MySQL through TLS/SSL.

    > [!NOTE]  
    > To enable **Configure**, SSL must be set to **True**.

    On selecting the button "Configure", a dialog-box appears. To use encryption while connecting to MySQL Database, path to the following three certificate files present in the dialog-box must be defined [Privacy Enhanced Mail Certificates (PEM)]:

    - **SSL Certificate Authority:** Specifies the path to a file with a list of trust SSL CAs.

    - **SSL Certificate:** Specifies the name of the SSL certificate file to use for establishing a secure connection.

    - **SSL Key:** Specifies the name of the SSL key file to use for establishing a secure connection.

    > [!NOTE]  
    > - The **OK** button is enabled when the required information has been provided. If any of the file paths are invalid, the "OK" button will remain disabled.
    > - The **Cancel** button closes the dialog box and **turns off** the SSL option from the main Connection Form.

1. For more information, see [Connect to MySQL (MySQLToSQL)](connect-to-mysql-mysqltosql.md)

## Reconnect to MySQL

Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database. You can work offline until you want to update metadata, load database objects into [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Azure SQL, and migrate data.

## Refresh MySQL Metadata

Metadata about the MySQL database isn't automatically refreshed. The metadata in MySQL Metadata Explorer is a snapshot of the metadata when you first connected, or the last time that you manually refreshed metadata. You can manually update metadata for all schemas, a single schema, or individual database objects.

#### Refresh metadata

1. Make sure that you are connected to the database.

1. In MySQL Metadata Explorer, select the check box next to each schema or database object that you want to update.

1. Right-click **Schemas**, or the individual schema or database object, and then select **Refresh from Database**.

    If you don't have an active connection, SSMA displays the **Connect to MySQL** dialog box so that you can connect.

1. In the Refresh from Database dialog box, specify which objects to refresh.

   - To refresh an object, select the **Active** field next to the object until an arrow appears.

   - To prevent an object from being refreshed, select the **Active** field next to the object until an **X** appears.

   - To refresh or decline a category of objects, select the **Active** field next to the category folder.

   - To view the definitions of the color coding, select the **Legend** button.

1. Select **OK**.

## See also

- [Migrating MySQL Databases to SQL Server - Azure SQL Database (MySQLToSQL)](migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)

## Next steps

- The next step in the migration process is [Connecting to SQL Server (MySQLToSQL)](connecting-to-sql-server-mysqltosql.md)
