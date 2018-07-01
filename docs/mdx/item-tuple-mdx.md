---
title: "Item (Tuple) (MDX) | Microsoft Docs"
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
# Item (Tuple) (MDX)


  Returns a tuple from a set.  
  
## Syntax  
  
```  
  
Index syntax  
Set_Expression.Item(Index)  
  
String expression syntax  
Set_Expression.Item(String_Expression1 [ ,String_Expression2,...n])  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *String_Expression1*  
 A valid string expression that is a typically a tuple expressed in a string.  
  
 *String_Expression2*  
 A valid string expression that is a typically a tuple expressed in a string.  
  
 *Index*  
 A valid numeric expression that specifies the specific tuple by position within the set to be returned.  
  
## Remarks  
 The **Item** function returns a tuple from the specified set. There are three possible ways to call the **Item** function:  
  
-   If a single string expression is specified, the **Item** function returns the specified tuple. For example, "([2005].Q3, [Store05])".  
  
-   If more than one string expression is specified, the **Item** function returns the tuple defined by the specified coordinates. The number of strings must match the number of axis, and each string must identify a unique hierarchy. For example, "[2005].Q3", "[Store05]".  
  
-   If an integer is specified, the **Item** function returns the tuple that is in the zero-based position specified by *Index*.  
  
## Examples  
 The following example returns ([1996],Sales):  
  
 `{([1996],Sales), ([1997],Sales), ([1998],Sales)}.Item(0)`  
  
 The following example uses a level expression and returns the Internet Sales Amount for each State-Province in Australia and its percent of the total Internet Sales Amount for Australia. This example uses the Item function to extract the first (and only tuple) from the set returned by the **Ancestors** function.  
  
```  
WITH MEMBER Measures.x AS [Measures].[Internet Sales Amount] /   
   ( [Measures].[Internet Sales Amount],    
      Ancestors   
      ( [Customer].[Customer Geography].CurrentMember,  
        [Customer].[Customer Geography].[Country]  
      ).Item (0)  
   ), FORMAT_STRING = '0%'  
SELECT {[Measures].[Internet Sales Amount], Measures.x} ON 0,  
{ Descendants   
   ( [Customer].[Customer Geography].[Country].&[Australia],  
     [Customer].[Customer Geography].[State-Province], SELF   
   )   
} ON 1  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
