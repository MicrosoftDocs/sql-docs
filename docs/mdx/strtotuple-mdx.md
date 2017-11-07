---
title: "StrToTuple (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STRTOTUPLE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "StrToTuple function"
ms.assetid: e162cc01-cddd-4654-baab-d73abdc33b80
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# StrToTuple (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the tuple specified by a Multidimensional Expressions (MDX)–formatted string.  
  
## Syntax  
  
```  
  
StrToTuple(Tuple_Specification [,CONSTRAINED] )   
```  
  
## Arguments  
 *Tuple_Specification*  
 A valid string expression specifying, directly or indirectly, a tuple.  
  
## Remarks  
 The **StrToTuple** function returns the specified set. The **StrToTuple** function is typically used with user-defined functions to return a tuple specification from an external function back to an MDX statement.  
  
-   When the CONSTRAINED flag is used, the tuple specification must contain qualified or unqualified member names. This flag is used to reduce the risk of injection attacks via the specified string. If a string is provided that is not directly resolvable to qualified or unqualified member names, the following error appears: "The restrictions imposed by the CONSTRAINED flag in the STRTOTUPLE function were violated."  
  
-   When the CONSTRAINED flag is not used, the specified tuple can resolve to a valid MDX expression that returns a tuple.  
  
## Examples  
 The following example returns the Reseller Sales Amount measure for the Bayern member for calendar year 2004. The tuple specification that is provided contains a valid MDX tuple expression.  
  
```  
SELECT StrToTuple ('([Geography].[State-Province].[Bayern],[Date].[Calendar Year].[CY 2004], [Measures].[Reseller Sales Amount])')  
ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the Reseller Sales Amount measure for the Bayern member for calendar year 2004. The tuple specification that is provided contains qualified member names, as required by the CONSTRAINED flag.  
  
```  
SELECT StrToTuple ('([Geography].[State-Province].[Bayern],[Date].[Calendar Year].[CY 2004], [Measures].[Reseller Sales Amount])', CONSTRAINED)  
ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the Reseller Sales Amount measure for the Bayern member for calendar year 2004. The tuple specification that is provided contains a valid MDX tuple expression.  
  
```  
SELECT StrToTuple ('([Geography].[State-Province].[Bayern],[Date].[Calendar Year].&[2003].NEXTMEMBER, [Measures].[Reseller Sales Amount])')  
ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns an error due to the CONSTRAINED flag. While the tuple specification provided contains a valid MDX tuple expression, the CONSTRAINED flag requires qualified or unqualified member names in the tuple specification.  
  
```  
SELECT StrToTuple ('([Geography].[State-Province].[Bayern],[Date].[Calendar Year].&[2003].NEXTMEMBER, [Measures].[Reseller Sales Amount])', CONSTRAINED)  
ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
