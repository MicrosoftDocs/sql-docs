---
title: "Using Connection Strings"
description: "Using Connection Strings"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "connection strings [ODBC]"
  - "connecting to data source [ODBC], Visual FoxPro"
  - "Visual FoxPro data source [ODBC], connecting"
---
# Using Connection Strings
You can use a connection string to connect to a Visual FoxPro data source.  
  
 For example, to connect to the TasTrade data source and override the current setting of Exclusive associated with the data source, you would use the string:  
  
```  
DSN=TasTrade;Exclusive=Yes  
```  
  
 For a list of the attribute keywords and values you can include in the connection string, see [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md).  
  
 For a complete explanation of connection string syntax, see [SQLBrowseConnect](../../odbc/reference/syntax/sqlbrowseconnect-function.md) in the *ODBC Programmer's Reference*.
