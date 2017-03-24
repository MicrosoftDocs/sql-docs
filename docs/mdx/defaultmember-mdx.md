---
title: "DefaultMember (MDX) | Microsoft Docs"
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
  - "DefaultMember"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "DefaultMember function"
ms.assetid: c1b53b3a-6e73-4c41-a4fe-9f5c96da5463
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DefaultMember (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the default member of a hierarchy.  
  
## Syntax  
  
```  
  
Hierarchy_Expression.DefaultMember  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
## Remarks  
 The default member on an attribute is used to evaluate expressions when an attribute is not included in a query.  
  
## Example  
 The following example uses the **DefaultMember** function, in conjunction with the **Name** function, to return the default member for the Destination Currency dimension in the Adventure Works cube. The example returns **US Dollar**. The **Name** function is used to return the name of the measure rather than the default property of the measure, which is **value**.  
  
```  
WITH MEMBER Measures.x AS   
   [Destination Currency].[Destination Currency].DefaultMember.Name  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [Define a Default Member](../analysis-services/multidimensional-models/attribute-properties-define-a-default-member.md)  
  
  
