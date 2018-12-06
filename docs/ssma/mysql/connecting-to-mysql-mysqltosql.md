---
title: "Connecting to MySQL (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Connecting to MySQL, MySQL permission"
  - "Connecting to MySQL,reconnecting"
ms.assetid: 084c7020-f729-4f91-90e0-143f85fa68d1
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connecting to MySQL (MySQLToSQL)
To migrate MySQL databases to SQL Server or SQL Azure, you must connect to the MySQL database that you want to migrate. When you connect, SSMA obtains metadata about all MySQL schemas, and then displays it in the MySQL Metadata Explorer pane. SSMA stores information about the database server, but does not store passwords.  
  
Your connection to the database stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database.  
  
Metadata about the MySQL database is not automatically updated. Instead, if you want to update the metadata in MySQL Metadata Explorer, you must manually update it. For more information, see the "Refreshing MySQL Metadata" section later in this topic.  
  
## Required MySQL Permissions  
The account that is used to connect to the MySQL database must have at least **CONNECT** permissions. This enables SSMA to obtain metadata from schemas owned by the connecting user. To obtain metadata for objects in other schemas and then convert objects in those schemas, the account must have the following permissions:  
  
-   'SHOW' privileges on database objects  
  
-   'SELECT' privilege on 'Information_schema'  
  
-   'SELECT' privilege on mysql (for UDFs)  
  
## Establishing a Connection to MySQL  
When you connect to a database, SSMA reads the database metadata, and then adds this metadata to the project file. This metadata is used by SSMA when it converts objects to SQL Server or SQL Azure syntax, and when it migrates data to SQL Server or SQL Azure. You can browse this metadata in the MySQL Metadata Explorer pane and review properties of individual database objects.  
  
> [!IMPORTANT]  
> Before you try to connect, make sure that the database server is running and can accept connections.  
  
**To connect to MySQL**  
  
1.  On the **File** menu, select **Connect to MySQL** (this option will be enabled after the creation of project).  
  
    If you are previously connected to MySQL, the command name will be **Reconnect to MySQL**.  
  
2.  In the **Provider** box, select MySQL ODBC 5.1 Driver (trusted). It is the default provider in the standard mode.  
  
3.  In the **Mode** box, select **Standard mode**. It is the default mode.  
  
    Use standard mode to specify the server name and port.  
  
4.  In **Standard mode**, provide the following values:  
  
    1.  In the **Server name** box, enter the MySQL server name. In the **Server port** box, enter the port number to be 3306. It is the default port.  
  
    2.  In the **User name** box, enter a MySQL account that has the necessary permissions.  
  
    3.  In the **Password** box, enter the password for the specified user name.  
  
5.  **SSL:** If you want to securely connect to MySQL, make use of Secure Socket Layer (SSL) by checking the **SSL** checkbox.  
  
6.  **Configure:** It provides an option to configure the connection to MySQL through Secure Socket Layer (SSL).  
  
    > [!NOTE]  
    > To enable **Configure**, SSL must be set to **True**.  
  
    On clicking the button "Configure", a dialog-box appears. To use encryption while connecting to MySQL Database, path to the following three certificate files present in the dialog-box must be defined [Privacy Enhanced Mail Certificates (PEM)]:  
  
    -   **SSL Certificate Authority:** Specifies the path to a file with a list of trust SSL CAs'.  
  
    -   **SSL Certificate:** Specifies the name of the SSL certificate file to use for establishing a secure connection.  
  
    -   **SSL KEY:** Specifies the name of the SSL key file to use for establishing a secure connection.  
  
    > [!NOTE]  
    > -   The **OK** button is enabled when the required information has been provided. If any of the file paths are invalid, the "OK" button will remain disabled.  
    > -   The **Cancel** button closes the dialog box and **turns off** the SSL option from the main Connection Form.  
  
7.  For more information, see [Connect to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connect-to-mysql-mysqltosql.md)  
  
## Reconnecting to MySQL  
Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the database. You can work offline until you want to update metadata, load database objects into SQL Server or SQL Azure, and migrate data.  
  
## Refreshing MySQL Metadata  
Metadata about the MySQL database is not automatically refreshed. The metadata in MySQL Metadata Explorer is a snapshot of the metadata when you first connected, or the last time that you manually refreshed metadata. You can manually update metadata for all schemas, a single schema, or individual database objects.  
  
**To refresh metadata**  
  
1.  Make sure that you are connected to the database.  
  
2.  In MySQL Metadata Explorer, select the check box next to each schema or database object that you want to update.  
  
3.  Right-click **Schemas**, or the individual schema or database object, and then select **Refresh from Database**.  
  
    If you do not have an active connection, SSMA will display the **Connect to MySQL** dialog box so that you can connect.  
  
4.  In the Refresh from Database dialog box, specify which objects to refresh.  
  
    -   To refresh an object, click the **Active** field adjacent to the object until an arrow appears.  
  
    -   To prevent an object from being refreshed, click the **Active** field adjacent to the object until an **X** appears.  
  
    -   To refresh or decline a category of objects, click the **Active** field adjacent to the category folder.  
  
    -   To view the definitions of the color coding, click the **Legend** button.  
  
5.  Click **OK**.  
  
## Next Step  
The next step in the migration process is [Connecting to SQL Server &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md)  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
