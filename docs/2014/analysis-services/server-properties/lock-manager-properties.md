---
title: "Lock Manager Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "lock manager properties [Analysis Services]"
  - "LockWaitGranularityMS property"
  - "DefaultLockTimeoutMS property"
  - "DeadlockDetectionGranularityMS property"
ms.assetid: 15fe9ead-825b-4ac3-9191-7a07caa2861b
author: minewiskan
ms.author: owend
manager: craigg
---
# Lock Manager Properties
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the lock manager server properties listed in the following table. For more information about additional server properties and how to set them, see [Configure Server Properties in Analysis Services](server-properties-in-analysis-services.md).  
  
 **Applies to:** Multidimensional and Tabular server mode  
  
## Properties  
 `DefaultLockTimeoutMS`  
 A signed 32-bit integer property that defines default lock timeout in milliseconds for internal lock requests.  
  
 The default value for this property is -1, which indicates there is no timeout for internal lock requests.  
  
 `LockWaitGranularityMS`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `DeadlockDetectionGranularityMS`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## See Also  
 [Configure Server Properties in Analysis Services](server-properties-in-analysis-services.md)   
 [Determine the Server Mode of an Analysis Services Instance](../instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
