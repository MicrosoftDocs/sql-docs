---
title: "Column Size, Decimal Digits, Transfer Octet Length, Display Size | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "display size of data types [ODBC]"
  - "data types [ODBC], column size"
  - "transfer octet length of data types [ODBC]"
  - "size of data types [ODBC]"
  - "data types [ODBC], display size"
  - "decimal digits of data types [ODBC]"
  - "data types [ODBC], decimal digits"
  - "SQL data types [ODBC], column characteristics"
  - "column size of data types [ODBC]"
  - "data types [ODBC], transfer octet length"
ms.assetid: 723107a1-be08-4ea3-a8c0-b2c45d38d1aa
author: MightyPen
ms.author: genemi
manager: craigg
---
# Column Size, Decimal Digits, Transfer Octet Length, and Display Size - ODBC
Data types are characterized by their column (or parameter) size, decimal digits, length, and display size. The following ODBC functions return these attributes for a parameter in an SQL statement or for an SQL data type on a data source. Each ODBC function returns a different set of these attributes, as follows:  
  
-   **SQLDescribeCol** returns the column size and decimal digits of the columns it describes.  
  
-   **SQLDescribeParam** returns the parameter size and decimal digits of the parameters it describes. **SQLBindParameter** sets the parameter size and decimal digits for a parameter in an SQL statement.  
  
-   The catalog functions **SQLColumns**, **SQLProcedureColumns**, and **SQLGetTypeInfo** return attributes for a column in a table, result set, or a procedure parameter and the catalog attributes of the data types in the data source. **SQLColumns** returns the column size, decimal digits, and length of a column in specified tables (such as the base table, view, or a system table). **SQLProcedureColumns** returns the column size, decimal digits, and length of a column in a procedure. **SQLGetTypeInfo** returns the maximum column size and the minimum and maximum decimal digits of an SQL data type on a data source.  
  
 The values returned by these functions for the column or parameter size correspond to "precision" as defined in ODBC 2.*x*. However, the values do not necessarily correspond to the values returned in SQL_DESC_PRECISION or any other one descriptor field. The same is true for decimal digits, which correspond to "scale" as defined in ODBC 2.*x*. It does not necessarily correspond to the values returned in SQL_DESC_SCALE or any other one descriptor field, but comes from different descriptor fields depending on the data type. For further information, see [Column Size](../../../odbc/reference/appendixes/column-size.md) and [Decimal Digits](../../../odbc/reference/appendixes/decimal-digits.md).  
  
 Similarly, the values for transfer octet length do not come from SQL_DESC_LENGTH. They come from the SQL_DESC_OCTET_LENGTH of a field of a descriptor for all character and binary types. There is no descriptor field that holds this information for other types.  
  
 The display size value for all data types corresponds to the value in a single descriptor field, SQL_DESC_DISPLAY_SIZE.  
  
 Descriptor fields describe the characteristics of a result set. Descriptor fields do not contain valid values about data before statement execution. The values for column size, decimal digits, and display size returned by **SQLColumns**, **SQLProcedureColumns**, and **SQLGetTypeInfo**,on the other hand, return characteristics of database objects, such as table columns and data types, that exist in the data source's catalog. Likewise, in its result set, **SQLColAttribute** returns the column size, decimal digits, and transfer octet length of columns at the data source; these values are not necessarily the same as the values in the SQL_DESC_PRECISION, SQL_DESC_SCALE, and SQL_DESC_OCTET_LENGTH descriptor fields.  
  
 For more information about these descriptor fields, see [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).  
  
 Related topics:  
  
-   [Column Size](../../../odbc/reference/appendixes/column-size.md)  
-   [Decimal Digits](../../../odbc/reference/appendixes/decimal-digits.md)  
-   [Transfer Octet Length](../../../odbc/reference/appendixes/transfer-octet-length.md)  
-   [Display Size](../../../odbc/reference/appendixes/display-size.md)
