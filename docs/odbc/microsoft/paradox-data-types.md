---
description: "Paradox Data Types"
title: "Paradox Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC], Paradox driver"
  - "desktop database drivers [ODBC], Paradox driver"
  - "Paradox data types [ODBC]"
  - "Jet-based ODBC drivers [ODBC], Paradox driver"
  - "data types [ODBC], Paradox driver"
  - "Paradox driver [ODBC], data types"
ms.assetid: 0c9e5d21-9321-49f8-a055-69459e1c9c85
author: David-Engel
ms.author: v-davidengel
---
# Paradox Data Types
The ODBC Paradox driver maps Paradox data types to ODBC SQL data types. The following table lists all Paradox data types and shows the ODBC SQL data types they are mapped to.  
  
|Paradox data type|ODBC data type|  
|-----------------------|--------------------|  
|ALPHANUMERIC|SQL_VARCHAR|  
|AUTOINCREMENT[1]|SQL_INTEGER|  
|BCD[1]|SQL_DOUBLE|  
|BYTES[1]|SQL_BINARY|  
|DATE|SQL_DATE|  
|IMAGE[2]|SQL_LONGVARBINARY|  
|LOGICAL[1]|SQL_BIT|  
|LONG[1]|SQL_INTEGER|  
|MEMO[2]|SQL_LONGVARCHAR|  
|MONEY[1]|SQL_DOUBLE|  
|NUMBER|SQL_DOUBLE|  
|SHORT|SQL_SMALLINT|  
|TIME[1]|SQL_TIMESTAMP|  
|TIMESTAMP[1]|SQL_TIMESTAMP|  
  
 [1]   Valid only for Paradox versions 5.*x*.  
  
 [2]   Valid only for Paradox versions 4.*x* and 5.*x*.  
  
> [!NOTE]  
>  **SQLGetTypeInfo** returns ODBC SQL data types. All conversions in Appendix D of the *ODBC Programmer's Reference* are supported for the ODBC SQL data types listed earlier in this topic.  
  
 The following table shows limitations on Paradox data types.  
  
|Data type|Description|  
|---------------|-----------------|  
|ALPHANUMERIC|Creating an ALPHANUMERIC column of zero or unspecified length actually returns a 255-byte column.|  
|BYTES|If you insert NULL into a binary column with the Paradox5 driver, it is changed to 0.|  
|LONG|The maximum negative value supported by the Paradox driver for the Long data type in Paradox 5.*x* is not -2^31 (-2147483648), as it should be since Long maps to the ODBC data type SQL_INTEGER. The maximum negative value supported for Long is actually -2^31 + 1 (-2147483647).|  
|TIMESTAMP|When a value is inserted into a TIMESTAMP column by the Paradox driver, then subsequently retrieved from the column, the retrieved value may differ from the inserted value by as much as 1 second because of rounding.|  
  
 More limitations on data types can be found in [Data Type Limitations](../../odbc/microsoft/data-type-limitations.md).
