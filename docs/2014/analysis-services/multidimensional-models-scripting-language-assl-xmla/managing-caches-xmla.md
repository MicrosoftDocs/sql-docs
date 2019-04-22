---
title: "Managing Caches (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "XMLA, cache"
  - "XML for Analysis, cache"
  - "clearing cache"
  - "cache [Analysis Services]"
ms.assetid: afad5c39-d4c3-4307-b3b9-a06617da0028
author: minewiskan
ms.author: owend
manager: craigg
---
# Managing Caches (XMLA)
  You can use the [ClearCache](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/clearcache-element-xmla) command in XML for Analysis (XMLA) to clear the cache of a specified dimension or partition. Clearing the cache forces [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to rebuild the cache for that object.  
  
## Specifying Objects  
 The [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) property of the `ClearCache` command can contain an object reference only for one of the following objects. An error occurs if an object reference is for an object other than one of following objects:  
  
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
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
