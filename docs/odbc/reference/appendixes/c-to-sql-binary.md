---
title: "C to SQL: Binary | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "binary data type [ODBC]"
  - "data conversions from C to SQL types [ODBC], binary"
  - "binary data transfers [ODBC]"
  - "converting data from c to SQL types [ODBC], binary"
ms.assetid: 3e9083f3-357b-41aa-833c-2c8aac2226cd
author: MightyPen
ms.author: genemi
manager: craigg
---
# C to SQL: Binary
The identifier for the binary ODBC C data type is:  
  
 SQL_C_BINARY  
  
 The following table shows the ODBC SQL data types to which binary C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR<br /><br /> SQL_VARCHAR<br /><br /> SQL_LONGVARCHAR|Byte length of data <= Column byte length<br /><br /> Byte length of data > Column byte length|n/a<br /><br /> 22001|  
|SQL_WCHAR<br /><br /> SQL_WVARCHAR<br /><br /> SQL_WLONGVARCHAR|Character length of data <= Column character length<br /><br /> Character length of data > Column character length|n/a<br /><br /> 22001|  
|SQL_DECIMAL<br /><br /> SQL_NUMERIC<br /><br /> SQL_TINYINT<br /><br /> SQL_SMALLINT<br /><br /> SQL_INTEGER<br /><br /> SQL_BIGINT<br /><br /> SQL_REAL<br /><br /> SQL_FLOAT<br /><br /> SQL_DOUBLE<br /><br /> SQL_BIT SQL_TYPE_DATE<br /><br /> SQL_TYPE_TIME<br /><br /> SQL_TYPE_TIMESTAMP|Byte length of data = SQL data length<br /><br /> Byte length of data <> SQL data length|n/a<br /><br /> 22003|  
|SQL_BINARY<br /><br /> SQL_VARBINARY<br /><br /> SQL_LONGVARBINARY|Length of data <= column length<br /><br /> Length of data > column length|n/a<br /><br /> 22001|
