---
title: "TOKEN  (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 9fdd06bf-5bc9-445c-95bf-709e0ca5989b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# TOKEN  (SSIS Expression)
  Returns a token (substring) from a string based on the specified delimiters that separate tokens in the string and the number of the token that denotes which token to be returned.  
  
## Syntax  
  
```  
TOKEN(character_expression, delimiter_string, occurrence)  
```  
  
## Arguments  
 *character_expression*  
 A string that contains tokens separated by delimiters.  
  
 *delimiter_string*  
 A string that contains delimiter characters. For example, "; ," contains three delimiter characters semi-colon, a blank space, and a comma.  
  
 *occurrence*  
 A signed or unsigned integer that specifies the token to be returned. For example, if you specify 3 as a value for this parameter, the third token in the string is returned.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 This function splits up the <character_expression> string into a set of tokens separated by the delimiters specified in the <delimiter_string> and then returns the Nth token where N is the number of occurrence of the token specified by the \<occurrence> parameter. See Examples section for sample usages of this function.  
  
 The following remarks apply to the TOKEN function:  
  
-   The delimiter string can contain one or more delimiter characters.  
  
-   If the value of \<occurrence> parameter is higher than the total number of tokens in the string, the function returns NULL.  
  
-   Leading delimiters are skipped.  
  
-   TOKEN works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before TOKEN performs its operation. Other data types must be explicitly cast to the DT_WSTR data type.  
  
-   TOKEN returns a null result if the character_expression is null.  
  
-   You can use variables and columns as values of all arguments in the expression.  
  
## Expression Examples  
 In the following example, the TOKEN function returns "a". The string "a little white dog" has 4 tokens "a", "little", "white", "dog" separated by the delimiter " " (space character). The second argument, a delimiter string, specifies only one delimiter, the space character, to be used in splitting the input string into tokens. The last argument, 1, specifies that the first token to be returned. The first token is "a" in this sample string.  
  
```  
TOKEN("a little white dog"," ",1)  
```  
  
 In the following example, the TOKEN function returns "dog". The delimiter string in this example contains 5 delimiters. The input string contains 4 tokens: "a", "little", "white", "dog".  
  
```  
TOKEN("a:little|white dog","| ,.:",4)  
```  
  
 In the following example, the TOKEN function returns "" (an empty string) because there are no 99 tokens in the string.  
  
```  
TOKEN("a little white dog"," ",99)  
```  
  
 In the following example, the TOKEN function returns the full string. The function parses the input string for delimiters and since there are none in the string, it just adds the entire string as the first token.  
  
```  
TOKEN("a little white dog","|",1)  
```  
  
 In the following example, the TOKEN function returns "a". It ignores all the leading space characters.  
  
```  
TOKEN("        a little white dog", " ", 1)  
```  
  
 In the following example, the TOKEN function returns the year from a date string.  
  
```  
TOKEN("2009/01/01", "/"), 1  
```  
  
 In the following example, the TOKEN function returns the file name from the specified path. For example, if the value of User::Path is "c:\program files\data\myfile.txt", the TOKEN function returns "myfile.txt". The TOKENCOUNT function returns 4 and the TOKEN function return the 4th token, "myfile.txt".  
  
```  
TOKEN(@[User::Path], "\\", TOKENCOUNT(@[User::Path], "\\"))  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
