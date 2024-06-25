---
title: "Default Data Source"
description: "Default Data Source"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
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
---
# Default Data Source
The driver may select a data source, called the default data source, in certain cases where the application does not explicitly specify one:  
  
-   In a call to **SQLConnect** where the *ServerName* argument is a zero-length string, a null pointer, or DEFAULT.  
  
-   In a call to **SQLDriverConnect** where *InConnectionString* either specifies **DSN**=DEFAULT or specifies with the **DSN** keyword a data source that is not contained in the system information.  
  
 It is driver-defined how the default data source is specified. This may involve administrative action and may depend on the user.
