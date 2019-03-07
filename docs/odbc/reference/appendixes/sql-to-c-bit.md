---
title: "SQL to C: Bit | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data from SQL to c types [ODBC], bit"
  - "bit data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], bit"
ms.assetid: 0eeaab8b-ad82-4a36-b464-9a1211d5f72c
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Bit
The identifier for the bit ODBC SQL data type is:  
  
 SQL_BIT  
  
 The following table shows the ODBC C data types to which bit SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  
  
|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR<br /><br /> SQL_C_WCHAR|*BufferLength* > 1<br /><br /> *BufferLength* <= 1|Data<br /><br /> Undefined|1<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_STINYINT<br /><br /> SQL_C_UTINYINT<br /><br /> SQL_C_TINYINT<br /><br /> SQL_C_SBIGINT<br /><br /> SQL_C_UBIGINT<br /><br /> SQL_C_SSHORT<br /><br /> SQL_C_USHORT<br /><br /> SQL_C_SHORT<br /><br /> SQL_C_SLONG<br /><br /> SQL_C_ULONG<br /><br /> SQL_C_LONG<br /><br /> SQL_C_FLOAT<br /><br /> SQL_C_DOUBLE<br /><br /> SQL_C_NUMERIC|None[a]|Data|Size of the C data type|n/a|  
|SQL_C_BIT|None[a]|Data|1[b]|n/a|  
|SQL_C_BINARY|*BufferLength* >= 1<br /><br /> *BufferLength* < 1|Data<br /><br /> Undefined|1<br /><br /> Undefined|n/a<br /><br /> 22003|  
  
 [a]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.  
  
 [b]   This is the size of the corresponding C data type.  
  
 When bit SQL data is converted to character C data, the possible values are "0" and "1".
