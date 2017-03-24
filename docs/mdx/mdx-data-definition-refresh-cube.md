---
title: "REFRESH CUBE Statement (MDX) | Microsoft Docs"
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
  - "Cube"
  - "REFRESH CUBE"
  - "REFRESH_CUBE"
  - "REFRESH"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "cubes [Analysis Services], cache"
  - "refreshing cache"
  - "REFRESH CUBE statement"
  - "cache [Analysis Services]"
ms.assetid: b8c087fb-5d17-4b13-b7cf-9929e9aab35c
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - REFRESH CUBE
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Refreshes the client cache for a cube.  
  
## Syntax  
  
```  
  
REFRESH CUBECube_Name   
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides a cube name.  
  
## Remarks  
 For client applications connected to an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], this statement causes the memory cached on the client application to be synchronized with the server. While this will ordinarily be detected and updated automatically, the length of time before this happens depends on the client connection string settings. The REFRESH CUBE statement refreshes data immediately.  
  
 For client applications connected to a local cube, the REFRESH CUBE statement causes the local cube file to be rebuilt.  
  
> [!IMPORTANT]  
>  Named sets that are stored on the server are not refreshed.  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
