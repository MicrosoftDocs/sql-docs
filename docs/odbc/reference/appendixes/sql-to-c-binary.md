---
title: "SQL to C: Binary | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data from SQL to c types [ODBC], binary"
  - "binary data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], binary"
  - "binary data transfers [ODBC]"
ms.assetid: 8c519072-ae4c-4d32-9d4e-775e3d3d6389
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Binary
The identifiers for the binary ODBC SQL data types are:  
  
 SQL_BINARY  
  
 SQL_VARBINARY  
  
 SQL_LONGVARBINARY  
  
 The following table shows the ODBC C data types to which binary SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  
  
|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|(Byte length of data) \* 2 < *BufferLength*<br /><br /> (Byte length of data) \* 2 >= *BufferLength*|Data<br /><br /> Truncated data|Length of data in bytes<br /><br /> Length of data in bytes|n/a<br /><br /> 01004|  
|SQL_C_WCHAR|(Character length of data) \* 2 < *BufferLength*<br /><br /> (Character length of data) \* 2 >= *BufferLength*|Data<br /><br /> Truncated data|Length of data in characters<br /><br /> Length of data in characters|n/a<br /><br /> 01004|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Truncated data|Length of data in bytes<br /><br /> Length of data in bytes|n/a<br /><br /> 01004|  
  
 When binary SQL data is converted to character C data, each byte (8 bits) of source data is represented as two ASCII characters. These characters are the ASCII character representation of the number in its hexadecimal form. For example, a binary 00000001 is converted to "01" and a binary 11111111 is converted to "FF".  
  
 The driver always converts individual bytes to pairs of hexadecimal digits and terminates the character string with a null byte. Because of this, if *BufferLength* is even and is less than the length of the converted data, the last byte of the **TargetValuePtr* buffer is not used. (The converted data requires an even number of bytes, the next-to-last byte is a null byte, and the last byte cannot be used.)  
  
> [!NOTE]  
>  Application developers are discouraged from binding binary SQL data to a character C data type. This conversion is usually inefficient and slow.
