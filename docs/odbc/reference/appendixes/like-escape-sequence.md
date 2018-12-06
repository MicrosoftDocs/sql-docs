---
title: "LIKE Escape Sequence | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC escape sequences [ODBC], LIKE"
  - "LIKE escape sequence [ODBC]"
  - "escape sequences [ODBC], LIKE"
ms.assetid: 798d75ea-be9d-4bef-b297-318bc327f1ca
author: MightyPen
ms.author: genemi
manager: craigg
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
