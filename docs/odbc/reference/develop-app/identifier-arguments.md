---
title: "Identifier Arguments | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "identifier arguments [ODBC]"
  - "catalog functions [ODBC], arguments"
  - "arguments in catalog functions [ODBC], identifier"
ms.assetid: b9de003f-cb49-4dec-b528-14a5b8ff12bd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Identifier Arguments
If a string in an identifier argument is quoted, the driver removes leading and trailing blanks and treats literally the string within the quotation marks. If the string is not quoted, the driver removes trailing blanks and folds the string to uppercase. Setting an identifier argument to a null pointer returns SQL_ERROR and SQLSTATE HY009 (Invalid use of null pointer), unless the argument is a catalog name and catalogs are not supported.  
  
 These arguments are treated as identifier arguments if the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE. In this case, the underscore (_) and the percent sign (%) will be treated as the actual character, not as a search pattern character. These arguments are treated as either an ordinary argument or a pattern argument, depending on the argument, if this attribute is set to SQL_FALSE.  
  
 Although identifiers containing special characters must be quoted in SQL statements, they must not be quoted when passed as catalog function arguments, because quote characters passed to catalog functions are interpreted literally. For example, suppose the identifier quote character (which is driver-specific and returned through **SQLGetInfo**) is a double quotation mark ("). The first call to **SQLTables** returns a result set containing information about the Accounts Payable table, while the second call returns information about the "Accounts Payable" table, which is probably not what was intended.  
  
```  
SQLTables(hstmt1, NULL, 0, NULL, 0, "Accounts Payable", SQL_NTS, NULL, 0);  
SQLTables(hstmt2, NULL, 0, NULL, 0, "\"Accounts Payable\"", SQL_NTS, NULL, 0);  
```  
  
 Quoted identifiers are used to distinguish a true column name from a pseudo-column of the same name, such as ROWID in Oracle. If "ROWID" is passed in an argument of a catalog function, the function will work with the ROWID pseudo-column if it exists. If the pseudo-column does not exist, the function will work with the "ROWID" column. If ROWID is passed in an argument of a catalog function, the function will work with the ROWID column.  
  
 For more information about quoted identifiers, see [Quoted Identifiers](../../../odbc/reference/develop-app/quoted-identifiers.md).
