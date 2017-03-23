---
title: "Root (MDX) | Microsoft Docs"
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
  - "Root"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Root function"
ms.assetid: f6c42e87-5a52-4e43-9dd1-ca757f2db79c
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Root (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a tuple that consists of the **All** members from each attribute hierarchy within the current scope in a cube, dimension, or tuple. For more information about Scope, see [SCOPE Statement &#40;MDX&#41;](../mdx/mdx-scripting-scope.md).  
  
> [!NOTE]  
>  If an attribute hierarchy does not have an **All** member, the tuple contains the default member for that hierarchy.  
  
## Syntax  
  
```  
  
Cube syntax  
Root ()  
Dimension syntax  
Root( Dimension_Name )  
Tuple syntax  
Root( Tuple_Expression )  
```  
  
## Arguments  
 *Dimension_Name*  
 A valid string expression specifying a dimension name.  
  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
## Remarks  
 If neither a dimension name nor a tuple expression is specified, the **Root** function returns a tuple that contains the **All** member (or the default member if the **All** member does not exist) from each attribute hierarchy in the cube. The order of members in the tuple is based on the sequence in which the attribute hierarchies are defined within the cube.  
  
 If a dimension name is specified, the **Root** function returns a tuple that contains the **All** member (or the default member if the **All** member does not exist) from each attribute hierarchy in the specified dimension based on the context of the current member. The order of members in the tuple is based on the sequence in which the attribute hierarchies are defined within the dimension.  
  
> [!NOTE]  
>  If a hierarchy name is specified, the **Tuple** function will pick the dimension name from the hierarchy name specified.  
  
 If a tuple expression is specified, the **Root** function returns a tuple that contains the intersection of the specified tuple and the **All** members of all other dimension attributes not explicitly included in the specified tuple.  
  
## Examples  
 The following example returns the tuple containing the **All** member (or the default if the **All** member does not exist) from each hierarchy in the Adventure Works cube.  
  
```  
SELECT Root()ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the tuple containing the **All** member (or the default if the **All** member does not exist) from each hierarchy in the Date dimension in the Adventure Works cube and the value for the specified member of Measures dimension that intersects with these default members.  
  
```  
SELECT Root([Date]) ON 0  
FROM [Adventure Works]  
WHERE [Measures].[Order Count]  
```  
  
 The following example returns the tuple containing specified tuple member (July 1, 2001, along with the **All** member (or the default if the **All** member does not exist) from each non-specified hierarchy in the Date dimension Adventure Works cube and the value for the specified member of Measures dimension that intersects with these members.  
  
```  
SELECT Root([Date].[July 1, 2001]) ON 0  
FROM [Adventure Works]  
WHERE [Measures].[Order Count]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
