---
title: "Connecting to SQL Server (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to SQL Server 2008, SQL Server permission"
  - "connecting to SQL Server 2008, synchronization"
ms.assetid: 08233267-693e-46e6-9ca3-3a3dfd3d2be7
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connecting to SQL Server (MySQLToSQL)
To migrate MySQL databases to SQL Server, you must connect to the target instance of SQL Server. When you connect, SSMA obtains metadata about all the databases in the instance of SQL Server and displays database metadata in the SQL Server Metadata Explorer. SSMA stores information of the instance of SQL Server you are connected to, but does not store passwords.  
  
Your connection to SQL Server stays active until you close the project. When you reopen the project, you must reconnect to SQL Server if you want an active connection to the server. You can work offline until you load database objects into SQL Server and migrate data.  
  
Metadata about the instance of SQL Server is not automatically synchronized. Instead, to update the metadata in SQL Server Metadata Explorer, you must manually update the SQL Server metadata. For more information, see the "Synchronizing SQL Server Metadata" section later in this topic.  
  
## Required SQL Server Permissions  
The account that is used to connect to SQL Server requires different permissions depending on the actions that the account performs:  
  
-   To convert MySQL objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, to update metadata from SQL Server, or to save converted syntax to scripts, the account must have permission to log on to the instance of SQL Server.  
  
-   To load database objects into SQL Server, the minimum permission requirement is membership in the **db_owner** database role in the target database.  
  
## Establishing a SQL Server Connection  
Before you convert MySQL database objects to SQL Server syntax, you must establish a connection to the instance of SQL Server where you want to migrate the MySQL database or databases.  
  
When you define the connection properties, you also specify the database where objects and data will be migrated. You can customize this mapping at the MySQL schema level after you connect to SQL Server. For more information, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md)  
  
> [!IMPORTANT]  
> Before you try to connect to SQL Server, make sure that the instance of SQL Server is running and can accept connections.  
  
**To connect to SQL Server**  
  
1.  On the **File** menu, select **Connect to SQL Server** (this option is enabled after the creation of a project).  
  
    If you have previously connected to SQL Server, the command name will be **Reconnect to SQL Server**.  
  
2.  In the connection dialog box, enter or select the name of the instance of SQL Server.  
  
    -   If you are connecting to the default instance on the local computer, you can enter **localhost** or a dot (**.**).  
  
    -   If you are connecting to the default instance on another computer, enter the name of the computer.  
  
    -   If you are connecting to a named instance on another computer, enter the computer name followed by a backslash and then the instance name, such as MyServer\MyInstance.  
  
3.  If your instance of SQL Server is configured to accept connections on a non-default port, enter the port number that is used for SQL Server connections in the **Server port** box. For the default instance of SQL Server, the default port number is 1433. For named instances, SSMA will try to obtain the port number from the SQL Server Browser Service.  
  
4.  In the **Authentication** box, select the authentication type to use for the connection. To use the current Windows account, select **Windows Authentication**. To use a SQL Server login, select SQL Server Authentication, and then provide the login name and password.  
  
5.  For Secure connection, two controls are added, the **Encrypt Connection** and **TrustServerCertificate** check boxes. Only when **Encrypt Connection** is checked, the **TrustServerCertificate** check box is visible. When **Encrypt Connection** is checked (true) and **TrustServerCertificate** is unchecked (false), it will validate the SQL Server SSL certificate. Validating the server certificate is a part of the SSL handshake and ensures that the server is the correct server to connect to. To ensure this, a certificate must be installed on the client side as well as on the server side.  
  
6.  Click Connect.  
  
**Higher version compatibility**  
  
It is allowed to connect/reconnect to higher versions of SQL Server.  
  
1.  You will be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 when the project created is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005.  
  
2.  You will be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 when the project created is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 but it is not allowed to connect to lower versions i.e. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005.  
  
3.  You will be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 when the project created is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012.  
  
4.  You will be able to connect to only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014 or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 when the project created is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014.  
  
5.  Higher version compatibility is not valid for "SQL Azure".  
  
||||||||  
|-|-|-|-|-|-|-|  
|**PROJECT TYPE Vs TARGET SERVER VERSION**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005<br /> (Version: 9.x)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008<br /> (Version: 10.x)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012<br />(Version:11.x)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014<br />(Version:12.x)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016<br />(Version:13.x)|SQL Azure|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005|Yes|Yes|Yes|Yes|Yes||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008||Yes|Yes|Yes|Yes||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012|||Yes|Yes|Yes||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]2014||||Yes|Yes||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]2016|||||Yes||  
|SQL Azure||||||Yes|  
  
> [!IMPORTANT]  
> Conversion of the database objects is carried out as per the project type but not as per the version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connected to. In case of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005 project, Conversion is carried out as per [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005 even though you are connected to a higher version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SQL Server 2008/SQL Server 2012/SQL Server 2014/SQL Server 2016).  
  
## Synchronizing SQL Server Metadata  
Metadata about SQL Server databases is not automatically updated. The metadata in SQL Server Metadata Explorer is a snapshot of the metadata when you first connected to SQL Server, or the last time that you manually updated metadata. You can manually update metadata for all databases, or for any single database or database object.  
  
**To synchronize metadata**  
  
1.  Make sure that you are connected to SQL Server.  
  
2.  In SQL Server Metadata Explorer, select the check box next to the database or database schema that you want to update.  
  
    For example, to update the metadata for all databases, select the box next to Databases.  
  
3.  Right-click Databases, or the individual database or database schema, and then select **Synchronize with Database**.  
  
## Next Step  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping between MySQL schemas and SQL Server databases and schemas, see [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md)  
  
-   To customize configuration options for the projects, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md)  
  
-   To customize the mapping of source and target data types, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md)  
  
-   If you do not have to perform any of these tasks, you can convert the MySQL database object definitions into SQL Server object definitions. For more information, see [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md)  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
