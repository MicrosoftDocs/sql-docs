---
title: "Error Handling (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [MDX], exceptions"
  - "exceptions [MDX]"
ms.assetid: bc6ff0af-9fe6-44d6-bc3c-801d71ea41a9
author: minewiskan
ms.author: owend
manager: craigg
---
# Error Handling (MDX)
  Each cube can control how errors within a Multidimensional Expressions (MDX) script are handled. Error handling is done through the `ScriptErrorHandlingMode` enumerator. The possible values for this enumerator are:  
  
 `IgnoreNone`  
 Causes the server to raise an error when [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] finds any error in the MDX script.  
  
 `IgnoreAll`  
 Causes the server to ignore all commands in the MDX script that contain an error, including syntax errors, name resolution errors, and more.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Cube.ScriptErrorHandlingMode%2A>  
  
  
