---
title: "STUFF (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/27/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STUFF"
  - "STUFF_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting characters"
  - "STUFF function"
  - "length characters"
  - "removing characters"
  - "replacing characters"
  - "characters [SQL Server], replacing"
  - "inserting data"
ms.assetid: abb0afa9-44f6-42a2-a871-5f471dfb222b
caps.latest.revision: 40
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STUFF (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  The STUFF function inserts a string into another string. It deletes a specified length of characters in the first string at the start position and then inserts the second string into the first string at the start position.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
STUFF ( character_expression , start , length , replaceWith_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data.  
  
 *start*  
 Is an integer value that specifies the location to start deletion and insertion. If *start* or *length* is negative, a null string is returned. If *start* is longer than the first *character_expression*, a null string is returned. *start* can be of type **bigint**.  
  
 *length*  
 Is an integer that specifies the number of characters to delete. If *length* is longer than the first *character_expression*, deletion occurs up to the last character in the last *character_expression*. *length* can be of type **bigint**.  
  
 *replaceWith_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data. This expression will replace *length* characters of *character_expression* beginning at *start*.  
  
## Return Types  
 Returns character data if *character_expression* is one of the supported character data types. Returns binary data if *character_expression* is one of the supported binary data types.  
  
## Remarks  
 If the start position or the length is negative, or if the starting position is larger than length of the first string, a null string is returned. If the start position is 0, a null value is returned. If the length to delete is longer than the first string, it is deleted to the first character in the first string.  
  
 An error is raised if the resulting value is larger than the maximum supported by the return type.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, both *character_expression* and *replaceWith_expression* can include surrogate pairs. The length parameter will count each surrogate in *character_expression* as a single character.  
  
## Examples  
 The following example returns a character string created by deleting three characters from the first string, `abcdef`, starting at position `2`, at `b`, and inserting the second string at the deletion point.  
  
```  
SELECT STUFF('abcdef', 2, 3, 'ijklmn');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------   
aijklmnef   
  
(1 row(s) affected)  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
