---
title: "Connecting with SQLDriverConnect | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting with SQLDriverConnect
**SQLDriverConnect** is used to connect to a data source using a connection string. **SQLDriverConnect** is used instead of **SQLConnect** for the following reasons:  
  
-   To let the application use driver-specific connection information.  
  
-   To request that the driver prompt the user for connection information.  
  
-   To connect without specifying a data source.  
  
 This section contains the following topics.  
  
-   [Driver-Specific Connection Information](../../../odbc/reference/develop-app/driver-specific-connection-information.md)  
  
-   [Prompting the User for Connection Information](../../../odbc/reference/develop-app/prompting-the-user-for-connection-information.md)  
  
-   [Connecting Using File Data Sources](../../../odbc/reference/develop-app/connecting-using-file-data-sources.md)  
  
-   [Connecting Directly to Drivers](../../../odbc/reference/develop-app/connecting-directly-to-drivers.md)
