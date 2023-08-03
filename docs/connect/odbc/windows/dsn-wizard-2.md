---
title: Data Source Wizard Screen 2 (ODBC Driver for SQL Server)
description: Learn how to select your authentication method in the Data Source Wizard to create a new ODBC connection to SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
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

### With Active Directory Interactive authentication

Specifies that the driver authenticate to SQL Server using Azure Active Directory Interactive mode by providing login ID. This option will trigger the Azure Authentication prompt dialog.

### With Managed Identity authentication

Specifies that the driver authenticate to SQL Server using a Managed Identity.

### With Active Directory Service Principal authentication

Specifies that the driver authenticate to SQL Server using an Azure Active Directory Service Principal.

### Login ID

Specifies the login ID the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** or **With Active Directory Interactive authentication using a login ID entered by the user** is selected. If **With Managed Identity authentication** is selected, specify the object ID of the managed identity or leave blank to use the default identity. This field only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the data source after it has been created except if using Managed Identity authentication.

### Password

Specifies the password the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** is selected. This field only applies to the connection made to determine the server default settings; it does not apply to subsequent connections made using the new data source.

Both the **Login ID** and **Password** boxes are disabled if **With Integrated Windows authentication** or **With Active Directory Integrated authentication** is selected.

### Next

Proceeds to the next screen of the wizard.

### Back

Returns to the previous screen of the wizard.

## Next steps

[Data Source Wizard Screen 1](dsn-wizard-1.md)  
[Data Source Wizard Screen 3](dsn-wizard-3.md)  
