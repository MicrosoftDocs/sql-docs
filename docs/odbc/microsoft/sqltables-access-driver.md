---
title: "SQLTables (Access Driver) | Microsoft Docs"
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
  - "SQLTables function [ODBC], Access Driver"
  - "Access driver [ODBC], SQLTables"
ms.assetid: 94423cf9-341a-4db6-bb10-8f5448df7fc3
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLTables (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Argument|Comments|  
|--------------|--------------|  
|*szTableOwner*|The only valid argument for *szTableOwner* is NULL because none of the drivers support owner names. With *szTableOwner* set to NULL, all tables are returned. NULL is returned in the TABLE_OWNER column.|  
|*szTableQualifier*|In the TABLE_QUALIFIER column, **SQLTables** will return the path to a database file.|  
|*SzTableType*|When the Microsoft Access driver is used, "SYSTEM TABLE" is supported for *szTableType* for system tables, "SYNONYM" is supported for attached tables, and "VIEW" is supported for row-returning queries.|  
  
## See Also  
 [SQLTables Function](../../odbc/reference/syntax/sqltables-function.md)