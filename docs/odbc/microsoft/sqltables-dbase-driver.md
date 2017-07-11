---
title: "SQLTables (dBASE Driver) | Microsoft Docs"
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
  - "DBase driver [ODBC], SQLTables"
  - "SQLTables function [ODBC], dBASE Driver"
ms.assetid: 45938efb-b678-47d8-9345-644fa26ad679
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLTables (dBASE Driver)
> [!NOTE]  
>  This topic provides dBASE Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Argument|Comments|  
|--------------|--------------|  
|*szTableOwner*|The only valid argument for *szTableOwner* is NULL because none of the drivers supports owner names. With *szTableOwner* set to NULL, all tables are returned. NULL is returned in the TABLE_OWNER column.|  
|*szTableQualifier*|In the TABLE_QUALIFIER column, **SQLTables** will return the path to a directory.|  
|*SzTableType*|For dBASE files, "TABLE" is the only table type supported.|