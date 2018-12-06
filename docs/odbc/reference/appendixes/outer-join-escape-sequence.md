---
title: "Outer Join Escape Sequence | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "outer join escape sequence [ODBC]"
  - "escape sequences [ODBC], outer join"
  - "ODBC escape sequences [ODBC], outer join"
ms.assetid: 2cfd1525-6677-4d36-9b9e-730496853750
author: MightyPen
ms.author: genemi
manager: craigg
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
