---
title: "SQLTables (Excel Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Excel driver [ODBC], SQLTables"
  - "SQLTables function [ODBC], Excel Driver"
ms.assetid: 9410b686-4b5b-4b51-b5ef-f9d2e7a48faa
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLTables (Excel Driver)
> [!NOTE]  
>  This topic provides Excel Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Argument|Comments|  
|--------------|--------------|  
|*szTableOwner*|The only valid argument for *szTableOwner* is NULL because none of the drivers support owner names. With *szTableOwner* set to NULL, all tables are returned. NULL is returned in the TABLE_OWNER column.|  
|*szTableQualifier*|When the Microsoft Excel 3.0 or 4.0 driver is used, if you call **SQLTables** with a value for *szTableQualifier* that is not the name of an existing table, the driver will create a table with that name.<br /><br /> In the TABLE_QUALIFIER column, **SQLTables** will return the path to a directory.|  
|*SzTableType*|For Microsoft Excel 3.0 or 4.0, "TABLE" is the only table type supported.<br /><br /> For later versions of Microsoft Excel files, "SYSTEM TABLE" is returned for sheet names (tables with a "$" on the end), and "TABLE" is returned for tables within worksheets.|
