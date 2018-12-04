---
title: "dBASE Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], DBasedriver"
  - "desktop database drivers [ODBC], DBasedriver"
  - "DBase driver [ODBC], data types"
  - "data types [ODBC], DBase driver"
  - "dbase data types [ODBC]"
  - "ODBC desktop database drivers [ODBC], DBasedriver"
ms.assetid: a0e31e6b-d02b-4ee2-9b37-5baf6a11c0a6
author: MightyPen
ms.author: genemi
manager: craigg
---
# dBASE Data Types
The following table shows how dBASE data types are mapped to ODBC SQL data types. Note that not all ODBC SQL data types are supported.  
  
|dBASE data type|ODBC data type|  
|---------------------|--------------------|  
|CHAR|SQL_VARCHAR|  
|DATE|SQL_DATE|  
|FLOAT[1]|SQL_DOUBLE|  
|LOGICAL|SQL_BIT|  
|MEMO|SQL_LONGVARCHAR|  
|NUMERIC (BCD)|SQL_DOUBLE|  
|OLEOBJECT[1]|SQL_LONGBINARY|  
  
 [1]   Valid only for dBASE version 5.*x*  
  
 Precision in dBASE III allows numbers with up to two-digit exponents and in dBASE IV numbers with up to three-digit exponents. Because numbers are stored as text, they are converted to numbers. If the number to convert does not fit in a field, unexplained results may occur.  
  
 While dBASE allows a precision and a scale to be specified with a NUMERIC data type, it is not supported by the ODBC dBASE driver. The ODBC dBASE driver always returns a precision of 15 and a scale of 0 for a NUMERIC data type.  
  
 A column created with the Numeric data type using the ODBC dBASE driver maps to the SQL_DOUBLE ODBC data type. Thus the data in this column is subject to rounding. This behavior is not the same as that of the NUMERIC data type in dBASE (type N), which is Binary Coded Decimal (BCD).  
  
> [!NOTE]  
>  **SQLGetTypeInfo** returns ODBC SQL data types. All conversions in Appendix D of the *ODBC Programmer's Reference* are supported for the ODBC SQL data types listed earlier in this topic.  
  
 The following table shows limitations on dBASE data types.  
  
|Data type|Description|  
|---------------|-----------------|  
|CHAR|Creating a CHAR column of zero or unspecified length actually returns a 254-byte column.|  
|Encrypted data|The dBASE driver does not support encrypted dBASE tables.|  
|LOGICAL|The dBASE driver cannot create an index on a LOGICAL column.|  
|MEMO|The maximum length of a MEMO column is 65,500 bytes.|  
  
 More limitations on data types can be found in [Data Type Limitations](../../odbc/microsoft/data-type-limitations.md).
