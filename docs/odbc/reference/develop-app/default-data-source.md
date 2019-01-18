---
title: "Default Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], connection functions"
  - "connecting to data source [ODBC], default data source"
  - "functions [ODBC], data source or driver connections"
  - "data sources [ODBC], default"
  - "default data source [ODBC]"
  - "connecting to data source [ODBC], functions"
  - "connecting to driver [ODBC], default data source"
  - "connecting to driver [ODBC], functions"
  - "connection functions [ODBC]"
  - "ODBC drivers [ODBC], connection functions"
ms.assetid: dd473cc6-f051-4aa0-ab14-3dd1b37fe99e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Default Data Source
The driver may select a data source, called the default data source, in certain cases where the application does not explicitly specify one:  
  
-   In a call to **SQLConnect** where the *ServerName* argument is a zero-length string, a null pointer, or DEFAULT.  
  
-   In a call to **SQLDriverConnect** where *InConnectionString* either specifies **DSN**=DEFAULT or specifies with the **DSN** keyword a data source that is not contained in the system information.  
  
 It is driver-defined how the default data source is specified. This may involve administrative action and may depend on the user.
