---
title: "DataMember (MDX) | Microsoft Docs"
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
  - "DATAMEMBER"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "DataMember function"
ms.assetid: 65bf21e8-1cb2-4ae8-bfbe-bb4d72589557
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataMember (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the system-generated data member that is associated with a nonleaf member of a dimension.  
  
## Syntax  
  
```  
  
Member_Expression.DataMember  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 This function operates on nonleaf members in any hierarchy and can be used by the [UPDATE CUBE Statement (MDX)](../mdx/mdx-data-manipulation-update-cube.md) command to writeback data to a nonleaf member directly, rather than to the leaf member's descendants.  
  
> [!NOTE]  
>  Returns the specified member if the specified member is a leaf member, or if the nonleaf member does not have an associated data member.  
  
## Example  
 The following example uses the **DataMember** function in a calculated measure to show the sales quota for each individual employee:  
  
```  
WITH MEMBER measures.InvidualQuota AS   
([Employee].[Employees].currentmember.datamember, [Measures].[Sales Amount Quota])  
,FORMAT_STRING='Currency'  
SELECT {[Measures].[Sales Amount Quota],[Measures].InvidualQuota} ON COLUMNS,  
[Employee].[Employees].MEMBERS ON ROWS  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [Key Concepts in MDX &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services.md)  
  
  
