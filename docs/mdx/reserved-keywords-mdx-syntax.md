---
title: "Reserved Keywords (MDX Syntax) | Microsoft Docs"
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
  - "reserved words [MDX]"
  - "Multidimensional Expressions [Analysis Services], reserved words"
  - "MDX [Analysis Services], reserved words"
ms.assetid: 0baab5fb-bd04-4ab3-b99a-9f91f3470fbb
caps.latest.revision: 26
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Reserved Keywords (MDX Syntax)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] reserves certain keywords for its exclusive use. For a list of reserved keywords, see [MDX Reserved Words](../mdx/mdx-reserved-words.md).  
  
 Reserved keywords follow these guidelines:  
  
-   You cannot include reserved keywords in a Multidimensional Expressions (MDX) statement in any location except that defined by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
-   No objects in the database should be specific a name that matches a reserved keyword. If such a name exists, the object must always be referred to using delimited identifiers. Although this method does allow for object names to be reserved words, using keywords to name objects should be avoided.  
  
-   Use a naming convention that avoids using reserved keywords. Consonants or vowels can be removed if an object name must look like a reserved keyword.  
  
## See Also  
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
  
