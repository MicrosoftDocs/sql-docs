---
title: "Instr (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Instr (MDX)


  Returns the position of the first occurrence of one string within another.  
  
## Syntax  
  
```  
InStr([start, ]searched_string, search_string[, compare])  
```  
  
## Arguments  
 *start*  
 (Optional) A numeric expression that sets the starting position for each search. If this value is omitted, the search begins at the first character position. If start is null, the function return value is undefined.  
  
 *searched_string*  
 The string expression to be searched.  
  
 *search_string*  
 The string expression that is to be searched for.  
  
 *Compare*  
 (optional) An integer value. This argument is always ignored. It is defined for compatibility with other **Instr** functions in other languages.  
  
## Return Value  
 An integer value with the starting position of *String2* in *String1*.  
  
 Also, **InStr** function returns the values listed in the following table depending on the condition:  
  
|Condition|Return value|  
|---------------|------------------|  
|String1 is zero-length|zero (0)|  
|String1 is null|undefined|  
|String2 is zero-length|start|  
|String2 is null|undefined|  
|String2 is not found|zero (0)|  
|start is greater than Len(String2)|zero (0)|  
  
## Remarks  
  
> [!WARNING]  
>  **Instr** always performs a case-insensitive comparison.  
  
## Example  
 The following example shows the usage of the **Instr** function and shows different result scenarios.  
  
```  
with   
    member [Date].[Date].[Results] as "Results"  
    member measures.[lowercase found in lowercase string] as InStr( "abcdefghijklmnñopqrstuvwxyz", "o")  
    member measures.[uppercase found in lowercase string] as InStr( "abcdefghijklmnñopqrstuvwxyz", "O")  
    member measures.[searched string is empty]            as InStr( "", "o")  
    member measures.[searched string is null]             as iif(IsError(InStr( null, "o")), "Is Error", iif(IsNull(InStr( null, "o")), "Is Null","Is undefined"))  
    member measures.[search string is empty]              as InStr( "abcdefghijklmnñopqrstuvwxyz", "")  
    member measures.[search string is empty start 10]     as InStr(10, "abcdefghijklmnñopqrstuvwxyz", "")  
    member measures.[search string is null]               as iif(IsError(InStr( null, "o")), "Is Error", iif(IsNull(InStr( null, "o")), "Is Null","Is undefined"))  
    member measures.[found from start 10]                 as InStr( 10, "abcdefghijklmnñopqrstuvwxyz", "o")  
    member measures.[NOT found from start 17]             as InStr( 17, "abcdefghijklmnñopqrstuvwxyz", "o")  
    member measures.[NULL start]                          as iif(IsError(InStr( null, "abcdefghijklmnñopqrstuvwxyz", "o")), "Is Error", iif(IsNull(InStr( null, "abcdefghijklmnñopqrstuvwxyz", "o")), "Is Null","Is undefined"))  
    member measures.[start greater than searched length]  as InStr( 170, "abcdefghijklmnñopqrstuvwxyz", "o")  
  
select  [Results] on columns,  
       { measures.[lowercase found in lowercase string]  
       , measures.[uppercase found in lowercase string]  
       , measures.[searched string is empty]  
       , measures.[searched string is null]  
       , measures.[search string is empty]  
       , measures.[search string is empty start 10]  
       , measures.[search string is null]  
       , measures.[found from start 10]  
       , measures.[NOT found from start 17]  
       , measures.[NULL start]   
       , measures.[start greater than searched length]  
       } on rows  
  
from [Adventure Works]  
```  
  
 The following table displays the obtained results.  
  
|||  
|-|-|  
||Results|  
|lowercase found in lowercase string|16|  
|uppercase found in lowercase string|16|  
|searched string is empty|0|  
|searched string is null|Is undefined|  
|search string is empty|1|  
|search string is empty start 10|10|  
|search string is null|Is undefined|  
|found from start 10|16|  
|NOT found from start 17|0|  
|NULL start|Is undefined|  
|start greater than searched length|0|  
  
  
