---
title: "UserName (MDX) | Microsoft Docs"
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
  - "UserName"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "UserName function"
ms.assetid: ecae549b-5c5e-4483-84e6-b713cd297d7e
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# UserName (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the domain name and user name of the current connection.  
  
## Syntax  
  
```  
  
UserName [ ( ) ]  
```  
  
## Remarks  
 The returned value is a string with the following format:  
  
 *domain-name\user-name*  
  
## Example  
 The following example returns the user name of the user that is executing the query.  
  
```  
WITH MEMBER Measures.x AS UserName  
SELECT Measures.x ON COLUMNS  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
