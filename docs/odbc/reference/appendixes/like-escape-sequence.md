---
title: "LIKE Escape Sequence"
description: "LIKE Escape Sequence"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC escape sequences [ODBC], LIKE"
  - "LIKE escape sequence [ODBC]"
  - "escape sequences [ODBC], LIKE"
---
# LIKE Escape Sequence
ODBC uses escape sequences for the LIKE clause. The syntax of this escape sequence is as follows:  
  
```  
{'escape-character'}  
```  
  
## Remarks  
 In BNF notation, the syntax is as follows:  
  
 *ODBC-like-escape* ::=  
  
 *ODBC-esc-initiator* escape '*escape-character*' *ODBC-esc-terminator*  
  
 *escape-character* ::= *character*  
  
 *ODBC-esc-initiator* ::= {  
  
 *ODBC-esc-terminator* ::= }  
  
 To determine if the driver supports the LIKE escape sequence, an application can call **SQLGetInfo** with the SQL_LIKE_ESCAPE_CLAUSE information type.
