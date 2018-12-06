---
title: "Data Source Wizard Screen 1 (ODBC Driver for SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Source Wizard Screen 1

Specify the name and description of the data source, and the name of the server running SQL Server to which the data source will connect. 
    
## Options

### Name

The data source name used by an ODBC application when it requests a connection to the data source. For example, "Personnel." The data source name is displayed in the ODBC Data Source Administrator dialog box.

### Description

(Optional) A description of the data source. For example, "Hire date, salary history, and current review of all employees."

### Select or enter a server name

The name of an instance of SQL Server on your network. You will need to specify a server in the next edit box.

In most cases, the ODBC driver can connect by using the default protocol order and the server name supplied in this box. Use SQL Server Configuration Manager if you want to create an alias for the server or configure client network libraries.

You can enter "(local)" in the server box when you are using the same computer as SQL Server. The user can then connect to the local instance of SQL Server, even when running a non-networked version of SQL Server. Multiple instances of SQL Server can run on the same computer. To specify a named instance of SQL Server, the server name is specified as _ServerName_\\_InstanceName_.

For more information about server names for different types of networks, see the SQL Server installation documentation in SQL Server Books Online.

### Finish

If the information specified on this screen is all that is needed to connect to SQL Server, you can click **Finish**. Defaults are used for all attributes specified on other screens of the wizard.

### Next

To proceed to the next screen of the wizard, click **Next**.

## Next steps

[Data Source Wizard Screen 2](../../../connect/odbc/windows/dsn-wizard-2.md)
