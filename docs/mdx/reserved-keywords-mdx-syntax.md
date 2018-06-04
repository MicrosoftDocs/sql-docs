---
title: "Reserved Keywords (MDX Syntax) | Microsoft Docs"
ms.date: 05/30/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Reserved Keywords (MDX Syntax)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] reserves certain keywords for its exclusive use. For a list of reserved keywords, see [MDX Reserved Words](../mdx/mdx-reserved-words.md).  
  
 Reserved keywords follow these guidelines:  
  
-   You cannot include reserved keywords in a Multidimensional Expressions (MDX) statement in any location except that defined by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
-   No objects in the database should be specific a name that matches a reserved keyword. If such a name exists, the object must always be referred to using delimited identifiers. Although this method does allow for object names to be reserved words, using keywords to name objects should be avoided.  
  
-   Use a naming convention that avoids using reserved keywords. Consonants or vowels can be removed if an object name must look like a reserved keyword.  
  
## See Also  
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
  
