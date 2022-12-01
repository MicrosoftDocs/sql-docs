---
title: "Automatic Matching of Syntax Pairs"
description: Learn about automatic matching of syntax pairs in Query Editor (delimiter matching), XMLA Query Editor (brace matching), and MDX and DMX (parenthesis matching).
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "IntelliSense [SQL Server], delimiter highlighting"
  - "IntelliSense [SQL Server], syntax pair matching"
ms.assetid: bfc54cda-bfd6-4545-a5b9-f9db2ae13769
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Automatic Matching of Syntax Pairs
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Automatic matching of syntax pairs gives you immediate feedback on whether syntax elements that must be coded in pairs are correctly paired. This is known as delimiter matching in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor, brace matching in the Analysis Services XMLA Query Editor, and parenthesis matching in the MDX and DMX editors.  
  
## Database Engine Query Editor Delimiter Matching  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor matches the delimiters that identify the boundaries of code blocks. The matching is done in two ways:  
  
-   The editor highlights both delimiters in a pair when you finish typing the second delimiter in the pair.  
  
-   Anytime the cursor is in one of the delimiters in a pair, you can use the CTRL+] keyboard shortcut to jump to the matching delimiter.  
  
### Delimiter Pairs  
 Automatic delimiter matching recognizes the following sets of delimiters:  
  
|Lead Delimiter|Closing Delimiter|  
|--------------------|-----------------------|  
|**(**|**)**|  
|**BEGIN**|**END**|  
|**BEGIN TRY**|**END TRY**|  
|**BEGIN CATCH**|**END CATCH**|  
  
 Automatic delimiter matching does not recognize the delimiters for bracketed identifiers ([ObjectName]), or quoted identifiers ("ObjectName"). Pair matching does not match the single quotation delimiters for string literals ('string') because color coding already gives a visual indication of whether the string has been delimited.  
  
### Delimiter Highlighting  
 Matching highlights both the lead and closing element of a pair of delimiters. This lets you visually identify code blocks and check for mismatched pairs of delimiters.  
  
 Delimiters are highlighted when you type the final letter that completes the pair. For example, for a BEGIN END pair where you type BEGIN first followed by END, highlighting turns on when you type the final letter in END. You do not have to type the lead delimiter followed by the closing delimiter to turn on highlighting. If you type END first, then scroll back up the script and type BEGIN, highlighting is turned on when you type the final letter in BEGIN. The final letter typed does not have to be the end letter in the delimiter. For example, you could misspell BEGIN as BEIN, when you insert the final G the BEGIN END pair is highlighted.  
  
 The delimiter pair remains highlighted until you move the cursor. The highlighting is turned off when the cursor is moved, even if the new cursor position remains in the same delimiter. You can turn the highlighting back on by deleting and retyping any letter in either member of the pair.  
  
## Analysis Services XMLA Query Editor Brace Matching  
 The XMLA Query Editor brace matching shows if you have closed elements by highlighting opening and closing braces. You can also use the CTRL+] keyboard shortcut to jump from one brace to the matching brace.  
  
 The XMLA Query Editor does brace matching for the following terms:  
  
-   Matching start and end tags.  
  
-   Any pair of "\<" and ">" angle brackets.  
  
-   Start and end of comments.  
  
-   Start and end of processing instructions.  
  
-   Start and end of CDATA blocks.  
  
-   Start and end of DTD declarations.  
  
-   Opening and closing quotes on attributes.  
  
## MDX and DMX Editor Parenthesis Matching  
 The Multi-Dimensional Expressions (MDX) and Data Mining Expressions (DMX) Editors automatically match parenthesis pairs in functions.
