---
title: "DROP SET Statement (MDX) | Microsoft Docs"
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
  - "SET"
  - "DROP"
  - "DROP SET"
  - "DROP_SET"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "DROP SET statement"
  - "deleting named sets"
  - "named sets [MDX]"
  - "removing named sets"
  - "dropping named sets"
ms.assetid: bbc37afb-af8c-41df-ba81-12771beb1c41
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - DROP SET
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a named set.  
  
## Syntax  
  
```  
  
DROP [SESSION] SET   
   <Cube_Reference>.Set_Name   
               [,<Cube_Reference>.Set_Name ...n]  
  
<Cube_Reference> ::= {CURRENTCUBE | Cube_Name}  
  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of the cube.  
  
 *Set_Name*  
 A valid string expression that provides that name of the named set to be dropped.  
  
## Remarks  
 For more information about named sets, see [Building Named Sets in MDX &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-named-sets-building-named-sets.md).  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
