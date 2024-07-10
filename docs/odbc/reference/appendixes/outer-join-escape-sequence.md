---
title: "Outer Join Escape Sequence"
description: "Outer Join Escape Sequence"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "outer join escape sequence [ODBC]"
  - "escape sequences [ODBC], outer join"
  - "ODBC escape sequences [ODBC], outer join"
---
# Outer Join Escape Sequence
ODBC uses escape sequences for outer joins. The syntax of this escape sequence is as follows:  
  
```  
{oj outer-join}  
```  
  
## Remarks  
 In BNF notation, the syntax is as follows:  
  
 *ODBC-outer-join-escape* ::=  
  
 *ODBC-esc-initiator* oj *outer-join ODBC-esc-terminator*  
  
 *outer-join* ::= *table-name* [*correlation-name*] {LEFT &#124; RIGHT &#124; FULL}  
  
 OUTER JOIN{*table-name* [*correlation-name*] &#124; *outer-join*} ON  
  
 *search-*  
  
 *condition*  
  
 *correlation-name* ::= *user-defined-name*  
  
 *ODBC-esc-initiator* ::= {  
  
 *ODBC-esc-terminator* ::= }  
  
 To determine which parts of this statement are supported, an application calls **SQLGetInfo** with the SQL_OJ_CAPABILITIES information type. For outer joins, *search-condition* must contain only the join condition between the specified *table-names*.
