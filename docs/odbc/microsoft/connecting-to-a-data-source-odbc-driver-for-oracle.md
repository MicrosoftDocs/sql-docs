---
description: "Connecting to a Data Source (ODBC Driver for Oracle)"
title: "Connecting to a Data Source (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to data source [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], connecting to data sources"
ms.assetid: f724a9c5-342a-4f4e-a030-ec34f7378eaf
author: David-Engel
ms.author: v-davidengel
---
# Connecting to a Data Source (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 An ODBC application can pass connection information in a number of ways. For example, the application might have the driver always prompt the user for connection information. Or the application might expect a connection string that specifies the data source connection. How you connect to a data source depends on the connection method used by your ODBC application.  
  
 One common way to connect to a data source is through the Data Source dialog box. If your ODBC application is set up to use a dialog box, that dialog box is displayed and prompts you for the appropriate data source connection information.  
  
 You can also connect to a data source using the [connection string](../../odbc/microsoft/connection-string-format-and-attributes.md).  
  
### To connect to a data source using a dialog box  
  
1.  When the Data Source dialog box appears, select an Oracle data source and then click OK. The Connect dialog box appears.  
  
2.  Fill in the appropriate information for the Connect dialog box, and then click OK.  
  
 After the connection information is verified, your application can use the ODBC Driver for Oracle to access the information that the data source contains.
