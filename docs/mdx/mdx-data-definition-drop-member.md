---
title: "DROP MEMBER Statement (MDX) | Microsoft Docs"
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
  - "Member"
  - "DROP_MEMBER"
  - "DROP MEMBER"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "deleting calculated members"
  - "calculated members [MDX]"
  - "DROP MEMBER statement"
  - "dropping calculated members"
  - "removing calculated members"
ms.assetid: e9819976-a9ec-4c48-b0b5-3f6938e200f5
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - DROP MEMBER
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a calculated member.  
  
## Syntax  
  
```  
  
DROP MEMBER   
   CURRENTCUBE | Cube_Name  
      .Member_Name   
               [,CURRENTCUBE | Cube_Name.Member_Name ...n]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides a cube name.  
  
 *Member_Identifier*  
 A valid string expression that provides a member name or member key.  
  
## See Also  
 [CREATE MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-member.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
