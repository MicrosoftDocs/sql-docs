---
title: "SQL Server Login Dialog Box (OLE DB) | Microsoft Docs"
description: "Using SQL Server Login Dialog Box"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.author: "v-beaziz"
author: MightyPen
manager: craigg
---
# SQL Server Login Dialog Box (OLE DB)
When you call an OLE DB connection without specifying enough information for the driver to connect to a SQL Server, the OLE DB driver displays the **SQL Server Login** dialog box.

![Screenshot of SQL Server Login Dialog Box](./media/sql-server-login-dialog.png)

## Options

### Server
The name of an instance of SQL Server on your network. Select a server\instance name from the list, or type the server\instance name in the **Server** box. Optionally, you can create a server alias on the client computer using **SQL Server Configuration Manager**, and type that name in the **Server** box.

You can enter "(local)" when you are using the same computer as SQL Server. You can then connect to a local instance of SQL Server, even when running a non-networked version of SQL Server.

For more information about server names for different types of networks, see the SQL Server installation documentation in SQL Server Books Online.

### Authentication Mode
You can select the following authentication options from the Authentication Mode dropdown:
- `SQL Server Authentication:` Authentication to SQL Server using login ID and password
- `Windows Authentication:` Authentication to SQL Server using the currently logged-in user's account
- `Active Directory - Password:` Active Directory authentication to Azure SQL DB using login ID and password
- `Active Directory - Integrated:` Integrated authentication to Azure SQL DB or SQL Server using the currently logged-in user's account


### Server SPN
If you use a trusted connection, you can specify a service principal name (SPN) for the server.

> [!NOTE]  
>  When connecting to Azure SQL DB, using SPNs with `Active Directory - Integrated` authentication option is not supported.

### Login ID
Specifies the SQL Server or Azure Active Directory login ID to use for the connection. The Login ID text box is only enabled if `Authentication Mode` is set to `SQL Server Authentication` or `Active Directory - Password`. Otherwise, the **Login ID** box is disabled.

### Password
Specifies the password for the SQL Server or Azure Active Directory login ID used for the connection if `Authentication Mode` is set to `SQL Server Authentication` or `Active Directory - Password`. Otherwise, the **Password** box is disabled.

### Options
Displays or hides the **Options** group. The **Options** button is enabled if **Server** has a value.

### Change Password
When this checkbox is selected, **New Password** and **Confirm New Password** boxes are enabled.

> [!NOTE]  
>  Using this option to change password used for `Active Directory - Password` authentication option is not supported.

### New Password
Specifies the new password.

### Confirm New Password
Specifies the new password a second time, for confirmation.

### Database
Specifies the default database to use on the connection. This setting overrides the default database specified for the login on the server. If no database is specified, the connection uses the default database specified for the login on the server.

### Mirror Server
Specifies the name of the failover partner of the database to be mirrored.

### Mirror SPN
Optionally, you can specify an SPN for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.

### Language
Specifies the national language to use for SQL Server system messages. The computer running SQL Server must have the language installed. This setting overrides the default language specified for the login on the server. If no language is specified, the connection uses the default language specified for the login on the server.

### Application Name
(Optional) Specifies the application name to be stored in the **program_name** column in the row for this connection in **sys.sysprocesses**.

### Workstation ID
(Optional) Specifies the workstation ID to be stored in the **hostname** column in the row for this connection in **sys.sysprocesses**.

### Use strong encryption for data
When checked, data that is passed through the connection will be encrypted.

### Trust server certificate
When checked, the server's certificate will be validated. Server's certificate must have the correct hostname of the server and issued by a trusted certificate authority.

> [!NOTE]  
> When using one of the legacy authentication modes, namely `Windows Authentication` or `SQL Server Authentication`, Trust server certificate option is applicable **only** when **Use strong encryption for data** is enabled. However, when using one of the newer authentication modes, Trust server certificate option is considered **regardless of encryption settings**. This new behavior was introduced for improvement in security.

## See Also
[Release notes for the Microsoft OLE DB Driver for SQL Server](release-notes-for-oledb-driver-for-sql-server.md)
