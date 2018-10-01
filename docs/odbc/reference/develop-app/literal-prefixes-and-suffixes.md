---
title: "Literal Prefixes and Suffixes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], literal prefixes and suffixes"
  - "literals [ODBC], prefixes and suffixes"
ms.assetid: 29f468f2-f557-4a92-b31d-569c63cc6272
author: MightyPen
ms.author: genemi
manager: craigg
---
# Literal Prefixes and Suffixes
In an SQL statement, a *literal* is a character representation of an actual data value. For example, in the following statement, ABC, FFFF, and 10 are literals:  
  
```  
SELECT CharCol, BinaryCol, IntegerCol FROM MyTable  
   WHERE CharCol = 'ABC' AND BinaryCol = 0xFFFF AND IntegerCol = 10  
```  
  
 Literals for some data types require special prefixes and suffixes. In the preceding example, the character literal (ABC) requires a single quotation mark (') as both a prefix and a suffix, the binary literal (FFFF) requires the characters 0x as a prefix, and the integer literal (10) does not require a prefix or suffix.  
  
 For all data types except date, time, and timestamps, interoperable applications should use the values returned in the LITERAL_PREFIX and LITERAL_SUFFIX columns in the result set created by **SQLGetTypeInfo**. For date, time, timestamp, and datetime interval literals, interoperable applications should use the escape sequences discussed in the preceding section.
