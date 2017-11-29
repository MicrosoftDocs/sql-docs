---
title: "Members (String) (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Members"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Members function"
ms.assetid: 21fca354-448b-4b05-93f4-111bde1568f1
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# Members (String) (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns a member specified by a string expression.  
  
## Syntax  
  
```  
  
Members(Member_Name)   
```  
  
## Arguments  
 *Member_Name*  
 A valid string expression that specifies a member name.  
  
## Remarks  
 The **Members (String)** function returns a single member whose name is specified. Typically, you use the **Members (String)** function with external functions, providing to the **Members (String)** function a string that identifies a member, and the **Members (String)** function returns the value for this specified member.  
  
## Example  
 The following example uses the **Members (String)** function to convert the specified string to a valid member, and then returns the default measure for the member specified in the string. The specified string is in single quotes. The default measure is the Reseller Sales Amount measure.  
  
```  
SELECT Members ('[Geography].[Geography].[Country].&[United States] ') ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
