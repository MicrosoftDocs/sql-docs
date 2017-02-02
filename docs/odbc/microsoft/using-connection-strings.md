---
title: "Using Connection Strings | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connection strings [ODBC]"
  - "connecting to data source [ODBC], Visual FoxPro"
  - "Visual FoxPro data source [ODBC], connecting"
ms.assetid: 57634960-47e9-49bf-95c1-6e3702ac8166
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Using Connection Strings
You can use a connection string to connect to a Visual FoxPro data source.  
  
 For example, to connect to the TasTrade data source and override the current setting of Exclusive associated with the data source, you would use the string:  
  
```  
DSN=TasTrade;Exclusive=Yes  
```  
  
 For a list of the attribute keywords and values you can include in the connection string, see [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md).  
  
 For a complete explanation of connection string syntax, see [SQLBrowseConnect](../../odbc/reference/syntax/sqlbrowseconnect-function.md) in the *ODBC Programmer's Reference*.