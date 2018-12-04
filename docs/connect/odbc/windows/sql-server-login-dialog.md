---
title: "SQL Server Login Dialog Box (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/21/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
author: MightyPen
ms.author: "v-jizho2"
manager: craigg
---
# SQL Server Login Dialog Box (ODBC)

When you call an ODBC connection without specifying enough information for the driver to connect to a SQL Server, the ODBC driver displays the **SQL Server Login** dialog box.

## Options

### Server

The name of an instance of SQL Server on your network. Select a server\instance name from the list, or type the server\instance name in the **Server** box. Optionally, you can create a server alias on the client computer using **SQL Server Configuration Manager**, and type that name in the **Server** box.

You can enter "(local)" when you are using the same computer as SQL Server. You can then connect to a local instance of SQL Server, even when running a non-networked version of SQL Server.

For more information about server names for different types of networks, see the SQL Server installation documentation in SQL Server Books Online.

### Authentication Mode

Selects the authentication mode from one of the following:
- **SQL Server** with login ID and password
- **Windows Integrated** authentication using the currently logged-in user's account
- **Active Directory Password** with login ID and password
- **Active Directory Integrated** authentication using the currently logged-in user's account
- **Active Directory Interactive** authentication with login ID

See [Data Source Wizard Screen 2](../../../connect/odbc/windows/dsn-wizard-2.md) for more information on the authentication modes.

### Server SPN

If you use a trusted connection, you can specify a service principal name (SPN) for the server.

### Login ID

Specifies the SQL Server or Azure Active Directory login ID to use for the connection if **Authentication Mode** is set to **SQL Server** or **Active Directory Password** or **Active Directory Interactive**. Otherwise, the **Login ID** box is disabled.

### Password

Specifies the password for the SQL Server or Azure Active Directory login ID used for the connection if **Authentication Mode** is set to **SQL Server** or **Active Directory Password**. Otherwise, the **Password** box is disabled.

### Options

Displays or hides the **Options** group. The **Options** button is enabled if **Server** has a value.

### Change Password

When this box is selected, displays the **New Password** and **Confirm New Password** boxes.

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

When selected, data that is passed through the connection will be encrypted. Logins are encrypted by default, even if the check box is cleared.

### Trust server certificate

This option is applicable only when **Use strong encryption for data** is enabled. When selected, the server's certificate will not be validated to have the correct hostname of the server and be issued by a trusted certificate authority.

## See Also

[Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)
