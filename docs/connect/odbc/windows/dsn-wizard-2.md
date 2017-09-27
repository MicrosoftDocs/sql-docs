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

### With Integrated Windows Authentication using the network login ID

Specifies that the ODBC driver for SQL Server request a secure (or trusted) connection to a SQL Server. When selected, SQL Server uses integrated login security to establish connections using this data source, regardless of the current login security mode at the server. Any login ID or password supplied is ignored. The SQL Server system administrator must have associated your Windows login with a SQL Server login ID (for example, by using SQL Server Management Studio).

Optionally, you can specify a service principal name (SPN) for the server.

### Login ID

Specifies the login ID the ODBC driver for SQL Server uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** is selected. This only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the data source after it has been created.

### Password

Specifies the password the SQL Server Native Client ODBC driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** is selected. This only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the new data source.

Both the **Login ID** and **Password** boxes are disabled if **With Integrated Windows Authentication using the network login ID** is selected, or if **Connect to SQL Server** to obtain default settings for the additional configuration options is cleared.

