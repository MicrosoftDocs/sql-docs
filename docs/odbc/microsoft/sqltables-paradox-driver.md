---
title: "SQLTables (Paradox Driver) | Microsoft Docs"
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
  - "Paradox driver [ODBC], SQLTables"
  - "SQLTables function [ODBC], Paradox Driver"
ms.assetid: d68adad6-97bd-4b47-bcf9-0102aafb00d4
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLTables (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Argument|Comments|  
|--------------|--------------|  
|*szTableOwner*|The only valid argument for *szTableOwner* is NULL because none of the drivers support owner names. With *szTableOwner* set to NULL, all tables are returned. NULL is returned in the TABLE_OWNER column.|  
|*szTableQualifier*|In the TABLE_QUALIFIER column, **SQLTables** will return the path to a directory.|  
|*SzTableType*|For Paradox files, "TABLE" is the only table type supported.|  
  
## See Also  
 [SQLTables Function](../../odbc/reference/syntax/sqltables-function.md)