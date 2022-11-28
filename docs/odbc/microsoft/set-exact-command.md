---
description: "SET EXACT Command"
title: "SET EXACT Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SET EXACT command [ODBC]"
ms.assetid: 9533d3e0-e7c1-49de-a3a3-0cc4373a91cb
author: David-Engel
ms.author: v-davidengel
---
# SET EXACT Command
Specifies the rules for comparing two strings of different lengths.  
  
## Syntax  
  
```  
  
SET EXACT ON | OFF  
```  
  
## Arguments  
 ON  
 Specifies that expressions must match character for character to be equivalent. Any trailing blanks in the expressions are ignored for the comparison. For the comparison, the shorter of the two expressions is padded on the right with blanks to match the length of the longer expression.  
  
 OFF  
 (Default.) Specifies that, to be equivalent, expressions must match character for character until the end of the expression on the right side is reached.  
  
## Remarks  
 The SET EXACT setting has no effect if both strings are the same length.  
  
## String Comparisons  
 Visual FoxPro has two relational operators that test for equality.  
  
 The = operator performs a comparison between two values of the same type. This operator is suited for comparing character, numeric, date, and logical data.  
  
 However, when you compare character expressions with the = operator, the results might not be exactly what you expect. Character expressions are compared character for character from left to right until one of the expressions isn't equal to the other, until the end of the expression on the right side of the = operator is reached (SET EXACT OFF), or until the ends of both expressions are reached (SET EXACT ON).  
  
 The == operator can be used when an exact comparison of character data is needed. If two character expressions are compared with the == operator, the expressions on both sides of the == operator must contain exactly the same characters, including blanks, to be considered equal. The SET EXACT setting is ignored when character strings are compared using ==.  
  
 The following table shows how the choice of operator and the SET EXACT setting affect comparisons. (An underscore represents a blank space.)  
  
|Comparison|= EXACT OFF|= EXACT ON|== EXACT ON or OFF|  
|----------------|------------------|-----------------|--------------------------|  
|"abc" = "abc"|Match|Match|Match|  
|"ab" = "abc"|No match|No match|No match|  
|"abc" = "ab"|Match|No match|No match|  
|"abc" = "ab_"|No match|No match|No match|  
|"ab" = "ab_"|No match|Match|No match|  
|"ab_" = "ab"|Match|Match|No match|  
|"" = "ab"|No match|No match|No match|  
|"ab" = ""|Match|No match|No match|  
|"__" = ""|Match|Match|No match|  
|"" = "___"|No match|Match|No match|  
|TRIM("___") = ""|Match|Match|Match|  
|"" = TRIM("___")|Match|Match|Match|  
  
## See Also  
 [SET ANSI Command](../../odbc/microsoft/set-ansi-command.md)
