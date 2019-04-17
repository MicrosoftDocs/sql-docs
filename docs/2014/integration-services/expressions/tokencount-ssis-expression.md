---
title: "TOKENCOUNT (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 1c0efed1-c2b3-4f20-a3a1-ad91283b7c0a
author: janinezhang
ms.author: janinez
manager: craigg
---
# TOKENCOUNT (SSIS Expression)
  Returns the number of tokens in a string that contains tokens separated by the specified delimiters.  
  
## Syntax  
  
```  
TOKENCOUNT(character_expression, delimiter_string)  
```  
  
## Arguments  
 *character_expression*  
 A string that contains tokens separated by delimiters.  
  
 *delimiter_string*  
 A string that contains delimiter characters. For example, "; ," contains three delimiter characters semi-colon, a blank space, and a comma.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 The following remarks apply to the TOKEN function:  
  
-   The delimiter string can contain one or more delimiter characters.  
  
-   Leading delimiters are skipped.  
  
-   TOKENCOUNT works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before TOKEN performs its operation. Other data types must be explicitly cast to the DT_WSTR data type.  
  
-   TOKENCOUNT returns 0 (zero) if the character_expression is null.  
  
-   You can use variables and columns as arguments for this expression.  
  
## Expression Examples  
 In the following example, the TOKENCOUNT function returns 3 because the string contains three tokens: "01", "12", "2011".  
  
```  
TOKENCOUNT("01/12/2011", "/")  
```  
  
 In the following example, the TOKENCOUNT function returns 4 because there are four tokens ("a", "little", "white", "dog").  
  
```  
TOKENCOUNT("a little white dog"," ")  
```  
  
 In the following example, the TOKENCOUNT function returns 1. The function parses the input string for delimiters and since there are none in the string, it just adds the entire string as the first token.  
  
```  
TOKENCOUNT("a little white dog","|")  
```  
  
 In the following example, the TOKENCOUNT function returns 4. The delimiter string in this example contains 5 delimiters. The input string contains 4 tokens: "a", "little", "white", "dog".  
  
```  
TOKENCOUNT("a:little|white dog","| ,.:")  
```  
  
 In the following example, the TOKENCOUNT function returns 4. It ignores all the leading space characters.  
  
```  
TOKENCOUNT("        a little white dog", " ")  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
