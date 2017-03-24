---
title: "Managing Caches (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "XMLA, cache"
  - "XML for Analysis, cache"
  - "clearing cache"
  - "cache [Analysis Services]"
ms.assetid: afad5c39-d4c3-4307-b3b9-a06617da0028
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Managing Caches (XMLA)
  You can use the [ClearCache](../../analysis-services/xmla/xml-elements-commands/clearcache-element-xmla.md) command in XML for Analysis (XMLA) to clear the cache of a specified dimension or partition. Clearing the cache forces [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to rebuild the cache for that object.  
  
## Specifying Objects  
 The [Object](../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md) property of the **ClearCache** command can contain an object reference only for one of the following objects. An error occurs if an object reference is for an object other than one of following objects:  
  
 Database  
 Clears the cache for all dimensions and partitions contained in the database.  
  
 Dimension  
 Clears the cache for the specified dimension.  
  
 Cube  
 Clears the cache for all partitions contained in the measure groups for the cube.  
  
 Measure group  
 Clears the cache for all partitions contained in the measure group.  
  
 Partition  
 Clears the cache for the specified partition.  
  
## See Also  
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  