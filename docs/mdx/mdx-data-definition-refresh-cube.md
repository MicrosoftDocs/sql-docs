---
title: "REFRESH CUBE Statement (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Data Definition - REFRESH CUBE


  Refreshes the client cache for a cube.  
  
## Syntax  
  
```  
  
REFRESH CUBECube_Name   
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides a cube name.  
  
## Remarks  
 For client applications connected to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], this statement causes the memory cached on the client application to be synchronized with the server. While this will ordinarily be detected and updated automatically, the length of time before this happens depends on the client connection string settings. The REFRESH CUBE statement refreshes data immediately.  
  
 For client applications connected to a local cube, the REFRESH CUBE statement causes the local cube file to be rebuilt.  
  
> [!IMPORTANT]  
>  Named sets that are stored on the server are not refreshed.  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
