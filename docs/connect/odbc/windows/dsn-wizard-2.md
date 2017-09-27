---
title: "Data Source Wizard Screen 2 | Microsoft Docs"
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
# Data Source Wizard Screen 2

Specify the method of authentication, and set up Microsoft SQL Server advanced-client entries and the login and password the ODBC driver for SQL Server will use to connect to SQL Server while configuring the data source.

## Options

### With Integrated Windows Authentication

Specifies that the driver request a secure (or trusted) connection to a SQL Server. When selected, SQL Server uses integrated login security to establish connections using this data source, regardless of the current login security mode at the server. Any login ID or password supplied is ignored. The SQL Server system administrator must have associated your Windows login with a SQL Server login ID (for example, by using SQL Server Management Studio).

Optionally, you can specify a service principal name (SPN) for the server.

### With Active Directory Integrated Authentication

Specifies that the driver authenticate to SQL Server using Azure Active Directory. When selected, SQL Server uses Azure Active Directory integrated login security to establish a connection using this data source, regardless of the current login security mode at the server.

### With SQL Server authentication

Specifies that the driver authenticate to SQL Server using a login ID and password.

### With Active Directory Password authentication

Specifies that the driver authenticate to SQL Server using an Azure Active Directory login ID and password.

### Login ID

Specifies the login ID the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** is selected. This only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the data source after it has been created.

### Password

Specifies the password the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** is selected. This only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the new data source.

Both the **Login ID** and **Password** boxes are disabled if **With Integrated Windows authentication** or **With Active Directory Integrated authentication** is selected.

## [< Back](../../../connect/odbc/windows/dsn-wizard-1.md) [Next >](../../../connect/odbc/windows/dsn-wizard-3.md)

Use these buttons to go to the previous or next page in the wizard.
