---
title: "Establishing a Connection | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], connection functions"
  - "SQLBrowseConnect function [ODBC], establising a connection"
  - "functions [ODBC], data source or driver connections"
  - "connecting to data source [ODBC], functions"
  - "connecting to driver [ODBC], functions"
  - "connection functions [ODBC]"
  - "SQLConnect function [ODBC], establising a connection"
  - "SQLDriverConnect function [ODBC], making a connection"
  - "ODBC drivers [ODBC], connection functions"
ms.assetid: 8e3c717e-35e3-47ef-b5d3-3a96eeb7b869
author: MightyPen
ms.author: genemi
manager: craigg
---
# Establishing a Connection
After allocating environment and connection handles and setting any connection attributes, the application is ready to connect to the data source or driver. There are three different functions the application can use to do this: **SQLConnect** (Core interface conformance level), **SQLDriverConnect** (Core), and **SQLBrowseConnect** (Level 1). Each of the three is designed to be used in a different scenario. Before connecting, the application can determine which of these functions is supported with the **ConnectFunctions** keyword returned by **SQLDrivers**.  
  
> [!NOTE]  
>  Some drivers limit the number of active connections they support. An application calls **SQLGetInfo** with the SQL_MAX_DRIVER_CONNECTIONS option to determine how many active connections a particular driver supports.  
  
 This section contains the following topics.  
  
-   [Default Data Source](../../../odbc/reference/develop-app/default-data-source.md)  
  
-   [Connecting with SQLConnect](../../../odbc/reference/develop-app/connecting-with-sqlconnect.md)  
  
-   [Connection Strings](../../../odbc/reference/develop-app/connection-strings.md)  
  
-   [Connecting with SQLDriverConnect](../../../odbc/reference/develop-app/connecting-with-sqldriverconnect.md)  
  
-   [Connecting with SQLBrowseConnect](../../../odbc/reference/develop-app/connecting-with-sqlbrowseconnect.md)
