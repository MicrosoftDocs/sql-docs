---
title: "Quoted Identifiers"
description: "Quoted Identifiers"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], quoted identifiers"
  - "quoted identifiers [ODBC]"
---
# Quoted Identifiers
In a SQL statement, identifiers containing special characters or match keywords must be enclosed in *identifier quote characters*; identifiers enclosed in such characters are known as *quoted identifiers* (also known as *delimited identifiers* in SQL-92). For example, the Accounts Payable identifier is quoted in the following **SELECT** statement:  
  
```  
SELECT * FROM "Accounts Payable"  
```  
  
 The reason for quoting identifiers is to make the statement parseable. For example, if Accounts Payable was not quoted in the previous statement, the parser would assume there were two tables, Accounts and Payable, and return a syntax error that they were not separated by a comma. The identifier quote character is driver-specific and is retrieved with the SQL_IDENTIFIER_QUOTE_CHAR option in **SQLGetInfo**. The lists of special characters and of keywords are retrieved with the SQL_SPECIAL_CHARACTERS and SQL_KEYWORDS options in **SQLGetInfo**.  
  
 To be safe, interoperable applications often quote all identifiers except those for pseudo-columns, such as the ROWID column in Oracle. **SQLSpecialColumns** returns a list of pseudo-columns. Also, if there are application-specific restrictions on where special characters can appear in an object name, it is best for interoperable applications not to use special characters in those positions.
