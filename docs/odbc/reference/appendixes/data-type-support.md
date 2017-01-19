---
title: "Data Type Support | Microsoft Docs"
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
  - "minimum SQL syntax supported [ODBC]"
  - "ODBC drivers [ODBC], minimum SQL syntax supported"
  - "data types [ODBC], ODBC drivers"
  - "ODBC drivers [ODBC], data types"
ms.assetid: 782b4490-372b-4366-aad7-a486fb8a07c8
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Data Type Support
ODBC drivers must support at least one of SQL_CHAR and SQL_VARCHAR. Support for other data types is determined by the driver's or data source's SQL-92 conformance level. An application should call **SQLGetTypeInfo** to determine the data types supported by the driver.  
  
 For more information on data types, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).