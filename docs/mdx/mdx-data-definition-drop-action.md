---
title: "DROP ACTION Statement (MDX) | Microsoft Docs"
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
  - "DROP"
  - "DROP ACTION"
  - "Action"
  - "DROP_ACTION"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "DROP ACTION statement"
  - "deleting actions"
  - "removing actions"
  - "actions [MDX]"
  - "dropping actions"
ms.assetid: 74b3cfee-dea8-4968-a54c-1754d52ee1bd
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - DROP ACTION
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a specified action from a specified cube.  
  
## Syntax  
  
```  
  
DROP ACTION CURRENTCUBE | Cube_Name  
   .Action_Name   
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the cube name.  
  
 *Action_Name*  
 A valid string expression that provides the name of the action being dropped.  
  
## See Also  
 [CREATE ACTION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-action.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
