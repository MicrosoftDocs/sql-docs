---
title: "SQL Server Login Dialog Box (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
caps.latest.revision: 22
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQL Server Login Dialog Box (ODBC)

When you call an ODBC connection without specifying enough information for the driver to connect to a SQL Server, the ODBC driver displays the **SQL Server Login** dialog box.

## Options

### **Server**

Each instance of SQL Server can have multiple sets of system messages, with each set in a different language (for example, English, Spanish, French, The name of an instance of SQL Server on your network. Select a server\instance name from the list, or type the server\instance name in the **Server** box. Optionally, you can create a server alias on the client computer using **SQL Server Configuration Manager**, and type that name in the **Server** box.

You can enter "(local)" when you are using the same computer as SQL Server. You can then connect to a local instance of SQL Server, even when running a non-networked version of SQL Server.

For more information about server names for different types of networks, see the SQL Server installation documentation in SQL Server Books Online.

### **Use Trusted Connection**

When this box is selected, specifies that the ODBC driver request a secure (or trusted) connection to an instance of SQL Server running on Microsoft Windows. SQL Server uses integrated login security to establish connections using this data source, regardless of the current login security mode at the server. Any login ID or password supplied is ignored. The SQL Server system administrator must have associated your Windows network ID with a SQL Server login ID.

When this box is cleared, SQL Server uses standard login security to establish connections using this data source. You must specify a login ID and a password for all connection requests.

**Important** Trusted Connections, using Windows Authentication, are much more secure than connections authenticated using SQL Server Authentication. Use Trusted Connections whenever possible.

### **Server SPN**

If you use a trusted connection, you can specify a service principal name (SPN) for the server.

### **Login ID**

Specifies the SQL Server login ID to use for the connection if **Use Trusted Connection** is cleared. If **Use Trusted Connection** is selected, the **Login ID** box is disabled.

### **Password**

Specifies the password for the SQL Server login ID used for the connection if **Use Trusted Connection** is cleared. If **Use Trusted Connection** is selected, the **Password** box is disabled.

### **Options**

Displays or hides the **Options** group. The **Options** button is enabled if **Server** has a value.

### **Change Password**

When this box is selected, displays the **New Password** and **Confirm New Password** boxes.

### **New Password**

Specifies the new password.

### **Confirm New Password**

Specifies the new password a second time, for confirmation.

### **Database**

Specifies the default database to use on the connection. This setting overrides the default database specified for the login on the server. If no database is specified, the connection uses the default database specified for the login on the server.

### **Mirror Server**
Specifies the name of the failover partner of the database to be mirrored.

### **Mirror SPN**

Optionally, you can specify an SPN for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.

### **Language**

Specifies the national language to use for SQL Server system messages. The computer running SQL Server must have the language installed. This setting overrides the default language specified for the login on the server. If no language is specified, the connection uses the default language specified for the login on the server.

### **Application Name**

(Optional) Specifies the application name to be stored in the **program_name** column in the row for this connection in **sys.sysprocesses**.

### **Workstation ID**

(Optional) Specifies the workstation ID to be stored in the **hostname** column in the row for this connection in **sys.sysprocesses**.
