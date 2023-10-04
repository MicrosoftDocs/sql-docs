---
title: "SQL Server Login Dialog Box (ODBC)"
description: "The SQL Server Login dialog may appear when an application makes an ODBC connection without specifying enough information to connect to the database."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: "08/08/2022"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
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
- **Managed Service Identity** authentication with Managed Identity
- **Active Directory Service Principal** authentication with Azure Active Directory service principal

See [Data Source Wizard Screen 2](../../../connect/odbc/windows/dsn-wizard-2.md) for more information on the authentication modes.

### Server SPN

If you use a trusted connection, you can specify a service principal name (SPN) for the server.

### Login ID

Specifies the SQL Server or Azure Active Directory login ID to use for the connection if **Authentication Mode** is set to **SQL Server**, **Active Directory Password**, **Active Directory Interactive**, **Managed Service Identity**, or **Active Directory Service Principal**. Otherwise, the **Login ID** box is disabled.

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

When selected, data that is passed through the connection will be encrypted. Logins are encrypted by default, even if the check box is cleared. This option is available in ODBC Driver 17 and older.

### Connection Encryption

Declares the connection encryption mode to be used. Selecting the **Optional** or **Mandatory** option is equivalent to having **Use strong encryption for data** unselected or selected, respectively. When **Strict** is used, the connection will be encrypted using TDS 8.0. This option is available in ODBC Driver 18 and newer.

### Server certificate (optional)

Specifies the server certificate (PEM, DER, or CER format) to match against the certificate returned by the server during encryption negotiation. When specified, certificate validation is done by checking if the server's certificate is an exact match against the certificate specified. The **Hostname in certificate** option is ignored when a server certificate is specified. This option is applicable only when **Connection Encryption** is set to **Strict** and is available in ODBC Driver 18.1 and newer.

### Hostname in certificate (optional)

Specifies the hostname to be used when validating the server's certificate. When left blank, the server name is used as the hostname for validation. A hostname can only be specified when **Trust server certificate** is unselected. This option is available in ODBC Driver 18 and newer.

### Trust server certificate

This option is applicable only when **Use strong encryption for data** is enabled (ODBC Driver 17 and older), or when **Connection Encryption** is set to **Optional** or **Mandatory** (ODBC Driver 18 and newer). When selected, the server's certificate won't be validated to have the correct hostname of the server and be issued by a trusted certificate authority. The server's certificate will always be validated when using the **Strict** encryption mode.

## See Also

[Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)
