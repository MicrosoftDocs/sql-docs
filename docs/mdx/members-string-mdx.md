---
title: "Members (String) (MDX) | Microsoft Docs"
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
---
# Members (String) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

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
  
  
