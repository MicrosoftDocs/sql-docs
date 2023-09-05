---
title: Connect to an existing database in SSDT
description: "Connect to an existing database in SSDT using SQL Server Object Explorer"
author: "subasak"
ms.author: "subasak"
ms.reviewer: "drskwier"
ms.date: "09/05/2023"
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Connect to an existing database in SSDT

This article shows how a user can connect to an existing Database  in SQL Server Data Tools (SSDT). SSDT allows you to connect to an existing database, run query with Transact-SQL (T-SQL), and view the results.

SSDT also offers you a plethora of features that you can use to work with your database. These are explained in detail in the following sections. Let us understand how we can connect to an existing database.

To Connect to an existing database the following can be done / needs to be referred to:  
  
-   [Connect using SQL Server Object Explorer](#ConnectToSSOX)  
  
-   [Know about Authentication Types](#AuthTypes)  
  
-   [Encrypt and Trust Server Certificate](#EncryptTrustServerCertificate) 

## <a name="ConnectToSSOX"></a>Connect to a database using SQL Server Object Explorer 
**SQL Server Object Explorer (SSOX)** is a tool available in SQL Server Data Tools (SSDT) for Visual Studio. It allows you to connect to and manage SQL Server databases within Visual Studio. To connect to a database using SQL Server Object Explorer in SSDT, follow these steps:
1.	**Open Visual Studio**: Make sure you have installed SQL Server Data Tools (SSDT) along with the appropriate version of Visual Studio. Launch Visual Studio.
2.	**Open SQL Server Object Explorer**: Go to the "View" menu and select "SQL Server Object Explorer." Alternatively, you can use the shortcut "Ctrl + \ (backslash)" and then type "Ctrl + S”.
3.	**Connect to a Database Server**: In the SQL Server Object Explorer window, click on the "Add SQL Server" button (it looks like a sheet with a + icon to its top left) or right-click on the "SQL Server" node and choose "Add SQL Server."
4.	**Enter Server Connection Details**: In the "Connect" dialog box, enter the connection details for the SQL Server instance you want to connect to. This includes the server name, authentication method (e.g., Windows Authentication or SQL Server Authentication), login credentials if applicable and Encryption Details. Once a SQL Server is connected, it would automatically appear under the Recent Connection Option in the ‘History’ tab.
5.	**Test Connection**: After entering the connection details, you can click the "Connect" button to test the connection. If the connection is successful, you should see the server and its databases listed in SQL Server Object Explorer.
6.	**Navigate and Manage Databases**: Once connected, you can expand the server node to view all the databases hosted on that server. You can further expand each database node to explore its tables, views, stored procedures, and other database objects.
7.	**Perform Actions**: Right-click on a database or any object to perform various actions like querying data, creating new objects, editing existing ones, and more.

:::image type="content" source="./media/connect-to-existing-database/connect.png" alt-text="A screenshot of the Connect Dialogue Box." border = "true":::



## <a name="AuthTypes"></a>Authentication types
SSDT lets you connect to databases in your Local Drive, Network and Azure. So considering the variety of scenarios, we have multiple authentication types. They are as follows:
:::image type="content" source="./media/connect-to-existing-database/authentication-types.png" alt-text="A screenshot of the different Authentication types." border = "true":::


- 	**Windows Authentication**: This is the default authentication method in SQL Server. It uses the Windows security system to authenticate users. This means that users do not need to create a separate SQL Server login, they can simply use their Windows username and password to connect to SQL Server.
- 	**SQL Server Authentication**: This authentication method allows users to create a separate SQL Server login and password. This is useful if you want to restrict access to SQL Server to specific users or groups.
- 	**Mixed Mode**: This authentication mode allows you to use both Windows Authentication and SQL Server Authentication. This is a good option if you have a mix of Windows and non-Windows users who need to access SQL Server. The different types of Mixed Mode Authentication are as follows:
    - 	**Active Directory Password Authentication**: This authentication method uses the user's Active Directory password to authenticate them to SQL Server. This is the simplest authentication method to configure, but it does not offer any additional security features.
    - 	**Active Directory Integrated Authentication**: This authentication method uses Kerberos to authenticate users to SQL Server. Kerberos is a more secure authentication protocol than Active Directory Password Authentication, but it requires that both the client and the server be joined to an Active Directory domain.
    - 	**Active Directory Interactive Authentication**: This authentication method allows users to authenticate to SQL Server by entering their Active Directory credentials in a dialog box. This is the most secure authentication method, but it can be inconvenient for users who must enter their credentials every time they connect to SQL Server.

### Summary
|Authentication Method|Description|  
|-----------------|-----------------|  
|Windows Authentication|Uses the Windows security system to authenticate users.| 
|SQL Server Authentication|Allows users to create a separate SQL Server login and password.|
|Active Directory Password Authentication|Uses the user's Active Directory password to authenticate them to SQL Server.|
|Active Directory Integrated Authentication|Uses Kerberos to authenticate users to SQL Server.|
|Active Directory Interactive Authentication|Allow users to authenticate to SQL Server by entering their Active Directory credentials in a dialog box.| 


## <a name="EncryptTrustServerCertificate"></a>Encrypt and Trust Server Certificate
SSDT in Visual Studio 17.8 and later includes an important change to the Encrypt property, which is now enabled by default for all connections, and SQL Server must be configured with TLS certificates signed by a trusted root certificate authority. In addition, if an initial connection attempt fails with encryption enabled (default), SSDT will provide a notification prompt with an option to attempt the connection with Trust Server Certificate enabled. Both the Encrypt and Trust server certificate properties are also available for manual editing. The [best practice](../relational-databases/security/securing-sql-server.md) is to support a trusted encrypted connection to the server.

:::image type="content" source="./media/connect-to-existing-database/encrypt.png" alt-text="A screenshot of the different Encryption Types." border = "true":::


For users connecting to Azure SQL Database, no changes to existing, saved connections are needed; Azure SQL Database supports encrypted connections and is configured with trusted certificates.

For users connecting to on-premises SQL Server, or SQL Server in a Virtual Machine, if Encrypt is set to True, ensure that you have a certificate from a trusted certificate authority (e.g. not a self-signed certificate). Alternatively, you may choose to connect without encryption (Encrypt set to False), or to trust the server certificate (Encrypt set to True and Trust server certificate set to True).

> [!NOTE]
>If the user attempts to use a 'Strict' Encryption or Encrypt is set to True and Trust Server Certificate as False and tries to connect to an on-premises server, or a SQL Server in Virtual Machine, an error message is displayed.

:::image type="content" source="./media/connect-to-existing-database/error-ssl.png" alt-text="A screenshot of the SSL Error expected if the SQL Server connection attempted does not have SSL certificate and the settings demand that." border = "true":::


## Next steps
Now that you are able to connect to your SQL Server, learn [Project Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md).






