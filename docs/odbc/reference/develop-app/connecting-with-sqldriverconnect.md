---
title: "Connecting with SQLDriverConnect | Microsoft Docs"
description: "SQLDriverConnect provides additional connection functionality over SQLConnect, including options to prompt the user for more information."
ms.custom: ""
ms.date: "08/20/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], connection functions"
  - "functions [ODBC], data source or driver connections"
  - "connecting to data source [ODBC], SQLDriverConnect"
  - "connecting to driver [ODBC], SQLDriverConnect"
  - "connecting to data source [ODBC], functions"
  - "connecting to driver [ODBC], functions"
  - "SQLDriverConnect function [ODBC], connecting"
  - "connection functions [ODBC]"
  - "ODBC drivers [ODBC], connection functions"
ms.assetid: e46e959f-d3c5-4ddb-810a-107bfcb83fd2
author: David-Engel
ms.author: v-davidengel
---
# Connecting with SQLDriverConnect

**SQLDriverConnect** is used to connect to a data source using a connection string. **SQLDriverConnect** is used instead of **SQLConnect** for the following scenarios:  
  
- Establish a connection using a connection string that contains the data source name, one or more user IDs, one or more passwords, and other information required by the data source.  
  
- Establish a connection using a partial connection string or no additional information; in this case, the Driver Manager and the driver can each prompt the user for connection information.  
  
- Establish a connection to a data source that is not defined in the system information. If the application supplies a partial connection string, the driver can prompt the user for connection information.  
  
- Establish a connection to a data source using a connection string constructed from the information in a .dsn file.  
  
After a connection is established, **SQLDriverConnect** returns the completed connection string. The application can use this string for subsequent connection requests.

This section contains the following topics.  
  
- [Driver-Specific Connection Information](driver-specific-connection-information.md)  
  
- [Prompting the User for Connection Information](prompting-the-user-for-connection-information.md)  
  
- [Connecting Using File Data Sources](connecting-using-file-data-sources.md)  
  
- [Connecting Directly to Drivers](connecting-directly-to-drivers.md)
