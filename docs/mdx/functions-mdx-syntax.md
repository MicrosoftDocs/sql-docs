---
title: "Functions (MDX Syntax) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "MDX [Analysis Services], functions"
  - "Multidimensional Expressions [Analysis Services], functions"
  - "functions [MDX]"
ms.assetid: 74ca5e79-1f33-4795-9d68-98eff9c190c1
caps.latest.revision: 25
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Functions (MDX Syntax)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Multidimensional Expressions (MDX) has several categories of intrinsic functions to perform certain operations. The following table lists the function categories that are available in MDX.  
  
> [!NOTE]  
>  For more information about individual functions, see [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md).  
  
|Function Category|Description|  
|-----------------------|-----------------|  
|Array functions|Provide arrays for use in stored procedures.<br /><br /> For more information, see [Using Stored Procedures &#40;MDX&#41;](../mdx/using-stored-procedures-mdx.md).|  
|Dimension functions|Return a reference to a dimension from a hierarchy, level, or member.<br /><br /> For more information, see [Using Dimension, Hierarchy, and Level Functions](../mdx/using-dimension-hierarchy-and-level-functions.md).|  
|Hierarchy functions|Return a reference to a hierarchy from a level or member.<br /><br /> For more information, see [Using Dimension, Hierarchy, and Level Functions](../mdx/using-dimension-hierarchy-and-level-functions.md).|  
|Level functions|Return a reference to a level from a member, dimension, hierarchy, or from a string expression.<br /><br /> For more information, see [Using Dimension, Hierarchy, and Level Functions](../mdx/using-dimension-hierarchy-and-level-functions.md).|  
|Logical functions|Perform logical operations and comparisons on objects and expressions.<br /><br /> For more information, see [Using Logical Functions](../mdx/using-logical-functions.md).|  
|Member functions|Return a reference to a member from other objects or from a string expression.<br /><br /> For more information, see [Using Member Functions](../mdx/using-member-functions.md).|  
|Numeric functions|Perform mathematical and statistical functions on objects and expressions.<br /><br /> For more information, see [Using Mathematical Functions](../mdx/using-mathematical-functions.md).|  
|Set functions|Return a reference to a set from other objects or from a string expression.<br /><br /> For more information, see [Using Set Functions](../mdx/using-set-functions.md).|  
|String functions|Return string values from other objects or from the server.<br /><br /> For more information, see [Using String Functions](../mdx/using-string-functions.md).|  
|Tuple functions|Return a reference to a tuple from a set or from a string expression.<br /><br /> For more information, see Using Tuple Functions.|  
  
## Uses of Functions  
 Functions can be used or included in any MDX expression. Functions can also be nested (one function used inside another function).  
  
## See Also  
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
  
