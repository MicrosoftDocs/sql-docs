---
description: "LIKE Predicate Escape Character"
title: "LIKE Predicate Escape Character | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "LIKE predicate [ODBC]"
  - "escape sequences [ODBC], LIKE predicate"
ms.assetid: 185d6109-48cf-4981-bc40-ec2a4a90cafc
author: David-Engel
ms.author: v-davidengel
---
# LIKE Predicate Escape Character
In a **LIKE** predicate, the percent sign (%) matches zero or more of any character and the underscore (_) matches any one character. To match an actual percent sign or underscore in a **LIKE** predicate, an escape character must come before the percent sign or underscore. The escape sequence that defines the **LIKE** predicate escape character is:  
  
 **{escape '** *escape-character* **'}**  
  
 where *escape-character* is any character supported by the data source.  
  
 For more information about the LIKE escape sequence, see [LIKE Escape Sequence](../../../odbc/reference/appendixes/like-escape-sequence.md) in Appendix C: SQL Grammar.  
  
 For example, the following SQL statements create the same result set of customer names that start with the characters "%AAA". The first statement uses the escape-sequence syntax. The second statement uses the native syntax for Microsoft® Access and is not interoperable. Notice that the second percent character in each **LIKE** predicate is a wildcard character that matches zero or more of any character.  
  
```  
SELECT Name FROM Customers WHERE Name LIKE '\%AAA%' {escape '\'}  
  
SELECT Name FROM Customers WHERE Name LIKE '[%]AAA%'  
```  
  
 To determine whether the **LIKE** predicate escape character is supported by a data source, an application calls **SQLGetInfo** with the SQL_LIKE_ESCAPE_CLAUSE option.
