---
description: "SET ANSI Command"
title: "SET ANSI Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "set ANSI command [ODBC]"
ms.assetid: cf9a01b2-14bf-458c-a73c-2a58ddef32d8
author: David-Engel
ms.author: v-davidengel
---
# SET ANSI Command
Determines how comparisons between strings of different lengths are made with the = operator in Visual FoxPro SQL commands.  
  
## Syntax  
  
```  
  
SET ANSI ON | OFF  
```  
  
## Arguments  
 ON  
 (Default for the driver; the default for Visual FoxPro is OFF.) Pads the shorter string with the blanks needed to make it equal to the longer string's length. The two strings are then compared character for character for their entire lengths. Consider this comparison:  
  
```  
'Tommy' = 'Tom'  
```  
  
 The result is False (.F.) if SET ANSI is on, because when padded, 'Tom' becomes 'Tom ' and the strings 'Tom ' and 'Tommy' don't match character for character.  
  
 The == operator uses this method for comparisons in Visual FoxPro SQL commands.  
  
 OFF  
 Specifies that the shorter string not be padded with blanks. The two strings are compared character for character until the end of the shorter string is reached. Consider this comparison:  
  
```  
'Tommy' = 'Tom'  
```  
  
 The result is True (.T.) when SET ANSI is off, because the comparison stops after 'Tom'.  
  
## Remarks  
 SET ANSI determines whether the shorter of two strings is padded with blanks when an SQL string comparison is made. SET ANSI has no effect on the == operator; when you use the == operator, the shorter string is always padded with blanks for the comparison.  
  
## String Order  
 In SQL commands, the left-to-right order of the two strings in a comparison is irrelevantswitching a string from one side of the = or == operator to the other doesn't affect the result of the comparison.  
  
## See Also  
 [SELECT - SQL Command](../../odbc/microsoft/select-sql-command.md)   
 [SET EXACT Command](../../odbc/microsoft/set-exact-command.md)
